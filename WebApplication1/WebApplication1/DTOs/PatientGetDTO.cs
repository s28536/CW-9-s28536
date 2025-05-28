using WebApplication1.DTOs;

namespace WebApplication1.DTOs;

public class PatientGetDTO
{
    public int Id { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public DateTime BirthDate { get; set; }
    public virtual ICollection<PrescriptionGetDTO> Prescriptions { get; set; } = null!;
}