using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ShiftsSchedule.Models;

namespace ShiftsSchedule.Data
{
    public class ShiftsScheduleContext : DbContext
    {
        private IConfigurationRoot config;

        public ShiftsScheduleContext(DbContextOptions<ShiftsScheduleContext> options, IConfigurationRoot config)
            : base(options)
        {
            this.config = config;
        }

        public DbSet<Worker> Workers { get; set; }
        public DbSet<Shift> Shifts { get; set; }
        public DbSet<Project> Projects { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer(config["database:connection"]);
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<WorkerShift>()
              .HasKey(ws => new { ws.ShiftId, ws.WorkerId });

            modelBuilder.Entity<WorkerShift>()
              .HasOne(s => s.Shift)
              .WithMany(w => w.Workers)
              .HasForeignKey(s => s.ShiftId);

            modelBuilder.Entity<WorkerShift>()
              .HasOne(w => w.Worker)
              .WithMany(s => s.Shifts)
              .HasForeignKey(w => w.WorkerId);

        }
    }
}
