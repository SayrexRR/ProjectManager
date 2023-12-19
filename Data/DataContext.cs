using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectManager.DataLayer.Entity;

namespace ProjectManager.DataLayer.Context
{
    public class DataContext : DbContext
    {
        public DbSet<Project> Projects { get; set; }
        public DbSet<MyTask> Tasks { get; set; }
        public DbSet<Point> Points { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=localhost;Database=ProjectManagerDb;Trusted_Connection=True;TrustServerCertificate=True");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Project>(m =>
            {
                m.Property(p => p.Name)
                .HasMaxLength(200)
                .IsRequired();

                m.Property(p => p.StartDate)
                .HasDefaultValue(DateTime.Now)
                .IsRequired();

                m.Property(p => p.EndDate)
                .IsRequired();

                m.HasMany(p => p.Tasks).WithOne(t => t.Project).HasForeignKey(t => t.ProjectId);
            });

            modelBuilder.Entity<MyTask>(m =>
            {
                m.Property(t => t.Title)
                .HasMaxLength(250)
                .IsRequired();

                m.Property(t => t.CreatedAt)
                .HasDefaultValue(DateTime.Now)
                .IsRequired();

                m.Property(t => t.Status)
                .HasConversion(
                    v => v.ToString(),
                    v => (Status)Enum.Parse(typeof(Status), v));

                m.HasMany(t => t.Points).WithOne(p => p.Task).HasForeignKey(p => p.TaskId);
            });

            modelBuilder.Entity<Point>(m =>
            {
                m.Property(p => p.Name)
                .HasMaxLength(200)
                .IsRequired();

                m.Property(p => p.IsCompleted)
                .HasDefaultValue(false)
                .IsRequired();
            });
        }
    }
}
