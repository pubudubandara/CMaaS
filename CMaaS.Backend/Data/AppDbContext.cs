using CMaaS.Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace CMaaS.Backend.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Tenant> Tenants { get; set; }
        public DbSet<ContentType> ContentTypes { get; set; }
        public DbSet<ContentEntry> ContentEntries { get; set; }


    }
}
