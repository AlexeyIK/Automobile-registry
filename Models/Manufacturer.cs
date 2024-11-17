namespace Postgres.Models;

public partial class Manufacturer
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Vehicle> VehiclesLists { get; set; } = new List<Vehicle>();
}
