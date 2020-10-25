using BasicXamarin.Contract.Entities;
using BasicXamarin.Data.Seed;
using Microsoft.EntityFrameworkCore;

namespace BasicXamarin.Data
{
    public class BasicDbContext : DbContext
    {
        public DbSet<Test> Tests { get; set; }

        private readonly string _connectionString;

        public BasicDbContext(string connectionString)
            : base()
        {
            _connectionString = connectionString;
            this.Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(_connectionString);
            base.OnConfiguring(optionsBuilder);     
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Seed();
        }
    }
}
