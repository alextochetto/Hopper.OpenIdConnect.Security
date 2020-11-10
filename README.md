![OpenIdConnect Security](https://github.com/alextochetto/Hopper.OpenIdConnect.Security/workflows/OpenIdConnect%20Security/badge.svg)

<p align="center">
  <img src="https://github.com/alextochetto/Hopper.OpenIdConnect.Security/blob/main/src/Extensions/Hopper.Authentication.OpenIdConnect.Pkce/Images/icon.png?raw=true" width="350px" alt="SharpSenses" />
</p>
<p>

# Hopper.Authentication.OpenIdConnect
This package it's an easy way to enable PKCE (Proof Key for Code Exchange) in ASP.NET Core application.
PKCE it's an extension to OAuth 2.0 for public clients (e.g., native and single-page applications) has been designed to prevent interception of authorization **code**.
Below there are some steps to understand how it works:

1. User Login within the application.

2. The package create a cryptography random key for `code_verifier` and generate the `code_challenge`.

3. The configuration of your application will redirect the user to authority server login page with `code_challenge`.

4. After login the authority server redirect user back to the application with an authorization code and `code_verifier` created in step 2.

5. The authority server verifies the `code_challenge` and `code_verifier`.

6. The authority server allow the Id Token and Access Token response.

## Hopper.Authentication.OpenIdConnect.Pkce
> Install-Package Hopper.Authentication.OpenIdConnect.Pkce

```
public void ConfigureServices(IServiceCollection services)
{
    services
        .AddAuthentication(options =>
        {
            // your code here
        })
        .AddCookie(options =>
        {
            // your code here
        })
        .AddOpenIdConnect(OpenIdConnectDefaults.AuthenticationScheme, o =>
        {
            // Enable PKCE(authorization code flow only)
            o.UsePkce();
        });
    services.AddControllersWithViews();
}
````
