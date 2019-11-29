using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FrontEnd.Web.Mvc.Models.PsbPendaftaran
{
    public class BuktiPendaftaranModel
    {
        public string NoPendaftaran { get; set; }
        public string NamaLengkap { get; set; }
        public string JalurPendaftaran { get; set; }
        public bool isLunas { get; set; } = true;
        public string Password { get; set; }
        public string JadwalTes { get; set; }
    }
}
