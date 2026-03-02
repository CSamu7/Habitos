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
    public virtual DbSet<Category> Categories { get; set; }
    public virtual DbSet<DailyRoutine> DailyRoutines { get; set; }
    public virtual DbSet<Routine> Routines { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>(entity =>
        {
            entity.Property(e => e.Email).HasMaxLength(256);
            entity.Property(e => e.NormalizedEmail).HasMaxLength(256);
            entity.Property(e => e.NormalizedUserName).HasMaxLength(256);
            entity.Property(e => e.UserName).HasMaxLength(256);
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.IdCategory).HasName("PK__Categori__E548B673567B220D");

            entity.Property(e => e.IdCategory).HasColumnName("id_category");
            entity.Property(e => e.Color)
                .HasDefaultValueSql("('#66ff33')")
                .HasColumnName("color");
            entity.Property(e => e.IdUser)
                .HasMaxLength(450)
                .HasColumnName("id_user");
            entity.Property(e => e.Name)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("name");

            entity.HasOne(d => d.IdUserNavigation).WithMany(p => p.Categories)
                .HasForeignKey(d => d.IdUser)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Categories_AspNetUsers");
        });

        modelBuilder.Entity<DailyRoutine>(entity =>
        {
            entity.HasKey(e => e.IdDailyRoutine).HasName("PK__DailyRoutines__448329FE1BAF407B");

            entity.Property(e => e.IdDailyRoutine).HasColumnName("id_daily_routine");
            entity.Property(e => e.CompletedAt)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("completed_at");
            entity.Property(e => e.Date)
                .HasDefaultValueSql("((sysdatetimeoffset() AT TIME ZONE 'UTC'))")
                .HasColumnName("date");
            entity.Property(e => e.IdRoutine).HasColumnName("id_routine");
            entity.Property(e => e.MinutesCompleted).HasColumnName("minutes_completed");
            entity.Property(e => e.TotalMinutes).HasColumnName("total_minutes");

            entity.HasOne(d => d.IdRoutineNavigation).WithMany(p => p.DailyRoutines)
                .HasForeignKey(d => d.IdRoutine)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DailyRoutines_Routines");
        });

        modelBuilder.Entity<Routine>(entity =>
        {
            entity.HasKey(e => e.IdRoutine).HasName("PK__Routines__855C1AF2DB47931C");

            entity.Property(e => e.IdRoutine).HasColumnName("id_routine");
            entity.Property(e => e.IdCategory).HasColumnName("id_category");
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

            entity.HasOne(d => d.IdCategoryNavigation).WithMany(p => p.Routines)
                .HasForeignKey(d => d.IdCategory)
                .HasConstraintName("FK_Routines_Categories");

            entity.HasOne(d => d.IdUserNavigation).WithMany(p => p.Routines)
                .HasForeignKey(d => d.IdUser)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Routines_AspNetUsers");
        });

        OnModelCreatingPartial(modelBuilder);
        base.OnModelCreating(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
