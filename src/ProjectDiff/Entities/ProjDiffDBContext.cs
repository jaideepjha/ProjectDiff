using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectDiff.Entities
{
    public class ProjDiffDBContext : DbContext
    {
        private static bool _created = false;
        public ProjDiffDBContext()
        {
            if (!_created)
            {
                _created = true;
                Database.EnsureCreated();
            }
        }
        public DbSet<FileProperty> FileProperties { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Snapshot> Snapshots { get; set; }
        public DbSet<Solution> Solutions { get; set; }
        public DbSet<SnapshotFileProperty> SnapshotFileProperties { get; set; }
        public DbSet<SnapshotProject> SnapshotProjects { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Filename=./ProjDiffDB.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<SnapshotProject>().HasKey(x => new { x.SnapshotID, x.ProjectID });
            modelBuilder.Entity<SnapshotFileProperty>().HasKey(x => new { x.SnapshotID, x.FilePropertyID });

            modelBuilder.Entity<SnapshotProject>()
                .HasOne(pt => pt.Snapshot)
                .WithMany(p => p.SnapshotProjects)
                .HasForeignKey(pt => pt.SnapshotID);

            modelBuilder.Entity<SnapshotProject>()
                .HasOne(pt => pt.Project)
                .WithMany(p => p.SnapshotProjects)
                .HasForeignKey(pt => pt.ProjectID);

            modelBuilder.Entity<SnapshotFileProperty>()
                .HasOne(pt => pt.Snapshot)
                .WithMany(p => p.SnapshotFileProperties)
                .HasForeignKey(pt => pt.SnapshotID);

            modelBuilder.Entity<SnapshotFileProperty>()
                .HasOne(pt => pt.FileProperty)
                .WithMany(p => p.SnapshotFileProperties)
                .HasForeignKey(pt => pt.FilePropertyID);

        }


    }
}
