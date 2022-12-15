using Microsoft.Owin.Security.OAuth;
using System.Security.Claims;
using System.Threading.Tasks;
using UrlShortenerApi.Services;

namespace UrlShortenerApi.Providers.OAuth
{
    public class TokenAccessProvider : OAuthAuthorizationServerProvider
    {
        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            var user = UserService.FindUserByUsernameAndPassword(context.UserName, context.Password);
            if (user != null && user.Id > 0)
            {
                var identity = new ClaimsIdentity(context.Options.AuthenticationType);
                identity.AddClaim(new Claim(ClaimTypes.Name, context.UserName));
                identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));

                await Task.Run(() => context.Validated(identity));
            }
            else
            {
                context.SetError("Invalid Access", "The credentials doesnt match or the user is no longer active!");
                return;
            }
        }

        /// <summary>
        /// Validates client token
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            await Task.Run(() => context.Validated());
        }
    }
}