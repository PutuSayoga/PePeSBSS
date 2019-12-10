using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FrontEnd.Web.Mvc.Models.Admin
{
    public class KelolaSoalWawancaraModel
    {
        public List<CrudSoalWawancara> ListSoal { get; set; }
        public CrudSoalWawancara SoalWawancara { get; set; }
    }
    public class CrudSoalWawancara
    {
        public int Id { get; set; }
        [Display(Name ="Kategori Soal")]
        public string Kategori { get { return "Wawancara"; } }
        [Display(Name = "Judul Soal", Prompt = "Masukkan judul soal")]
        [Required(ErrorMessage = "Judul soal tidak boleh kosong")]
        public string Judul { get; set; }
        [Display(Name = "Jalur Pendaftaran", Prompt = "Soal untuk jalur")]
        [Required(ErrorMessage = "Soal untuk jalur pendaftaran..")]
        public string Jalur { get; set; }
        [Display(Name = "Target Soal", Prompt = "Target soal")]
        [Required(ErrorMessage = "Soal dikhususkan untuk..")]
        public string Target { get; set; }
        [Display(Name = "Deskripsi", Prompt = "Deskripsi tambahan untuk soal ini")]
        [Required(ErrorMessage = "Deskripsi tidak boleh kosong")]
        public string Deskripsi { get; set; }
        public int JumlahPertanyaan { get; set; }
    }
}
