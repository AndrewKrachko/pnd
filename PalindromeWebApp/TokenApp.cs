using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace PalindromeWebApp
{
    public class AuthOptions
    {
        public const string ISSUER = "PalindromApp";
        public const string AUDIENCE = "Client";
        const string KEY = "qwerty1234andDifferentOther$igns";
        public const int LIFETIME = 1;
        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(KEY));
        }

        private static bool ValidateToken(string token, out string username)
        {
            username = null;

            var simplePrinciple = new JwtSecurityTokenHandler().ReadToken(token);
            

            //var identity = simplePrinciple.Identity as ClaimsIdentity;

            //if (identity == null)
            //    return false;

            //if (!identity.IsAuthenticated)
            //    return false;

            //var usernameClaim = identity.FindFirst(ClaimTypes.Name);
            //username = usernameClaim?.Value;

            //if (string.IsNullOrEmpty(username))
            //    return false;

            // More validate to check whether username exists in system

            return true;
        }

        protected Task<IPrincipal> AuthenticateJwtToken(string token)
        {
            string username;

            if (ValidateToken(token, out username))
            {
                var claims = new List<Claim>{new Claim(ClaimTypes.Name, username)};

                var identity = new ClaimsIdentity(claims, "Jwt");
                IPrincipal user = new ClaimsPrincipal(identity);

                return Task.FromResult(user);
            }

            return Task.FromResult<IPrincipal>(null);
        }
    }
}
