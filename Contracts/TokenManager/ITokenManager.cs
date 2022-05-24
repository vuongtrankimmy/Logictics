using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.TokenManager
{
    /// <summary>
    /// The token manager.
    /// </summary>
    public interface ITokenManager
    {
        /// <summary>
        /// Generates the jwt token.
        /// </summary>
        /// <param name="claims">The claims.</param>
        /// <returns>A string.</returns>
        string GenerateJwtToken(IEnumerable<Claim> claims);
        /// <summary>
        /// Generates the jwt refresh.
        /// </summary>
        /// <returns>A string.</returns>
        string GenerateJwtRefresh();
        /// <summary>
        /// Gets the principal from expired token.
        /// </summary>
        /// <param name="token">The token.</param>
        /// <returns>A ClaimsPrincipal.</returns>
        ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
    }
}
