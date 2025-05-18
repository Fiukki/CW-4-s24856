using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CW_4_s24856.Models;

[Table("Prescription")]
public class Prescription
{
    [Key]
    public int IdPrescription { get; set; }
    
    public DateTime Date { get; set; }
    
    public DateTime DueDate { get; set; }
    public int IdPatient { get; set; } 
    public int IdDoctor { get; set; }
    
    [ForeignKey(nameof(IdPatient))]
    public virtual Patient Patient { get; set; }
    [ForeignKey(nameof(IdDoctor))]
    public virtual Doctor Doctor { get; set; }
    
    public virtual ICollection<PrescriptionMedicament> PrescriptionMedicament { get; set; }
}