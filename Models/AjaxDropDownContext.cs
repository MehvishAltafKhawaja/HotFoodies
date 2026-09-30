using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ASPNETMVC.Models;

public partial class AjaxDropDownContext : DbContext
{
    public AjaxDropDownContext()
    {
    }

    public AjaxDropDownContext(DbContextOptions<AjaxDropDownContext> options)
        : base(options)
    {
    }

    public virtual DbSet<District> Districts { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<State> States { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<District>(entity =>
        {
            entity.HasKey(e => e.DistrictId).HasName("pk_distId");

            entity.ToTable("districts");

            entity.Property(e => e.DistrictId).HasColumnName("districtId");
            entity.Property(e => e.DistrictName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("districtName");
            entity.Property(e => e.StateId).HasColumnName("stateId");

            entity.HasOne(d => d.State).WithMany(p => p.Districts)
                .HasForeignKey(d => d.StateId)
                .HasConstraintName("fk_stateId");
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.EmpId).HasName("pk_empId");

            entity.ToTable("Employee");

            entity.Property(e => e.EmpId).HasColumnName("empId");
            entity.Property(e => e.EmpDob).HasColumnName("empDOB");
            entity.Property(e => e.EmpGender)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("empGender");
            entity.Property(e => e.EmpName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("empName");
            entity.Property(e => e.StateId).HasColumnName("stateId");

            entity.HasOne(d => d.State).WithMany(p => p.Employees)
                .HasForeignKey(d => d.StateId)
                .HasConstraintName("fk_stateId1");
        });

        modelBuilder.Entity<State>(entity =>
        {
            entity.HasKey(e => e.StateId).HasName("pk_state");

            entity.ToTable("states");

            entity.Property(e => e.StateId).HasColumnName("stateId");
            entity.Property(e => e.StateName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("stateName");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
