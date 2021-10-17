using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Identity.Web;

namespace B2cDemo.Security
{
    public static class SecurityExtensions
    {
        public static IServiceCollection AddSecurity(this IServiceCollection services)
        {
            services
                .AddAuthentication(OpenIdConnectDefaults.AuthenticationScheme)
                .AddMicrosoftIdentityWebApp(b2c =>
                {
                    b2c.Domain = "uvetab2cdemo.onmicrosoft.com";
                    b2c.Instance = "https://uvetab2cdemo.onmicrosoft.com";
                    b2c.ClientId = "2653f39b-737c-4f75-bcb3-d503545c5f3d";
                    b2c.TenantId = "bd839d5e-5127-412f-b4d6-429b32677774";
                    b2c.SignedOutCallbackPath = "/signout-callback-oidc";
                    b2c.SignUpSignInPolicyId = "B2C_1_Login";
                });
            // .AddMicrosoftIdentityWebApp(Configuration.GetSection("AzureB2C"));
            services.AddAuthorization(authorization =>
            {
                authorization.AddPolicy(Policies.OrdersRead, policy => policy.RequireAssertion(context =>
                {
                    var user = context.User;
                    var claims = user.Claims;
                    return claims?.Any(claim => claim.Type == "extensions_orders_read" || claim.Type == "extensions_orders_full");
                }));
            });
            services.AddAuthorization(authorization =>
            {
                authorization.AddPolicy(Policies.OrdersFull, policy => policy.RequireClaim("extensions_orders_full"));
            });

            return services;
        }
    }
}