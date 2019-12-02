using BackEnd.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FrontEnd.Web.Mvc.Models.PsbTes
{
    public class SeleksiModel
    {
        public List<AkunSeleksi> ListAkun { get; set; }
    }
    public class AkunSeleksi
    {
        public int Id { get; set; }
        public string JalurPendaftaran { get; set; }
        public string NoPendaftaran { get; set; }
        public string NamaLengkap { get; set; }
        public double NilaiMipa { get; set; }
        public double NilaiIps { get; set; }
        public double NilaiTpa { get; set; }
        public double SkorAkhir
        {
            get
            {
                return ((0.3 * NilaiMipa) + (0.3 * NilaiIps) + (0.4 * NilaiTpa));
            }
        }
        public bool Keterangan { get; set; }
    }
}
