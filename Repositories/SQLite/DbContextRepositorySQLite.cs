using Entities.Database;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repositories.SQLite
{
    public class DbContextRepositorySQLite : DbContext
    {
        public virtual DbSet<Stats> Stats { get; set; }

        public DbContextRepositorySQLite(DbContextOptions<DbContextRepositorySQLite> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=database.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }
    }
}
