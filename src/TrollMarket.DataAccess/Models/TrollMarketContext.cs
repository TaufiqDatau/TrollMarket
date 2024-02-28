using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace TrollMarket.DataAccess.Models;

public partial class TrollMarketContext : DbContext
{
    public TrollMarketContext()
    {
    }

    public TrollMarketContext(DbContextOptions<TrollMarketContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Account> Accounts { get; set; }

    public virtual DbSet<AccountRole> AccountRoles { get; set; }

    public virtual DbSet<Cart> Carts { get; set; }

    public virtual DbSet<Ledger> Ledgers { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<Shipment> Shipments { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Account>(entity =>
        {
            entity.HasKey(e => e.Username).HasName("PK__Account__536C85E5372B07D2");

            entity.ToTable("Account");

            entity.Property(e => e.Username)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Address).IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.Password)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Balance)
                .HasColumnType("money")
                .IsRequired()
                .HasDefaultValue(0.00);
        });

        modelBuilder.Entity<AccountRole>(entity =>
        {
            entity.HasKey(e => new { e.Username, e.Role }).HasName("PK__AccountR__BECDD1F64D18B86D");

            entity.Property(e => e.Username)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Role)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasConversion(
                v => v.ToString(),
                v => (RoleEnum)Enum.Parse(typeof(RoleEnum), v)
                );

            entity.HasOne(d => d.UsernameNavigation).WithMany(p => p.AccountRoles)
                .HasForeignKey(d => d.Username)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__AccountRo__Usern__267ABA7A");
        });

        modelBuilder.Entity<Cart>(entity =>
        {
            entity.HasKey(e => new { e.Buyer, e.ProductId, e.ShipmentId }).HasName("PK__Cart__4AE88BF7967CD93E");

            entity.ToTable("Cart");

            entity.Property(e => e.Buyer)
                .HasMaxLength(255)
                .IsUnicode(false);

            entity.HasOne(d => d.BuyerNavigation).WithMany(p => p.Carts)
                .HasForeignKey(d => d.Buyer)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Cart__Buyer__33D4B598");

            entity.HasOne(d => d.Product).WithMany(p => p.Carts)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Cart__ProductId__34C8D9D1");

            entity.HasOne(d => d.Shipment).WithMany(p => p.Carts)
                .HasForeignKey(d => d.ShipmentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Cart__ShipmentId__35BCFE0A");
        });

        modelBuilder.Entity<Ledger>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Ledger__3214EC27D14BF15C");

            entity.ToTable("Ledger");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.CurrentBalance).HasColumnType("money");
            entity.Property(e => e.FinalBalance).HasColumnType("money");
            entity.Property(e => e.Timestamps).HasColumnType("datetime");
            entity.Property(e => e.TransactionValue).HasColumnType("money");
            entity.Property(e => e.Type)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasConversion(
                v => v.ToString(),
                v => (TransactionTypeEnum)Enum.Parse(typeof(TransactionTypeEnum), v)
                );
            entity.Property(e => e.Username)
                .HasMaxLength(255)
                .IsUnicode(false);

            entity.HasOne(d => d.UsernameNavigation).WithMany(p => p.Ledgers)
                .HasForeignKey(d => d.Username)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Ledger__Username__2A4B4B5E");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Orders__3214EC2700B8CBD8");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Buyer)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.ShipmentPrice).HasColumnType("money");
            entity.Property(e => e.TransactionDate).HasColumnType("datetime");
            entity.Property(e => e.UnitPrice).HasColumnType("money");

            entity.HasOne(d => d.BuyerNavigation).WithMany(p => p.Orders)
                .HasForeignKey(d => d.Buyer)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Orders__Buyer__38996AB5");

            entity.HasOne(d => d.Product).WithMany(p => p.Orders)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Orders__ProductI__398D8EEE");

            entity.HasOne(d => d.Shipment).WithMany(p => p.Orders)
                .HasForeignKey(d => d.ShipmentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Orders__Shipment__3A81B327");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Product__3214EC270C45E56F");

            entity.ToTable("Product");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Category)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Description).IsUnicode(false);
            entity.Property(e => e.DiscontinueDate).HasColumnType("datetime");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Price).HasColumnType("money");
            entity.Property(e => e.Seller)
                .HasMaxLength(255)
                .IsUnicode(false);

            entity.HasOne(d => d.SellerNavigation).WithMany(p => p.Products)
                .HasForeignKey(d => d.Seller)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Product__Seller__2E1BDC42");
        });

        modelBuilder.Entity<Shipment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Shipment__3214EC27F5F3C9DC");

            entity.ToTable("Shipment");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Name)
                .HasMaxLength(155)
                .IsUnicode(false);
            entity.Property(e => e.Price).HasColumnType("money");
            entity.Property(e => e.ServiceStopDate).HasColumnType("datetime");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
