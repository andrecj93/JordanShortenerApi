using System.Linq;
using UrlShortenerApi.DataAccess.Models;

namespace UrlShortenerApi.Tests.Moqs
{
    internal class TestShortLinkDbSet : TestDbSet<ShortLink>
    {
        public override ShortLink Find(params object[] keyValues)
        {
            return this.SingleOrDefault(s => s.Token == (string)keyValues.Single());
        }
    }
}
