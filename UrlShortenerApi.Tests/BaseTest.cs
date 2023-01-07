using System.Net.Http;
using System.Security.Claims;
using UrlShortenerApi.Controllers;
using UrlShortenerApi.Tests.Moqs;

namespace UrlShortenerApi.Tests
{
    public class BaseTest
    {
        /// <summary>
        /// //Moq Uri
        /// </summary>
        protected const string Uri = "https://localhost:44364";

        protected void MoqIdentity(ShortLinkController shortLinkController)
        {
            var claims = new Claim[]
            {
                new Claim(ClaimTypes.NameIdentifier, "1"),
                new Claim(ClaimTypes.Name, "admin")
            };

            var identity = new ClaimsIdentity(claims, "Basic");
            var user = new ClaimsPrincipal(identity);

            shortLinkController.User = user;
        }

        protected ShortLinkController CreateMoqShortLinkController(TestUrlShortenerApiContext TestUrlShortenerApiContext)
        {
            return new ShortLinkController(TestUrlShortenerApiContext)
            {
                Request = new HttpRequestMessage(HttpMethod.Post, Uri)
            };
        }
    }
}
