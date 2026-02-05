using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace OrganisationSetup.Models.DAL;

public partial class IMSOrganisationSetupContext : DbContext
{
    public IMSOrganisationSetupContext()
    {
    }

    // This constructor is what Program.cs uses to inject the connection string
    public IMSOrganisationSetupContext(DbContextOptions<IMSOrganisationSetupContext> options)
        : base(options)
    {
    }

    public virtual DbSet<OSBranch> OSBranches { get; set; }
    public virtual DbSet<OSCompany> OSCompanies { get; set; }
    public virtual DbSet<OSUser> OSUsers { get; set; }
    public virtual DbSet<Right> Rights { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // We leave this empty. 
        // If it's not configured by DI, you could put a fallback here, 
        // but for web apps, keeping it empty ensures the Program.cs settings are used.
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<OSBranch>(entity =>
        {
            entity.HasNoKey().ToTable("OSBranch");
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
        });

        modelBuilder.Entity<OSCompany>(entity =>
        {
            entity.HasNoKey().ToTable("OSCompany");
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
        });

        modelBuilder.Entity<OSUser>(entity =>
        {
            entity.HasNoKey().ToTable("OSUser");
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
        });

        modelBuilder.Entity<Right>(entity =>
        {
            entity.HasNoKey().ToView("Right");
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}