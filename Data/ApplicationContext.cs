using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Postgres.Models;

namespace AutomobileRegisty__kursovaya_;

public partial class ApplicationContext : DbContext
{
    private const string DB_HOST = "localhost";
    private const string DB_NAME = "alexk";
    private const string DB_USERNAME = "postgres";
    private const string DB_PASSWORD = "";

    public virtual DbSet<AutomobileColor> Colors { get; set; }

    public virtual DbSet<Manufacturer> Manufacturers { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<VehicleType> VehicleTypes { get; set; }

    public virtual DbSet<Vehicle> VehiclesList { get; set; }

    public ApplicationContext()
    {
    }

    public ApplicationContext(DbContextOptions<ApplicationContext> options)
        : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.LogTo(message => Debug.WriteLine(message)).EnableSensitiveDataLogging();
        optionsBuilder.UseNpgsql($"Host={DB_HOST};Database={DB_NAME};Username={DB_USERNAME};Password={DB_PASSWORD}");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AutomobileColor>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("color_pkey");
            entity.ToTable("colors");
            entity.Property(e => e.Id)
                .HasDefaultValueSql("nextval('color_id_seq'::regclass)")
                .HasColumnName("id");
            entity.Property(e => e.ColorName)
                .HasColumnName("color_name");
        });

        modelBuilder.Entity<Manufacturer>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("manufacturer_pkey");
            entity.ToTable("manufacturers");
            entity.Property(e => e.Id)
                .HasDefaultValueSql("nextval('manufacturer_id_seq'::regclass)")
                .HasColumnName("id");
            entity.Property(e => e.Name)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("roles_pkey");
            entity.ToTable("roles");
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .HasColumnName("name");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("users_pkey");
            entity.ToTable("users");
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.FamilyName)
                .HasMaxLength(255)
                .HasColumnName("family_name");
            entity.Property(e => e.FirstName)
                .HasMaxLength(255)
                .HasColumnName("first_name");
            entity.Property(e => e.Login)
                .HasMaxLength(255)
                .HasColumnName("login");
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .HasColumnName("password");
            entity.Property(e => e.Role).HasColumnName("role");
            entity.Property(e => e.PassportDate)
                .HasColumnType("date")
                .HasColumnName("passport_date");
            entity.Property(e => e.PassportNumber)
                .HasMaxLength(10)
                .HasColumnName("passport_number");
            entity.HasOne(d => d.RoleNavigation).WithMany(p => p.Users)
                .HasForeignKey(d => d.Role)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("users_role_fkey");
        });

        modelBuilder.Entity<VehicleType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("vehicle_type_pkey");
            entity.ToTable("vehicle_types");
            entity.Property(e => e.Id)
                .HasDefaultValueSql("nextval('vehicle_type_id_seq'::regclass)")
                .HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(32)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Vehicle>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("vehicles_list_pkey");
            entity.ToTable("vehicles_list");
            entity.Property(e => e.Id)
                .HasDefaultValueSql("nextval('vehicles_list_id_seq'::regclass)")
                .HasColumnName("id");
            entity.Property(e => e.Color).HasColumnName("color");
            entity.Property(e => e.EngineVolume).HasColumnName("engine_volume");
            entity.Property(e => e.Manufacturer).HasColumnName("manufacturer");
            entity.Property(e => e.Mass).HasColumnName("mass");
            entity.Property(e => e.Model)
                .HasMaxLength(64)
                .HasColumnName("model");
            entity.Property(e => e.EnginePower).HasColumnName("power");
            entity.Property(e => e.Type).HasColumnName("type");
            entity.Property(e => e.Vin)
                .HasMaxLength(17)
                .HasColumnName("vin");
            entity.Property(e => e.Year).HasColumnName("year");
            entity.Property(e => e.Number)
                .HasMaxLength(9)
                .HasColumnName("number");

            entity.Property(e => e.OwnedBy).HasColumnName("owned_by");
            entity.Property(e => e.Creator).HasColumnName("created_by");
            entity.Property(e => e.Editor).HasColumnName("edited_by");
            entity.Property(e => e.CreatedAt)
                .HasColumnName("created_at")
                .HasColumnType("timestamp without time zone");
            entity.Property(e => e.EditedAt)
                .HasColumnName("edited_at")
                .HasColumnType("timestamp without time zone");

            entity.HasOne(d => d.ColorNavigation).WithMany(p => p.VehiclesLists)
                .HasForeignKey(d => d.Color)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("vehicles_list_color_fkey");

            entity.HasOne(d => d.ManufacturerNavigation).WithMany(p => p.VehiclesLists)
                .HasForeignKey(d => d.Manufacturer)
                .HasConstraintName("vehicles_list_manufacturer_fkey");

            entity.HasOne(d => d.TypeNavigation).WithMany(p => p.VehiclesLists)
                .HasForeignKey(d => d.Type)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("vehicles_list_type_fkey");

            entity.HasOne(d => d.OwnedByNavigation)
                .WithMany(p => p.OwnedVehicles)
                .HasForeignKey(d => d.OwnedBy)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("vehicles_list_owned_by_fkey");

            entity.HasOne(d => d.CreatorNavigation)
                .WithMany(p => p.CreatedVehicles)
                .HasForeignKey(d => d.Creator)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("vehicles_list_creator_fkey");

            entity.HasOne(d => d.EditorNavigation)
                .WithMany(p => p.ModifiedVehicles)
                .HasForeignKey(d => d.Editor)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("vehicles_list_editor_fkey");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
