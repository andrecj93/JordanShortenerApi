using Newtonsoft.Json;

namespace UrlShortenerApi.Models.RequestModels
{
    public class InsertLinkRequestParams
    {
        [JsonProperty("fullLink")]
        public string FullLink { get; set; }
    }
}