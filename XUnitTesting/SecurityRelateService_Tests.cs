using BackEnd.Abstraction;
using BackEnd.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace XUnitTesting
{
    public class SecurityRelateService_Tests
    {
        [Fact]
        public void GeneratePassword_Length8()
        {
            // Arrange
            var securityRelate = new SecurityRelateHelper();

            // Action
            var password = securityRelate.GeneratePassword();

            // Assert
            Assert.Equal(8, password.Length);
        }
        [Fact]
        public void GeneratePassword_CharDiferent4orMore()
        {
            // Arrange
            var securityRelate = new SecurityRelateHelper();

            // Action
            var password = securityRelate.GeneratePassword();

            // Assert
            Assert.True(password.Distinct().Count() >= 4);
        }
        [Fact]
        public void GeneratePassword_AnyCapitalAlphabet()
        {
            // Arrange
            var securityRelate = new SecurityRelateHelper();

            // Action
            var password = securityRelate.GeneratePassword();

            // Assert
            Assert.True(password.IndexOfAny("ABCDEFGHJKLMNOPQRSTUVWXYZ".ToCharArray()) != -1);
        }
        [Fact]
        public void GeneratePassword_AnyNormalAlphabet()
        {
            // Arrange
            var securityRelate = new SecurityRelateHelper();

            // Action
            var password = securityRelate.GeneratePassword();

            // Assert
            Assert.True(password.IndexOfAny("abcdefghijkmnopqrstuvwxyz".ToCharArray()) != -1);
        }
        [Fact]
        public void GeneratePassword_AnyNumber()
        {
            // Arrange
            var securityRelate = new SecurityRelateHelper();

            // Action
            var password = securityRelate.GeneratePassword();

            // Assert
            Assert.True(password.IndexOfAny("0123456789".ToCharArray()) != -1);
        }
        [Fact]
        public void GeneratePassword_AnyUnicChar()
        {
            // Arrange
            var securityRelate = new SecurityRelateHelper();

            // Action
            var password = securityRelate.GeneratePassword();

            // Assert
            Assert.True(password.IndexOfAny("!@$?_-".ToCharArray()) != -1);
        }

        [Theory]
        [InlineData("Test")]
        [InlineData("S3cretKey")]
        [InlineData("@7q$o6oK")]
        public void EncryptDecrypt_CanLockAndUnlock(string plainText)
        {
            // Arrange
            var securityRelate = new SecurityRelateHelper();

            // Action
            string chiperText = securityRelate.Encrypt(plainText);
            string decryptText = securityRelate.Decrypt(chiperText);

            // Assert
            Assert.Equal(plainText, decryptText);
        }
    }
}
