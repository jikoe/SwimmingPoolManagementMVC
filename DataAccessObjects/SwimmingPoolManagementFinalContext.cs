using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace BussinessObjects.Models;

public partial class SwimmingPoolManagementFinalContext : DbContext
{
    public SwimmingPoolManagementFinalContext()
    {
    }

    public SwimmingPoolManagementFinalContext(DbContextOptions<SwimmingPoolManagementFinalContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Invoice> Invoices { get; set; }

    public virtual DbSet<InvoiceDetail> InvoiceDetails { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<SwimmingPool> SwimmingPools { get; set; }

    public virtual DbSet<Ticket> Tickets { get; set; }

    public virtual DbSet<TicketType> TicketTypes { get; set; }

    public virtual DbSet<TicketUsage> TicketUsages { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer(config.GetConnectionString("DBContext"));
        }
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.CategoryId).HasName("PK__Categori__19093A0BE33F3929");

            entity.Property(e => e.CategoryName).HasMaxLength(100);
        });

        modelBuilder.Entity<Invoice>(entity =>
        {
            entity.HasKey(e => e.InvoiceId).HasName("PK__Invoices__D796AAB528C12DFD");

            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Discount)
                .HasDefaultValue(0m)
                .HasColumnType("decimal(5, 2)");
            entity.Property(e => e.FinalAmount)
                .HasComputedColumnSql("([TotalAmount]-([TotalAmount]*[Discount])/(100))", false)
                .HasColumnType("decimal(29, 8)");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasDefaultValue("Paid");
            entity.Property(e => e.TotalAmount).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.User).WithMany(p => p.Invoices)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Invoices__UserId__4BAC3F29");
        });

        modelBuilder.Entity<InvoiceDetail>(entity =>
        {
            entity.HasKey(e => e.InvoiceDetailId).HasName("PK__InvoiceD__1F157811EB6F2667");

            entity.Property(e => e.Price).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.SubTotal)
                .HasComputedColumnSql("([Quantity]*[Price])", false)
                .HasColumnType("decimal(29, 2)");

            entity.HasOne(d => d.Invoice).WithMany(p => p.InvoiceDetails)
                .HasForeignKey(d => d.InvoiceId)
                .HasConstraintName("FK__InvoiceDe__Invoi__4F7CD00D");

            entity.HasOne(d => d.Product).WithMany(p => p.InvoiceDetails)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK__InvoiceDe__Produ__5070F446");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.ProductId).HasName("PK__Products__B40CC6CD402B736F");

            entity.Property(e => e.IsRentable).HasDefaultValue(false);
            entity.Property(e => e.Price).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.ProductName).HasMaxLength(150);
            entity.Property(e => e.RentalPrice)
                .HasDefaultValue(0m)
                .HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.Category).WithMany(p => p.Products)
                .HasForeignKey(d => d.CategoryId)
                .HasConstraintName("FK__Products__Catego__45F365D3");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("PK__Roles__8AFACE1A5B38AAB6");

            entity.HasIndex(e => e.RoleName, "UQ__Roles__8A2B61605C69BE1F").IsUnique();

            entity.Property(e => e.RoleName).HasMaxLength(50);
        });

        modelBuilder.Entity<SwimmingPool>(entity =>
        {
            entity.HasKey(e => e.PoolId).HasName("PK__Swimming__EEFA8AEF0AA1CFFC");

            entity.Property(e => e.Location).HasMaxLength(200);
            entity.Property(e => e.Name).HasMaxLength(100);
        });

        modelBuilder.Entity<Ticket>(entity =>
        {
            entity.HasKey(e => e.TicketId).HasName("PK__Tickets__712CC607350B833E");

            entity.Property(e => e.ExpiryDate).HasColumnType("datetime");
            entity.Property(e => e.IsUsed).HasDefaultValue(false);
            entity.Property(e => e.PurchaseDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Customer).WithMany(p => p.Tickets)
                .HasForeignKey(d => d.CustomerId)
                .HasConstraintName("FK__Tickets__Custome__571DF1D5");

            entity.HasOne(d => d.TicketType).WithMany(p => p.Tickets)
                .HasForeignKey(d => d.TicketTypeId)
                .HasConstraintName("FK__Tickets__TicketT__5812160E");
        });

        modelBuilder.Entity<TicketType>(entity =>
        {
            entity.HasKey(e => e.TicketTypeId).HasName("PK__TicketTy__6CD684312E2D0DF8");

            entity.Property(e => e.Price).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.TicketName).HasMaxLength(100);
        });

        modelBuilder.Entity<TicketUsage>(entity =>
        {
            entity.HasKey(e => e.UsageId).HasName("PK__TicketUs__29B1972032B62ABA");

            entity.Property(e => e.Shift).HasMaxLength(20);

            entity.HasOne(d => d.Ticket).WithMany(p => p.TicketUsages)
                .HasForeignKey(d => d.TicketId)
                .HasConstraintName("FK__TicketUsa__Ticke__5AEE82B9");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Users__1788CC4C81F578A7");

            entity.HasIndex(e => e.Username, "UQ__Users__536C85E4E9298BEF").IsUnique();

            entity.Property(e => e.FullName).HasMaxLength(100);
            entity.Property(e => e.Password).HasMaxLength(100);
            entity.Property(e => e.Username).HasMaxLength(50);

            entity.HasOne(d => d.Role).WithMany(p => p.Users)
                .HasForeignKey(d => d.RoleId)
                .HasConstraintName("FK__Users__RoleId__3B75D760");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
