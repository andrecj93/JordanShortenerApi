using System.Web.Mvc;
using UrlShortenerApi.Business.Services;
using UrlShortenerApi.DataAccess.Context;

namespace UrlShortenerApi.Controllers
{
    /// <summary>
    /// Controller that handles the get of a Token
    /// </summary>
    public class UrlController : Controller
    {
        private readonly IUrlShortenerApiContext db = new UrlShortenerApiDbContext();
        private ShortLinkService ShortLinkService { get; set; }

        public UrlController()
        {
            ShortLinkService = new ShortLinkService(db);
        }

        public UrlController(IUrlShortenerApiContext context)
        {
            db = context;
            ShortLinkService = new ShortLinkService(context);
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        // GET: /{token}
        public ActionResult Click(string token)
        {
            var fullUrl = ShortLinkService.GetFullUrlByToken(token);
            db.SaveChanges();
            return RedirectPermanent(fullUrl);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}