using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FrontEnd.Web.Mvc.Models.PsbPendaftaran
{
    public class DaftarUlangModel
    {
        public string NoPendaftaran { get; set; }
        public string JalurPendaftaran { get; set; }
        public string NamaLengkap { get; set; }
        public string Status { get; set; }
        public IEnumerable<string> ListBerkas { get; set; }
    }
}
