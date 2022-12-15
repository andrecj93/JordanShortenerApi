using System.Data.Entity;
using UrlShortenerApi.DataAccess.Models;

namespace UrlShortenerApi.DataAccess.Context
{
    public class UrlShortenerApiDbContext : DbContext, IUrlShortenerApiContext
    {
        public UrlShortenerApiDbContext() : base("UrlShortenerDbContext")
        {
        }

        public DbSet<ShortLink> ShortLinks { get; set; }
        public DbSet<User> Users { get; set; }
    }
}