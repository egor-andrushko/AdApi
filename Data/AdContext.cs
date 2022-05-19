using AdApi.Models;
using Microsoft.EntityFrameworkCore;

namespace AdApi.Data
{
    public class AdContext : DbContext
    {
        public AdContext(DbContextOptions<AdContext> options)
            : base(options)
        {
        }

        public DbSet<Ad> Ads => Set<Ad>();
        public DbSet<Photo> Photos => Set<Photo>();
    }
}
