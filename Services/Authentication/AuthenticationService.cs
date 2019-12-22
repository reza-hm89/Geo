using DAL;
using DataContract.Auth;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using ServiceContract.Authentication;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;


namespace Service.Users
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly JwtSettings jwtSettings;
        private readonly ApplicationDbContext context;

        public AuthenticationService(IOptions<JwtSettings> _jwtSettings, ApplicationDbContext _context)
        {
            jwtSettings = _jwtSettings.Value;
            context = _context;
        }


        public string GenerateToken(string userId)
        {
            try
            {              
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(jwtSettings.Secret);

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                    new Claim(ClaimTypes.Name, userId)
                    }),
                    
                    Expires = DateTime.UtcNow.AddDays(7),
                    //NotBefore = DateTime.UtcNow.AddDays(7),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };

                var token = tokenHandler.CreateToken(tokenDescriptor);

                return tokenHandler.WriteToken(token);

            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
