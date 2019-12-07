using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FrontEnd.Web.Mvc.Models.Admin
{
    public class RincianSoalWawancaraModel
    {
        public int Id { get; set; }
        [Display(Name ="Judul Soal")]
        public string JudulSoal { get; set; }
        [Display(Name = "Jumlah Pertanyaan")]
        public int JumlahPertanyaan { get; set; }
        [Display(Name = "Target Soal")]
        public string Target { get; set; }
        [Display(Name = "Soal Jalur")]
        public string Jalur { get; set; }
        [Display(Name = "Deskripsi")]
        public string Deskripsi { get; set; }
        public List<CrudPertanyaanWawancaraModel> ListPertanyaanWawancara { get; set; }
        public CrudPertanyaanWawancaraModel CrudPertanyaanWawancara { get; set; }
    }
    public class CrudPertanyaanWawancaraModel
    {
        public int Id { get; set; }
        public int SoalId { get; set; }
        [Display(Name ="Isi Pertanyaan", Prompt ="Isi dari pertanyaan ini")]
        [Required(ErrorMessage = "Isi tidak boleh kosong")]
        public string Isi { get; set; }
    }
}