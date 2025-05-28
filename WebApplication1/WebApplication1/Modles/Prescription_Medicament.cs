using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Modles;


[Table("Prescription_Medicament")]
[PrimaryKey(nameof(PrescriptionId), nameof(MedicamentId))]
public class Prescription_Medicament
{
    [Column("MedicamentId")] public int MedicamentId { get; set; }

    [Column("PrescriptionId")] public int PrescriptionId { get; set; }

    [ForeignKey(nameof(MedicamentId))] public virtual Medicament Medicament { get; set; } = null!;

    [ForeignKey(nameof(PrescriptionId))] public virtual Prescription Prescription { get; set; } = null!;

    public int Dose { get; set; }
    
    [MaxLength(100)]
    public string Details { get; set; } = null!;
}
    