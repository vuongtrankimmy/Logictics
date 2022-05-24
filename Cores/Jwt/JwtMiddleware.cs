 using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Cores.Jwt
{
    /// <summary>
    /// The jwt middleware.
    /// </summary>
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;
        readonly IOptions<TokenManagement> _tokenManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="JwtMiddleware"/> class.
        /// </summary>
        /// <param name="next">The next.</param>
        /// <param name="tokenManagement">The token management.</param>
        public JwtMiddleware(RequestDelegate next, IOptions<TokenManagement> tokenManagement)
        {
            _tokenManager = tokenManagement;
            _next = next;
        }

        /// <summary>
        /// Invokes the.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns>A Task.</returns>
        public async Task Invoke(HttpContext context)
        {
            //Get the upload token, which can be customized and extended
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last()
                        ?? context.Request.Headers["X-Token"].FirstOrDefault()
                        ?? context.Request.Query["Token"].FirstOrDefault()
                        ?? context.Request.Cookies["Token"];

            if (token != null)
                AttachUserToContext(context, token);
            
            await _next(context);
        }

        /// <summary>
        /// Attaches the user to context.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="token">The token.</param>
        private void AttachUserToContext(HttpContext context, string token)
        {
            try
            {
                var tokenValue = _tokenManager?.Value;
                if (tokenValue != null)
                {
                    var tokenHandler = new JwtSecurityTokenHandler();
                    var key = Encoding.UTF8.GetBytes(tokenValue.Secret);
                    tokenHandler.ValidateToken(token, new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        // set clockskew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
                        ClockSkew = TimeSpan.Zero
                    }, out SecurityToken validatedToken);

                    var jwtToken = (JwtSecurityToken)validatedToken;
                    var user = jwtToken.Claims;

                    //Write authentication information to facilitate the use of business classes
                    var claimsIdentity = new ClaimsIdentity(user);
                    Thread.CurrentPrincipal = new ClaimsPrincipal(claimsIdentity);

                    // attach user to context on successful jwt validation
                    context.Items["User"] = user;                    
                }
            }
            catch
            {
                // do nothing if jwt validation fails
                // user is not attached to context so request won't have access to secure routes
                throw;
            }
        }
    }
}
