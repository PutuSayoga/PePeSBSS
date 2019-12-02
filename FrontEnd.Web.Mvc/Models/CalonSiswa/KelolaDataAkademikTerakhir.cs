using System.ComponentModel.DataAnnotations;

namespace FrontEnd.Web.Mvc.Models.CalonSiswa
{
    public class KelolaDataAkademikTerakhir
    {
        [Required(ErrorMessage = "Nama sekolah tidak boleh kosong")]
        [Display(Name = "Nama Sekolah", Prompt = "Nama sekolah asal kamu")]
        public string NamaSekolah { get; set; }
        [Required(ErrorMessage = "Jenis sekolah tidak boleh kosong")]
        [Display(Name = "Jenis Sekolah", Prompt = "Jenis sekolah asal kamu")]
        public string JenisSekolah { get; set; }
        [Required(ErrorMessage = "Status sekolah tidak boleh kosong")]
        [Display(Name = "Status Sekolah", Prompt = "Status sekolah asal kamu")]
        public string StatusSekolah { get; set; }
        [Required(ErrorMessage = "Alamat sekolah tidak boleh kosong")]
        [Display(Name = "Alamat Sekolah", Prompt = "Masukkan alamat sekolah asal kamu")]
        public string AlamatSekolah { get; set; }
        [Display(Name = "Nomor Peserta UN", Prompt = "Wajib diisi jika sudah punya")]
        public string NoPesertaUn { get; set; }
        [Display(Name = "Nomor Seri SKHUN", Prompt = "Wajib diisi jika sudah punya")]
        public string NoSeriSkhun { get; set; }
        [Display(Name = "Nomor Seri Ijazah", Prompt = "Wajib diisi jika sudah punya")]
        public string NoSeriIjazah { get; set; }
    }
}
