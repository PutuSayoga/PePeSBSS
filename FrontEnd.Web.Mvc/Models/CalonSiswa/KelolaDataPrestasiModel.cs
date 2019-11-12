using System;
using System.ComponentModel.DataAnnotations;

namespace FrontEnd.Web.Mvc.Models.CalonSiswa
{
    public class KelolaDataPrestasiModel
    {
        [Required]
        public string NamaKejuaraan { get; set; }
        [Required]
        public string Jenis { get; set; }
        [Required]
        public string Tingkat { get; set; }
        [Required]
        public string Peringkat { get; set; }
        [Required]
        public DateTime Tahun { get; set; }
        [Required]
        public string Penyelenggara { get; set; }
    }
}
