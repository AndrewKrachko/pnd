using NUnit.Framework;
using PalindromeApp;

namespace PalindromeTests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [TestCase("aa")]
        [TestCase("a44a")]
        [TestCase("а роза упала на лапу Азора")]
        public void PalindromeValidString(string value)
        {
            var isPalindrome = PalindromeValidator.IsStringPalindrome(value);

            Assert.IsTrue(isPalindrome);
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase("a")]
        [TestCase("1")]
        [TestCase("  ")]
        [TestCase("v  vb")]
        public void PalindromeInvalidString(string value)
        {
            var isPalindrome = PalindromeValidator.IsStringPalindrome(value);

            Assert.IsFalse(isPalindrome);
        }
    }
}