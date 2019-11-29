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
        public void GeneratePassword_TestDefaultSetting()
        {
            // Arrange
            var securityRelate = new SecurityRelateHelper();

            // Action
            var password = securityRelate.GeneratePassword();

            // Assert
            Assert.Equal(8, password.Length);
            Assert.True(password.Distinct().Count() >= 4);
            Assert.True(password.IndexOfAny("ABCDEFGHJKLMNOPQRSTUVWXYZ".ToCharArray()) != -1);
            Assert.True(password.IndexOfAny("abcdefghijkmnopqrstuvwxyz".ToCharArray()) != -1);
            Assert.True(password.IndexOfAny("0123456789".ToCharArray()) != -1);
            Assert.True(password.IndexOfAny("!@$?_-".ToCharArray()) != -1);
        }

        [Theory]
        [InlineData("Test")]
        [InlineData("S3cretKey")]
        [InlineData("123")]
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
