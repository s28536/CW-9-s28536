using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.DTOs;
using WebApplication1.Modles;

namespace WebApplication1.Service;

public interface IDbService
{
    public Task<int> CreatePrescriptionAsync(PrescriptionCreateDTO prescriptionData);
    
    public Task<PatientGetDTO> GetPatientByIdAsync(int id);
}


public class DbService(AppDbContext data) : IDbService
{
    public async Task<int> CreatePrescriptionAsync(PrescriptionCreateDTO prescriptionData)
    {
        if (prescriptionData.Medicaments.Count > 10)
        {
            throw new Exception("");
        }

        if (prescriptionData.DueDate < prescriptionData.Date)
        {
            throw new Exception("");
        }

        var patient = await data.Patients.FirstOrDefaultAsync(p =>
            p.FirstName == prescriptionData.Patient.FirstName &&
            p.LastName == prescriptionData.Patient.LastName &&
            p.BirthDate == prescriptionData.Patient.BirthDate);


        if (patient == null)
        {
            patient = new Patient
            {
                FirstName = prescriptionData.Patient.FirstName,
                LastName = prescriptionData.Patient.LastName,
                BirthDate = prescriptionData.Patient.BirthDate,
            };
            
            await data.Patients.AddAsync(patient);
            await data.SaveChangesAsync();
        }
        
        
        var medicamentsId = prescriptionData.Medicaments.Select(m => m.MedicamentId).ToList();
        var validMedicamentsIds = await data.Medicaments.Where(m => medicamentsId.Contains(m.IdMedicament))
            .Select(m => m.IdMedicament)
            .ToListAsync();

        var missing = medicamentsId.Except(validMedicamentsIds).ToList();
        if (missing.Any())
            throw new Exception("");

        var prescription = new Prescription
        {
            PatientId = patient.IdPatient,
            DueDate = prescriptionData.DueDate,
            Date = prescriptionData.Date,
            DoctorId = 1
        };
        
        await data.Prescriptions.AddAsync(prescription);
        await data.SaveChangesAsync();

        var prescritionMedicaments = prescriptionData.Medicaments.Select(m => new Prescription_Medicament
        {
            PrescriptionId = prescription.IdPrescription,
            MedicamentId = m.MedicamentId,
            Dose = m.Dose,
            Details = m.Description
        });
        
        await data.PrescriptionMedicaments.AddRangeAsync(prescritionMedicaments);
        await data.SaveChangesAsync();

        return prescription.IdPrescription;
    }

    public async Task<PatientGetDTO> GetPatientByIdAsync(int id)
    {
        var patient = await data.Patients.Include(p => p.Prescriptions)
                .ThenInclude(p => p.Doctor)
            .Include(p => p.Prescriptions)
            .ThenInclude(p => p.PrescriptionMedicaments)
            .ThenInclude(pm => pm.Medicament)
            .FirstOrDefaultAsync(p => p.IdPatient == id);

        if (patient == null)
        {
            throw new Exception("");
        }

        return new PatientGetDTO
        {
            Id = patient.IdPatient,
            FirstName = patient.FirstName,
            LastName = patient.LastName,
            BirthDate = patient.BirthDate,
            Prescriptions = patient.Prescriptions.OrderBy(p => p.DueDate).Select(p => new PrescriptionGetDTO
            {
                Id = p.IdPrescription,
                Date = p.Date,
                DueDate = p.DueDate,
                Doctor = new DoctorGetDTO
                {
                    Id = p.Doctor.IdDoctor,
                    FirstName = p.Doctor.FirstName
                },
                Medicaments = p.PrescriptionMedicaments.Select(pm => new MedicamentGetDTO
                {
                    Id = pm.Medicament.IdMedicament,
                    Name = pm.Medicament.Name,
                    Dose = pm.Dose,
                    Description = pm.Details
                }).ToList()
            }).ToList()
        };
    }
}