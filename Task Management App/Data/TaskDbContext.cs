using Microsoft.EntityFrameworkCore;
using TaskManagement.Api.Entities;
using Task = TaskManagement.Api.Entities.Task;

namespace TaskManagement.Api.Data
{
    public class TaskDbContext : DbContext
    {
        public TaskDbContext(DbContextOptions<TaskDbContext> options)
            : base(options)
        {
        }

        public DbSet<Task> Tasks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Task>().ToTable("Tasks");

            modelBuilder.Entity<Task>(entity =>
            {
                entity.HasKey(t => t.TaskId);
                entity.Property(t => t.Title).IsRequired();
                entity.Property(t => t.Description).IsRequired();
                entity.Property(t => t.DueDate).IsRequired();
                entity.Property(t => t.Status).IsRequired();
            });
        }
    }
}
