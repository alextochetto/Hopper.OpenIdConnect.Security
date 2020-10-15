using System;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;

namespace Microsoft.AspNetCore.Authentication.OpenIdConnect
{
    public static class AuthorizationCodeReceivedExtension
    {
        public static Func<AuthorizationCodeReceivedContext, Task> OnAuthorizationCodeReceivedEvent(this Func<AuthorizationCodeReceivedContext, Task> authorizationCodeReceivedContext)
        {
            return context =>
            {
                if (context.TokenEndpointRequest?.GrantType != OpenIdConnectGrantTypes.AuthorizationCode) 
                    return Task.CompletedTask;
                if (context.Properties.Items.TryGetValue("code_verifier", out string codeVerifier))
                    context.TokenEndpointRequest.Parameters.Add("code_verifier", codeVerifier);
                return Task.CompletedTask;
            };
        }
    }
}
