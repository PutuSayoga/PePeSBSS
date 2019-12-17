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
        public void GeneratePassword_CorrectDefaultOutput()
        {
            // Arrange
            var securityRelate = new SecurityRelateHelper();

            // Action
            var password = securityRelate.GeneratePassword();

            // Assert
            Assert.Equal(8, password.Length); // Panjang 8 karakter
            Assert.True(password.Distinct().Count() >= 4); // Karakter beda 4 atau lebih
            Assert.True(password.IndexOfAny("ABCDEFGHJKLMNOPQRSTUVWXYZ".ToCharArray()) != -1); // Ada huruf kapital
            Assert.True(password.IndexOfAny("abcdefghijkmnopqrstuvwxyz".ToCharArray()) != -1); // Ada huruf kecil
            Assert.True(password.IndexOfAny("0123456789".ToCharArray()) != -1); // Ada angka
            Assert.True(password.IndexOfAny("!@$?_-".ToCharArray()) != -1); // Ada karakter unik
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
