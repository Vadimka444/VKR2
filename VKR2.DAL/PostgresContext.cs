using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using VKR2.DAL;
using VKR2.DAL.Models;
using VKR2.DAL.QueryResults;

namespace VKR2.DAL;

public partial class PostgresContext : DbContext
{
    public PostgresContext()
    {
    }

    public PostgresContext(DbContextOptions<PostgresContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Attendance> Attendances { get; set; }

    public virtual DbSet<Attendancesociety> Attendancesocieties { get; set; }

    public virtual DbSet<Cabinet> Cabinets { get; set; }

    public virtual DbSet<Curriculum> Curricula { get; set; }

    public virtual DbSet<Familyconnection> Familyconnections { get; set; }

    public virtual DbSet<Group> Groups { get; set; }

    public virtual DbSet<Groupdistribution> Groupdistributions { get; set; }

    public virtual DbSet<Nachislsumma> Nachislsummas { get; set; }

    public virtual DbSet<Parent> Parents { get; set; }

    public virtual DbSet<Paysumma> Paysummas { get; set; }

    public virtual DbSet<Post> Posts { get; set; }

    public virtual DbSet<Pupil> Pupils { get; set; }

    public virtual DbSet<Reason> Reasons { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Rolesuser> Rolesusers { get; set; }

    public virtual DbSet<Schedule> Schedules { get; set; }

    public virtual DbSet<Schedulesociety> Schedulesocieties { get; set; }

    public virtual DbSet<Society> Societies { get; set; }

    public virtual DbSet<Societydistribution> Societydistributions { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<Worker> Workers { get; set; }

    public virtual DbSet<Workersingroup> Workersingroups { get; set; }

    public virtual DbSet<Workerspost> Workersposts { get; set; }

    // --------------------- //

    public virtual DbSet<GroupScheduleResult> GroupScheduleResults { get; set; }
    public virtual DbSet<PupilAttendanceResult> PupilAttendanceResults { get; set; }
    public virtual DbSet<PupilParentResult> PupilParentsResults { get; set; }
    public virtual DbSet<PupilsInfoResult> GroupPupilsResults { get; set; }
    public virtual DbSet<PupilsInfoResult> SocietyPupilsResults { get; set; }
    public virtual DbSet<SocietyPaymentResult> SocietyPaymentsResults { get; set; }
    public virtual DbSet<WorkerSocietyProfitResult> WorkerSocietyProfitResults { get; set; }
    public virtual DbSet<PupilsPaymentsReportResult> PupilsPaymentsReportResults { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=postgres;Username=postgres;Password=1234");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Attendance>(entity =>
        {
            entity.HasKey(e => e.Visitingcd).HasName("attendance_pkey");

            entity.ToTable("attendance");

            entity.Property(e => e.Visitingcd).HasColumnName("visitingcd");
            entity.Property(e => e.Availability)
                .HasDefaultValue(true)
                .HasColumnName("availability");
            entity.Property(e => e.Pupilcd).HasColumnName("pupilcd");
            entity.Property(e => e.Reasoncd).HasColumnName("reasoncd");
            entity.Property(e => e.Visitdate).HasColumnName("visitdate");

            entity.HasOne(d => d.Pupil).WithMany(p => p.Attendances)
                .HasForeignKey(d => d.Pupilcd)
                .HasConstraintName("attendance_pupilcd_fkey");

            entity.HasOne(d => d.Reason).WithMany(p => p.Attendances)
                .HasForeignKey(d => d.Reasoncd)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("attendance_reasoncd_fkey");
        });

        modelBuilder.Entity<Attendancesociety>(entity =>
        {
            entity.HasKey(e => e.Visitingcd).HasName("attendancesociety_pkey");

            entity.ToTable("attendancesociety");

            entity.Property(e => e.Visitingcd).HasColumnName("visitingcd");
            entity.Property(e => e.Availability)
                .HasDefaultValue(true)
                .HasColumnName("availability");
            entity.Property(e => e.Pupilcd).HasColumnName("pupilcd");
            entity.Property(e => e.Reasoncd).HasColumnName("reasoncd");
            entity.Property(e => e.Societycd).HasColumnName("societycd");
            entity.Property(e => e.Visitdate).HasColumnName("visitdate");

            entity.HasOne(d => d.Pupil).WithMany(p => p.Attendancesocieties)
                .HasForeignKey(d => d.Pupilcd)
                .HasConstraintName("attendancesociety_pupilcd_fkey");

            entity.HasOne(d => d.Reason).WithMany(p => p.Attendancesocieties)
                .HasForeignKey(d => d.Reasoncd)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("attendancesociety_reasoncd_fkey");

            entity.HasOne(d => d.Society).WithMany(p => p.Attendancesocieties)
                .HasForeignKey(d => d.Societycd)
                .HasConstraintName("attendancesociety_societycd_fkey");
        });

        modelBuilder.Entity<Cabinet>(entity =>
        {
            entity.HasKey(e => e.Cabinetcd).HasName("cabinets_pkey");

            entity.ToTable("cabinets");

            entity.Property(e => e.Cabinetcd).HasColumnName("cabinetcd");
            entity.Property(e => e.Cabinetno).HasColumnName("cabinetno");
            entity.Property(e => e.Location)
                .HasMaxLength(30)
                .HasColumnName("location");
        });

        modelBuilder.Entity<Curriculum>(entity =>
        {
            entity.HasKey(e => e.Lessoncd).HasName("curriculum_pkey");

            entity.ToTable("curriculum");

            entity.Property(e => e.Lessoncd).HasColumnName("lessoncd");
            entity.Property(e => e.Duration)
                .HasMaxLength(20)
                .HasColumnName("duration");
            entity.Property(e => e.Quantity).HasColumnName("quantity");
            entity.Property(e => e.Title)
                .HasMaxLength(30)
                .HasColumnName("title");
        });

        modelBuilder.Entity<Familyconnection>(entity =>
        {
            entity.HasKey(e => e.Familyconnectioncd).HasName("familyconnections_pkey");

            entity.ToTable("familyconnections");

            entity.Property(e => e.Familyconnectioncd).HasColumnName("familyconnectioncd");
            entity.Property(e => e.Kinshipdegree)
                .HasMaxLength(20)
                .HasColumnName("kinshipdegree");
            entity.Property(e => e.Parentcd).HasColumnName("parentcd");
            entity.Property(e => e.Pupilcd).HasColumnName("pupilcd");

            entity.HasOne(d => d.Parent).WithMany(p => p.Familyconnections)
                .HasForeignKey(d => d.Parentcd)
                .HasConstraintName("familyconnections_parentcd_fkey");

            entity.HasOne(d => d.Pupil).WithMany(p => p.Familyconnections)
                .HasForeignKey(d => d.Pupilcd)
                .HasConstraintName("familyconnections_pupilcd_fkey");
        });

        modelBuilder.Entity<Group>(entity =>
        {
            entity.HasKey(e => e.Groupcd).HasName("groups_pkey");

            entity.ToTable("groups");

            entity.Property(e => e.Groupcd).HasColumnName("groupcd");
            entity.Property(e => e.Maxage).HasColumnName("maxage");
            entity.Property(e => e.Minage).HasColumnName("minage");
            entity.Property(e => e.Numberofseats).HasColumnName("numberofseats");
            entity.Property(e => e.Title)
                .HasMaxLength(30)
                .HasColumnName("title");
        });

        modelBuilder.Entity<Groupdistribution>(entity =>
        {
            entity.HasKey(e => e.Distributioncd).HasName("groupdistributions_pkey");

            entity.ToTable("groupdistributions");

            entity.Property(e => e.Distributioncd).HasColumnName("distributioncd");
            entity.Property(e => e.Groupcd).HasColumnName("groupcd");
            entity.Property(e => e.Pupilcd).HasColumnName("pupilcd");

            entity.HasOne(d => d.Group).WithMany(p => p.Groupdistributions)
                .HasForeignKey(d => d.Groupcd)
                .HasConstraintName("groupdistributions_groupcd_fkey");

            entity.HasOne(d => d.Pupil).WithMany(p => p.Groupdistributions)
                .HasForeignKey(d => d.Pupilcd)
                .HasConstraintName("groupdistributions_pupilcd_fkey");
        });

        modelBuilder.Entity<Nachislsumma>(entity =>
        {
            entity.HasKey(e => e.Nachislfactcd).HasName("nachislsumma_pkey");

            entity.ToTable("nachislsumma");

            entity.Property(e => e.Nachislfactcd).HasColumnName("nachislfactcd");
            entity.Property(e => e.Nachislmonth).HasColumnName("nachislmonth");
            entity.Property(e => e.Nachislsum)
                .HasPrecision(10, 2)
                .HasColumnName("nachislsum");
            entity.Property(e => e.Nachislyear).HasColumnName("nachislyear");
            entity.Property(e => e.Pupilcd).HasColumnName("pupilcd");
            entity.Property(e => e.Societycd).HasColumnName("societycd");

            entity.HasOne(d => d.Pupil).WithMany(p => p.Nachislsummas)
                .HasForeignKey(d => d.Pupilcd)
                .HasConstraintName("nachislsumma_pupilcd_fkey");

            entity.HasOne(d => d.Society).WithMany(p => p.Nachislsummas)
                .HasForeignKey(d => d.Societycd)
                .HasConstraintName("nachislsumma_societycd_fkey");
        });

        modelBuilder.Entity<Parent>(entity =>
        {
            entity.HasKey(e => e.Parentcd).HasName("parents_pkey");

            entity.ToTable("parents");

            entity.Property(e => e.Parentcd).HasColumnName("parentcd");
            entity.Property(e => e.Address)
                .HasMaxLength(100)
                .HasColumnName("address");
            entity.Property(e => e.Fio)
                .HasMaxLength(70)
                .HasColumnName("fio");
            entity.Property(e => e.Passport)
                .HasMaxLength(10)
                .HasColumnName("passport");
            entity.Property(e => e.Phone)
                .HasMaxLength(16)
                .HasColumnName("phone");
        });

        modelBuilder.Entity<Paysumma>(entity =>
        {
            entity.HasKey(e => e.Payfactcd).HasName("paysumma_pkey");

            entity.ToTable("paysumma");

            entity.Property(e => e.Payfactcd).HasColumnName("payfactcd");
            entity.Property(e => e.Paydate).HasColumnName("paydate");
            entity.Property(e => e.Paymonth).HasColumnName("paymonth");
            entity.Property(e => e.Paysum)
                .HasPrecision(10, 2)
                .HasColumnName("paysum");
            entity.Property(e => e.Payyear).HasColumnName("payyear");
            entity.Property(e => e.Pupilcd).HasColumnName("pupilcd");
            entity.Property(e => e.Societycd).HasColumnName("societycd");

            entity.HasOne(d => d.Pupil).WithMany(p => p.Paysummas)
                .HasForeignKey(d => d.Pupilcd)
                .HasConstraintName("paysumma_pupilcd_fkey");

            entity.HasOne(d => d.Society).WithMany(p => p.Paysummas)
                .HasForeignKey(d => d.Societycd)
                .HasConstraintName("paysumma_societycd_fkey");
        });

        modelBuilder.Entity<Post>(entity =>
        {
            entity.HasKey(e => e.Postcd).HasName("post_pkey");

            entity.ToTable("post");

            entity.Property(e => e.Postcd).HasColumnName("postcd");
            entity.Property(e => e.Title)
                .HasMaxLength(30)
                .HasColumnName("title");
        });

        modelBuilder.Entity<Pupil>(entity =>
        {
            entity.HasKey(e => e.Pupilcd).HasName("pupils_pkey");

            entity.ToTable("pupils");

            entity.Property(e => e.Pupilcd).HasColumnName("pupilcd");
            entity.Property(e => e.Birthcertificatenumber)
                .HasMaxLength(12)
                .HasColumnName("birthcertificatenumber");
            entity.Property(e => e.Dateofbirth).HasColumnName("dateofbirth");
            entity.Property(e => e.Fio)
                .HasMaxLength(70)
                .HasColumnName("fio");
            entity.Property(e => e.Gender)
                .HasMaxLength(7)
                .IsFixedLength()
                .HasColumnName("gender");
        });

        modelBuilder.Entity<Reason>(entity =>
        {
            entity.HasKey(e => e.Reasoncd).HasName("reasons_pkey");

            entity.ToTable("reasons");

            entity.Property(e => e.Reasoncd).HasColumnName("reasoncd");
            entity.Property(e => e.Title)
                .HasMaxLength(50)
                .HasColumnName("title");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Rolecd).HasName("roles_pkey");

            entity.ToTable("roles");

            entity.HasIndex(e => e.Rolename, "roles_rolename_key").IsUnique();

            entity.Property(e => e.Rolecd).HasColumnName("rolecd");
            entity.Property(e => e.Rolename)
                .HasMaxLength(20)
                .HasColumnName("rolename");
        });

        modelBuilder.Entity<Rolesuser>(entity =>
        {
            entity.HasKey(e => e.Rolesuserscd).HasName("rolesusers_pkey");

            entity.ToTable("rolesusers");

            entity.Property(e => e.Rolesuserscd).HasColumnName("rolesuserscd");
            entity.Property(e => e.Rolecd).HasColumnName("rolecd");
            entity.Property(e => e.Usercd).HasColumnName("usercd");

            entity.HasOne(d => d.Role).WithMany(p => p.Rolesusers)
                .HasForeignKey(d => d.Rolecd)
                .HasConstraintName("rolesusers_rolecd_fkey");

            entity.HasOne(d => d.User).WithMany(p => p.Rolesusers)
                .HasForeignKey(d => d.Usercd)
                .HasConstraintName("rolesusers_usercd_fkey");
        });

        modelBuilder.Entity<Schedule>(entity =>
        {
            entity.HasKey(e => e.Schedulecd).HasName("schedule_pkey");

            entity.ToTable("schedule");

            entity.Property(e => e.Schedulecd).HasColumnName("schedulecd");
            entity.Property(e => e.Cabinetcd).HasColumnName("cabinetcd");
            entity.Property(e => e.EndTime).HasColumnName("end_time");
            entity.Property(e => e.Groupcd).HasColumnName("groupcd");
            entity.Property(e => e.Lessoncd).HasColumnName("lessoncd");
            entity.Property(e => e.Scheduledate).HasColumnName("scheduledate");
            entity.Property(e => e.StartTime).HasColumnName("start_time");
            entity.Property(e => e.Workercd).HasColumnName("workercd");

            entity.HasOne(d => d.Cabinet).WithMany(p => p.Schedules)
                .HasForeignKey(d => d.Cabinetcd)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("schedule_cabinetcd_fkey");

            entity.HasOne(d => d.Group).WithMany(p => p.Schedules)
                .HasForeignKey(d => d.Groupcd)
                .HasConstraintName("schedule_groupcd_fkey");

            entity.HasOne(d => d.Lesson).WithMany(p => p.Schedules)
                .HasForeignKey(d => d.Lessoncd)
                .HasConstraintName("schedule_lessoncd_fkey");

            entity.HasOne(d => d.Worker).WithMany(p => p.Schedules)
                .HasForeignKey(d => d.Workercd)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("schedule_workercd_fkey");
        });

        modelBuilder.Entity<Schedulesociety>(entity =>
        {
            entity.HasKey(e => e.Schedulecd).HasName("schedulesociety_pkey");

            entity.ToTable("schedulesociety");

            entity.Property(e => e.Schedulecd).HasColumnName("schedulecd");
            entity.Property(e => e.Cabinetcd).HasColumnName("cabinetcd");
            entity.Property(e => e.EndTime).HasColumnName("end_time");
            entity.Property(e => e.Scheduledate).HasColumnName("scheduledate");
            entity.Property(e => e.Societycd).HasColumnName("societycd");
            entity.Property(e => e.StartTime).HasColumnName("start_time");
            entity.Property(e => e.Workercd).HasColumnName("workercd");

            entity.HasOne(d => d.Cabinet).WithMany(p => p.Schedulesocieties)
                .HasForeignKey(d => d.Cabinetcd)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("schedulesociety_cabinetcd_fkey");

            entity.HasOne(d => d.Society).WithMany(p => p.Schedulesocieties)
                .HasForeignKey(d => d.Societycd)
                .HasConstraintName("schedulesociety_societycd_fkey");

            entity.HasOne(d => d.Worker).WithMany(p => p.Schedulesocieties)
                .HasForeignKey(d => d.Workercd)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("schedulesociety_workercd_fkey");
        });

        modelBuilder.Entity<Society>(entity =>
        {
            entity.HasKey(e => e.Societycd).HasName("society_pkey");

            entity.ToTable("society");

            entity.Property(e => e.Societycd).HasColumnName("societycd");
            entity.Property(e => e.Maxage).HasColumnName("maxage");
            entity.Property(e => e.Minage).HasColumnName("minage");
            entity.Property(e => e.Numberofseats).HasColumnName("numberofseats");
            entity.Property(e => e.Price)
                .HasPrecision(10, 2)
                .HasColumnName("price");
            entity.Property(e => e.Title)
                .HasMaxLength(30)
                .HasColumnName("title");
        });

        modelBuilder.Entity<Societydistribution>(entity =>
        {
            entity.HasKey(e => e.Distributioncd).HasName("societydistributions_pkey");

            entity.ToTable("societydistributions");

            entity.Property(e => e.Distributioncd).HasColumnName("distributioncd");
            entity.Property(e => e.Pupilcd).HasColumnName("pupilcd");
            entity.Property(e => e.Societycd).HasColumnName("societycd");

            entity.HasOne(d => d.Pupil).WithMany(p => p.Societydistributions)
                .HasForeignKey(d => d.Pupilcd)
                .HasConstraintName("societydistributions_pupilcd_fkey");

            entity.HasOne(d => d.Society).WithMany(p => p.Societydistributions)
                .HasForeignKey(d => d.Societycd)
                .HasConstraintName("societydistributions_societycd_fkey");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Usercd).HasName("users_pkey");

            entity.ToTable("users");

            entity.Property(e => e.Usercd).HasColumnName("usercd");
            entity.Property(e => e.Email)
                .HasMaxLength(70)
                .HasColumnName("email");
            entity.Property(e => e.Parentcd).HasColumnName("parentcd");
            entity.Property(e => e.Passwordhash)
                .HasMaxLength(256)
                .HasColumnName("passwordhash");
            entity.Property(e => e.Workercd).HasColumnName("workercd");
        });

        modelBuilder.Entity<Worker>(entity =>
        {
            entity.HasKey(e => e.Workercd).HasName("workers_pkey");

            entity.ToTable("workers");

            entity.Property(e => e.Workercd).HasColumnName("workercd");
            entity.Property(e => e.Address)
                .HasMaxLength(100)
                .HasColumnName("address");
            entity.Property(e => e.Dateofbirth).HasColumnName("dateofbirth");
            entity.Property(e => e.Fio)
                .HasMaxLength(50)
                .HasColumnName("fio");
            entity.Property(e => e.Passport)
                .HasMaxLength(10)
                .HasColumnName("passport");
            entity.Property(e => e.Phone)
                .HasMaxLength(16)
                .HasColumnName("phone");
        });

        modelBuilder.Entity<Workersingroup>(entity =>
        {
            entity.HasKey(e => e.Workeringroupcd).HasName("workersingroups_pkey");

            entity.ToTable("workersingroups");

            entity.Property(e => e.Workeringroupcd).HasColumnName("workeringroupcd");
            entity.Property(e => e.Groupcd).HasColumnName("groupcd");
            entity.Property(e => e.Workercd).HasColumnName("workercd");

            entity.HasOne(d => d.Group).WithMany(p => p.Workersingroups)
                .HasForeignKey(d => d.Groupcd)
                .HasConstraintName("workersingroups_groupcd_fkey");

            entity.HasOne(d => d.Worker).WithMany(p => p.Workersingroups)
                .HasForeignKey(d => d.Workercd)
                .HasConstraintName("workersingroups_workercd_fkey");
        });

        modelBuilder.Entity<Workerspost>(entity =>
        {
            entity.HasKey(e => e.Workerpostcd).HasName("workersposts_pkey");

            entity.ToTable("workersposts");

            entity.HasIndex(e => new { e.Workercd, e.Postcd }, "workersposts_workercd_postcd_key").IsUnique();

            entity.Property(e => e.Workerpostcd).HasColumnName("workerpostcd");
            entity.Property(e => e.Postcd).HasColumnName("postcd");
            entity.Property(e => e.Workercd).HasColumnName("workercd");

            entity.HasOne(d => d.Post).WithMany(p => p.Workersposts)
                .HasForeignKey(d => d.Postcd)
                .HasConstraintName("workersposts_postcd_fkey");

            entity.HasOne(d => d.Worker).WithMany(p => p.Workersposts)
                .HasForeignKey(d => d.Workercd)
                .HasConstraintName("workersposts_workercd_fkey");
        });

        OnModelCreatingPartial(modelBuilder);


        // Конфигурация для результатов функций
        modelBuilder.Entity<GroupScheduleResult>().HasNoKey();
        modelBuilder.Entity<PupilAttendanceResult>().HasNoKey();
        modelBuilder.Entity<PupilParentResult>().HasNoKey();
        modelBuilder.Entity<PupilsInfoResult>().HasNoKey();
        modelBuilder.Entity<SocietyPaymentResult>().HasNoKey();
        modelBuilder.Entity<PupilsPaymentsReportResult>().HasNoKey();
        modelBuilder.Entity<WorkerSocietyProfitResult>().HasNoKey();
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
