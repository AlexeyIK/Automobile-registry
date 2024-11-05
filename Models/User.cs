using System;
using System.Collections.Generic;

namespace PostgreTest.Models;

public partial class User
{
    public int Id { get; set; }

    public string FirstName { get; set; } = null!;

    public string FamilyName { get; set; } = null!;

    public string Login { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string? PassportNumber { get; set; }

    public int Role { get; set; }

    public DateOnly? PassportDate { get; set; }

    public virtual Role RoleNavigation { get; set; } = null!;

    public virtual ICollection<Vehicle> VehiclesListCreatedByNavigations { get; set; } = new List<Vehicle>();

    public virtual ICollection<Vehicle> VehiclesListOwnedByNavigations { get; set; } = new List<Vehicle>();
}
