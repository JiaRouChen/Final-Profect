using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ProductAPI.Models;

public partial class dbProductsContext : DbContext
{
    public dbProductsContext(DbContextOptions<dbProductsContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Category> Category { get; set; }

    public virtual DbSet<sProduct> sProduct { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.CatId).HasName("PK__Category__6A1C8AFAB0F5DD60");

            entity.Property(e => e.CatId)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.CatName)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<sProduct>(entity =>
        {
            entity.HasKey(e => e.PId).HasName("PK__sProduct__C5775540A21A559D");

            entity.Property(e => e.PId)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.CatId)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.PName)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.Cat).WithMany(p => p.sProduct)
                .HasForeignKey(d => d.CatId)
                .HasConstraintName("FK_Category");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
