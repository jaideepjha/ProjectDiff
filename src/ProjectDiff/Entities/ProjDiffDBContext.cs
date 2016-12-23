using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectDiff.Entities
{
    public class ProjDiffDBContext : DbContext
    {
        public DbSet<FileProperty> FileProperties { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Snapshot> Snapshots { get; set; }
        public DbSet<Solution> Solutions { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Filename=./ProjDiffDB.db");
        }

    }
}
