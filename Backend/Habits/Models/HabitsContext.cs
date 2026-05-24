using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Habits.Models;

public partial class HabitsContext : IdentityDbContext<User>
{
    public HabitsContext()
    {
    }

    public HabitsContext(DbContextOptions<HabitsContext> options)
        : base(options)
    {
    }

    public virtual DbSet<DailyTask> DailyTasks { get; set; }
    public virtual DbSet<Routine> Routines { get; set; }
    public virtual DbSet<RoutineCategory> RoutineCategories { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        modelBuilder.Entity<User>(entity =>
        {
            entity.Property(e => e.Email).HasMaxLength(256);
            entity.Property(e => e.NormalizedEmail).HasMaxLength(256);
            entity.Property(e => e.NormalizedUserName).HasMaxLength(256);
            entity.Property(e => e.TimeZone)
                .HasMaxLength(64)
                .IsUnicode(false);
            entity.Property(e => e.UserName).HasMaxLength(256);
        });

        modelBuilder.Entity<DailyTask>(entity =>
        {
            entity.HasKey(e => e.IdDailyTask).HasName("PK__DailyTasks__448329FE1BAF407B");

            entity.Property(e => e.IdDailyTask).HasColumnName("id_daily_task");
            entity.Property(e => e.CompletedAt)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("completed_at");
            entity.Property(e => e.Date)
                .HasDefaultValueSql("((sysdatetimeoffset() AT TIME ZONE 'UTC'))")
                .HasColumnName("date");
            entity.Property(e => e.IdRoutine).HasColumnName("id_routine");
            entity.Property(e => e.MinutesCompleted).HasColumnName("minutes_completed");
            entity.Property(e => e.TotalMinutes).HasColumnName("total_minutes");

            entity.HasOne(d => d.IdRoutineNavigation).WithMany(p => p.DailyTasks)
                .HasForeignKey(d => d.IdRoutine)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DailyTasks_Routines");
        });

        modelBuilder.Entity<Routine>(entity =>
        {
            entity.HasKey(e => e.IdRoutine).HasName("PK__Routines__855C1AF2E3DF1B41");

            entity.Property(e => e.IdRoutine).HasColumnName("id_routine");
            entity.Property(e => e.IdRoutineCategory).HasColumnName("id_routine_category");
            entity.Property(e => e.IdUser)
                .HasMaxLength(450)
                .HasColumnName("id_user");
            entity.Property(e => e.IsActive)
                .HasDefaultValue(true)
                .HasColumnName("is_active");
            entity.Property(e => e.Minutes).HasColumnName("minutes");
            entity.Property(e => e.Name)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("name");

            entity.HasOne(d => d.IdRoutineCategoryNavigation).WithMany(p => p.Routines)
                .HasForeignKey(d => d.IdRoutineCategory)
                .HasConstraintName("FK_Routines_RoutineCategories");

            entity.HasOne(d => d.IdUserNavigation).WithMany(p => p.Routines)
                .HasForeignKey(d => d.IdUser)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Routines_AspNetUsers");
        });

        modelBuilder.Entity<RoutineCategory>(entity =>
        {
            entity.HasKey(e => e.IdRoutineCategory).HasName("PK__RoutineC__ED7A3D1E3BB83357");

            entity.Property(e => e.IdRoutineCategory).HasColumnName("id_routine_category");
            entity.Property(e => e.Color)
                .HasMaxLength(6)
                .IsUnicode(false)
                .HasDefaultValue("66ff33")
                .HasColumnName("color");
            entity.Property(e => e.IdUser)
                .HasMaxLength(450)
                .HasColumnName("id_user");
            entity.Property(e => e.Name)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("name");

            entity.HasOne(d => d.IdUserNavigation).WithMany(p => p.RoutineCategories)
                .HasForeignKey(d => d.IdUser)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_RoutineCategories_AspNetUsers");
        });

        OnModelCreatingPartial(modelBuilder);
        base.OnModelCreating(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
