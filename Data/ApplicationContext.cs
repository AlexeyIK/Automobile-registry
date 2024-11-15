using Microsoft.EntityFrameworkCore;
using PostgreTest.Models;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace AutomobileRegisty__kursovaya_;

public partial class ApplicationContext : DbContext
{
    public ApplicationContext()
    {
    }

    public ApplicationContext(DbContextOptions<ApplicationContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AutomobileColor> Colors { get; set; }

    public virtual DbSet<Manufacturer> Manufacturers { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<VehicleType> VehicleTypes { get; set; }

    public virtual DbSet<Vehicle> VehiclesList { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            IConfiguration configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var connectionString = configuration.GetConnectionString("DefaultConnection");
            optionsBuilder.UseNpgsql(connectionString);
        }
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
                .HasMaxLength(32)
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
                .HasColumnType("character varying")
                .HasColumnName("name");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("roles_pkey");

            entity.ToTable("roles");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(32)
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
            entity.Property(e => e.PassportDate).HasColumnName("passport_date");
            entity.Property(e => e.PassportNumber)
                .HasMaxLength(10)
                .HasColumnName("passport_number");
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .HasColumnName("password");
            entity.Property(e => e.Role).HasColumnName("role");

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
            entity.HasKey(e => e.Id).HasName("veicles_list_pkey");

            entity.ToTable("vehicles_list");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("nextval('veicles_list_id_seq'::regclass)")
                .HasColumnName("id");
            entity.Property(e => e.Color).HasColumnName("color");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.CreatedDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_date");
            entity.Property(e => e.EditedDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("edited_date");
            entity.Property(e => e.EngineVolume).HasColumnName("engine_volume");
            entity.Property(e => e.Manufacturer).HasColumnName("manufacturer");
            entity.Property(e => e.Mass).HasColumnName("mass");
            entity.Property(e => e.Model)
                .HasMaxLength(64)
                .HasColumnName("model");
            entity.Property(e => e.OwnedBy).HasColumnName("owned_by");
            entity.Property(e => e.EnginePower).HasColumnName("power");
            entity.Property(e => e.Type).HasColumnName("type");
            entity.Property(e => e.Vin)
                .HasMaxLength(17)
                .HasColumnName("vin");
            entity.Property(e => e.Year).HasColumnName("year");

            entity.HasOne(d => d.ColorNavigation).WithMany(p => p.VehiclesLists)
                .HasForeignKey(d => d.Color)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("veicles_list_color_fkey");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.VehiclesListCreatedByNavigations)
                .HasForeignKey(d => d.CreatedBy)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("vehicles_list_created_by_fkey");

            entity.HasOne(d => d.ManufacturerNavigation).WithMany(p => p.VehiclesLists)
                .HasForeignKey(d => d.Manufacturer)
                .HasConstraintName("veicles_list_manufacturer_fkey");

            entity.HasOne(d => d.OwnedByNavigation).WithMany(p => p.VehiclesListOwnedByNavigations)
                .HasForeignKey(d => d.OwnedBy)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("vehicles_list_owned_by_fkey");

            entity.HasOne(d => d.TypeNavigation).WithMany(p => p.VehiclesLists)
                .HasForeignKey(d => d.Type)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("veicles_list_type_fkey");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
