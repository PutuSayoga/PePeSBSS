using BackEnd.Domains;
using System.Collections.Generic;

namespace FrontEnd.Web.Mvc.Models.Admin
{
    public class KelolaSoalAkademikModel
    {
        public IEnumerable<Soal> ListSoal { get;  set; }
        public CrudSoalAkademik SoalAkademik { get; set; }
    }
    public class CrudSoalAkademik
    {
        public string Judul { get; set; }
        public string Kategori { get; set; }
        public string Deskripsi { get; set; }
        public int BatasWaktu { get; set; }
    }
}
