using System;

namespace Microsoft.AspNetCore.Authentication.OpenIdConnect
{
    public static class OpenIdConnectOptionsExtension
    {
        public static void AddPkce(this OpenIdConnectOptions openIdConnectOptions)
        {
            openIdConnectOptions.UsePkce = true;
            openIdConnectOptions.Events.OnAuthorizationCodeReceived.OnAuthorizationCodeReceivedEvent();
        }
    }
}
