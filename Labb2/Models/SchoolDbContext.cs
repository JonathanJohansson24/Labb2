using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Labb2.Models;

public partial class SchoolDbContext : DbContext
{
    public SchoolDbContext()
    {
    }

    public SchoolDbContext(DbContextOptions<SchoolDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Befattningar> Befattningars { get; set; }

    public virtual DbSet<Betyg> Betygs { get; set; }

    public virtual DbSet<Betygskala> Betygskalas { get; set; }

    public virtual DbSet<Kurser> Kursers { get; set; }

    public virtual DbSet<Personal> Personals { get; set; }

    public virtual DbSet<Studenter> Studenters { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data source = DESKTOP-53F8V9T;Initial Catalog = SchoolDB;Integrated Security=True;TrustServerCertificate=Yes;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Befattningar>(entity =>
        {
            entity.HasKey(e => e.BefattningsId).HasName("PK__Befattni__75F2456BD0770BE5");

            entity.ToTable("Befattningar");

            entity.Property(e => e.BefattningsId).HasColumnName("befattningsID");
            entity.Property(e => e.Befattningstyp)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("befattningstyp");
        });

        modelBuilder.Entity<Betyg>(entity =>
        {
            entity.HasKey(e => e.BetygId).HasName("PK__Betyg__36622077A000D35C");

            entity.ToTable("Betyg");

            entity.Property(e => e.BetygId).HasColumnName("betygID");
            entity.Property(e => e.BetygskalaId).HasColumnName("betygskalaID");
            entity.Property(e => e.Datumförbetyg).HasColumnName("datumförbetyg");
            entity.Property(e => e.ElevId).HasColumnName("elevID");
            entity.Property(e => e.KursId).HasColumnName("kursID");
            entity.Property(e => e.PersonalId).HasColumnName("personalID");

            entity.HasOne(d => d.Betygskala).WithMany(p => p.Betygs)
                .HasForeignKey(d => d.BetygskalaId)
                .HasConstraintName("FK_Betyg_Betygskala");

            entity.HasOne(d => d.Elev).WithMany(p => p.Betygs)
                .HasForeignKey(d => d.ElevId)
                .HasConstraintName("FK_Betyg_Studenter");

            entity.HasOne(d => d.Kurs).WithMany(p => p.Betygs)
                .HasForeignKey(d => d.KursId)
                .HasConstraintName("FK_Betyg_Kurser");

            entity.HasOne(d => d.Personal).WithMany(p => p.Betygs)
                .HasForeignKey(d => d.PersonalId)
                .HasConstraintName("FK_Betyg_Personal");
        });

        modelBuilder.Entity<Betygskala>(entity =>
        {
            entity.HasKey(e => e.BetygskalaId).HasName("PK__Betygska__41E203A8C751C2D1");

            entity.ToTable("Betygskala");

            entity.Property(e => e.BetygskalaId).HasColumnName("betygskalaID");
            entity.Property(e => e.Betydelse)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("betydelse");
            entity.Property(e => e.Betygpoäng).HasColumnName("betygpoäng");
        });

        modelBuilder.Entity<Kurser>(entity =>
        {
            entity.HasKey(e => e.KursId).HasName("PK__Kurser__BCCFFF3BF43CD227");

            entity.ToTable("Kurser");

            entity.Property(e => e.KursId).HasColumnName("KursID");
            entity.Property(e => e.Kursnamn)
                .HasMaxLength(25)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Personal>(entity =>
        {
            entity.HasKey(e => e.PersonalId).HasName("PK__Personal__1BEA6B79ED5C36DB");

            entity.ToTable("Personal");

            entity.Property(e => e.PersonalId).HasColumnName("personalID");
            entity.Property(e => e.BefattningsId).HasColumnName("befattningsID");
            entity.Property(e => e.Personalefternamn)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("personalefternamn");
            entity.Property(e => e.Personalförnamn)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("personalförnamn");

            entity.HasOne(d => d.Befattnings).WithMany(p => p.Personals)
                .HasForeignKey(d => d.BefattningsId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Personal_Befattningar");
        });

        modelBuilder.Entity<Studenter>(entity =>
        {
            entity.HasKey(e => e.ElevId).HasName("PK__Studente__B37E8FC980830FD2");

            entity.ToTable("Studenter");

            entity.Property(e => e.ElevId).HasColumnName("elevID");
            entity.Property(e => e.Elevefternamn)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("elevefternamn");
            entity.Property(e => e.Elevförnamn)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("elevförnamn");
            entity.Property(e => e.Klassnamn)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("klassnamn");
            entity.Property(e => e.Personnummer)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("personnummer");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
