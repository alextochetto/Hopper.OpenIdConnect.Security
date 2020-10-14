using System;
using System.Threading.Tasks;
// using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;

//namespace Hopper.OpenIdConnect.Security.Pkce
namespace Microsoft.AspNetCore.Authentication.OpenIdConnect
{
    public static class AuthorizationCodeReceived
    {
        public static Func<AuthorizationCodeReceivedContext, Task> OnAuthorizationCodeReceivedEvent()
        {
            return context =>
            {
                // only when authorization code is being swapped for tokens
                if (context.TokenEndpointRequest?.GrantType != OpenIdConnectGrantTypes.AuthorizationCode) 
                    return Task.CompletedTask;
                // get stored code_verifier
                if (context.Properties.Items.TryGetValue("code_verifier", out string codeVerifier))
                {
                    // add code_verifier to token request
                    context.TokenEndpointRequest.Parameters.Add("code_verifier", codeVerifier);
                }
                return Task.CompletedTask;
            };
        }
    }
}
