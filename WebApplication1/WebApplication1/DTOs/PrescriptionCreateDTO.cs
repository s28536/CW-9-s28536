using WebApplication1.DTOs;

namespace WebApplication1.DTOs;

public class PrescriptionCreateDTO
{
    public PatientCreateDTO Patient { get; set; } = null!;
    public virtual ICollection<MedicamentCreateDTO> Medicaments { get; set; } = null!;
    
    public DateTime Date { get; set; }
    public DateTime DueDate { get; set; }
}
