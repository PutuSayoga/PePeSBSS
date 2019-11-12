using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FrontEnd.Web.Mvc.Models.CalonSiswa
{
    public class KelolaDataDiriModel
    {
        [Required]
        public string NamaLengkap { get; set; }
        public string NoPendaftaran { get; set; }
        public string JalurPendaftaran { get; set; }
        [Required]
        public string NamaPanggilan { get; set; }
        [Required]
        public bool IsPerempuan { get; set; }
        [Required]
        public string TempatLahir { get; set; }
        [Required]
        public DateTime TanggalLahir { get; set; }
        [Required]
        public string Agama { get; set; }
        [Required]
        public string Alamat { get; set; }
        public string Rt { get; set; }
        public string Rw { get; set; }
        public string DusunDesaLurah { get; set; }
        public string Kecamatan { get; set; }
        public string KotaKabupaten { get; set; }
        public string KodePos { get; set; }
        public string NoTelp { get; set; }
        [Required]
        public string NoHp { get; set; }
        [Required]
        public string Email { get; set; }
        public byte? JumlahSaudara { get; set; }
        public byte? AnakKe { get; set; }
        public string StatusDalamKeluarga { get; set; }
        public byte? TinggiBadan { get; set; }
        public byte? BeratBadan { get; set; }
        public string GolDarah { get; set; }
        public string CitaCita { get; set; }
        public string Hobi { get; set; }
        public string RiwayatSakit { get; set; }
        public string KelainanJasmani { get; set; }
    }
}
