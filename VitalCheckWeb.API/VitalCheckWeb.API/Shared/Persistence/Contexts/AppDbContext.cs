using Microsoft.EntityFrameworkCore;
using VitalCheckWeb.API.Security.Domain.Models;
using VitalCheckWeb.API.Shared.Extensions;
using VitalCheckWeb.API.VitalCheck.Domain.Models;

namespace VitalCheckWeb.API.Shared.Persistence.Contexts;

public class AppDbContext : DbContext
{
    public DbSet<Client> Clients { get; set; }
    public DbSet<Dispatch> Dispatches { get; set; }
    public DbSet<Inventory> Inventories { get; set; }
    public DbSet<Medicine> Medicines { get; set; }
    public DbSet<MedicineType> MedicineTypes { get; set; }
    public DbSet<Sale> Sales { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<UserPlan> UserPlans { get; set; }
    public DbSet<UserType> UserTypes { get; set; }
    
    public AppDbContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        
        builder.Entity<Client>().ToTable("Clients");
        builder.Entity<Client>().HasKey(p => p.ClientID);
        builder.Entity<Client>().Property(p => p.ClientID).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Client>().Property(p => p.DNI).IsRequired().HasMaxLength(7);
        builder.Entity<Client>().Property(p => p.FirstName).IsRequired().HasMaxLength(255);
        builder.Entity<Client>().Property(p => p.LastName).IsRequired().HasMaxLength(255);
        builder.Entity<Client>().HasIndex(p => p.DNI).IsUnique();
        
        // Relationships
        builder.Entity<Client>()
            .HasMany(p => p.Sales)
            .WithOne(p => p.Client)
            .HasForeignKey(p => p.ClientID);

        builder.Entity<Dispatch>().ToTable("Dispatches");
        builder.Entity<Dispatch>().HasKey(p => p.DispatchID);
        builder.Entity<Dispatch>().Property(p => p.DispatchID).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Dispatch>().Property(p => p.Quantity).IsRequired();
        builder.Entity<Dispatch>().Property(p => p.Description).HasMaxLength(255);
        builder.Entity<Dispatch>().Property(p => p.EntryDate).IsRequired();
        builder.Entity<Dispatch>().Property(p => p.ExpiryDate).IsRequired();

        // Relationships
        builder.Entity<Dispatch>()
            .HasOne(d => d.User1)
            .WithMany(u => u.Dispatches1)
            .HasForeignKey(d => d.User1ID)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<Dispatch>()
            .HasOne(d => d.User2)
            .WithMany(u => u.Dispatches2)
            .HasForeignKey(d => d.User2ID)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<Inventory>().ToTable("Inventories");
        builder.Entity<Inventory>().HasKey(p => p.InventoryID);
        builder.Entity<Inventory>().Property(p => p.InventoryID).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Inventory>().Property(p => p.Quantity).IsRequired();
        builder.Entity<Inventory>().Property(p => p.SalePrice).IsRequired().HasColumnType("decimal(18,2)");

        builder.Entity<Medicine>().ToTable("Medicines");
        builder.Entity<Medicine>().HasKey(p => p.MedicineID);
        builder.Entity<Medicine>().Property(p => p.MedicineID).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Medicine>().Property(p => p.CommercialName).IsRequired().HasMaxLength(255);
        builder.Entity<Medicine>().Property(p => p.GenericName).IsRequired().HasMaxLength(255);
        builder.Entity<Medicine>().Property(p => p.CostPrice).IsRequired().HasColumnType("decimal(18,2)");
        
        // Relationships
        builder.Entity<Medicine>()
            .HasMany(p => p.Inventories)
            .WithOne(p => p.Medicine)
            .HasForeignKey(p => p.MedicineID);

        builder.Entity<Medicine>()
            .HasMany(p => p.Dispatches)
            .WithOne(p => p.Medicine)
            .HasForeignKey(p => p.MedicineID);

        builder.Entity<Medicine>()
            .HasMany(p => p.Sales)
            .WithOne(p => p.Medicine)
            .HasForeignKey(p => p.MedicineID);

        builder.Entity<MedicineType>().ToTable("MedicineTypes");
        builder.Entity<MedicineType>().HasKey(p => p.MedicineTypeID);
        builder.Entity<MedicineType>().Property(p => p.MedicineTypeID).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<MedicineType>().Property(p => p.TypeName).IsRequired().HasMaxLength(255);

        // Relationships
        builder.Entity<MedicineType>()
            .HasMany(p => p.Medicines)
            .WithOne(p => p.MedicineType)
            .HasForeignKey(p => p.MedicineTypeID);
        
        builder.Entity<UserPlan>().ToTable("UserPlans");
        builder.Entity<UserPlan>().HasKey(p => p.UserPlanID);
        builder.Entity<UserPlan>().Property(p => p.UserPlanID).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<UserPlan>().Property(p => p.PlanName).IsRequired().HasMaxLength(255);

        // Relationships
        builder.Entity<UserPlan>()
            .HasMany(p => p.Users)
            .WithOne(p => p.UserPlan)
            .HasForeignKey(p => p.UserPlanID);
        
        builder.Entity<UserType>().ToTable("UserTypes");
        builder.Entity<UserType>().HasKey(t => t.UserTypeID);
        builder.Entity<UserType>().Property(t => t.UserTypeID).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<UserType>().Property(t => t.TypeName).IsRequired().HasMaxLength(255);

        // Relationships
        builder.Entity<UserType>()
            .HasMany(t => t.Users)
            .WithOne(t => t.UserType)
            .HasForeignKey(t => t.UserTypeID);

        // Relationships
        builder.Entity<Sale>().ToTable("Sales");
        builder.Entity<Sale>().HasKey(p => p.SaleID);
        builder.Entity<Sale>().Property(p => p.SaleID).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Sale>().Property(p => p.Quantity).IsRequired();
        builder.Entity<Sale>().Property(p => p.TotalPrice).IsRequired().HasColumnType("decimal(18,2)");
        builder.Entity<Sale>().Property(p => p.Date).IsRequired();

        builder.Entity<User>().ToTable("Users");
        builder.Entity<User>().HasKey(p => p.UserID);
        builder.Entity<User>().Property(p => p.UserID).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<User>().Property(p => p.UserName).IsRequired().HasMaxLength(255);
        builder.Entity<User>().Property(p => p.Email).IsRequired().HasMaxLength(255);
        builder.Entity<User>().Property(p => p.Password).IsRequired().HasMaxLength(255);
        builder.Entity<User>().Property(p => p.RUC).IsRequired();
        
        // Relationships
        builder.Entity<User>()
            .HasMany(p => p.Inventories)
            .WithOne(p => p.User)
            .HasForeignKey(p => p.UserID);
        
        builder.Entity<User>()
            .HasMany(p => p.Sales)
            .WithOne(p => p.User)
            .HasForeignKey(p => p.UserID);

        builder.UseSnakeCaseNamingConvention();
    }
    
}