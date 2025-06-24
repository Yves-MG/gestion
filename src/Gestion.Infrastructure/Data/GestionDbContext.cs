using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gestion.Core.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Gestion.Infrastructure.Data
{
    public class GestionDbContext : IdentityDbContext<User, Role, Guid>
    {
        public GestionDbContext(DbContextOptions<GestionDbContext> options) : base(options) { }
        public DbSet<Absence> Absence { get; set; }
        public DbSet<AdministrativeDocument> AdministrativeDocument { get; set; }
        public DbSet<AdministrativeTask> AdministrativeTask { get; set; }
        public DbSet<Courrier> Courrier { get; set; }
        public DbSet<Notifications> Notifications { get; set; }
        public DbSet<Resident> Resident { get; set; }
        public DbSet<Rooms> Rooms { get; set; }
        public new DbSet<User> Users { get; set; }
        public new DbSet<Role> Roles { get; set; }
        public DbSet<WashingMachine> WashingMachine { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Absence>()
                .HasOne(a => a.Resident)
                .WithMany(r => r.Absences)
                .HasForeignKey(a => a.ResidentId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Absence>()
               .HasOne(a => a.CreatedByUser)
               .WithMany()
               .HasForeignKey(a => a.CreatedByUserId)
               .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<AdministrativeDocument>()
                .HasOne(a => a.Resident)
                .WithMany(r => r.AdministrativeDocuments)
                .HasForeignKey(a => a.ResidentId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<AdministrativeDocument>()
                .HasOne(a => a.UploadedBy)
                .WithMany()
                .HasForeignKey(a => a.UploadedById)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<AdministrativeTask>()
                .HasOne(a => a.Resident)
                 .WithMany(r => r.AdministrativeTasks)
                .HasForeignKey(a => a.ResidentId)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<AdministrativeTask>()
                .HasOne(a => a.AssignedTo)
                .WithMany()
                .HasForeignKey(a => a.AssignedToId)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Courrier>()
                .HasOne(a => a.Resident)
                .WithMany(r => r.Courriers)
                .HasForeignKey(a => a.ResidentId)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Courrier>()
                .HasOne(a => a.DeliveryPerson)
                .WithMany()
                .HasForeignKey(a => a.DeliveryPersonId)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Notifications>()
                .HasOne(a => a.Resident)
                .WithMany(r => r.Notifications)
                .HasForeignKey(a => a.ResidentId)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Notifications>()
                .HasOne(a => a.CreatedByUser)
                .WithMany()
                .HasForeignKey(a => a.CreatedByUserId)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Resident>()
                .HasMany(a => a.Absences)
                .WithOne(a => a.Resident)
                .HasForeignKey(a => a.ResidentId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Resident>()
                .HasMany(a => a.AdministrativeDocuments)
                .WithOne(a => a.Resident)
                .HasForeignKey(a => a.ResidentId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Resident>()
                .HasMany(a => a.AdministrativeTasks)
                .WithOne(a => a.Resident)
                .HasForeignKey(a => a.ResidentId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Resident>()
                .HasMany(a => a.Courriers)
                .WithOne(a => a.Resident)
                .HasForeignKey(a => a.ResidentId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Resident>()
                .HasMany(a => a.Notifications)
                .WithOne(a => a.Resident)
                .HasForeignKey(a => a.ResidentId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Resident>()
                .HasMany(a => a.WashingMachines)
                .WithOne(a => a.Resident)
                .HasForeignKey(a => a.ResidentId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Resident>()
                .HasOne(a => a.Room)
                .WithMany()
                .HasForeignKey(a => a.RoomId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Resident>()
                .HasOne(a => a.UserAdd)
                .WithMany()
                .HasForeignKey(a => a.UserAddId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Resident>()
                .HasOne(a => a.SocialUser)
                .WithMany()
                .HasForeignKey(a => a.SocialUserId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<WashingMachine>()
                .HasOne(a => a.Resident)
                .WithMany(r => r.WashingMachines)
                .HasForeignKey(a => a.ResidentId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<WashingMachine>()
                .HasOne(a => a.CreatedByUser)
                .WithMany()
                .HasForeignKey(a => a.CreatedByUserId)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Rooms>()
                .HasMany(a => a.Residents)
                .WithOne(a => a.Room)
                .HasForeignKey(a => a.RoomId)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Rooms>()
                .HasOne(a => a.CreatedUser)
                .WithMany()
                .HasForeignKey(a => a.CreatedUserId)
                .OnDelete(DeleteBehavior.Cascade);

        }

    }
}
