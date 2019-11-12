using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FrontEnd.Web.Mvc.Models.PsbPendaftaran
{
    public class DaftarBaruModel
    {
        public string JalurPendaftaran { get; set; }
        public string NamaLengkap { get; set; }
        public string Nik { get; set; }
        public string Nisn { get; set; }
        public DateTime JadwalTes { get; set; }
        public IEnumerable<string> ListBerkas { get; set; }
    }
}
