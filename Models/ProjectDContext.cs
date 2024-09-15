using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using static D05Project.Models.Admin;
using D05Project.Models;


namespace D05Project.Models;

public partial class ProjectDContext : DbContext
{
    public ProjectDContext(DbContextOptions<ProjectDContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Admin> Admin { get; set; }

    public virtual DbSet<Cart> Cart { get; set; }

    public virtual DbSet<Category> Category { get; set; }

    public virtual DbSet<Member> Member { get; set; }

    public virtual DbSet<OrderDetail> OrderDetail { get; set; }

    public virtual DbSet<Orders> Orders { get; set; }

    public virtual DbSet<ProcurmentSheet> ProcurmentSheet { get; set; }

    public virtual DbSet<Product> Product { get; set; }

    public virtual DbSet<PurchaseRecord> PurchaseRecord { get; set; }

    public virtual DbSet<Stocksheet> Stocksheet { get; set; }

    public virtual DbSet<Supplier> Supplier { get; set; }

    public virtual DbSet<SysAdmin> SysAdmin { get; set; }

    public virtual DbSet<withdrawalRecord> withdrawalRecord { get; set; }

    public virtual DbSet<Book> Book { get; set; }

    public IEnumerable Addresses { get; internal set; }
    public IEnumerable Products { get; internal set; }
    public IEnumerable Members { get; internal set; }
    public object OrderDetails { get; internal set; }

    //public DbSet<Rebook> Rebooks { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Admin>(entity =>
        {
            entity.HasKey(e => e.aId).HasName("PK__Admin__DE518A0660AAA025");

            entity.Property(e => e.aId)
                .HasMaxLength(6)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.Name)
                .HasMaxLength(8)
                .IsUnicode(false);
            entity.Property(e => e.Password)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.cId)
                .HasMaxLength(10)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.sysId)
                .HasMaxLength(6)
                .IsUnicode(false)
                .IsFixedLength();

            entity.HasOne(d => d.sys).WithMany(p => p.Admin)
                .HasForeignKey(d => d.sysId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Admin__sysId__398D8EEE");
        });

        modelBuilder.Entity<Cart>(entity =>
        {
            entity.HasKey(e => e.cartId).HasName("PK__Cart__415B03B8CD7FFAFB");

            entity.Property(e => e.cartId)
                .HasMaxLength(25)
                .IsUnicode(false);
            entity.Property(e => e.MemId)
                .HasMaxLength(11)
                .IsUnicode(false);
            entity.Property(e => e.orderId)
                .HasMaxLength(11)
                .IsUnicode(false);
            entity.Property(e => e.pId)
                .HasMaxLength(4)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.productName)
                .HasMaxLength(25)
                .IsUnicode(false);
            entity.Property(e => e.productprice).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.productqty).HasColumnType("decimal(18, 0)");
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.catId).HasName("PK__Category__17B6DD0688A8B1FC");

            entity.Property(e => e.catId)
                .HasMaxLength(4)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.catName)
                .HasMaxLength(40)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Member>(entity =>
        {
            entity.HasKey(e => e.MemId).HasName("PK__Member__E9341AC2E4E6F30E");

            entity.Property(e => e.MemId)
                .HasMaxLength(11)
                .IsUnicode(false);
            entity.Property(e => e.MemEmail)
                .HasMaxLength(25)
                .IsUnicode(false);
            entity.Property(e => e.MemName)
                .HasMaxLength(11)
                .IsUnicode(false);
            entity.Property(e => e.MemTel).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.Memaddress)
                .HasMaxLength(40)
                .IsUnicode(false);
        });

        modelBuilder.Entity<OrderDetail>(entity =>
        {
            entity.HasKey(e => new { e.orderId, e.pId }).HasName("PK__OrderDet__35DA5E0BCC06AB76");

            entity.Property(e => e.orderId)
                .HasMaxLength(11)
                .IsUnicode(false);
            entity.Property(e => e.pId)
                .HasMaxLength(4)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.ProductPrice).HasColumnType("money");
            entity.Property(e => e.quantity).HasColumnType("decimal(6, 0)");

            entity.HasOne(d => d.order).WithMany(p => p.OrderDetail)
                .HasForeignKey(d => d.orderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_OrderDetail_Orders");

            entity.HasOne(d => d.pIdNavigation).WithMany(p => p.OrderDetail)
                .HasForeignKey(d => d.pId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_OrderDetail_Product");
        });

        modelBuilder.Entity<Orders>(entity =>
        {
            entity.HasKey(e => e.orderId).HasName("PK__Orders__0809335D68C2D055");

            entity.Property(e => e.orderId)
                .HasMaxLength(11)
                .IsUnicode(false);
            entity.Property(e => e.Freight).HasColumnType("money");
            entity.Property(e => e.MemId)
                .HasMaxLength(11)
                .IsUnicode(false);
            entity.Property(e => e.SName).HasMaxLength(20);
            entity.Property(e => e.aId)
                .HasMaxLength(6)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.orderDate).HasColumnType("datetime");
            entity.Property(e => e.requiredDate).HasColumnType("datetime");
            entity.Property(e => e.shippedDate).HasColumnType("datetime");

            entity.HasOne(d => d.Mem).WithMany(p => p.Orders)
                .HasForeignKey(d => d.MemId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Orders__MemId__5CD6CB2B");

            entity.HasOne(d => d.aIdNavigation).WithMany(p => p.Orders)
                .HasForeignKey(d => d.aId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Orders__aId__5DCAEF64");
        });

        modelBuilder.Entity<ProcurmentSheet>(entity =>
        {
            entity.HasKey(e => e.pNo).HasName("PK__Procurme__DD36BDD16B20EA2A");

            entity.Property(e => e.pNo)
                .HasMaxLength(8)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.aId)
                .HasMaxLength(6)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.pNote)
                .HasMaxLength(10)
                .IsUnicode(false);

            entity.HasOne(d => d.aIdNavigation).WithMany(p => p.ProcurmentSheet)
                .HasForeignKey(d => d.aId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ProcurmentS__aId__4222D4EF");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.pId).HasName("PK__Product__DD36D562690961A1");

            entity.Property(e => e.pId)
                .HasMaxLength(4)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.catId)
                .HasMaxLength(4)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.prodoctName)
                .HasMaxLength(25)
                .IsUnicode(false);
            entity.Property(e => e.prodoctPrice).HasColumnType("money");
            entity.Property(e => e.quantity).HasColumnType("decimal(6, 0)");
            entity.Property(e => e.sId)
                .HasMaxLength(4)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.saveQuantity).HasColumnType("decimal(6, 0)");
            entity.Property(e => e.unit)
                .HasMaxLength(6)
                .IsUnicode(false)
                .IsFixedLength();

            entity.HasOne(d => d.cat).WithMany(p => p.Product)
                .HasForeignKey(d => d.catId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Product__catId__48CFD27E");

            entity.HasOne(d => d.sIdNavigation).WithMany(p => p.Product)
                .HasForeignKey(d => d.sId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Product_Supplier");
        });

        modelBuilder.Entity<PurchaseRecord>(entity =>
        {
            entity.HasKey(e => new { e.sId, e.pId, e.sNo }).HasName("PK__Purchase__8FD06A58B850EA69");

            entity.Property(e => e.sId)
                .HasMaxLength(4)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.pId)
                .HasMaxLength(4)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.sNo)
                .HasMaxLength(8)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.quantity).HasColumnType("decimal(6, 0)");

            entity.HasOne(d => d.pIdNavigation).WithMany(p => p.PurchaseRecord)
                .HasForeignKey(d => d.pId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__PurchaseRec__pId__5165187F");

            entity.HasOne(d => d.sIdNavigation).WithMany(p => p.PurchaseRecord)
                .HasForeignKey(d => d.sId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__PurchaseRec__sId__5070F446");

            entity.HasOne(d => d.sNoNavigation).WithMany(p => p.PurchaseRecord)
                .HasForeignKey(d => d.sNo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__PurchaseRec__sNo__52593CB8");
        });

        modelBuilder.Entity<Stocksheet>(entity =>
        {
            entity.HasKey(e => e.sNo).HasName("PK__Stockshe__DDDE606E62D7F5A2");

            entity.Property(e => e.sNo)
                .HasMaxLength(8)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.aId)
                .HasMaxLength(6)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.sNote)
                .HasMaxLength(10)
                .IsUnicode(false);

            entity.HasOne(d => d.aIdNavigation).WithMany(p => p.Stocksheet)
                .HasForeignKey(d => d.aId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Stocksheet__aId__3F466844");
        });

        modelBuilder.Entity<Supplier>(entity =>
        {
            entity.HasKey(e => e.sId).HasName("PK__Supplier__DDDED96EEFEC1E6E");

            entity.Property(e => e.sId)
                .HasMaxLength(4)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.Charge)
                .HasMaxLength(8)
                .IsUnicode(false);
            entity.Property(e => e.address)
                .HasMaxLength(40)
                .IsUnicode(false);
            entity.Property(e => e.company)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.phone).HasColumnType("decimal(18, 0)");
        });

        modelBuilder.Entity<SysAdmin>(entity =>
        {
            entity.HasKey(e => e.sysId).HasName("PK__SysAdmin__9BCC7A1A0D6CEA3B");

            entity.Property(e => e.sysId)
                .HasMaxLength(6)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.syscId)
                .HasMaxLength(10)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.sysname)
                .HasMaxLength(8)
                .IsUnicode(false);
            entity.Property(e => e.syspassword)
                .HasMaxLength(10)
                .IsUnicode(false);
        });

        modelBuilder.Entity<withdrawalRecord>(entity =>
        {
            entity.HasKey(e => new { e.sId, e.pId, e.pNo }).HasName("PK__withdraw__30D08285D00DB336");

            entity.Property(e => e.sId)
                .HasMaxLength(4)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.pId)
                .HasMaxLength(4)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.pNo)
                .HasMaxLength(8)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.quantity).HasColumnType("decimal(6, 0)");

            entity.HasOne(d => d.pIdNavigation).WithMany(p => p.withdrawalRecord)
                .HasForeignKey(d => d.pId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__withdrawalR__pId__4CA06362");

            entity.HasOne(d => d.pNoNavigation).WithMany(p => p.withdrawalRecord)
                .HasForeignKey(d => d.pNo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__withdrawalR__pNo__4D94879B");

            entity.HasOne(d => d.sIdNavigation).WithMany(p => p.withdrawalRecord)
                .HasForeignKey(d => d.sId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__withdrawalR__sId__4BAC3F29");
        });
        modelBuilder.Entity<Book>(entity =>
        {
            entity.HasKey(e => e.GId).HasName("PK__Book__7C4A4F017B035A7C"); // Choose a suitable name for the primary key

            entity.Property(e => e.GId)
                .HasColumnName("GId");

            entity.Property(e => e.Title)
                .HasMaxLength(30);

            entity.Property(e => e.Description)
                .HasColumnType("nvarchar(MAX)");

            entity.Property(e => e.Author)
                .HasMaxLength(20);

            entity.Property(e => e.TimeStamp)
                .HasColumnType("datetime2(7)");

            entity.Property(e => e.Photo)
                .HasColumnType("varbinary(MAX)");

            entity.Property(e => e.ImageType)
                .HasColumnType("nvarchar(MAX)");
        });

      

            // Configure the ReBook entity
            modelBuilder.Entity<ReBook>(entity =>
            {
                entity.ToTable("ReBook"); // Table name in the database

                // Configure primary key
                entity.HasKey(e => e.GId);

                // Configure column properties
                entity.Property(e => e.GId)
                    .ValueGeneratedNever(); // This matches the DatabaseGeneratedOption.None

             

                entity.Property(e => e.Description)
                    .HasColumnType("NVARCHAR(MAX)");

                entity.Property(e => e.Author)
                    .HasMaxLength(10);

                entity.Property(e => e.TimeStamp)
                    .HasColumnType("DATETIME2(7)");

                // Note: Photo and ImageType are not included in the original SQL table definition
                // Add them if necessary, otherwise, they can be omitted
             

            });
        }



    
    
    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

public DbSet<D05Project.Models.ReBook> ReBook { get; set; } = default!;

}
