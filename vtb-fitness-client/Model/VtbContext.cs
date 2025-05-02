using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace vtb_fitness_client.Model;

public partial class VtbContext : DbContext
{
    public VtbContext()
    {
    }

    public VtbContext(DbContextOptions<VtbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<BankingDetail> BankingDetails { get; set; }

    public virtual DbSet<Exercise> Exercises { get; set; }

    public virtual DbSet<ExerciseType> ExerciseTypes { get; set; }

    public virtual DbSet<Passport> Passports { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Tariff> Tariffs { get; set; }

    public virtual DbSet<Tracker> Trackers { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserTariff> UserTariffs { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseNpgsql("Host=localhost;Database=vtb;Username=postgres;Password=1234");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<BankingDetail>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("banking_details_pkey");

            entity.ToTable("banking_details");

            entity.HasIndex(e => e.Bik, "banking_details_bik_key").IsUnique();

            entity.HasIndex(e => e.CorrespondentAccount, "banking_details_correspondent_account_key").IsUnique();

            entity.HasIndex(e => e.CurrentAccount, "banking_details_current_account_key").IsUnique();

            entity.HasIndex(e => e.Inn, "banking_details_inn_key").IsUnique();

            entity.HasIndex(e => e.Kpp, "banking_details_kpp_key").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.BankName)
                .HasMaxLength(30)
                .HasColumnName("bank_name");
            entity.Property(e => e.Bik)
                .HasMaxLength(9)
                .HasColumnName("bik");
            entity.Property(e => e.CorrespondentAccount)
                .HasMaxLength(20)
                .HasColumnName("correspondent_account");
            entity.Property(e => e.CurrentAccount)
                .HasMaxLength(20)
                .HasColumnName("current_account");
            entity.Property(e => e.DebitCardNumber)
                .HasMaxLength(16)
                .HasColumnName("debit_card_number");
            entity.Property(e => e.Inn)
                .HasMaxLength(12)
                .HasColumnName("inn");
            entity.Property(e => e.Kpp)
                .HasMaxLength(9)
                .HasColumnName("kpp");
        });

        modelBuilder.Entity<Exercise>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("exercise_pkey");

            entity.ToTable("exercise");

            entity.HasIndex(e => e.Name, "uq_exercise_name").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ImgName).HasColumnName("img_name");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.TypeId).HasColumnName("type_id");

            entity.HasOne(d => d.Type).WithMany(p => p.Exercises)
                .HasForeignKey(d => d.TypeId)
                .HasConstraintName("fk_exercise_type_id");
        });

        modelBuilder.Entity<ExerciseType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("exercise_type_pkey");

            entity.ToTable("exercise_type");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name).HasColumnName("name");
        });

        modelBuilder.Entity<Passport>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("passport_pkey");

            entity.ToTable("passport");

            entity.HasIndex(e => e.Number, "passport_number_key").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.BirthDate).HasColumnName("birth_date");
            entity.Property(e => e.BirthPlace)
                .HasMaxLength(200)
                .HasColumnName("birth_place");
            entity.Property(e => e.IssuedBy)
                .HasMaxLength(100)
                .HasColumnName("issued_by");
            entity.Property(e => e.IssuedDate).HasColumnName("issued_date");
            entity.Property(e => e.Number)
                .HasMaxLength(6)
                .HasColumnName("number");
            entity.Property(e => e.Series)
                .HasMaxLength(4)
                .HasColumnName("series");
            entity.Property(e => e.UnitCode)
                .HasMaxLength(7)
                .HasColumnName("unit_code");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("uq_role_id");

            entity.ToTable("role");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name).HasColumnName("name");
        });

        modelBuilder.Entity<Tariff>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("tariff_pkey");

            entity.ToTable("tariff");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.GymEndTime).HasColumnName("gym_end_time");
            entity.Property(e => e.GymStartTime).HasColumnName("gym_start_time");
            entity.Property(e => e.HammamEndTime).HasColumnName("hammam_end_time");
            entity.Property(e => e.HammamStartTime).HasColumnName("hammam_start_time");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.Period).HasColumnName("period");
            entity.Property(e => e.PoolEndTime).HasColumnName("pool_end_time");
            entity.Property(e => e.PoolStartTime).HasColumnName("pool_start_time");
            entity.Property(e => e.Price).HasColumnName("price");
            entity.Property(e => e.Pros)
                .HasColumnType("character varying(250)[]")
                .HasColumnName("pros");
            entity.Property(e => e.TrainerWorkoutsPerWeek).HasColumnName("trainer_workouts_per_week");
        });

        modelBuilder.Entity<Tracker>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("tracker_pkey");

            entity.ToTable("tracker");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ExerciseId).HasColumnName("exercise_id");
            entity.Property(e => e.Meters).HasColumnName("meters");
            entity.Property(e => e.Reps).HasColumnName("reps");
            entity.Property(e => e.Sits).HasColumnName("sits");
            entity.Property(e => e.Timestamp)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("timestamp");
            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.Weight).HasColumnName("weight");

            entity.HasOne(d => d.Exercise).WithMany(p => p.Trackers)
                .HasForeignKey(d => d.ExerciseId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_tracker_exercise_id");

            entity.HasOne(d => d.User).WithMany(p => p.Trackers)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_tracker_user_id");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("user_pkey");

            entity.ToTable("user");

            entity.HasIndex(e => e.Email, "user_email_key").IsUnique();

            entity.HasIndex(e => e.Login, "user_login_key").IsUnique();

            entity.HasIndex(e => e.Phone, "user_phone_key").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.BankingDetailsId).HasColumnName("banking_details_id");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .HasColumnName("email");
            entity.Property(e => e.Lastname)
                .HasMaxLength(50)
                .HasColumnName("lastname");
            entity.Property(e => e.Login)
                .HasMaxLength(25)
                .HasColumnName("login");
            entity.Property(e => e.Middlename)
                .HasMaxLength(50)
                .HasColumnName("middlename");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
            entity.Property(e => e.PassportId).HasColumnName("passport_id");
            entity.Property(e => e.PasswordHash)
                .HasMaxLength(250)
                .HasColumnName("password_hash");
            entity.Property(e => e.Pfp).HasColumnName("pfp");
            entity.Property(e => e.Phone)
                .HasMaxLength(10)
                .HasColumnName("phone");
            entity.Property(e => e.RoleId).HasColumnName("role_id");
            entity.Property(e => e.WorkingInVtbSince).HasColumnName("working_in_vtb_since");

            entity.HasOne(d => d.BankingDetails).WithMany(p => p.Users)
                .HasForeignKey(d => d.BankingDetailsId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("fk_user_banking_details_id");

            entity.HasOne(d => d.Passport).WithMany(p => p.Users)
                .HasForeignKey(d => d.PassportId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("fk_user_passport_id");

            entity.HasOne(d => d.Role).WithMany(p => p.Users)
                .HasForeignKey(d => d.RoleId)
                .HasConstraintName("fk_user_role_id");
        });

        modelBuilder.Entity<UserTariff>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("user_tariff_pkey");

            entity.ToTable("user_tariff");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AcquiredAt)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("acquired_at");
            entity.Property(e => e.ExpiresAt)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("expires_at");
            entity.Property(e => e.MoneyPaid).HasColumnName("money_paid");
            entity.Property(e => e.TariffId).HasColumnName("tariff_id");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Tariff).WithMany(p => p.UserTariffs)
                .HasForeignKey(d => d.TariffId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("fk_ut_tariff_id");

            entity.HasOne(d => d.User).WithMany(p => p.UserTariffs)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("fk_ut_user_id");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
