namespace WebApplication1.DTOs;

public class PatientCreateDTO
{
    public int? Id { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public DateTime BirthDate { get; set; }
}