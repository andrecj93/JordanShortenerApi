using System;
using System.Data.Entity;
using UrlShortenerApi.DataAccess.Models;

namespace UrlShortenerApi.DataAccess.Context
{
    public interface IUrlShortenerApiContext : IDisposable
    {
        DbSet<ShortLink> ShortLinks { get; }
        DbSet<User> Users { get; }

        int SaveChanges();
    }
}