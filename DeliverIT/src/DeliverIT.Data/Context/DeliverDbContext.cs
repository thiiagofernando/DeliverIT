using DeliverIT.Business.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace DeliverIT.Data.Context
{
    public class DeliverDbContext : DbContext
    {
        public DeliverDbContext(DbContextOptions options) : base(options) { }

        public DbSet<Conta> conta { get; set; }
        public DbSet<Usuario> usuario { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //varchar(100) default para string para nao criar varchar(MAX)
            foreach (var property in modelBuilder.Model.GetEntityTypes()
                  .SelectMany(e => e.GetProperties()
                      .Where(p => p.ClrType == typeof(string))))
            {
                property.SetColumnType("varchar(100)");
            }

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(DeliverDbContext).Assembly);

            base.OnModelCreating(modelBuilder);
        }
    }
}
