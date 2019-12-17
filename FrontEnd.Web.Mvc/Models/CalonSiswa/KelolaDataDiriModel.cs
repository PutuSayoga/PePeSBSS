using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FrontEnd.Web.Mvc.Models.CalonSiswa
{
    public class KelolaDataDiriModel
    {
        [Display(Name ="Nomor Induk Kependudukan")]
        public string Nik { get; set; }
        [Required(ErrorMessage = "Nama lengkap tidak boleh kosong")]
        [Display(Name ="Nama Lengkap", Prompt = "Nama lengkap sesuai KK")]
        public string NamaLengkap { get; set; }
        [Display(Name = "Nomor Pendaftaran")]
        public string NoPendaftaran { get; set; }
        [Display(Name = "Jalur Pendaftaran")]
        public string JalurPendaftaran { get; set; }
        [Required(ErrorMessage = "Nama Panggilan tidak boleh kosong")]
        [Display(Name ="Nama Panggilan", Prompt ="Bagaimana kami memanggilmu")]
        public string NamaPanggilan { get; set; }
        [Required(ErrorMessage = "Pilih salah satu jenis kelamin")]
        [Display(Name = "Jenis Kelamin")]
        public bool IsPerempuan { get; set; }
        [Required(ErrorMessage = "Tempat lahir tidak boleh kosong")]
        [Display(Name = "Tempat Lahir", Prompt = "Tempat lahir sesuai KK")]
        public string TempatLahir { get; set; }
        [Required(ErrorMessage = "Tanggal lahir tidak boleh kosong")]
        [Display(Name = "Tanggal Lahir", Prompt ="Tanggal lahir kamu")]
        [DataType(DataType.Date)]
        public DateTime TanggalLahir { get; set; }
        [Required(ErrorMessage = "Agama tidak boleh kosong")]
        [Display(Name = "Agama", Prompt = "Kepercayaan yang dianut")]
        public string Agama { get; set; }
        [Required(ErrorMessage = "Alamat tidak boleh kosong")]
        [Display(Name = "Alamat", Prompt = "Alamat tempat kamu tinggal")]
        public string Alamat { get; set; }
        [Display(Name = "Rt")]
        public string Rt { get; set; }
        [Display(Name = "Rw")]
        public string Rw { get; set; }
        [Display(Name = "Dusun/Desa/Kelurahan", Prompt ="Dusun/Desa/Kelurahan tempat kamu tinggal")]
        public string DusunDesaLurah { get; set; }
        [Display(Name = "Kecamatan", Prompt = "Kecamatan tempat kamu tinggal")]
        public string Kecamatan { get; set; }
        [Display(Name = "Kota/Kabupaten", Prompt = "Kota/Kabupaten tempat kamu tinggal")]
        public string KotaKabupaten { get; set; }
        [Display(Name = "Kode Pos", Prompt = "Kode pos tempat kamu tinggal")]
        public string KodePos { get; set; }
        [Display(Name = "Nomor Telepon", Prompt = "Nomor telepon rumah")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Masukkan nomor telepon yang benar ex. 034123456789")]
        public string NoTelp { get; set; }
        [Required(ErrorMessage = "Nomor Hp tidak boleh kosong")]
        [Display(Name = "Nomor Hp", Prompt = "Nomor Hp yang bisa dihubungi")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Masukkan nomor Hp yang benar ex. 08123456789")]
        public string NoHp { get; set; }
        [Required(ErrorMessage = "E-mail tidak boleh kosong")]
        [Display(Name = "E-Mail", Prompt = "E-mail yang aktif")]
        [EmailAddress(ErrorMessage = "Masukkan alamat email yang benar")]
        public string Email { get; set; }
        [Display(Name = "Jumlah Saudara")]
        public byte? JumlahSaudara { get; set; }
        [Display(Name = "Anak ke")]
        public byte? AnakKe { get; set; }
        [Display(Name = "Status Dalam Keluarga")]
        public string StatusDalamKeluarga { get; set; }
        [Display(Name = "Tinggi Badan")]
        public byte? TinggiBadan { get; set; }
        [Display(Name = "Berat Badan")]
        public byte? BeratBadan { get; set; }
        [Display(Name = "Golongan Darah")]
        public string GolDarah { get; set; }
        [Display(Name = "Cita-cita", Prompt = "Cita-cita yang ingin kamu capai")]
        public string CitaCita { get; set; }
        [Display(Name = "Hobi", Prompt = "Jika lebih dari satu pisahkan dengan koma")]
        public string Hobi { get; set; }
        [Display(Name = "Riwaya Sakit", Prompt = "Jika lebih dari satu pisahkan dengan koma")]
        public string RiwayatSakit { get; set; }
        [Display(Name = "Kelainan Jasmani", Prompt = "Kami akan selalu membantu")]
        public string KelainanJasmani { get; set; }
    }
}
