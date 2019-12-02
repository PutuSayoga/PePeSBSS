using System;
using System.ComponentModel.DataAnnotations;

namespace FrontEnd.Web.Mvc.Models.CalonSiswa
{
    public class KelolaDataOrangTuaModel
    {
        [Required(ErrorMessage = "Nama lengkap Ayah tidak boleh kosong")]
        [Display(Name = "Nama Lengkap", Prompt = "Masukkan nama lengkap Ayah")]
        public string NamaLengkapAyah { get; set; }
        [Required(ErrorMessage = "Tempat lahir Ayah tidak boleh kosong")]
        [Display(Name = "Tempat Lahir", Prompt = "Masukkan tempat lahir Ayah")]
        public string TempatLahirAyah { get; set; }
        [Required(ErrorMessage = "Tanggal lahir tidak boleh kosong")]
        [Display(Name = "Tanggal Lahir", Prompt = "Masukkan tanggal lahir ayar")]
        [DataType(DataType.Date)]
        public DateTime? TanggalLahirAyah { get; set; }
        [Required(ErrorMessage = "Alamat Ayah tidak boleh kosong")]
        [Display(Name = "Alamat", Prompt = "Masukkan alamat rumah Ayah")]
        public string AlamatAyah { get; set; }
        [Required(ErrorMessage = "Agama Ayah tidak boleh kosong")]
        [Display(Name = "Agama", Prompt = "Kepercayaan yang dianut Ayah")]
        public string AgamaAyah { get; set; }
        [Required(ErrorMessage = "Pendidikan terakhir Ayah tidak boleh kosong")]
        [Display(Name = "Pendidikan Terakhir", Prompt = "Pendidikan terakhir Ayah")]
        public string PendidikanTerakhirAyah { get; set; }
        [Display(Name = "Pekerjaan", Prompt = "Pekerjaan Ayah saat ini")]
        public string PekerjaanAyah { get; set; }
        [Display(Name = "Penghasilan", Prompt = "Penghasilan Ayah perbulan")]
        public int PenghasilanAyah { get; set; }
        [Display(Name = "Nomor Telepon", Prompt = "Nomor telepon rumah Ayah")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Masukkan nomor telepon yang benarex. 034123456789")]
        public string NoTelpAyah { get; set; }
        [Required(ErrorMessage ="Nomor Hp Ayah tidak boleh kosong")]
        [Display(Name = "Nomor Hp", Prompt = "Nomor Hp Ayah")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Masukkan nomor Hp yang benarex. 08123456789")]
        public string NoHpAyah { get; set; }
        [Required(ErrorMessage ="Alamat Email Ayah tidak boleh kosong")]
        [Display(Name = "E-Mail", Prompt = "Alamat Email aktif Ayah")]
        [EmailAddress(ErrorMessage = "Masukkan alamat email yang benar")]
        public string EmailAyah { get; set; }
        [Display(Name = "Status Dalam Keluarga", Prompt = "Status Ayah dalam keluarga")]
        public string StatusDalamKeluargaAyah { get; set; }
        [Display(Name = "Keterangan", Prompt = "Keadaan Ayah saat ini")]
        public string KeteranganAyah { get; set; }


        [Required(ErrorMessage = "Nama lengkap Ibu tidak boleh kosong")]
        [Display(Name = "Nama Lengkap", Prompt = "Masukkan nama lengkap Ibu")]
        public string NamaLengkapIbu { get; set; }
        [Required(ErrorMessage = "Tempat lahir Ibu tidak boleh kosong")]
        [Display(Name = "Tempat Lahir", Prompt = "Masukkan tempat lahir Ibu")]
        public string TempatLahirIbu { get; set; }
        [Required(ErrorMessage = "Tanggal lahir tidak boleh kosong")]
        [Display(Name = "Tanggal Lahir", Prompt = "Masukkan tanggal lahir ayar")]
        [DataType(DataType.Date)]
        public DateTime? TanggalLahirIbu { get; set; }
        [Required(ErrorMessage = "Alamat Ibu tidak boleh kosong")]
        [Display(Name = "Alamat", Prompt = "Masukkan alamat rumah Ibu")]
        public string AlamatIbu { get; set; }
        [Required(ErrorMessage = "Agama Ibu tidak boleh kosong")]
        [Display(Name = "Agama", Prompt = "Kepercayaan yang dianut Ibu")]
        public string AgamaIbu { get; set; }
        [Required(ErrorMessage = "Pendidikan terakhir Ibu tidak boleh kosong")]
        [Display(Name = "Pendidikan Terakhir", Prompt = "Pendidikan terakhir Ibu")]
        public string PendidikanTerakhirIbu { get; set; }
        [Display(Name = "Pekerjaan", Prompt = "Pekerjaan Ibu saat ini")]
        public string PekerjaanIbu { get; set; }
        [Display(Name = "Penghasilan", Prompt = "Penghasilan Ibu perbulan")]
        public int PenghasilanIbu { get; set; }
        [Display(Name = "Nomor Telepon", Prompt = "Nomor telepon rumah Ibu")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Masukkan nomor telepon yang benar ex. 034123456789")]
        public string NoTelpIbu { get; set; }
        [Required(ErrorMessage = "Nomor Hp Ibu tidak boleh kosong")]
        [Display(Name = "Nomor Hp", Prompt = "Nomor Hp Ibu")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Masukkan nomor Hp yang benar ex. 08123456789")]
        public string NoHpIbu { get; set; }
        [Required(ErrorMessage = "Alamat Email Ibu tidak boleh kosong")]
        [Display(Name = "E-Mail", Prompt = "Alamat Email aktif Ibu")]
        [EmailAddress(ErrorMessage = "Masukkan alamat email yang benar")]
        public string EmailIbu { get; set; }
        [Display(Name = "Status Dalam Keluarga", Prompt = "Status Ibu dalam keluarga")]
        public string StatusDalamKeluargaIbu { get; set; }
        [Display(Name = "Keterangan", Prompt = "Keadaan Ibu saat ini")]
        public string KeteranganIbu { get; set; }


        [Display(Name = "Nama Wali", Prompt = "Masukkan nama lengkap Wali")]
        public string NamaLengkapWali { get; set; }
        [Display(Name = "Tempat Lahir", Prompt = "Masukkan tempat lahir Wali")]
        public string TempatLahirWali { get; set; }
        [Display(Name = "Tanggal Lahir", Prompt = "Masukkan tanggal lahir ayar")]
        [DataType(DataType.Date)]
        public DateTime? TanggalLahirWali { get; set; }
        [Display(Name = "Alamat", Prompt = "Masukkan alamat rumah Wali")]
        public string AlamatWali { get; set; }
        [Display(Name = "Agama", Prompt = "Kepercayaan yang dianut Wali")]
        public string AgamaWali { get; set; }
        [Display(Name = "Pendidikan Terakhir", Prompt = "Pendidikan terakhir Wali")]
        public string PendidikanTerakhirWali { get; set; }
        [Display(Name = "Pekerjaan", Prompt = "Pekerjaan Wali saat ini")]
        public string PekerjaanWali { get; set; }
        [Display(Name = "Penghasilan", Prompt = "Penghasilan Wali perbulan")]
        public int PenghasilanWali { get; set; }
        [Display(Name = "Nomor Telepon", Prompt = "Nomor telepon rumah Wali")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Masukkan nomor telepon yang benar ex. 034123456789")]
        public string NoTelpWali { get; set; }
        [Display(Name = "Nomor Hp", Prompt = "Nomor Hp Wali")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Masukkan nomor Hp yang benar ex. 08123456789")]
        public string NoHpWali { get; set; }
        [Display(Name = "E-Mail", Prompt = "Alamat Email aktif Wali")]
        [EmailAddress(ErrorMessage = "Masukkan alamat email yang benar")]
        public string EmailWali { get; set; }
    }
}
