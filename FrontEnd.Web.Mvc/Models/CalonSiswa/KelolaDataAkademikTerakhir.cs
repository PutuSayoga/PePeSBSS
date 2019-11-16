using System.ComponentModel.DataAnnotations;

namespace FrontEnd.Web.Mvc.Models.CalonSiswa
{
    public class KelolaDataAkademikTerakhir
    {
        [Required]
        public string NamaSekolah { get; set; }
        [Required]
        public string JenisSekolah { get; set; }
        [Required]
        public string StatusSekolah { get; set; }
        [Required]
        public string AlamatSekolah { get; set; }
        public string NoPesertaUn { get; set; }
        public string NoSeriSkhun { get; set; }
        public string NoSeriIjazah { get; set; }
    }
}
