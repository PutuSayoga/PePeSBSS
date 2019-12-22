using System;
using System.Collections.Generic;
using System.Text;
using BackEnd.Services;
using BackEnd.Helper;
using Xunit;
using Xunit.Abstractions;
using BackEnd.Abstraction;

namespace XUnitTesting
{
    public class UjianService_Tests
    {
        private readonly ITestOutputHelper output;
        private IDbConnectionHelper _connectionHelper;

        public UjianService_Tests(ITestOutputHelper output)
        {
            this.output = output;
            _connectionHelper = new DbConnectionHelper
                ("Server=DESKTOP-3QK0RTT\\LOCAL_INSTANCE; Database=SkripsiDatabase; Trusted_Connection=true");
        }

        [Theory]
        [InlineData(13)]
        [InlineData(18)]
        [InlineData(1020)]
        public void IsExistInRangkumanAkademik_ExistShouldReturnTrue(int akunPendaftaranId)
        {
            // Arrange
            var _ujianService = new UjianService(_connectionHelper, null, null);

            // Action
            bool isExist = _ujianService.IsExistInRangkumanAkademik(akunPendaftaranId);

            // Assert
            Assert.True(isExist);
            output.WriteLine($"id {akunPendaftaranId} ada pada tabel RangkumanAkademik");
        }

        [Theory]
        [InlineData(34)]
        [InlineData(64)]
        [InlineData(25)]
        public void IsExistInRangkumanAkademik_NotExistShouldReturnFalse(int akunPendaftaranId)
        {
            // Arrange
            var _ujianService = new UjianService(_connectionHelper, null, null);

            // Action
            bool isExist = _ujianService.IsExistInRangkumanAkademik(akunPendaftaranId);

            // Assert
            Assert.False(isExist);
            output.WriteLine($"id {akunPendaftaranId} tidak ada pada tabel RangkumanAkademik");
        }

        [Theory]
        [InlineData(27, 40, 67.5)]
        [InlineData(17, 50, 34)]
        [InlineData(0, 50, 0)]
        public void Mark_CorectOutput(int corectAnswer, int totalPertanyaan, double expected)
        {
            // Arrange
            var _ujianService = new UjianService(null, null, null);

            // Action
            double actual = _ujianService.Mark(corectAnswer, totalPertanyaan);

            // Assert
            Assert.Equal(expected, actual);
        }
    }
}
