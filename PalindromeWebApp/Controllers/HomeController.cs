using Items;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using PalindromeWebApp.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace PalindromeWebApp.Controllers
{
    public class HomeController : Controller
    {
        private IRepository _repository;

        public HomeController(IRepository repository)
        {
            _repository = repository;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Authorise(string userName, string password)
        {
            if (_repository.GetUserByName(userName, out var dbUser))
            {
                if (dbUser.Name == userName && dbUser.Password == password)
                {
                    var identity = GetClaim(userName, password);

                    var now = DateTime.UtcNow;
                    var jwt = new JwtSecurityToken(
                            issuer: AuthOptions.ISSUER,
                            audience: null,
                            notBefore: now,
                            claims: identity.Claims,
                            expires: now.Add(TimeSpan.FromMinutes(AuthOptions.LIFETIME)),
                            signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
                    var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

                    var cookieOptions = new CookieOptions();
                    cookieOptions.Expires = DateTimeOffset.UtcNow.AddMinutes(5);
                    Response.Cookies.Append("jwt", encodedJwt, cookieOptions);
                    Response.Cookies.Append("userName", userName, cookieOptions);

                    return RedirectPermanent("../Palindrome/Palindrome");
                }
            }

            return RedirectPermanent("Login");
        }

        private ClaimsIdentity GetClaim(string username, string password)
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

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
