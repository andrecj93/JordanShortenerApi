using NUnit.Framework;
using System.Collections;
using System.Web.Http;
using System.Web.Http.Results;
using UrlShortenerApi.Controllers;
using UrlShortenerApi.Models;
using UrlShortenerApi.Models.RequestModels;
using UrlShortenerApi.Tests.Moqs;
using UrlShortenerApi.Utilities.UrlValidator;

namespace UrlShortenerApi.Tests.Controllers
{
    public class ShortLinkControllerTest : BaseTest
    {
        private ShortLinkController ShortLinkController { get; set; }

        [SetUp]
        public void SetUp() => ShortLinkController = CreateMoqShortLinkController(new TestUrlShortenerApiContext());

        [Test, TestCaseSource(nameof(TestDataBadRequest))]
        public void Post_InvalidUri_ShouldRespondWithBadRequest(string link, string expectedInvalidMsg)
        {
            // Arrange
            MoqIdentity(ShortLinkController);
            var model = new InsertLinkRequestParams()
            {
                FullLink = link
            };

            // Act
            IHttpActionResult actionResult = ShortLinkController.Post(model);
            var contentResult = actionResult as BadRequestErrorMessageResult;

            // Assert
            Assert.IsNotNull(contentResult);
            Assert.That(contentResult.Message == expectedInvalidMsg);
        }

        [Test, TestCaseSource(nameof(TestDataOkRequest))]
        public void Post_ValidUri_ShouldRespondWithOkRequest(string link)
        {
            // Moq IPrincipal Identity
            MoqIdentity(ShortLinkController);

            // Arrange
            var model = new InsertLinkRequestParams()
            {
                FullLink = link
            };

            // Act
            IHttpActionResult actionResult = ShortLinkController.Post(model);
            var res = actionResult as OkNegotiatedContentResult<ShortLinkResponse>;

            // Assert
            Assert.IsNotNull(res);
            Assert.IsNotEmpty(res.Content.ShortLink);
            Assert.IsNotEmpty(res.Content.Code);
            Assert.That(res.Content.ShortLink.Contains(Uri));
        }

        [Test, TestCaseSource(nameof(TestDataOkRequest))]
        public void Post_NotAuthenticated_ReturnsUnauthorized(string link)
        {
            // Arrange
            var model = new InsertLinkRequestParams()
            {
                FullLink = link
            };

            // Act
            IHttpActionResult actionResult = ShortLinkController.Post(model);
            var res = actionResult as UnauthorizedResult;

            // Assert
            Assert.IsNotNull(res);
        }

        private static IEnumerable TestDataBadRequest()
        {
            yield return new TestCaseData(null, UrlValidationModel.EmptyInputValue);
            yield return new TestCaseData(string.Empty, UrlValidationModel.EmptyInputValue);
            yield return new TestCaseData(" ", UrlValidationModel.EmptyInputValue);
            yield return new TestCaseData("http://", UrlValidationModel.InvalidUriFormat);
            yield return new TestCaseData("//google.com", UrlValidationModel.InvalidScheme);
            yield return new TestCaseData("://google.com", UrlValidationModel.InvalidUriFormat);
            yield return new TestCaseData("f://google.com", UrlValidationModel.InvalidScheme);
            yield return new TestCaseData("htttp://google.com", UrlValidationModel.InvalidScheme);
            yield return new TestCaseData("google.com", UrlValidationModel.InvalidUriFormat);
            yield return new TestCaseData("ftp://google.com", UrlValidationModel.InvalidScheme);
            yield return new TestCaseData("http:google.com", UrlValidationModel.InvalidUriFormat);
            yield return new TestCaseData("www.google.com", UrlValidationModel.InvalidUriFormat);
        }

        private static IEnumerable TestDataOkRequest()
        {
            yield return new TestCaseData("http://google.com");
            yield return new TestCaseData("https://google.com");
            yield return new TestCaseData("https://google.com/index.html");
        }
    }
}
