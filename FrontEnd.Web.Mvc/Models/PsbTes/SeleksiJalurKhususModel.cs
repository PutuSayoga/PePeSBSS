using BackEnd.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FrontEnd.Web.Mvc.Models.PsbTes
{
    public class SeleksiJalurKhususModel
    {
        public IEnumerable<AkunKhusus> ListJalurKhusus { get; set; }
    }
    public class AkunKhusus
    {
        public int Id { get; set; }
        public string NoPendaftaran { get; set; }
        public string NamaLengkap { get; set; }
        public double NilaiMipa { get; set; }
        public double NilaiIps { get; set; }
        public double NilaiTpa { get; set; }
        public double SkorAkhir { get { return ((0.3 * NilaiMipa) + (0.3 * NilaiIps) + (0.4 * NilaiTpa)); } }
        public string Keterangan { get { return SkorAkhir > 70 ? "Lolos" : "Tidak Lolos"; } }
    }
}
