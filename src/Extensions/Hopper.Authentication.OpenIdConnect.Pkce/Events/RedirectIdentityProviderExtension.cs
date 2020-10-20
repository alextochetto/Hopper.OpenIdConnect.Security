using System;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;

namespace Microsoft.AspNetCore.Authentication.OpenIdConnect
{
    public static class RedirectIdentityProviderExtension
    {
        public static Func<RedirectContext, Task> OnRedirectToIdentityProviderEvent(this Func<RedirectContext, Task> redirectContext)
        {
            string codeVerifier = CryptoServiceProvider.Create();

            return context =>
            {
                if (context.ProtocolMessage.RequestType != OpenIdConnectRequestType.Authentication) 
                    return Task.CompletedTask;
                context.Properties.Items.Add("code_verifier", codeVerifier);
                string codeChallenge;
                using (var sha256 = SHA256.Create())
                {
                    var challengeBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(codeVerifier));
                    codeChallenge = CryptoServiceProvider.Encode(challengeBytes);
                }
                context.ProtocolMessage.Parameters.Add("code_challenge", codeChallenge);
                context.ProtocolMessage.Parameters.Add("code_challenge_method", "S256");
                return Task.CompletedTask;
            };
        }
    }
}