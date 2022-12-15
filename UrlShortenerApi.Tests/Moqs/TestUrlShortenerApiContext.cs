using System.Data.Entity;
using UrlShortenerApi.DataAccess.Context;
using UrlShortenerApi.DataAccess.Models;

namespace UrlShortenerApi.Tests.Moqs
{
    public class TestUrlShortenerApiContext : IUrlShortenerApiContext
    {
        public TestUrlShortenerApiContext()
        {
            ShortLinks = new TestShortLinkDbSet();
        }

        public DbSet<ShortLink> ShortLinks { get; set; }

        public DbSet<User> Users => throw new System.NotImplementedException();


        public int SaveChanges()
        {
            return 0;
        }

        public void Dispose()
        {

        }
    }
}
