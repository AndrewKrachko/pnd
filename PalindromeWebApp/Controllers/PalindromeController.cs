using Items;
using Microsoft.AspNetCore.Mvc;
using PalindromeApp;
using PalindromeWebApp.Models;

namespace PalindromeWebApp.Controllers
{
    public class PalindromeController : Controller
    {
        private IAuthenticationService _authenticationService;

        public PalindromeController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        public IActionResult Index()
        {
            return RedirectPermanent("Palindrome/Palindrome");
        }

        public IActionResult Palindrome(string palindrome = "")
        {
            var responseCookies = Request.Cookies;
            if (!_authenticationService.AuthenticateUser(responseCookies["userName"], responseCookies["token"]))
            {
                return RedirectToActionPermanent("Login", "Home");
            }

            var isPalindrome = PalindromeValidator.IsStringPalindrome(palindrome);
            var palindromeValidator = new PalindromeState() { PalindromeSting = palindrome, IsValidPalindrome = isPalindrome };

            return View(palindromeValidator);
        }
    }
}
