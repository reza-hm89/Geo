using DataContract.Auth;
using Newtonsoft.Json;
using ServiceContract.Authentication;
using System.Security.Claims;
using System.Threading.Tasks;

namespace WebFramework.Jwt
{
    public class Tokens
    {
        public static async Task<string> GenerateJwt(ClaimsIdentity identity, IJwtFactory jwtFactory, string userName,
            JwtIssuerOptions jwtOptions, JsonSerializerSettings serializerSettings)
        {            
            return await jwtFactory.GenerateEncodedToken(userName, identity);
        }
    }
}
