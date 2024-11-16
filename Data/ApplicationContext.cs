using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PostgreTest.Models;

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

            entity.HasData(
                new AutomobileColor
                {
                    Id = 1,
                    ColorName = "Белый"
                },
                new AutomobileColor
                {
                    Id = 2,
                    ColorName = "Чёрный"
                },
                new AutomobileColor
                {
                    Id = 3,
                    ColorName = "Серый"
                },
                new AutomobileColor
                {
                    Id = 4,
                    ColorName = "Серебристый"
                },
                new AutomobileColor
                {
                    Id = 5,
                    ColorName = "Красный"
                },
                new AutomobileColor
                {
                    Id = 6,
                    ColorName = "Синий"
                },
                new AutomobileColor
                {
                    Id = 7,
                    ColorName = "Коричневый"
                },
                new AutomobileColor
                {
                    Id = 8,
                    ColorName = "Зелёный"
                },
                new AutomobileColor
                {
                    Id = 9,
                    ColorName = "Бежевый"
                },
                new AutomobileColor
                {
                    Id = 10,
                    ColorName = "Жёлтый"
                },
                new AutomobileColor
                {
                    Id = 11,
                    ColorName = "Оранжевый"
                },
                new AutomobileColor
                {
                    Id = 12,
                    ColorName = "Бордовый"
                }
            );
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

            entity.HasData(
                new Manufacturer
                {
                    Id = 1,
                    Name = "BMW"
                },
                new Manufacturer
                {
                    Id = 2,
                    Name = "Mercedes-Benz"
                },
                new Manufacturer
                {
                    Id = 3,
                    Name = "Volkswagen"
                },
                new Manufacturer
                {
                    Id = 4,
                    Name = "Audi"
                },
                new Manufacturer
                {
                    Id = 5,
                    Name = "Porsche"
                },
                new Manufacturer
                {
                    Id = 6,
                    Name = "Volvo"
                },
                new Manufacturer
                {
                    Id = 7,
                    Name = "Toyota"
                },
                new Manufacturer
                {
                    Id = 8,
                    Name = "Honda"
                },
                new Manufacturer
                {
                    Id = 9,
                    Name = "Nissan"
                },
                new Manufacturer
                {
                    Id = 10,
                    Name = "Mazda"
                },
                new Manufacturer
                {
                    Id = 11,
                    Name = "Subaru"
                },
                new Manufacturer
                {
                    Id = 12,
                    Name = "Mitsubishi"
                },
                new Manufacturer
                {
                    Id = 13,
                    Name = "Ford"
                },
                new Manufacturer
                {
                    Id = 14,
                    Name = "Chevrolet"
                },
                new Manufacturer
                {
                    Id = 15,
                    Name = "Chrysler"
                },
                new Manufacturer
                {
                    Id = 16,
                    Name = "Jeep"
                },
                new Manufacturer
                {
                    Id = 17,
                    Name = "Dodge"
                },
                new Manufacturer
                {
                    Id = 18,
                    Name = "Cadillac"
                }
            );
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("roles_pkey");

            entity.ToTable("roles");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(32)
                .HasColumnName("name");

            entity.HasData(
                new Role
                {
                    Id = 1,
                    Name = "Администратор"
                },
                new Role
                {
                    Id = 2,
                    Name = "Менеджер"
                },
                new Role
                {
                    Id = 3,
                    Name = "Клиент"
                }
            );
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

            entity.HasData(
                new VehicleType
                {
                    Id = 1,
                    Name = "Седан"
                },
                new VehicleType
                {
                    Id = 2,
                    Name = "Хэтчбек"
                },
                new VehicleType
                {
                    Id = 3,
                    Name = "Универсал"
                },
                new VehicleType
                {
                    Id = 4,
                    Name = "Купе"
                },
                new VehicleType
                {
                    Id = 5,
                    Name = "Кабриолет"
                },
                new VehicleType
                {
                    Id = 6,
                    Name = "Внедорожник"
                },
                new VehicleType
                {
                    Id = 7,
                    Name = "Кроссовер"
                },
                new VehicleType
                {
                    Id = 8,
                    Name = "Минивэн"
                },
                new VehicleType
                {
                    Id = 9,
                    Name = "Лифтбек"
                }
            );
        });

        modelBuilder.Entity<Vehicle>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("veicles_list_pkey");

            entity.ToTable("vehicles_list");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("nextval('veicles_list_id_seq'::regclass)")
                .HasColumnName("id");
            entity.Property(e => e.Color).HasColumnName("color");
            entity.Property(e => e.Creator).HasColumnName("created_by");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_date");
            entity.Property(e => e.EditedAt)
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
            entity.Property(e => e.Creator)
                .HasColumnName("creator")
                .HasColumnType("integer");
            entity.Property(e => e.Editor)
                .HasColumnName("editor")
                .HasColumnType("integer");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.EditedAt)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("edited_at");

            entity.HasOne(d => d.ColorNavigation).WithMany(p => p.VehiclesLists)
                .HasForeignKey(d => d.Color)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("veicles_list_color_fkey");

            entity.HasOne(d => d.CreatorNavigation).WithMany(p => p.VehiclesListCreatedByNavigations)
                .HasForeignKey(d => d.Creator)
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

            entity.HasOne(d => d.CreatorNavigation)
                .WithMany(p => p.VehiclesCreated)
                .HasForeignKey(d => d.Creator)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("vehicles_list_creator_fkey");

            entity.HasOne(d => d.EditorNavigation)
                .WithMany(p => p.VehiclesEdited)
                .HasForeignKey(d => d.Editor)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("vehicles_list_editor_fkey");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
