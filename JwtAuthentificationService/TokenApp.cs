using Items;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace JwtAuthenticationService
{
    public class JwtAuthenticator : IAuthenticationService
    {
        private IRepository _repository;

        public const string ISSUER = "PalindromApp";
        const string KEY = "qwerty1234andDifferentOther$igns";
        public const int LIFETIME = 1;

        public JwtAuthenticator(IRepository repository)
        {
            _repository = repository;
        }

        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(KEY));
        }

        public bool AuthoriseUser(string userName, string password, out string token)
        {

            if (_repository.GetUserByName(userName, out var dbUser) &&
                dbUser.Name == userName &&
                dbUser.Password == password)
            {
                var identity = GetClaim(userName);

                var now = DateTime.UtcNow;
                var jwt = new JwtSecurityToken(
                        issuer: ISSUER,
                        audience: null,
                        notBefore: now,
                        claims: identity.Claims,
                        expires: now.Add(TimeSpan.FromMinutes(LIFETIME)),
                        signingCredentials: new SigningCredentials(GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
                token = new JwtSecurityTokenHandler().WriteToken(jwt);

                return true;
            }

            token = string.Empty;

            return false;
        }

        private ClaimsIdentity GetClaim(string username)
        {
            var claims = new List<Claim>
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, username),
                    new Claim(ClaimsIdentity.DefaultRoleClaimType, "User")
                };
            ClaimsIdentity claim =
            new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                ClaimsIdentity.DefaultRoleClaimType);
            return claim;
        }

        public bool AuthenticateUser(string userName, string token)
        {
            try
            {
                var tokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = GetSymmetricSecurityKey(),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                };

                var decodedToken = new JwtSecurityTokenHandler().
                    ValidateToken(token, tokenValidationParameters, out var validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;

                return true;
            }
            catch (Exception)
            {
            }

            return false;
        }
    }

}
