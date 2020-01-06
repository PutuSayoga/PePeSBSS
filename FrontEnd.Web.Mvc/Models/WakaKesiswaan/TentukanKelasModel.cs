using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FrontEnd.Web.Mvc.Models.WakaKesiswaan
{
    public class TentukanKelasModel
    {
        public List<SiswaDitentukan> ListSiswaDitentukan { get; set; }
    }

    public class SiswaDitentukan
    {
        public string NamaLengkap { get; set; }
        public string NoPendaftaranSucces { get; set; }
        public string JalurPendaftaranSucces { get; set; }
        public string Nis { get; set; }
    }
}
