using System;
using System.Linq;
using UrlShortenerApi.DataAccess.Context;
using UrlShortenerApi.DataAccess.Models;

namespace UrlShortenerApi.Services
{
    public class UserService
    {
        /// <summary>
        /// Checks if a user and its password match in DB
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public static User FindUserByUsernameAndPassword(string username, string password)
        {
            using (var dbContext = new UrlShortenerApiDbContext())
            {
                return dbContext.Users.FirstOrDefault(user => user.Username.Equals(username, StringComparison.OrdinalIgnoreCase) && user.Password == password && user.Status);
            }
        }

        internal static User FindUserByUsername(string username)
        {
            using (var dbContext = new UrlShortenerApiDbContext())
            {
                return dbContext.Users.FirstOrDefault(user => user.Username.Equals(username, StringComparison.OrdinalIgnoreCase));
            }
        }

        public static void CreateUserIfNotExists(string username, string password)
        {
            using (var dbContext = new UrlShortenerApiDbContext())
            {
                bool exists = dbContext.Users.Any(user => user.Username.Equals(username, StringComparison.OrdinalIgnoreCase));

                if (!exists)
                {
                    dbContext.Users.Add(new User()
                    {
                        Username = username,
                        Password = password,
                        CreatedAt = DateTime.Now,
                        Status = true,
                    });
                    dbContext.SaveChanges();
                }
            }
        }
    }
}