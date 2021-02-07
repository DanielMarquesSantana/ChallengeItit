using ProjectChallenge.ItiDigital.Validation.Services;
using Xunit;

namespace ProjectChallenge.ItiDigital.Validation.Tests
{
    public class ValidationServiceTest
    {

        [Theory]
        [InlineData("")]
        [InlineData("aa")]
        [InlineData("ab")]
        [InlineData("AAAbbbCc")]
        [InlineData("AbTp9!foo")]
        [InlineData("AbTp9!foA")]
        [InlineData("AbTp9 fok")]
        [InlineData("AbTp9!fok;")]
        public void PasswordValidation_Minimal_Require_Not_Success(string password)
        {
            //Arrange
            var validationService = new ValidationService();

            //Act
            var result = validationService.ValidationPassaword(password);

            //Assert
            Assert.False(result.IsValid, $"The password {password} should not be valid!");
        }

        [Theory]
        [InlineData("AbTp9!fok")]
        [InlineData("AbTp9!fok@")]
        [InlineData("AbTp9!fok%")]
        public void PasswordValidation_Minimal_Require_Success(string password)
        {
            //Arrange
            var validationService = new ValidationService();

            //Act
            var result = validationService.ValidationPassaword(password);

            //Assert
            Assert.True(result.IsValid, $"The password {password} is valid!");
        }

        [Theory]
        [InlineData("AbTp9!fok",true)]
        [InlineData("AbTp9!fok@", true)]
        [InlineData("AbTp9!fok%", true)]
        [InlineData("",false)]
        [InlineData("aa", false)]
        [InlineData("ab", false)]
        [InlineData("AAAbbbCc", false)]
        [InlineData("AbTp9!foo", false)]
        [InlineData("AbTp9!foA", false)]
        [InlineData("AbTp9 fok", false)]
        [InlineData("AbTp9!fok;", false)]

        public void PasswordValidation_Extension(string password, bool expectedResult)
        {
            //Act
            var result = password.PatternPassword();

            //Assert
            Assert.Equal(expectedResult, result);
        }
    }
}
