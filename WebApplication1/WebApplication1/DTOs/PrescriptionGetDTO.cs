namespace WebApplication1.DTOs;

public class PrescriptionGetDTO
{
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public DateTime DueDate { get; set; }

    public virtual ICollection<MedicamentGetDTO> Medicaments { get; set; } = null!;
    public virtual DoctorGetDTO Doctor { get; set; } = null!;
}