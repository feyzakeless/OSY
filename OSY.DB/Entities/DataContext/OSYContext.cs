using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using OSY.DB.Entities;

#nullable disable

namespace OSY.DB.Entities.DataContext
{
    public partial class OSYContext : DbContext
    {
        public OSYContext()
        {
        }

        public OSYContext(DbContextOptions<OSYContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Apartment> Apartment { get; set; }
        public virtual DbSet<Bill> Bill { get; set; }
        public virtual DbSet<Housing> Housing { get; set; }
        public virtual DbSet<Resident> Resident { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=OnlineSiteManagement;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Apartment>(entity =>
            {
                entity.Property(e => e.ApartmentType)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.HasOne(d => d.Blok)
                    .WithMany(p => p.Apartment)
                    .HasForeignKey(d => d.BlokId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Apartment_Housing");
            });

            modelBuilder.Entity<Bill>(entity =>
            {
                entity.Property(e => e.BillType)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Iapartment).HasColumnName("IApartment");

                entity.Property(e => e.Paid).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.TotalDept).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.UnPaid).HasColumnType("decimal(18, 2)");

                entity.HasOne(d => d.IapartmentNavigation)
                    .WithMany(p => p.Bill)
                    .HasForeignKey(d => d.Iapartment)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Bill_Apartment");
            });

            modelBuilder.Entity<Housing>(entity =>
            {
                entity.Property(e => e.BlokName)
                    .IsRequired()
                    .HasMaxLength(10);
            });

            modelBuilder.Entity<Resident>(entity =>
            {
                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Idate)
                    .HasColumnType("datetime")
                    .HasColumnName("IDate")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.IdentityNo)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.IsAdmin)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.PhoneNumber)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.PlateNo).HasMaxLength(50);

                entity.Property(e => e.Surname)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Udate)
                    .HasColumnType("datetime")
                    .HasColumnName("UDate")
                    .HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.Apart)
                    .WithMany(p => p.Resident)
                    .HasForeignKey(d => d.ApartId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Resident_Apartment");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
