using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using UrlShortenerApi.Utilities;

namespace UrlShortenerApi.Tests.Token
{
    [TestFixture]
    public class TokenGeneratorTest
    {
        private static string ValidCharacters { get; set; } = Options.ValidCharacters;

        [Test]
        public void CanGenerateMultipleTokens()
        {
            int count = 10;
            List<string> tokens = new List<string>();
            var random = new Random();

            for (int i = 0; i < count; i++)
            {
                string item = GenerateToken(random, 2, 10);
                tokens.Add(item);
                Console.WriteLine(item);
            }

            Assert.That(tokens.Count == count);
        }

        [Test]
        public static void CanGenerate10TokensDifferent()
        {
            int count = 10;
            var dictionary = new Dictionary<string, int>(count);
            var random = new Random();

            for (int i = 0; i < count; i++)
            {
                string item = GenerateToken(random, 2, 10);
                dictionary.Add(item, i);
            }

            Assert.That(dictionary.Count == count);
        }

        private static string GenerateToken(Random random, int minLength, int maxLength)
        {
            string token = string.Empty;

            var chars = Enumerable.Range(0, ValidCharacters.Length - 1)
                .OrderBy(o => random.Next())
                .Select(i => ValidCharacters[i]);

            token = string.Join("", chars);

            int length = random.Next(minLength, maxLength);
            int start = random.Next(0, token.Length - length - 1);

            token = token.Substring(start, length);

            return token;
        }
    }
}
