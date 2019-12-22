using BackEnd.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;
using Xunit.Abstractions;

namespace XUnitTesting
{
    public class SecurityRelateService_Tests
    {
        private readonly ITestOutputHelper _output;

        public SecurityRelateService_Tests(ITestOutputHelper output)
        {
            this._output = output;
        }

        [Fact]
        public void GeneratePassword_NotLoopng()
        {
            // Arrange
            var securityRelate = new SecurityRelateHelper();

            // Action
            var password = securityRelate.GeneratePassword(requireUppercase: false, requireDigit:false, requiredLength:0, requiredUniqueChars:0, requireLowercase:false, requireNonAlphanumeric:false);

            // Assert
            Assert.Equal(0, password.Length); // Panjang 1 karakter
            _output.WriteLine($"Output: {password}");
        }

        [Fact]
        public void GeneratePassword_Looping()
        {
            // Arrange
            var securityRelate = new SecurityRelateHelper();

            // Action
            var password = securityRelate.GeneratePassword(requireUppercase: false, requireDigit: false, requireLowercase: false, requireNonAlphanumeric:false);

            // Assert
            Assert.Equal(8, password.Length); // Panjang 8 karakter
            Assert.True(password.Distinct().Count() >= 4); // Karakter beda 4 atau lebih
            _output.WriteLine($"Output: {password}");
        }

        [Fact]
        public void GeneratePassword_UppercaseRequired()
        {
            // Arrange
            var securityRelate = new SecurityRelateHelper();

            // Action
            var password = securityRelate.GeneratePassword(requireDigit: false, requireLowercase: false, requireNonAlphanumeric:false);

            // Assert
            Assert.True(password.IndexOfAny("ABCDEFGHJKLMNOPQRSTUVWXYZ".ToCharArray()) != -1); // Ada huruf kapital
            _output.WriteLine($"Output: {password}");
        }

        [Fact]
        public void GeneratePassword_LowerCaseRequired()
        {
            // Arrange
            var securityRelate = new SecurityRelateHelper();

            // Action
            var password = securityRelate.GeneratePassword(requireUppercase: false, requireDigit: false, requireNonAlphanumeric: false);

            // Assert
            Assert.True(password.IndexOfAny("abcdefghijkmnopqrstuvwxyz".ToCharArray()) != -1); // Ada huruf kecil
            _output.WriteLine($"Output: {password}");
        }

        [Fact]
        public void GeneratePassword_DigitRequired()
        {
            // Arrange
            var securityRelate = new SecurityRelateHelper();

            // Action
            var password = securityRelate.GeneratePassword(requireUppercase: false, requireLowercase: false, requireNonAlphanumeric: false);

            // Assert
            Assert.True(password.IndexOfAny("0123456789".ToCharArray()) != -1); // Ada angka
            _output.WriteLine($"Output: {password}");
        }

        [Fact]
        public void GeneratePassword_NonAlphaNumericRequired()
        {
            // Arrange
            var securityRelate = new SecurityRelateHelper();

            // Action
            var password = securityRelate.GeneratePassword(requireUppercase: false, requireDigit: false, requireLowercase: false);

            // Assert
            Assert.True(password.IndexOfAny("!@$?_-".ToCharArray()) != -1); // Ada karakter unik
            _output.WriteLine($"Output: {password}");
        }

        [Fact]
        public void GeneratePassword_Default()
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
            _output.WriteLine($"Output: {password}");
        }

        [Theory]
        [InlineData("pl41nT3xt")]
        [InlineData("@qAdje4wd")]
        [InlineData("Qwertsa12")]
        public void Encrypt_ChangePlainText(string plainText)
        {
            // Arrange
            var securityRelate = new SecurityRelateHelper();

            // Action
            string chiperText = securityRelate.Encrypt(plainText);

            // Assert
            Assert.NotEqual(plainText, chiperText);
            string msg = $"Kasus Uji: Masukkan paramter plainText dengan {plainText}\n" +
                $"Hasil yang diinginkan: BUKAN {plainText}\n" +
                $"Hasil aktual: {chiperText}";
            _output.WriteLine(msg);
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
            string msg = $"Kasus Uji: Masukkan paramter plainText dengan {plainText}, kemudian diubahmenjadi {chiperText}\n" +
                $"Hasil yang diinginkan: {plainText}\n" +
                $"Hasil aktual: {decryptText}";
            _output.WriteLine(msg);
        }
    }
}
