using NUnit.Framework;
using System.Collections;
using UrlShortenerApi.Utilities.UrlValidator;

namespace UrlShortenerApi.Tests.Url
{
    [TestFixture]
    internal class UrlValidatorTest
    {
        [Test, TestCaseSource(nameof(AddUrls))]
        public void Validate_Input_ExpectedResult(string input, bool expectedResult, string expectedInvalidMessage)
        {
            //Act
            var result = UrlValidator.Validate(input);

            //Assert
            Assert.True(expectedResult == result.IsValid);
            Assert.That(expectedInvalidMessage.Equals(result.ValidationMessage));
        }

        private static IEnumerable AddUrls()
        {
            yield return new TestCaseData("http://google.com", true, UrlValidationModel.PassedValidation);
            yield return new TestCaseData("https://google.com", true, UrlValidationModel.PassedValidation);
            yield return new TestCaseData("https://google.com/index.html", true, UrlValidationModel.PassedValidation);
            yield return new TestCaseData(null, false, UrlValidationModel.EmptyInputValue);
            yield return new TestCaseData(string.Empty, false, UrlValidationModel.EmptyInputValue);
            yield return new TestCaseData(" ", false, UrlValidationModel.EmptyInputValue);
            yield return new TestCaseData("http://", false, UrlValidationModel.InvalidUriFormat);
            yield return new TestCaseData("//google.com", false, UrlValidationModel.InvalidScheme);
            yield return new TestCaseData("://google.com", false, UrlValidationModel.InvalidUriFormat);
            yield return new TestCaseData("f://google.com", false, UrlValidationModel.InvalidScheme);
            yield return new TestCaseData("htttp://google.com", false, UrlValidationModel.InvalidScheme);
            yield return new TestCaseData("google.com", false, UrlValidationModel.InvalidUriFormat);
            yield return new TestCaseData("ftp://google.com", false, UrlValidationModel.InvalidScheme);
            yield return new TestCaseData("http:google.com", false, UrlValidationModel.InvalidUriFormat);
            yield return new TestCaseData("www.google.com", false, UrlValidationModel.InvalidUriFormat);
        }
    }
}
