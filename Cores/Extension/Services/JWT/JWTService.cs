using System;
using System.Linq;
using System.Text;
using Cores.Jwt;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Primitives;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Http;

namespace Cores.Extension.Services.JWT
{
    internal static class JWTService
    {
        #region Authentication Jwt Bearer
        /// <summary>
        /// AuthenticationJwt Bearer
        /// Security and Using JWT
        ///JSON Web Tokens(JWT) are becoming more popular by the day in web development.It is very easy to implement JWT Authentication due to the.NET Core’s built-in support.JWT is an open standard and it allows us to transmit the data between a client and a server as a JSON object in a secure way.
        ///</summary>
        /// <param name="services"></param>
        /// <param name="config"></param>
        public static void AuthenticationJwtBearer(this IServiceCollection services, IConfiguration config)
        {
            #region Authentication
            var token = config.GetSection("tokenManagement").Get<TokenManagement>();
            services.AddSingleton(token);
            services.AddAuthentication(x =>
            {
                x.DefaultScheme = "Cookies";
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddCookie("Cookies")
                .AddJwtBearer(x =>
                {
                    x.RequireHttpsMetadata = true;
                    x.SaveToken = true;
                    x.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidIssuer = token.Issuer,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(token.Secret)),
                        ValidAudience = token.Audience,
                        ValidateAudience = false,
                        ValidateLifetime = false//here we are saying that we don't care about the token's expiration date

                    };
                });
            #endregion       
        }
        /// <summary>
        /// Configures the authentication jwt bearer.
        /// </summary>
        /// <param name="app">The app.</param>
        /// <param name="config">The config.</param>
        public static void ConfigureAuthenticationJwtBearer(this IApplicationBuilder app, IConfiguration config)
        {
            app.UseMiddleware<JwtMiddleware>();
            app.UseCookiePolicy();
            app.UseSession();
            app.Use(async (context, next) =>
            {
                var jwtToken = context.Session.GetString("JwtToken");
                if (!string.IsNullOrEmpty(jwtToken))
                {
                    context.Request.Headers.Clear();
                    context.Request.Headers.Add("Authorization", "Bearer " + jwtToken);
                }
                else
                {
                    var token1 = context.Request.Headers.TryGetValue("Authorization", out StringValues token);
                    var token3 = context.Request.Query.TryGetValue("access_token", out StringValues token2);
                    if (token1)
                    {
                        // pull token from header or querystring; websockets don't support headers so fallback to query is required
                        //var tokenValue = token.FirstOrDefault() 
                        //?? token2.FirstOrDefault();
                        var tokenValue = token.FirstOrDefault()
                        ?? "";
                        const string prefix = "Bearer ";
                        // remove prefix of header value
                        if (tokenValue?.StartsWith(prefix) ?? false)
                        {
                            //accessToken = tokenValue.Substring(prefix.Length);//
                            jwtToken = tokenValue[prefix.Length..];
                        }
                        else
                        {
                            jwtToken = tokenValue;
                        }
                        if (jwtToken != "")
                        {
                            context.Request.Headers.Clear();
                            context.Request.Headers.Add("Authorization", "Bearer " + jwtToken);
                            context.Session.SetString("JwtToken", jwtToken);
                        }
                    }
                }
                await next.Invoke();
            });
            app.UseAuthentication();
            app.UseAuthorization();
        }
        #endregion
    }
}
