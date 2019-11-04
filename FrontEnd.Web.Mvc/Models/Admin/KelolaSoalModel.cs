using BackEnd.Domains;
using System.Collections.Generic;

namespace FrontEnd.Web.Mvc.Models.Admin
{
    public class KelolaSoalModel
    {
        public IEnumerable<Soal> ListSoalAkademik { get;  set; }
        public TambahSoal TambahSoalAkademik { get; set; }
    }
    public class TambahSoal
    {
        public string Judul { get; set; }
        public string Kategori { get; set; }
        public string Target { get; set; }
        public int BatasWaktu { get; set; }
    }
}
