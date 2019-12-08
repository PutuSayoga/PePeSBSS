using System;
using System.Collections.Generic;

namespace BackEnd.Domains
{
    public class Penanggungjawab
    {
        public int CalonSiswaId { get; set; }
        public string Sebagai { get; set; }
        public string NamaLengkap { get; set; }
        public string TempatLahir { get; set; }
        public DateTime? TanggalLahir { get; set; }
        public string Alamat { get; set; }
        public string Agama { get; set; }
        public string PendidikanTerakhir { get; set; }
        public string Pekerjaan { get; set; }
        public int Penghasilan { get; set; }
        public string NoTelp { get; set; }
        public string NoHp { get; set; }
        public string Email { get; set; }
        public string StatusDalamKeluarga { get; set; }
        public string Keterangan { get; set; }

        public CalonSiswa CalonSiswa { get; set; }
    }
}
