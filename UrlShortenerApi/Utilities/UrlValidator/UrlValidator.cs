using System;

namespace UrlShortenerApi.Utilities.UrlValidator
{
    public static class UrlValidator
    {
        public static UrlValidationModel Validate(string url)
        {
            var validation = new UrlValidationModel();

            if (string.IsNullOrWhiteSpace(url))
            {
                validation.IsValid = false;
                validation.ValidationMessage = UrlValidationModel.EmptyInputValue;
                return validation;
            }

            try
            {
                var uri = new Uri(url);
                var leftPart = uri.GetLeftPart(UriPartial.Scheme);

                if (leftPart.Equals(UrlValidationModel.HttpScheme) || leftPart.Equals(UrlValidationModel.HttpsScheme))
                {
                    validation.IsValid = true;
                    validation.ValidationMessage = UrlValidationModel.PassedValidation;
                    return validation;
                }

                validation.IsValid = false;
                validation.ValidationMessage = UrlValidationModel.InvalidScheme;
            }
            catch (UriFormatException)
            {
                validation.IsValid = false;
                validation.ValidationMessage = UrlValidationModel.InvalidUriFormat;
            }

            return validation;
        }
    }
}