using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;

namespace UrlShortenerApi.Business
{
    public class NotFoundWithMessageResult : IHttpActionResult
    {
        private readonly string Message;

        public NotFoundWithMessageResult(string msg)
        {
            Message = msg;
        }

        public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            var response = new HttpResponseMessage(System.Net.HttpStatusCode.NotFound)
            {
                Content = new StringContent(Message)
            };
            return Task.FromResult(response);
        }
    }
}