using System;
using System.Collections.Generic;

namespace BackEnd.Domains
{
    public partial class DataDiri
    {
        public int? CalonSiswaId { get; set; }
        public string NamaPanggilan { get; set; }
        public bool IsPerempuan { get; set; }
        public string TempatLahir { get; set; }
        public DateTime TanggalLahir { get; set; }
        public string Alamat { get; set; }
        public string Agama { get; set; }
        public string Rt { get; set; }
        public string Rw { get; set; }
        public string DusunDesaLurah { get; set; }
        public string Kecamatan { get; set; }
        public string KotaKabupaten { get; set; }
        public string KodePos { get; set; }
        public string NoTelp { get; set; }
        public string NoHp { get; set; }
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

        public virtual CalonSiswa CalonSiswa { get; set; }
    }
}
