namespace VitalCheckWeb.API.VitalCheck.Domain.Models;

public class MedicineType
{
    public int MedicineTypeID { get; set; }
    public string TypeName { get; set; }

    // Relationships
    public IList<Medicine> Medicines { get; set; } = new List<Medicine>();
}