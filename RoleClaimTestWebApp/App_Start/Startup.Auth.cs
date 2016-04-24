using System.IdentityModel.Tokens;
using System.Threading.Tasks;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Owin;
using Microsoft.Owin.Security.OpenIdConnect;
using System.Web.Helpers;
using System.Security.Claims;

namespace RoleClaimTestWebApp
{
    public partial class Startup
    {
        public void ConfigureAuth(IAppBuilder app)
        {
            app.SetDefaultSignInAsAuthenticationType(CookieAuthenticationDefaults.AuthenticationType);
            app.UseCookieAuthentication(new CookieAuthenticationOptions());
            AntiForgeryConfig.UniqueClaimTypeIdentifier = ClaimTypes.GivenName;
            var app1 = new OpenIdConnectAuthenticationOptions
            {
                ClientId = "<<your client ID>>", // client Id that you get for your app in AAD
                Authority = "https://login.microsoftonline.com/<<your tenant>>.onmicrosoft.com", // replace this with your tenant
                PostLogoutRedirectUri = "https://localhost:44300/", // this is the URL where we will be redirected after successful login
                Notifications = new OpenIdConnectAuthenticationNotifications
                {
                    RedirectToIdentityProvider = notification =>
                    {
                        notification.ProtocolMessage.Prompt = "select_account"; // this is just to show user selection prompt always. Helps during dev.
                        return Task.FromResult(0);
                    }
                },
                RedirectUri = "https://localhost:44300", // where to receive the token ,
                TokenValidationParameters = new TokenValidationParameters
                {
                    RoleClaimType = "roles",
                }
            };

            app.UseOpenIdConnectAuthentication(app1);
        }
    }
}