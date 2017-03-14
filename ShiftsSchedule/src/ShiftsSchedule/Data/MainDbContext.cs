using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Configuration;
using ShiftsSchedule.Models;

namespace ShiftsSchedule.Data
{
    public sealed class ShiftsScheduleContext : IdentityDbContext<ApplicationUser>
    {
        private readonly IConfigurationRoot _config;

        public ShiftsScheduleContext(DbContextOptions<ShiftsScheduleContext> options, IConfigurationRoot config)
            : base(options)
        {
            this._config = config;
          //  Database.EnsureCreated();
        }

        public DbSet<Worker> Workers { get; set; }
        public DbSet<Shift> Shifts { get; set; }
        public DbSet<Project> Projects { get; set; }

        public DbSet<Specialty> Specialties { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer(_config["database:connection"]);
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
            base.OnModelCreating(modelBuilder);

        }
    }
}
