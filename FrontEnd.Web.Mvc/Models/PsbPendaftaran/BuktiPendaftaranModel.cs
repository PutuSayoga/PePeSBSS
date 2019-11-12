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
        public int Biaya { get; } = 100000;
        public string BiayaTerbilang { get; } = "Seratus Ribu Rupiah";
        public string Password { get; set; }
        public string JadwalTes { get; set; }
    }
}
