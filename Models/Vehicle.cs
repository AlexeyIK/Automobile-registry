#nullable enable

namespace PostgreTest.Models;

public partial class Vehicle
{
    public int Id { get; set; }

    public int Manufacturer { get; set; }

    public string Model { get; set; } = null!;

    public string Vin { get; set; } = null!;

    public short Year { get; set; }

    public int Color { get; set; }

    public int Type { get; set; }

    public short EngineVolume { get; set; }

    public short EnginePower { get; set; }

    public short Mass { get; set; }

    public int? OwnedBy { get; set; }

    public int Creator { get; set; }

    public int? Editor { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? EditedAt { get; set; }

    public virtual AutomobileColor ColorNavigation { get; set; } = null!;

    public virtual User CreatorNavigation { get; set; } = null!;

    public virtual User? EditorNavigation { get; set; }

    public virtual Manufacturer ManufacturerNavigation { get; set; } = null!;

    public virtual User? OwnedByNavigation { get; set; }

    public virtual VehicleType TypeNavigation { get; set; } = null!;
}
