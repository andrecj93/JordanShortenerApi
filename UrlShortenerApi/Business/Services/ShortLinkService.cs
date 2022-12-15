using System;
using System.Linq;
using UrlShortenerApi.Business.Exceptions;
using UrlShortenerApi.DataAccess.Context;
using UrlShortenerApi.DataAccess.Models;
using UrlShortenerApi.Utilities;

namespace UrlShortenerApi.Business.Services
{
    public class ShortLinkService
    {
        public IUrlShortenerApiContext DbContext { get; set; }

        public ShortLinkService(IUrlShortenerApiContext dbContext)
        {
            DbContext = dbContext;
        }

        /// <summary>
        /// Generates a token based on <see cref="Options.ValidCharacters"/>
        /// </summary>
        /// <returns></returns>
        public string GenerateToken()
        {
            var random = new Random();
            string token = string.Empty;
            bool isValid = false;

            while (!isValid)
            {
                var chars = Enumerable.Range(0, Options.ValidCharacters.Length - 1)
                    .OrderBy(o => random.Next())
                    .Select(i => Options.ValidCharacters[i]);

                token = string.Join("", chars);

                int length = random.Next(Options.MinLength, Options.MaxLength);
                int start = random.Next(0, token.Length - length - 1);

                token = token.Substring(start, length);

                isValid = !DbContext.ShortLinks.Any(s => s.Token == token);
            }

            return token;
        }

        /// <summary>
        /// Gets the full link by token
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public string GetFullUrlByToken(string token)
        {
            if (string.IsNullOrWhiteSpace(token) || token.Length > Options.MaxLength || token.IndexOfAny(Options.InvalidCharacters.ToCharArray()) != -1)
                throw new TokenInvalidException();

            var shortLink = DbContext.ShortLinks.FirstOrDefault(s => s.Token.Equals(token, StringComparison.Ordinal));

            if (shortLink == null)
                throw new TokenNotFoundException();

            UpdateClicks(shortLink);

            if (!IsLinkActive(shortLink))
            {
                DbContext.SaveChanges(); // Its set in here to be able save the changes before throwing
                throw new TokenNoLongerActiveException();
            }

            return System.Web.HttpUtility.UrlDecode(shortLink.FullLink);
        }

        /// <summary>
        /// Checks if the link is active and also checks if the date has expired
        /// </summary>
        /// <param name="shortenLink"></param>
        /// <returns></returns>
        public bool IsLinkActive(ShortLink shortenLink)
        {
            if (!shortenLink.Active)
            {
                return false;
            }

            if (DateTime.Now > shortenLink.ExpireDate)
            {
                shortenLink.Active = false;
                return false;
            }

            return true;
        }

        /// <summary>
        /// Checks if the fullLink already exists
        /// </summary>
        /// <param name="fullLink"></param>
        /// <returns></returns>
        public ShortLink DoesFullLinkAlreadyExistsOnDatabase(string fullLink)
        {
            return DbContext.ShortLinks.FirstOrDefault(f => f.FullLink.Equals(fullLink, StringComparison.OrdinalIgnoreCase));
        }

        /// <summary>
        /// Updates the clicks for a given link
        /// </summary>
        /// <param name="link"></param>
        public void UpdateClicks(ShortLink link)
        {
            ++link.Clicks;
            link.LastClickDate = DateTime.Now;
        }

        /// <summary>
        /// Removes expired links
        /// </summary>
        /// <returns></returns>
        public bool RemoveNotActiveAndExpiredLinks()
        {
            var linksToRemove = DbContext.ShortLinks.Where(s => !s.Active || DateTime.Now > s.ExpireDate);
            if (linksToRemove.Any())
            {
                DbContext.ShortLinks.RemoveRange(linksToRemove);
                return true;
            }

            return false;
        }
    }
}