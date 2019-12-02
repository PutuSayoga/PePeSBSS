using System;
using System.ComponentModel.DataAnnotations;

namespace FrontEnd.Web.Mvc.Models.CalonSiswa
{
    public class KelolaDataPrestasiModel
    {
        [Required(ErrorMessage = "Nama kejuaraan tidak boleh kosong")]
        [Display(Name = "Nama Kejuaraan", Prompt = "Nama kejuaraan yang diikuti")]
        public string NamaKejuaraan { get; set; }
        [Required(ErrorMessage = "Jenis kejuaraan tidak boleh kosong")]
        [Display(Name = "Jenis Kejuaraan", Prompt = "Jenis kejuaraan yang diikuti")]
        public string Jenis { get; set; }
        [Required(ErrorMessage = "Tingkat kejuaraan tidak boleh kosong")]
        [Display(Name = "Tingkat Kejuaraan", Prompt = "Tingkat kejuaraan yang diikuti")]
        public string Tingkat { get; set; }
        [Required(ErrorMessage = "Peringkat tidak boleh kosong")]
        [Display(Name = "Peringkat", Prompt = "Peringkat yang didapatkan")]
        public string Peringkat { get; set; }
        [Required(ErrorMessage = "Tanggal diselenggarakan tidak boleh kosong")]
        [Display(Name = "Tanggal Diselenggarakan", Prompt = "Masukkan tanggal dilaksanakannya kejuaraan")]
        [DataType(DataType.Date)]
        public DateTime Tanggal { get; set; }
        [Required(ErrorMessage = "Penyelenggara tidak boleh kosong")]
        [Display(Name = "Penyeleggara", Prompt = "Masukkan siapa yang menyelenggara kejuaraan in")]
        public string Penyelenggara { get; set; }
    }
}
