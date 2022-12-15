using System;
using System.Net;
using System.Net.Http;
using System.Web.Http.Filters;
using UrlShortenerApi.Business.Exceptions;

namespace UrlShortenerApi.App_Start.Filters
{
    public class UrlShortenerApiFilter : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext ctx)
        {
            HttpStatusCode code = HttpStatusCode.InternalServerError;
            var ex = ctx.Exception;

            if (ex is TokenNotFoundException)
            {
                code = HttpStatusCode.NotFound;
            }
            else if (ex is ArgumentException)
            {
                code = HttpStatusCode.BadRequest;
            }

            ctx.Response = ctx.Request.CreateResponse(code);
        }
    }
}