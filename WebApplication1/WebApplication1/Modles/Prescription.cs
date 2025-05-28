using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Modles;

[Table("Prescription")]
public class Prescription
{
    [Key]
    public int IdPrescription { get; set; }
    
    public DateTime Date { get; set; }
    
    public DateTime DueDate { get; set; }
    
    [Column("PatientId")]
    public int PatientId { get; set; }
    
    [Column("DoctorId")]
    public int DoctorId { get; set; }
    
    [ForeignKey(nameof(PatientId))]
    public virtual Patient Patient { get; set; } = null!;
    
    [ForeignKey(nameof(DoctorId))]
    public virtual Doctor Doctor { get; set; } = null!;
    
    public virtual ICollection<Prescription_Medicament>  PrescriptionMedicaments { get; set; } = null!;
    
}