#nullable enable

namespace Postgres.Models;

public partial class User
{
    public int Id { get; set; }

    public string FirstName { get; set; } = null!;

    public string FamilyName { get; set; } = null!;

    public string Login { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string? PassportNumber { get; set; }

    public DateOnly? PassportDate { get; set; }

    public int Role { get; set; }

    public virtual Role RoleNavigation { get; set; } = null!;

    public virtual ICollection<Vehicle> OwnedVehicles { get; set; } = new List<Vehicle>();

    public virtual ICollection<Vehicle> CreatedVehicles { get; set; } = new List<Vehicle>();

    public virtual ICollection<Vehicle> ModifiedVehicles { get; set; } = new List<Vehicle>();
}
