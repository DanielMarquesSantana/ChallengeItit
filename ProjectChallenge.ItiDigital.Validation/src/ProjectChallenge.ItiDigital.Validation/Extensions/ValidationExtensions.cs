using System.Text.RegularExpressions;

namespace ProjectChallenge.ItiDigital.Validation
{
    public static class ValidationExtensions
    {
        public static bool PatternPassword(this string inputPassword)
        {
            var hasNumber = new Regex(@"[0-9]+");
            var hasUpperChar = new Regex(@"[A-Z]+");
            var hasLowerChar = new Regex(@"[a-z]+");
            var hasMinimum9Chars = new Regex(@".{9,}");
            var hasSpecialCharacters = new Regex(@"((?=[\W])*[^!@#$%^&*()\-\+\w])");
            var hasIqualsCharacters = new Regex(@"(.)(.*\1)");

            var isValidated = hasNumber.IsMatch(inputPassword)
                           && hasUpperChar.IsMatch(inputPassword)
                           && hasLowerChar.IsMatch(inputPassword)
                           && hasMinimum9Chars.IsMatch(inputPassword)
                           && !hasSpecialCharacters.IsMatch(inputPassword)
                           && !hasIqualsCharacters.IsMatch(inputPassword);

            return isValidated;
        }

    }
}
