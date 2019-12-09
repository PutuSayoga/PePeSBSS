using BackEnd.Abstraction;
using BackEnd.Services;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace XUnitTesting
{
    public class TestPenerimaanService_Tests
    {
        [Theory]
        [InlineData(27, 40, 67.5)]
        [InlineData(17, 50, 34)]
        public void Mark_CorectAnswer(int corectAnswer, int totalPertanyaan, double expected)
        {
            // Arrange
            var _testPenerimaanService = new UjianService(null, null, null);

            // Action
            double actual = _testPenerimaanService.Mark(corectAnswer, totalPertanyaan);

            // Assert
            Assert.Equal(expected, actual);
        }
    }
}
