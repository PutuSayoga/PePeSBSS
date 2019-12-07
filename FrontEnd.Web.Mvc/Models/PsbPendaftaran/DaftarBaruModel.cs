using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FrontEnd.Web.Mvc.Models.PsbPendaftaran
{
    public class DaftarBaruModel
    {
        [Required(ErrorMessage = "Jalur Pendaftaran tidak boleh kosong")]
        [Display(Name ="Jalur Pendaftaran")]
        public string JalurPendaftaran { get; set; }
        [Required(ErrorMessage = "Nama Lengkap tidak boleh kosong")]
        [Display(Name = "Nama Lengkap", Prompt ="Nama lengkap sesuai KK")]
        public string NamaLengkap { get; set; }
        [Required(ErrorMessage = "NIK tidak boleh kosong")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Masukkan NIK yang benar")]
        [Display(Name = "Nomor Induk Keluarga", Prompt ="NIK yang tercantum pada KK")]
        [StringLength(16, MinimumLength =16, ErrorMessage ="Panjang NIK 16 karakter")]
        public string Nik { get; set; }
        [Required(ErrorMessage = "NISN tidak boleh kosong")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Masukkan NIK yang benar")]
        [StringLength(10, MinimumLength = 10, ErrorMessage = "Panjang NISN 10 karakter")]
        [Display(Name = "Nomor Induk Siswa Nasional", Prompt ="NISN pendaftar")]
        public string Nisn { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "Tanggal Ujian")]
        public DateTime JadwalTes { get; set; }
    }
}
