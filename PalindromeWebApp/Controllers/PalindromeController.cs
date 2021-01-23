using Microsoft.AspNetCore.Mvc;
using PalindromeWebApp.Models;
using PolyndromeApp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PalindromeWebApp.Controllers
{
    public class PalindromeController : Controller
    {
        public IActionResult Index()
        {
            return RedirectPermanent("Palindrome/Palindrome");
        }

        public IActionResult Palindrome(string palindrome = "")
        {
            var isPalindrome = PalindromeValidator.IsStringPalindrome(palindrome);
            var palindromeValidator = new PalindromeState() { PalindromeSting= palindrome, IsValidPalindrome = isPalindrome};

            return View(palindromeValidator);
        }
    }
}
