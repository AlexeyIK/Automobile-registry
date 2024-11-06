namespace PostgreTest.Models;

public partial class VehicleType
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Vehicle> VehiclesLists { get; set; } = new List<Vehicle>();
}
