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

    public virtual DbSet<Routine> Routines { get; set; }
    public virtual DbSet<RoutineCategory> RoutineCategories { get; set; }
    public virtual DbSet<DailyTask> DailyTasks { get; set; }
    public virtual DbSet<User> Users { get; set; } 
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
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
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
