using BackEnd.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FrontEnd.Web.Mvc.Models.PsbPendaftaran
{
    public class ListDaftarUlangModel
    {
        public IEnumerable<AkunDaftarUlang> ListDaftarUlang { get; set; }
    }
    public class AkunDaftarUlang
    {
        public int Id { get; set; }
        public string NamaLengkap { get; set; }
        public string NoPendaftaran { get; set; }
        public string JalurPendaftaran { get; set; }
        public string AsalSekolah { get; set; }
    }
}
