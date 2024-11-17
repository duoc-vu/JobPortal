using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace JobPortal.Models;

public partial class JobPortalDbContext : DbContext
{
    public JobPortalDbContext()
    {
    }

    public JobPortalDbContext(DbContextOptions<JobPortalDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<TblAccount> TblAccounts { get; set; }

    public virtual DbSet<TblApplicant> TblApplicants { get; set; }

    public virtual DbSet<TblCandidate> TblCandidates { get; set; }

    public virtual DbSet<TblEmployer> TblEmployers { get; set; }

    public virtual DbSet<TblJob> TblJobs { get; set; }

    public virtual DbSet<TblRole> TblRoles { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=LAPTOP-VRFJ0V2J\\DUOCVU;Initial Catalog=JobPortalDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TblAccount>(entity =>
        {
            entity.HasKey(e => e.IUserId).HasName("PK__TblAccou__BA95FFD10E123A7F");

            entity.ToTable("TblAccount");

            entity.Property(e => e.IUserId).HasColumnName("iUserID");
            entity.Property(e => e.IRoleId).HasColumnName("iRoleID");
            entity.Property(e => e.SPassword)
                .HasMaxLength(255)
                .HasColumnName("sPassword");
            entity.Property(e => e.SUsername)
                .HasMaxLength(100)
                .HasColumnName("sUsername");

            entity.HasOne(d => d.IRole).WithMany(p => p.TblAccounts)
                .HasForeignKey(d => d.IRoleId)
                .HasConstraintName("FK__TblAccoun__iRole__4F7CD00D");
        });

        modelBuilder.Entity<TblApplicant>(entity =>
        {
            entity.HasKey(e => new { e.IUserId, e.IJobId }).HasName("PK__TblAppli__319ADB92B21AC341");

            entity.ToTable("TblApplicant");

            entity.Property(e => e.IUserId).HasColumnName("iUserID");
            entity.Property(e => e.IJobId).HasColumnName("iJobID");
            entity.Property(e => e.SCv)
                .HasMaxLength(255)
                .HasColumnName("sCV");
            entity.Property(e => e.SIntroduction).HasColumnName("sIntroduction");
            entity.Property(e => e.SStatus)
                .HasMaxLength(50)
                .HasColumnName("sStatus");

            entity.HasOne(d => d.IJob).WithMany(p => p.TblApplicants)
                .HasForeignKey(d => d.IJobId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__TblApplic__iJobI__5BE2A6F2");

            entity.HasOne(d => d.IUser).WithMany(p => p.TblApplicants)
                .HasForeignKey(d => d.IUserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__TblApplic__iUser__5AEE82B9");
        });

        modelBuilder.Entity<TblCandidate>(entity =>
        {
            entity.HasKey(e => e.IUserId).HasName("PK__TblCandi__BA95FFD1E690B58D");

            entity.ToTable("TblCandidate");

            entity.Property(e => e.IUserId)
                .ValueGeneratedNever()
                .HasColumnName("iUserID");
            entity.Property(e => e.FExperience).HasColumnName("fExperience");
            entity.Property(e => e.SAddress)
                .HasMaxLength(255)
                .HasColumnName("sAddress");
            entity.Property(e => e.SAvt)
                .HasMaxLength(255)
                .HasColumnName("sAvt");
            entity.Property(e => e.SDetail).HasColumnName("sDetail");
            entity.Property(e => e.SEmail)
                .HasMaxLength(255)
                .HasColumnName("sEmail");
            entity.Property(e => e.SIndustry)
                .HasMaxLength(255)
                .HasColumnName("sIndustry");
            entity.Property(e => e.SName)
                .HasMaxLength(255)
                .HasColumnName("sName");
            entity.Property(e => e.SPhone)
                .HasMaxLength(20)
                .HasColumnName("sPhone");

            entity.HasOne(d => d.IUser).WithOne(p => p.TblCandidate)
                .HasForeignKey<TblCandidate>(d => d.IUserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__TblCandid__iUser__5535A963");
        });

        modelBuilder.Entity<TblEmployer>(entity =>
        {
            entity.HasKey(e => e.IUserId).HasName("PK__TblEmplo__BA95FFD15028F36C");

            entity.ToTable("TblEmployer");

            entity.Property(e => e.IUserId)
                .ValueGeneratedNever()
                .HasColumnName("iUserID");
            entity.Property(e => e.FCompanySize).HasColumnName("fCompanySize");
            entity.Property(e => e.SAddress)
                .HasMaxLength(255)
                .HasColumnName("sAddress");
            entity.Property(e => e.SAvt)
                .HasMaxLength(255)
                .HasColumnName("sAvt");
            entity.Property(e => e.SCompanyField)
                .HasMaxLength(255)
                .HasColumnName("sCompanyField");
            entity.Property(e => e.SDetail).HasColumnName("sDetail");
            entity.Property(e => e.SEmail)
                .HasMaxLength(255)
                .HasColumnName("sEmail");
            entity.Property(e => e.SName)
                .HasMaxLength(255)
                .HasColumnName("sName");
            entity.Property(e => e.SPhone)
                .HasMaxLength(20)
                .HasColumnName("sPhone");

            entity.HasOne(d => d.IUser).WithOne(p => p.TblEmployer)
                .HasForeignKey<TblEmployer>(d => d.IUserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__TblEmploy__iUser__52593CB8");
        });

        modelBuilder.Entity<TblJob>(entity =>
        {
            entity.HasKey(e => e.IJobId).HasName("PK__TblJob__B0F24439F87B2DAB");

            entity.ToTable("TblJob");

            entity.Property(e => e.IJobId).HasColumnName("iJobID");
            entity.Property(e => e.DTime)
                .HasColumnType("date")
                .HasColumnName("dTime");
            entity.Property(e => e.FSalary).HasColumnName("fSalary");
            entity.Property(e => e.IEmployerId).HasColumnName("iEmployerID");
            entity.Property(e => e.SAddress)
                .HasMaxLength(255)
                .HasColumnName("sAddress");
            entity.Property(e => e.SAvt)
                .HasMaxLength(255)
                .HasColumnName("sAvt");
            entity.Property(e => e.SDetail).HasColumnName("sDetail");
            entity.Property(e => e.SJobName)
                .HasMaxLength(255)
                .HasColumnName("sJobName");
            entity.Property(e => e.SStatus)
                .HasMaxLength(50)
                .HasColumnName("sStatus");

            entity.HasOne(d => d.IEmployer).WithMany(p => p.TblJobs)
                .HasForeignKey(d => d.IEmployerId)
                .HasConstraintName("FK__TblJob__iEmploye__5812160E");
        });

        modelBuilder.Entity<TblRole>(entity =>
        {
            entity.HasKey(e => e.IRoleId).HasName("PK__TblRole__D69F8CBE3AA0A6EB");

            entity.ToTable("TblRole");

            entity.Property(e => e.IRoleId).HasColumnName("iRoleID");
            entity.Property(e => e.SRoleName)
                .HasMaxLength(100)
                .HasColumnName("sRoleName");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
