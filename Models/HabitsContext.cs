using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Habits.Models;

public partial class HabitsContext : DbContext
{
    public HabitsContext()
    {
    }

    public HabitsContext(DbContextOptions<HabitsContext> options)
        : base(options)
    {
    }

    public virtual DbSet<DailyTask> DailyTasks { get; set; }

    public virtual DbSet<Group> Groups { get; set; }

    public virtual DbSet<Schedule> Schedules { get; set; }

    public virtual DbSet<SchedulesTask> SchedulesTasks { get; set; }

    public virtual DbSet<Task> Tasks { get; set; }

    public virtual DbSet<User> Users { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<DailyTask>(entity =>
        {
            entity.HasKey(e => e.IdDailyTask).HasName("PK__DailyTas__448329FEDBD0CF95");

            entity.Property(e => e.IdDailyTask).HasColumnName("id_daily_task");
            entity.Property(e => e.CompletedAt)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("completed_at");
            entity.Property(e => e.Date)
                .HasDefaultValueSql("(sysdatetime())")
                .HasColumnName("date");
            entity.Property(e => e.IdTask).HasColumnName("id_task");
            entity.Property(e => e.MinutesCompleted).HasColumnName("minutes_completed");
            entity.Property(e => e.TotalMinutes).HasColumnName("total_minutes");

            entity.HasOne(d => d.IdTaskNavigation).WithMany(p => p.DailyTasks)
                .HasForeignKey(d => d.IdTask)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DailyTasks_Tasks");
        });

        modelBuilder.Entity<Group>(entity =>
        {
            entity.HasKey(e => e.IdGroup).HasName("PK__Groups__8BE8BA1BED37C6F0");

            entity.Property(e => e.IdGroup).HasColumnName("id_group");
            entity.Property(e => e.Color)
                .HasDefaultValueSql("('#66ff33')")
                .HasColumnName("color");
            entity.Property(e => e.IdUser).HasColumnName("id_user");
            entity.Property(e => e.Name)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("name");

            entity.HasOne(d => d.IdUserNavigation).WithMany(p => p.Groups)
                .HasForeignKey(d => d.IdUser)
                .HasConstraintName("FK_Groups_Users");
        });

        modelBuilder.Entity<Schedule>(entity =>
        {
            entity.HasKey(e => e.IdSchedule).HasName("PK__Schedule__15FE7E33C6663388");

            entity.Property(e => e.IdSchedule).HasColumnName("id_schedule");
            entity.Property(e => e.Days)
                .HasMaxLength(1)
                .HasDefaultValueSql("((0))")
                .IsFixedLength()
                .HasColumnName("days");
            entity.Property(e => e.IdUser).HasColumnName("id_user");
            entity.Property(e => e.Name)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("name");

            entity.HasOne(d => d.IdUserNavigation).WithMany(p => p.Schedules)
                .HasForeignKey(d => d.IdUser)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Schedules_Users");
        });

        modelBuilder.Entity<SchedulesTask>(entity =>
        {
            entity.HasKey(e => new { e.IdSchedule, e.IdTask }).HasName("PK__Schedule__79E352522DEB3726");

            entity.ToTable("Schedules_Tasks");

            entity.Property(e => e.IdSchedule).HasColumnName("id_schedule");
            entity.Property(e => e.IdTask).HasColumnName("id_task");
            entity.Property(e => e.Position).HasColumnName("position");

            entity.HasOne(d => d.IdScheduleNavigation).WithMany(p => p.SchedulesTasks)
                .HasForeignKey(d => d.IdSchedule)
                .HasConstraintName("FK_Schedules_Tasks_Schedules");

            entity.HasOne(d => d.IdTaskNavigation).WithMany(p => p.SchedulesTasks)
                .HasForeignKey(d => d.IdTask)
                .HasConstraintName("FK_Schedules_Tasks_Tasks");
        });

        modelBuilder.Entity<Task>(entity =>
        {
            entity.HasKey(e => e.IdTask).HasName("PK__Tasks__C1D2C6176E6BF301");

            entity.Property(e => e.IdTask).HasColumnName("id_task");
            entity.Property(e => e.IdGroup).HasColumnName("id_group");
            entity.Property(e => e.IdUser).HasColumnName("id_user");
            entity.Property(e => e.IsActive)
                .HasDefaultValue(true)
                .HasColumnName("is_active");
            entity.Property(e => e.Minutes).HasColumnName("minutes");
            entity.Property(e => e.Name)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.RepeatedEvery)
                .HasDefaultValue(1)
                .HasColumnName("repeated_every");
            entity.Property(e => e.UnavailableDays)
                .HasMaxLength(1)
                .HasDefaultValueSql("((0))")
                .IsFixedLength()
                .HasColumnName("unavailable_days");

            entity.HasOne(d => d.IdGroupNavigation).WithMany(p => p.Tasks)
                .HasForeignKey(d => d.IdGroup)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK_Tasks_Groups");

            entity.HasOne(d => d.IdUserNavigation).WithMany(p => p.Tasks)
                .HasForeignKey(d => d.IdUser)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Tasks_Users");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.IdUser).HasName("PK__Users__D2D14637FBBA8416");

            entity.Property(e => e.IdUser).HasColumnName("id_user");
            entity.Property(e => e.Email)
                .HasMaxLength(256)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.FreeTime).HasColumnName("free_time");
            entity.Property(e => e.IsActive).HasColumnName("is_active");
            entity.Property(e => e.LastSession)
                .HasPrecision(0)
                .HasDefaultValueSql("(sysdatetime())")
                .HasColumnName("last_session");
            entity.Property(e => e.MinGoal)
                .HasDefaultValue(0.70m)
                .HasColumnType("decimal(6, 6)")
                .HasColumnName("min_goal");
            entity.Property(e => e.Password)
                .HasMaxLength(128)
                .IsUnicode(false)
                .HasColumnName("password");
            entity.Property(e => e.Streak).HasColumnName("streak");
            entity.Property(e => e.Username)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("username");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
