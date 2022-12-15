using System.Web.Mvc;
using UrlShortenerApi.App_Start.Filters;

namespace UrlShortenerApi
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new UrlShortenerMvcFilter());
        }
    }
}
