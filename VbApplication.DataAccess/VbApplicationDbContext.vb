Imports System
Imports System.Collections.Generic
Imports Microsoft.EntityFrameworkCore
Imports VbApplication.Modell.Models

Namespace Models
    Partial Public Class VbApplicationDbContext
        Inherits DbContext

        Public Sub New()
        End Sub

        Public Sub New(options As DbContextOptions(Of VbApplicationDbContext))
            MyBase.New(options)
        End Sub

        Public Overridable Property PasswordHistories As DbSet(Of PasswordHistory)

        Public Overridable Property Users As DbSet(Of User)

        Protected Overrides Sub OnConfiguring(optionsBuilder As DbContextOptionsBuilder)
            'TODO /!\ To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
            optionsBuilder.UseSqlServer("Data Source=DESKTOP-R04PVQ3\SQLEXPRESS; Initial Catalog=VbApplicationDb; Integrated Security=true; TrustServerCertificate=True")
        End Sub

        Protected Overrides Sub OnModelCreating(modelBuilder As ModelBuilder)
            modelBuilder.Entity(Of PasswordHistory)(
                Sub(entity)
                    entity.ToTable("PasswordHistory")

                    entity.Property(Function(e) e.ChangeDate).HasColumnType("datetime")
                    entity.Property(Function(e) e.PasswordHash).IsRequired()
                    entity.Property(Function(e) e.PasswordSalt).IsRequired()

                    entity.HasOne(Function(d) d.User).WithMany(Function(p) p.PasswordHistories).
                        HasForeignKey(Function(d) d.UserId).
                        OnDelete(DeleteBehavior.ClientSetNull).
                        HasConstraintName("FK_PasswordHistory_User")
                End Sub)

            modelBuilder.Entity(Of User)(
                Sub(entity)
                    entity.ToTable("User")

                    entity.Property(Function(e) e.Active).HasDefaultValueSql("((1))")
                    entity.Property(Function(e) e.CreatedDate).HasColumnType("datetime")
                    entity.Property(Function(e) e.CustDateOfBirth).HasColumnType("date")
                    entity.Property(Function(e) e.CustGender).HasMaxLength(5)
                    entity.Property(Function(e) e.CustTitle).HasMaxLength(20)
                    entity.Property(Function(e) e.Department).HasMaxLength(50)
                    entity.Property(Function(e) e.Email).
                        HasMaxLength(50).
                        IsUnicode(False)
                    entity.Property(Function(e) e.EmpPosition).HasMaxLength(20)
                    entity.Property(Function(e) e.EmpSalary).HasColumnType("decimal(18, 2)")
                    entity.Property(Function(e) e.InternGrade).HasMaxLength(150)
                    entity.Property(Function(e) e.InternSchool).HasMaxLength(100)
                    entity.Property(Function(e) e.LegalPersonFax).HasMaxLength(11)
                    entity.Property(Function(e) e.LegalPersonPhone).HasMaxLength(14)
                    entity.Property(Function(e) e.LegalPersonTaxNumber).HasMaxLength(11)
                    entity.Property(Function(e) e.LegalpersonCompanyName).HasMaxLength(50)
                    entity.Property(Function(e) e.Password).IsUnicode(False)
                    entity.Property(Function(e) e.SupContactName).HasMaxLength(50)
                    entity.Property(Function(e) e.SupContactSurname).HasMaxLength(50)
                    entity.Property(Function(e) e.SupContactTitle).HasMaxLength(50)
                    entity.Property(Function(e) e.UpdatedDate).HasColumnType("datetime")
                    entity.Property(Function(e) e.UserName).
                        HasMaxLength(50).
                        IsUnicode(False)
                End Sub)

            OnModelCreatingPartial(modelBuilder)
        End Sub

        Partial Private Sub OnModelCreatingPartial(modelBuilder As ModelBuilder)
        End Sub
    End Class
End Namespace
