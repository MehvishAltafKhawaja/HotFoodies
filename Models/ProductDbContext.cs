using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ASPNETMVC.Models;

public partial class ProductDbContext : DbContext
{
    public ProductDbContext()
    {
    }

    public ProductDbContext(DbContextOptions<ProductDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<ProductTable> ProductTables { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ProductTable>(entity =>
        {
            entity.HasKey(e => e.ProdId).HasName("pk_prodid");

            entity.ToTable("ProductTable");

            entity.Property(e => e.ProdId).ValueGeneratedNever();
            entity.Property(e => e.ProdBatchId)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.ProdName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.ProdPrice).HasColumnType("decimal(10, 2)");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
