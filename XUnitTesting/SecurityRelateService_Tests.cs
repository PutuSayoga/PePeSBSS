using BackEnd.Services;
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

            // Action
            var password = SecurityRelateService.GeneratePassword();

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
        [InlineData("Sm4Bs$")]
        public void EncryptDecrypt_CanLockAndUnlock(string plainText)
        {
            // Arrange


            // Action
            string chiperText = SecurityRelateService.Encrypt(plainText);
            string decryptText = SecurityRelateService.Decrypt(chiperText);

            // Assert
            Assert.Equal(plainText, decryptText);
        }
    }
}
