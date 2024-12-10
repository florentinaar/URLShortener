using Microsoft.EntityFrameworkCore;
using URLShortener.Models;

namespace URLShortener.Data
{
    public class DataContext(DbContextOptions options) : DbContext(options)
    {
        public DbSet<Url> Urls{ get; set; }

    }
}
