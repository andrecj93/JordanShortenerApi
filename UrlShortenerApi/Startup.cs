using Microsoft.Owin;
using Microsoft.Owin.Cors;
using Microsoft.Owin.Security.OAuth;
using Owin;
using System;
using UrlShortenerApi.Providers.OAuth;
using UrlShortenerApi.Services;
using UrlShortenerApi.Utilities;

[assembly: OwinStartup(typeof(UrlShortenerApi.Startup))]
namespace UrlShortenerApi
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            Options.MaxLength = Settings.TokenGenerator_MaxLength;
            Options.MinLength = Settings.TokenGenerator_MinLength;
            Options.ExpireDays = Settings.TokenGenerator_ExpireDays;

            // Activate Cors
            app.UseCors(CorsOptions.AllowAll); //TODO CHECKS THIS

            ActivateTokenGenerator(app);

            UserService.CreateUserIfNotExists("admin", @"YvuQ7)\ULBgn927x");
        }

        /// <summary>
        /// Adds middleware for Authorization Server using Bearer Authentication
        /// </summary>
        /// <param name="app"></param>
        private void ActivateTokenGenerator(IAppBuilder app)
        {
            var authOptions = new OAuthAuthorizationServerOptions()
            {
                AllowInsecureHttp = true, // For requests that doesnt have HTTPS
                TokenEndpointPath = new PathString("/api/token"), // The endpoint for token
                AccessTokenExpireTimeSpan = TimeSpan.FromMinutes(Settings.BearerAuthTokenExpireInMinutes), // Expects to aquire new token after this time
                Provider = new TokenAccessProvider() //Middleware Authorization Provider
            };

            // Adds resources for OAuth2 to the Owin App
            app.UseOAuthAuthorizationServer(authOptions);

            // Generates Bearer Authentication
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());
        }
    }
}