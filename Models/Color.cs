namespace Postgres.Models;

public partial class AutomobileColor
{
    public int Id { get; set; }

    public string ColorName { get; set; } = null!;

    public virtual ICollection<Vehicle> VehiclesLists { get; set; } = new List<Vehicle>();
}
