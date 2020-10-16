using System;

namespace Microsoft.AspNetCore.Authentication.OpenIdConnect
{
    public static class OpenIdConnectOptionsExtension
    {
        public static void UsePkce(this OpenIdConnectOptions openIdConnectOptions)
        {
            openIdConnectOptions.UsePkce = true;
            openIdConnectOptions.Events.OnRedirectToIdentityProvider.OnRedirectToIdentityProviderEvent();
            openIdConnectOptions.Events.OnAuthorizationCodeReceived.OnAuthorizationCodeReceivedEvent();
        }
    }
}
