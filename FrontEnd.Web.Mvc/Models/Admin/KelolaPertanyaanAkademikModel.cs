using System.ComponentModel.DataAnnotations;

namespace FrontEnd.Web.Mvc.Models.Admin
{
    public class KelolaPertanyaanAkademikModel
    {
        public int SoalId { get; set; }
        public int Id { get; set; }
        [Required(ErrorMessage ="Isi pertanyaan tidak boleh kosong")]
        [Display(Name ="Isi Pertanyaan", Prompt ="Isi dari sebuah pertanyaan")]
        public string Isi { get; set; }
        [Required(ErrorMessage = "Opsi A tidak boleh kosong")]
        [Display(Name = "Opsi A", Prompt = "pilihan untuk soal A")]
        public string OpsiA { get; set; }
        [Required(ErrorMessage = "Opsi B tidak boleh kosong")]
        [Display(Name = "Opsi B", Prompt = "pilihan untuk soal B")]
        public string OpsiB { get; set; }
        [Required(ErrorMessage = "Opsi C tidak boleh kosong")]
        [Display(Name = "Opsi C", Prompt = "pilihan untuk soal C")]
        public string OpsiC { get; set; }
        [Required(ErrorMessage = "Opsi D tidak boleh kosong")]
        [Display(Name = "Opsi D", Prompt = "pilihan untuk soal D")]
        public string OpsiD { get; set; }
        public string OpsiE { get; set; }
        [Required(ErrorMessage = "Jawaban tidak boleh kosong")]
        [Display(Name = "Kunci Jawaban", Prompt = "Kunci jawaban unutuk pertanyaan ini")]
        public char Jawaban { get; set; }
    }
}
