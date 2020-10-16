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
            RNGCryptoServiceProvider cryptoServiceProvider = new RNGCryptoServiceProvider();
            var byteArray = new byte[32];
            cryptoServiceProvider.GetBytes(byteArray);
            string codeVerifier = Encode(byteArray);

            return context =>
            {
                if (context.ProtocolMessage.RequestType != OpenIdConnectRequestType.Authentication) 
                    return Task.CompletedTask;
                // var codeVerifier = CryptoRandom.CreateUniqueId(32);
                context.Properties.Items.Add("code_verifier", codeVerifier);
                string codeChallenge;
                using (var sha256 = SHA256.Create())
                {
                    var challengeBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(codeVerifier));
                    codeChallenge = Encode(challengeBytes);
                }
                context.ProtocolMessage.Parameters.Add("code_challenge", codeChallenge);
                context.ProtocolMessage.Parameters.Add("code_challenge_method", "S256");
                return Task.CompletedTask;
            };
        }

        /// <summary>
        /// Encodes the byte array.
        /// </summary>
        /// <param name="byteArray">argument</param>
        /// <returns></returns>
        private static string Encode(byte[] byteArray)
        {
            var base64 = Convert.ToBase64String(byteArray);
            return base64.Split('=')[0].Replace('+', '-').Replace('/', '_');
        }
    }
}