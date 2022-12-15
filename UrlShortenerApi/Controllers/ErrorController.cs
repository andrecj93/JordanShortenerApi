using System.Web.Mvc;

namespace UrlShortenerApi.Controllers
{
    public class ErrorController : Controller
    {
        public ViewResult NotFound() => View("Error404");
        public ViewResult BadRequest() => View("Error500");
    }
}