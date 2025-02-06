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
        optionsBuilder.UseSqlServer
            ("Data Source=MSI\\SQLEXPRESS2022; Initial Catalog=MiniDigitalWallet; User Id=sa; Password=sasa; TrustServerCertificate=True;");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TblTransaction>(entity =>
        {
            entity.HasKey(e => e.TransactionId);

            entity.ToTable("Tbl_Transaction");

            entity.Property(e => e.TransactionId)
                .ValueGeneratedNever()
                .HasColumnName("TransactionID");
            entity.Property(e => e.Dates).HasColumnType("datetime");
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
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Tbl_Transaction_Tbl_User");
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
