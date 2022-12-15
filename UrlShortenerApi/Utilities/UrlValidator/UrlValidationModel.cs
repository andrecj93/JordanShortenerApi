namespace UrlShortenerApi.Utilities.UrlValidator
{
    public class UrlValidationModel
    {
        public const string InvalidScheme = "Invalid URI scheme.";
        public const string EmptyInputValue = "Empty link value.";
        public const string InvalidUriFormat = "Invalid URI format.";
        public const string PassedValidation = "Passed validation";
        public const string HttpScheme = "http://";
        public const string HttpsScheme = "https://";

        public bool IsValid { get; set; }
        public string ValidationMessage { get; set; }
    }
}