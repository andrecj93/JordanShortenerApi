using System;
using System.Net;
using System.Web.Mvc;
using UrlShortenerApi.Business.Exceptions;
using UrlShortenerApi.Properties;

namespace UrlShortenerApi.App_Start.Filters
{
    public class UrlShortenerMvcFilter : HandleErrorAttribute
    {
        public override void OnException(ExceptionContext filterContext)
        {
            HttpStatusCode code = HttpStatusCode.InternalServerError;
            var ex = filterContext.Exception;
            string viewName = "Error500";
            string customMessage = Resources.UnknownError;

            if (ex is TokenNotFoundException || ex is TokenNoLongerActiveException || ex is TokenInvalidException)
            {
                code = HttpStatusCode.NotFound;
                viewName = "Error404";

                if (ex is TokenNotFoundException)
                    customMessage = Resources.TokenNotFound;
                else if (ex is TokenInvalidException)
                    customMessage = Resources.TokenInvalid;
                else
                    customMessage = Resources.TokenNotActive;
            }

            if (ex is ArgumentException)
            {
                code = HttpStatusCode.BadRequest;
                viewName = "Error500";
            }

            var viewResult = new ViewResult()
            {
                ViewName = viewName,
            };

            viewResult.ViewBag.CustomMessage = customMessage;

            filterContext.Result = viewResult;

            filterContext.ExceptionHandled = true;
            filterContext.HttpContext.Response.Clear();
            filterContext.HttpContext.Response.StatusCode = (int)code;
            filterContext.HttpContext.Response.TrySkipIisCustomErrors = true;
        }
    }
}