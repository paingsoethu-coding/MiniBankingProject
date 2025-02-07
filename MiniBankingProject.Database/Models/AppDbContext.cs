using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace MiniBankingProject.Database.Models;

public partial class AppDbContext : DbContext
{
    public AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<TblTransaction> TblTransactions { get; set; }

    public virtual DbSet<TblUser> TblUsers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=MSI\\SQLEXPRESS2022; Database=MiniDigitalWallet; User Id=sa; Password=sasa; TrustServerCertificate=True;");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TblTransaction>(entity =>
        {
            entity.HasKey(e => e.TransactionId).HasName("PK__Tbl_Tran__55433A4BA3E7F165");

            entity.ToTable("Tbl_Transaction");

            entity.HasIndex(e => e.TransactionNo, "UQ__Tbl_Tran__554342D9F2D15EC8").IsUnique();

            entity.Property(e => e.TransactionId)
                .HasMaxLength(36)
                .IsUnicode(false)
                .HasDefaultValueSql("(newid())")
                .IsFixedLength()
                .HasColumnName("TransactionID");
            entity.Property(e => e.Dates)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.FromMobileNo)
                .HasMaxLength(13)
                .IsUnicode(false);
            entity.Property(e => e.ToMobileNo)
                .HasMaxLength(13)
                .IsUnicode(false);
            entity.Property(e => e.TransactionNo).ValueGeneratedOnAdd();
            entity.Property(e => e.TransactionType)
                .HasMaxLength(13)
                .IsUnicode(false);
            entity.Property(e => e.TransferedAmount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.User).WithMany(p => p.TblTransactions)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_Tbl_Transaction_User");
        });

        modelBuilder.Entity<TblUser>(entity =>
        {
            entity.HasKey(e => e.UserId);

            entity.ToTable("Tbl_User");

            entity.Property(e => e.UserId).HasColumnName("UserID");
            entity.Property(e => e.Balance).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.MobileNo)
                .HasMaxLength(13)
                .IsUnicode(false);
            entity.Property(e => e.Pin)
                .HasMaxLength(6)
                .IsUnicode(false);
            entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
