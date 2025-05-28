namespace WebApplication1.DTOs;

public class MedicamentGetDTO
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public int Dose { get; set; }
    public string Description { get; set; } = null!;
}