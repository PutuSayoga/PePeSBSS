using System;
using System.Collections.Generic;
using System.Text;
using BackEnd.Abstraction;
using BackEnd.Helper;
using BackEnd.Services;
using Xunit;
using Xunit.Abstractions;

namespace XUnitTesting
{
    public class PendaftaranService_Tests
    {
        private readonly ITestOutputHelper _output;
        private readonly IDbConnectionHelper _connectionHelper; 
        public PendaftaranService_Tests(ITestOutputHelper output)
        {
            this._output = output;
            _connectionHelper = new DbConnectionHelper("Server=DESKTOP-3QK0RTT\\LOCAL_INSTANCE; Database=SkripsiDatabase; Trusted_Connection=true");
        }

        [Theory]
        [InlineData("0000000000000002")]
        public void IsExistCalonSiswa_ExistShouldReturnTrue(string nik)
        {
            // Arrange
            var _pendaftaranService = new PendaftaranService(_connectionHelper, null);

            // Action
            bool isExist = _pendaftaranService.IsExistCalonSiswa(nik);

            // Assert
            Assert.True(isExist);
            string msg = $"Kasus Uji: Nik 0000000000000002 ada pada database dan masukkan parameter nik dengan {nik}\n" +
                $"Hasil yang diinginkan: return true\n" +
                $"Hasil aktual: {isExist}";
            _output.WriteLine(msg);
        }

        [Theory]
        [InlineData("9999999999999999")]
        public void IsExistCalonSiswa_NotExistShouldReturnFalse(string nik)
        {
            // Arrange
            var _pendaftaranService = new PendaftaranService(_connectionHelper, null);

            // Action
            bool isExist = _pendaftaranService.IsExistCalonSiswa(nik);

            // Assert
            Assert.False(isExist);
            string msg = $"Kasus Uji: Nik 9999999999999999 TIDAK ada pada database dan masukkan parameter nik dengan {nik}\n" +
                $"Hasil yang diinginkan: return false\n" +
                $"Hasil aktual: {isExist}";
            _output.WriteLine(msg);
        }

        [Theory]
        [InlineData("Reguler", "204003")]
        public void CreateNoPendaftaran_NewNoPendaftaran(string jalur, string nilai)
        {
            // Arrange
            var _pendaftaranService = new PendaftaranService(_connectionHelper, null);

            // Action
            string newNoPendaftaran = _pendaftaranService.CreateNoPendaftaran(jalur);

            // Assert
            Assert.Equal(nilai, newNoPendaftaran);
            string msg = $"Kasus Uji: nomor terakhir akun pendaftaran jalur reguler adalah 204002, masukkan parameter jalur dengan \"{jalur}\"\n" +
                $"Hasil yang diinginkan: 204003\n" +
                $"Hasil aktual: {newNoPendaftaran}";
            _output.WriteLine(msg);
        }

        [Theory]
        [InlineData("0000000000000002", 1004)]
        public void GetCalonSiswaId_ReturnValid(string nik, int expected)
        {
            // Arrange
            var _pendaftaranService = new PendaftaranService(_connectionHelper, null);

            // Action
            int calonSiswaId = _pendaftaranService.GetCalonSiswaId(nik);

            // Assert
            Assert.Equal(expected, calonSiswaId); 
            string msg = $"Kasus Uji: Nik 0000000000000002 memiliki Id 1004 dan masukkan parameter nik dengan {nik}\n" +
                $"Hasil yang diinginkan: {expected}\n" +
                $"Hasil aktual: {calonSiswaId}";
            _output.WriteLine(msg);
        }
    }
}
