namespace WebApplication1.DTOs;

public class MedicamentCreateDTO
{
    public int MedicamentId { get; set; }
    public int Dose { get; set; }
    public string Description { get; set; } = null!;
}