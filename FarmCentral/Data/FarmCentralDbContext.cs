using System;
using System.Collections.Generic;
using FarmCentral.Models;
using Microsoft.EntityFrameworkCore;
using Type = FarmCentral.Models.Type;

namespace FarmCentral.Data;

public partial class FarmCentralDbContext : DbContext
{
    public FarmCentralDbContext()
    {
    }

    public FarmCentralDbContext(DbContextOptions<FarmCentralDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<Farmer> Farmers { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<Type> Types { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.EmployeeId).HasName("PK__Employee__C134C9A1D227EEFD");
        });

        modelBuilder.Entity<Farmer>(entity =>
        {
            entity.HasKey(e => e.FarmerId).HasName("PK__Farmer__EC6F88C8FBF44BAE");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasOne(d => d.Farmer).WithMany(p => p.Products)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Farmer");

            entity.HasOne(d => d.Type).WithMany(p => p.Products)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Type");
        });

        modelBuilder.Entity<Type>(entity =>
        {
            entity.HasKey(e => e.TypeId).HasName("PK__Type__F04DF11A9D0480E2");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
