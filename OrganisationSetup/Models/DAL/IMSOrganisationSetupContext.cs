using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.DataProtection.EntityFrameworkCore;
namespace OrganisationSetup.Models.DAL;

public partial class IMSOrganisationSetupContext : DbContext, IDataProtectionKeyContext
{
    public IMSOrganisationSetupContext(DbContextOptions<IMSOrganisationSetupContext> options)
        : base(options)
    {

    }
    public DbSet<DataProtectionKey> DataProtectionKeys { get; set; }
    public virtual DbSet<IRight> IRights { get; set; }

    public virtual DbSet<OSBranch> OSBranches { get; set; }

    public virtual DbSet<OSCompany> OSCompanies { get; set; }

    public virtual DbSet<OSUser> OSUsers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<IRight>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("IRight");

            entity.Property(e => e.Id).ValueGeneratedOnAdd();
        });

        modelBuilder.Entity<OSBranch>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__OSBranch__3214EC0765E6ED06");

            entity.ToTable("OSBranch");

            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
        });

        modelBuilder.Entity<OSCompany>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__OSCompan__3214EC07180641D0");

            entity.ToTable("OSCompany");

            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
        });

        modelBuilder.Entity<OSUser>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__OSUser__3214EC0740E9DCA6");

            entity.ToTable("OSUser");

            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
