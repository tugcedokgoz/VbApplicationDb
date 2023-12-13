using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace VbApplication.Model.Models;

public partial class DbApplicationVbContext : DbContext
{
    public DbApplicationVbContext()
    {
    }

    public DbApplicationVbContext(DbContextOptions<DbApplicationVbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<Intern> Interns { get; set; }

    public virtual DbSet<LegalPerson> LegalPeople { get; set; }

    public virtual DbSet<Supplier> Suppliers { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=DESKTOP-R04PVQ3\\SQLEXPRESS02; Initial Catalog=DbApplicationVb; Integrated Security=true; TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Customer__3214EC0749249C61");

            entity.ToTable("Customer");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.DateOfBirth).HasColumnType("date");
            entity.Property(e => e.Email).HasMaxLength(50);
            entity.Property(e => e.Gender).HasMaxLength(5);
            entity.Property(e => e.Password).HasMaxLength(15);
            entity.Property(e => e.Title).HasMaxLength(20);
            entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
            entity.Property(e => e.UserName).HasMaxLength(20);

            entity.HasOne(d => d.User).WithMany(p => p.Customers)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Customer_Users");
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Employee__3214EC07284A0491");

            entity.ToTable("Employee");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.Department).HasMaxLength(50);
            entity.Property(e => e.Email).HasMaxLength(50);
            entity.Property(e => e.Password).HasMaxLength(15);
            entity.Property(e => e.Position).HasMaxLength(20);
            entity.Property(e => e.Salary).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
            entity.Property(e => e.UserName).HasMaxLength(20);

            entity.HasOne(d => d.User).WithMany(p => p.Employees)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Employee_Users");
        });

        modelBuilder.Entity<Intern>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Intern__3214EC07779202B8");

            entity.ToTable("Intern");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.Department).HasMaxLength(50);
            entity.Property(e => e.Email).HasMaxLength(50);
            entity.Property(e => e.Grade).HasMaxLength(150);
            entity.Property(e => e.Password).HasMaxLength(15);
            entity.Property(e => e.School).HasMaxLength(100);
            entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
            entity.Property(e => e.UserName).HasMaxLength(20);

            entity.HasOne(d => d.User).WithMany(p => p.Interns)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Intern_Users");
        });

        modelBuilder.Entity<LegalPerson>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__LegalPer__3214EC0700F222FC");

            entity.ToTable("LegalPerson");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.CompanyName).HasMaxLength(50);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.Email).HasMaxLength(50);
            entity.Property(e => e.Fax).HasMaxLength(11);
            entity.Property(e => e.Password).HasMaxLength(15);
            entity.Property(e => e.Phone).HasMaxLength(14);
            entity.Property(e => e.TaxNumber).HasMaxLength(11);
            entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
            entity.Property(e => e.UserName).HasMaxLength(20);

            entity.HasOne(d => d.User).WithMany(p => p.LegalPeople)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_LegalPerson_Users");
        });

        modelBuilder.Entity<Supplier>(entity =>
        {
            entity.ToTable("Supplier");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.ContactName).HasMaxLength(50);
            entity.Property(e => e.ContactSurname).HasMaxLength(50);
            entity.Property(e => e.ContactTitle).HasMaxLength(50);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.Email).HasMaxLength(50);
            entity.Property(e => e.Password).HasMaxLength(15);
            entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
            entity.Property(e => e.UserName).HasMaxLength(20);

            entity.HasOne(d => d.User).WithMany(p => p.Suppliers)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Supplier_Users");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Users__3214EC0764E2E622");

            entity.HasIndex(e => e.Email, "UQ__Users__A9D10534FEC791D4").IsUnique();

            entity.Property(e => e.Active)
                .IsRequired()
                .HasDefaultValueSql("((1))");
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Password)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
            entity.Property(e => e.UserName)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
