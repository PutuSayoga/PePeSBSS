using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FrontEnd.Web.Mvc.Models.TataUsaha
{
    public class KelolaMutasiMasukModel
    {
        public AkunMutasiMasuk MutasiMasuk { get; set; }
        public List<AkunMutasiMasuk> ListAkunMutasi { get; set; }
    }
    public class AkunMutasiMasuk
    {
        public int Id { get; set; }
        [Display(Name = "Nomor Pendaftaran")]
        public string NoPendaftaran { get; set; }
        public string JalurPendaftaran { get { return "Mutasi"; } }
        [Display(Name ="Nama Lengkap", Prompt ="Nama lengkap pendaftar")]
        [Required(ErrorMessage ="Nama lengkap tidak boleh kosong")]
        public string NamaLengkap { get; set; }

        [Required(ErrorMessage = "NIK tidak boleh kosong")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Masukkan NIK yang benar")]
        [Display(Name = "Nomor Induk Kependudukan", Prompt = "NIK yang tercantum pada KK")]
        [StringLength(16, MinimumLength = 16, ErrorMessage = "Panjang NIK 16 karakter")]
        public string Nik { get; set; }

        [Required(ErrorMessage ="Sekolah asal tidak boleh kosong")]
        [Display(Name ="Asal Sekolah", Prompt ="Nama sekolah asal")]
        public string SekolahAsal { get; set; }

        [Required(ErrorMessage = "NISN tidak boleh kosong")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Masukkan NISN yang benar")]
        [StringLength(10, MinimumLength = 10, ErrorMessage = "Panjang NISN 10 karakter")]
        [Display(Name = "Nomor Induk Siswa Nasional", Prompt = "NISN pendaftar")]
        public string Nisn { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Tanggal Ujian")]
        public DateTime TanggalUjian { get; set; }

        [Display(Name ="Status")]
        public string Status { get; set; }
    }
}
