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
        public IEnumerable<CrudPertanyaanAkademikModel> ListPertanyaanAkademik { get; set; }
        public CrudPertanyaanAkademikModel CrudPertanyaanAkademik { get; set; }
    }
    public class CrudPertanyaanAkademikModel
    {
        public int Id { get; set; }
        public string Isi { get; set; }
        public string OpsiA { get; set; }
        public string OpsiB { get; set; }
        public string OpsiC { get; set; }
        public string OpsiD { get; set; }
        public string OpsiE { get; set; }
        public char Jawaban { get; set; }
    }
}
