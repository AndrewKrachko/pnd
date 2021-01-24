using Items;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PalindromeWebApp.Models;
using System;
using System.Diagnostics;

namespace PalindromeWebApp.Controllers
{
    public class HomeController : Controller
    {
        private IAuthenticationService _authenticationService;

        public HomeController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
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
            if (_authenticationService.AuthoriseUser(userName, password, out var token))
            {
                var cookieOptions = new CookieOptions();
                cookieOptions.Expires = DateTimeOffset.UtcNow.AddMinutes(5);
                Response.Cookies.Append("token", token, cookieOptions);
                Response.Cookies.Append("userName", userName, cookieOptions);

                return RedirectToActionPermanent("Palindrome", "Palindrome");
            }

            return RedirectPermanent("Login");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
