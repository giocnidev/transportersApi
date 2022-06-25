using Entities.Database;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repositories
{
    public class DbContextRepository : DbContext
    {
        public virtual DbSet<Stats> Stats { get; set; }

        public DbContextRepository(DbContextOptions<DbContextRepository> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder){
            optionsBuilder.UseSqlite("Data Source=database.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
        }
    }
}
