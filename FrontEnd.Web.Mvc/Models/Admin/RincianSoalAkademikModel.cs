using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FrontEnd.Web.Mvc.Models.Admin
{
    public class RincianSoalAkademikModel
    {
        public int Id { get; set; }
        public string JudulSoal { get; set; }
        public string Kategori { get; set; }
        public int JumlahPertanyaan { get; set; }
        public int BatasWaktu { get; set; }
        public string Deskripsi { get; set; }
        public IEnumerable<KelolaPertanyaanAkademikModel> ListPertanyaanAkademik { get; set; }
    }
}
