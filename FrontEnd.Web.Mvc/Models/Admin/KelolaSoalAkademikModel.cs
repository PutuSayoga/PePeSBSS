using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FrontEnd.Web.Mvc.Models.Admin
{
    public class KelolaSoalAkademikModel
    {
        public List<CrudSoalAkademik> ListSoal { get;  set; }
        public CrudSoalAkademik SoalAkademik { get; set; }
    }
    public class CrudSoalAkademik
    {
        [Display(Name = "Judul Soal", Prompt = "Masukkan judul soal")]
        [Required(ErrorMessage = "Judul soal tidak boleh kosong")]
        public string Judul { get; set; }
        [Display(Name = "Kategori", Prompt = "Masukkan kategori soal")]
        [Required(ErrorMessage = "Kategori tidak boleh kosong")]
        public string Kategori { get; set; }
        [Display(Name = "Deskripsi", Prompt = "Deskripsi tambahan untuk soal ini")]
        [Required(ErrorMessage = "Deskripsi tidak boleh kosong")]
        public string Deskripsi { get; set; }
        [Display(Name = "Batas Waktu", Prompt = "Waktu pengerjaan")]
        [Required(ErrorMessage = "Batas Waktu tidak boleh kosong")]
        public int BatasWaktu { get; set; }
        public int JumlahPertanyaan { get; set; }
        public int Id { get; set; }
    }
}
