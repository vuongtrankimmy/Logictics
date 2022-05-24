using Entities.Extensions.Tokens;
using Newtonsoft.Json;
using System.Linq;
using System.Security.Claims;
using System.Threading;

namespace Entities.Extensions.NullDySession
{
    /// 
    ///Current session object
    /// 
    /// <summary>
    /// The null dy session.
    /// </summary>
    public class NullDySession
    {
        /// 
        ///Get dysession instance
        /// 
        /// <summary>
        /// Gets the instance.
        /// </summary>
        public static NullDySession Instance { get; } = new NullDySession();
        /// 
        ///Get current user information
        /// 
        /// <summary>
        /// Gets the token user.
        /// </summary>
        public static TokenView TokenUser
        {
            get
            {
                var claimsPrincipal = Thread.CurrentPrincipal as ClaimsPrincipal;

                var claimsIdentity = claimsPrincipal?.Identity as ClaimsIdentity;

                var device_id = claimsIdentity?.Claims.FirstOrDefault(c => c.Type == "_id");
                if (device_id == null || string.IsNullOrEmpty(device_id.Value))
                {
                    return null;
                }
                TokenView tokenView =null;
                var userClaim = claimsIdentity?.Claims.ToList();
                if(userClaim != null && userClaim.Any())
                {
                    var claim =JsonConvert.SerializeObject(userClaim.ToDictionary(x => x.Type, y=>y.Value));
                    tokenView = JsonConvert.DeserializeObject<TokenView>(claim);
                }
                return tokenView;
            }
        }

        /// <summary>
        /// Prevents a default instance of the <see cref="NullDySession"/> class from being created.
        /// </summary>
        private NullDySession()
        {
        }
    }
}
