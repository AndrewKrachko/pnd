using System;

namespace PolyndromeApp
{
    public class PalindromeValidator
    {
        public static bool IsStringPalindrome(string palindrome)
        {
            var result = false;

            if (palindrome != null)
            {
                var charArray = palindrome.ToLower().Replace(" ", "").ToCharArray();

                for (int i = 0; i < charArray.Length - 1; i++)
                {
                    if (charArray[i] == charArray[charArray.Length - i - 1])
                    {
                        result = true;
                    }
                    else
                    {
                        result = false;
                        break;
                    }
                }
            }

            return result;
        }
    }
}
