using System;
using System.Collections.Generic;

namespace BackEnd.Domains
{
    public class Siswa
    {
        public int Id { get; set; }
        public int CalonSiswaId { get; set; }
        public DateTime TanggalMasuk { get; set; }
        public int? KelasId { get; set; }
        public string Nis { get; set; }
        public string Status { get; set; }

        public Kelas Kelas { get; set; }
        public CalonSiswa CalonSiswa { get; set; }
        public MutasiKeluar MutasiKeluar { get; set; }
    }
}
