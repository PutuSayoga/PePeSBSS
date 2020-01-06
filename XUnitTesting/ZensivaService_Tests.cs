using BackEnd.Abstraction;
using BackEnd.Helper;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Xunit.Abstractions;

namespace XUnitTesting
{
    public class ZensivaService_Tests
    {
        private readonly ITestOutputHelper _output;
        private readonly INotif _notif;
        public ZensivaService_Tests(ITestOutputHelper output)
        {
            _output = output;
            _notif = new ZensivaService();
        }

        [Fact]
        public void SendNotif_BisaDiakses()
        {
            string result = _notif.SendNotif("08123123", "test");
            _output.WriteLine(result);
        }
    }
}
