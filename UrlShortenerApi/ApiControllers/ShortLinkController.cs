using Microsoft.AspNet.Identity;
using System;
using System.Web;
using System.Web.Http;
using UrlShortenerApi.Business.Services;
using UrlShortenerApi.DataAccess.Context;
using UrlShortenerApi.DataAccess.Models;
using UrlShortenerApi.Models;
using UrlShortenerApi.Models.RequestModels;
using UrlShortenerApi.Utilities;
using UrlShortenerApi.Utilities.UrlValidator;

namespace UrlShortenerApi.Controllers
{
    /// <summary>
    /// Controller that handles ShortLinks Generator
    /// </summary>
    [Authorize]
    [RoutePrefix("api")]
    public class ShortLinkController : ApiController
    {
        private readonly IUrlShortenerApiContext db = new UrlShortenerApiDbContext();
        private ShortLinkService ShortLinkService { get; set; }

        public ShortLinkController()
        {
            ShortLinkService = new ShortLinkService(db);
        }

        public ShortLinkController(IUrlShortenerApiContext context)
        {
            db = context;
            ShortLinkService = new ShortLinkService(context);
        }

        // POST: api/shortLink
        [Route("shortLink")]
        [HttpPost]
        public IHttpActionResult Post([FromBody] InsertLinkRequestParams insertParams)
        {
            if (!ModelState.IsValid)
                return BadRequest($"Invalid Request");

            if (!User.Identity.IsAuthenticated)
                return Unauthorized();

            var validateLink = UrlValidator.Validate(insertParams.FullLink);
            if (!validateLink.IsValid)
            {
                return BadRequest(validateLink.ValidationMessage);
            }

            string token = ShortLinkService.GenerateToken();

            if (string.IsNullOrWhiteSpace(token))
            {
                return InternalServerError(new Exception("Couldn't create the token!"));
            }

            string shortenedLink = $"{GetUrlAuthorityLeftPart()}/{token}";

            var urlShorten = new ShortLink()
            {
                Clicks = 0,
                Token = token,
                FullLink = insertParams.FullLink,
                CreatedDate = DateTime.Now,
                ExpireDate = DateTime.Now.AddDays(Options.ExpireDays),
                Active = true,
                ShortenedLink = shortenedLink,
                CreatedByUserId = GetAuthenticatedUserId(),
                CreatedByIp = HttpContext.Current?.Request?.UserHostAddress ?? "NotFound"
            };

            db.ShortLinks.Add(urlShorten);
            db.SaveChanges();

            return Ok(new ShortLinkResponse { ShortLink = shortenedLink, Code = token });
        }

        [HttpPost]
        [Route("api/shortLink/deleteExpired")]
        public IHttpActionResult DeleteExpired()
        {
            bool hasLinksToRemove = ShortLinkService.RemoveNotActiveAndExpiredLinks();

            if (hasLinksToRemove)
            {
                int deletedRows = db.SaveChanges();
                return Ok(deletedRows);
            }

            return Ok(0);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        #region -= AUX =-

        /// <summary>
        /// Gets the left part of the Request Uri - Authority
        /// </summary>
        /// <returns></returns>
        private string GetUrlAuthorityLeftPart()
        {
            return Request.RequestUri.GetLeftPart(UriPartial.Authority);
        }

        /// <summary>
        /// Gets the current Authenticated User defined by identity and gotten by the request to /token with proper credentials
        /// </summary>
        /// <returns></returns>
        private int GetAuthenticatedUserId()
        {
            return Convert.ToInt32(User.Identity.GetUserId());
        }

        #endregion
    }
}
