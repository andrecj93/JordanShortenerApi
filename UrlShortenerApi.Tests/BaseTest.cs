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
            var user = new ClaimsPrincipal(new ClaimsIdentity(
                            new Claim[] {
                    new Claim(ClaimTypes.NameIdentifier, "1"),
                    new Claim(ClaimTypes.Name, "admin")
                            }, "Bearer"));

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
