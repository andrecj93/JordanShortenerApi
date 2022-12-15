using NUnit.Framework;
using System;
using System.Collections;
using System.Web.Http;
using System.Web.Http.Results;
using UrlShortenerApi.Business.Exceptions;
using UrlShortenerApi.Controllers;
using UrlShortenerApi.Models;
using UrlShortenerApi.Models.RequestModels;
using UrlShortenerApi.Tests.Moqs;

namespace UrlShortenerApi.Tests.Controllers
{
    internal class UrlControllerTest : BaseTest
    {
        private UrlController UrlController { get; set; }
        private TestUrlShortenerApiContext TestUrlShortenerApiContext { get; set; }

        [SetUp]
        public void SetUp()
        {
            TestUrlShortenerApiContext = new TestUrlShortenerApiContext();
            UrlController = new UrlController(TestUrlShortenerApiContext)
            {
                // Moq Request just to get the completed link
                //Request = new HttpRequestMessage(HttpMethod.Post, Uri),
            };
        }

        [Test, TestCaseSource(nameof(Data_ClickShouldThrowException))]
        public void Click_ShouldThrowException(string token, Type exceptionType)
        {
            Assert.Throws(exceptionType, () => UrlController.Click(token));
        }

        [Test]
        public void Should_RedirectToFullLink_ExistingToken()
        {
            // Arrange
            var model = new InsertLinkRequestParams()
            {
                FullLink = "http://uon.pt"
            };

            var shortLinkController = CreateMoqShortLinkController(TestUrlShortenerApiContext);
            MoqIdentity(shortLinkController);

            /// Creates the token in this Moq Test so we can get it 
            IHttpActionResult createResult = shortLinkController.Post(model);
            var res = createResult as OkNegotiatedContentResult<ShortLinkResponse>;

            // Act
            var result = UrlController.Click(res.Content.Code) as System.Web.Mvc.RedirectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Permanent);
            Assert.AreEqual(model.FullLink, result.Url);
        }

        private static IEnumerable Data_ClickShouldThrowException()
        {
            yield return new TestCaseData(string.Empty, typeof(TokenInvalidException));
            yield return new TestCaseData(" ", typeof(TokenInvalidException));
            yield return new TestCaseData(null, typeof(TokenInvalidException));
            yield return new TestCaseData("..113da--", typeof(TokenInvalidException));
            yield return new TestCaseData("BLABLA", typeof(TokenNotFoundException));
            yield return new TestCaseData("asfdjkasdfoijio3", typeof(TokenInvalidException));
        }
    }
}
