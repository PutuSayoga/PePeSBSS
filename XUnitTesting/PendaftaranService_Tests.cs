using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using BackEnd.Abstraction;
using BackEnd.Domains;
using BackEnd.Helper;
using BackEnd.Services;
using Xunit;
using Xunit.Abstractions;
using Dapper;

namespace XUnitTesting
{
    public class PendaftaranService_Tests
    {
        private readonly ITestOutputHelper _output;
        private readonly IDbConnectionHelper _connectionHelper;
        private readonly ISecurityRelate _securityRelateHelper;
        public PendaftaranService_Tests(ITestOutputHelper output)
        {
            this._output = output;
            _connectionHelper = new DbConnectionHelper("Server=DESKTOP-JSK4FQ3\\MOCKINSTANCE; Database=SkripsiTest; Trusted_Connection=true");
            _securityRelateHelper = new SecurityRelateHelper();
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

        [Fact]
        public void CreateAkunPendaftaran_ShouldReturnFullAkun()
        {
            // Arrange
            var newAkun = new AkunPendaftaran()
            {
                JalurPendaftaran = "Reguler",
                CalonSiswa = new CalonSiswa() { Nik = "0000000000000002" }
            };
            var fullAkun = new AkunPendaftaran();
            var _pendaftaranService = new PendaftaranService(_connectionHelper, _securityRelateHelper);

            // Action
            fullAkun = _pendaftaranService.CreateAkunPendaftaran(newAkun);

            // Assert
            Assert.NotNull(fullAkun);
            string msg = $"akun pendaftaran:\n" +
                $"nomor pendaftaran = {fullAkun.NoPendaftaran}\n" +
                $"password = {fullAkun.Password}\n" +
                $"jalur pendaftaran = {fullAkun.JalurPendaftaran}\n" +
                $"nik = {fullAkun.CalonSiswa.Nik}\n" +
                $"calon siswa id = {fullAkun.CalonSiswaId}";
            _output.WriteLine(msg);
        }

        [Theory]
        [InlineData("201006", 2019)]
        public void GetAkunPendaftaranId_ReturnValue(string noPendaftaran, int expected)
        {
            // Arrange
            var _pendaftaranService = new PendaftaranService(_connectionHelper, null);

            // Action
            int akunId = _pendaftaranService.GetAkunPendaftaranId(noPendaftaran);

            // Assert
            Assert.Equal(expected, akunId);
            string msg = $"Kasus Uji: NoPendaftaran 201006 memiliki Id 2019 dan masukkan parameter noPendaftaran dengan {noPendaftaran}\n" +
                $"Hasil yang diinginkan: {expected}\n" +
                $"Hasil aktual: {akunId}";
            _output.WriteLine(msg);
        }

        #region Not Ready
        [Fact]
        public void InsertCalonSiswa_Insert()
        {
            // Arrange
            var _pendaftaranService = new PendaftaranService(_connectionHelper, null);
            var newCalonSiswa = new CalonSiswa()
            {
                Nik = "9999999999999999",
                NamaLengkap = "Tesu Indrawan",
                Nisn = "9999999999"
            };


            // Action
            _pendaftaranService.InsertCalonSiswa(newCalonSiswa);
            bool isExist = _pendaftaranService.IsExistCalonSiswa(newCalonSiswa.Nik);

            // Assert
            Assert.True(isExist);
            string msg = $"Kasus Uji: Membuat objek calon siswa kemudian melakukan penyimpanan\n" +
                $"Hasil yang diinginkan: data tersimpan\n";
            if (isExist)
                msg += "Hasil aktual: data tersimpan";
            else
                msg += "Hasil aktual: data tidak tersimpan";
            _output.WriteLine(msg);

            //using (SqlConnection conn = new SqlConnection(_connectionHelper.GetConnectionString()))
            //{
            //    conn.Execute("DELETE FROM CalonSiswa WHERE Nik = @Nik", new { Nik = newCalonSiswa.Nik });
            //}
        }

        [Fact]
        public void InsertAkunPendaftaran_Insert()
        {
            // Arrange
            var _pendaftaranService = new PendaftaranService(_connectionHelper, _securityRelateHelper);
            var newAkun = new AkunPendaftaran()
            {
                JalurPendaftaran = "Reguler",
                JadwalTes = DateTime.Now,
                CalonSiswaId = 3004,
                NoPendaftaran = "204011",
                Password = "qwertyuiop"
            };

            // Action
            _pendaftaranService.InsertAkunPendaftaran(newAkun);
            bool isExist = _pendaftaranService.GetAkunPendaftaranId(newAkun.NoPendaftaran) != 0;

            // Assert
            Assert.True(isExist);
            string msg = $"Kasus Uji: Membuat objek akun pendaftaran kemudian melakukan penyimpanan\n" +
                $"Hasil yang diinginkan: data tersimpan\n";
            if (isExist)
                msg += "Hasil aktual: data tersimpan";
            else
                msg += "Hasil aktual: data tidak tersimpan";
            _output.WriteLine(msg);

            //using (SqlConnection conn = new SqlConnection(_connectionHelper.GetConnectionString()))
            //{
            //    conn.Execute("DELETE FROM CalonSiswa WHERE Nik = @Nik", new { Nik = newCalonSiswa.Nik });
            //}
        }
        #endregion
    }
}
