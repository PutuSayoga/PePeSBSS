using System.ComponentModel.DataAnnotations;

namespace FrontEnd.Web.Mvc.Models.Admin
{
    public class KelolaPertanyaanAkademikModel
    {
        public int SoalId { get; set; }
        public int Id { get; set; }
        [Required]
        public string Isi { get; set; }
        [Required]
        public string OpsiA { get; set; }
        [Required]
        public string OpsiB { get; set; }
        [Required]
        public string OpsiC { get; set; }
        [Required]
        public string OpsiD { get; set; }
        public string OpsiE { get; set; }
        [Required]
        public char Jawaban { get; set; }
    }
}
