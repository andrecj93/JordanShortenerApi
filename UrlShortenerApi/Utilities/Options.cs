using System;

namespace UrlShortenerApi.Utilities
{
    public static class Options
    {
        /// <summary>
        /// String with the collection of allowed characters to be use for the generation of the short links.
        /// The default characters are "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz"
        /// Using the full English alphabet plus all numerals from 0-9 that gives us 62 available characters, meaning we have: 
        /// (62^2) + (62^3) + (62^4) + (62^5) + (62^6) possible unique tokens which equals: `57 billion 731 million 386 thousand 924´.
        /// Ou seja >> E(ValidCharacters.Length^n), _minLength >= n <= MaxLength
        /// </summary>
        public static string ValidCharacters { get; set; } = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";

        public const string InvalidCharacters = "!@#$%^&*()_+{}[]\"'<>?/.,;-`\\~";

        /// <summary>
        /// The minimum length (default = 2) of the generated random unique token.
        /// </summary>
        private static int _minLength = 2;

        /// <summary>
        /// The minimum length (default = 2) of the generated random unique token.
        /// </summary>
        public static int MinLength
        {
            get => _minLength;
            set
            {
                if (value >= MaxLength)
                    throw new ArgumentException($"The {nameof(MinLength)} must be less that the {nameof(MaxLength)}. {value} < {MaxLength}?");
                else
                    _minLength = value;
            }
        }

        /// <summary>
        /// The maximum length (default = 6) of the generated random unique token.
        /// </summary>
        private static int _maxLength = 6;

        /// <summary>
        /// The maximum length (default = 6) of the generated random unique token.
        /// </summary>
        public static int MaxLength
        {
            get => _maxLength;
            set
            {
                if (value <= MinLength)
                    throw new ArgumentException($"The {nameof(MaxLength)} must be greater that the {nameof(MinLength)}. {value} > {MinLength}?");
                else
                    _maxLength = value;
            }
        }

        /// <summary>
        /// The days after DateTime.Now that the token will expire
        /// </summary>
        private static int _expireDays = 100;

        /// <summary>
        /// The days after DateTime.Now that the token will expire
        /// </summary>
        public static int ExpireDays
        {
            get => _expireDays;
            set
            {
                if (value <= 0)
                    throw new ArgumentException($"The {nameof(ExpireDays)} must be greater than zero");
                else
                    _expireDays = value;
            }
        }

    }
}