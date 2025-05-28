using Microsoft.EntityFrameworkCore;
using WebApplication1.Modles;

namespace WebApplication1.Data;

public class AppDbContext : DbContext
{
    public DbSet<Patient> Patients { get; set; }
    public DbSet<Doctor> Doctors { get; set; }
    public DbSet<Medicament> Medicaments { get; set; }
    public DbSet<Prescription> Prescriptions { get; set; }
    public DbSet<Prescription_Medicament> PrescriptionMedicaments { get; set; } = null!;

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        var patient = new List<Patient>
        {
            new()
            {
                IdPatient = 1,
                FirstName = "Bogdan",
                LastName = "Boner",
                BirthDate = new DateTime(2023, 1, 1)
            },
            new()
            {
                IdPatient = 2,
                FirstName = "Domino",
                LastName = "Jachaś",
                BirthDate = new DateTime(2023, 1, 1)
            }
        };

        var doctor = new List<Doctor>
        {
            new()
            {
                IdDoctor = 1,
                FirstName = "John",
                LastName = "Doe",
                Email = "john.doe@gmail.com"
            }
        };

        var medicament = new List<Medicament>
        {
            new()
            {
                IdMedicament = 1,
                Description = "Opis",
                Name = "NazwaNowa",
                Type = "Kapsułki"
            }
        };

        var presciption = new List<Prescription>
        {
            new()
            {
                IdPrescription = 1,
                PatientId = 1,
                DoctorId = 1,
                Date = new DateTime(2023, 1, 1),
                DueDate = new DateTime(2024, 1, 1)
            }
        };

        var prescription_Medicament = new List<Prescription_Medicament>
        {
            new()
            {
                PrescriptionId = 1,
                MedicamentId = 1,
                Dose = 1,
                Details = "Szczegóły"
            }
        };
        
        modelBuilder.Entity<Patient>().HasData(patient);
        modelBuilder.Entity<Doctor>().HasData(doctor);
        modelBuilder.Entity<Medicament>().HasData(medicament);
        modelBuilder.Entity<Prescription>().HasData(presciption);
        modelBuilder.Entity<Prescription_Medicament>().HasData(prescription_Medicament);
    }
}
    
    
    
