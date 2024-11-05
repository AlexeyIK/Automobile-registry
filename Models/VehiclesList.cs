using System;
using System.Collections.Generic;

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

    public short Power { get; set; }

    public short Mass { get; set; }

    public int? OwnedBy { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime EditedDate { get; set; }

    public virtual AutomobileColor ColorNavigation { get; set; } = null!;

    public virtual User? CreatedByNavigation { get; set; }

    public virtual Manufacturer ManufacturerNavigation { get; set; } = null!;

    public virtual User? OwnedByNavigation { get; set; }

    public virtual VehicleType TypeNavigation { get; set; } = null!;
}
