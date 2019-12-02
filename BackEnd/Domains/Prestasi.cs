using System;
using System.Collections.Generic;

namespace BackEnd.Domains
{
    public class Prestasi
    {
        public int CalonSiswaId { get; set; }
        public string NamaKejuaraan { get; set; }
        public string Jenis { get; set; }
        public string Tingkat { get; set; }
        public string Peringkat { get; set; }
        public DateTime Tahun { get; set; }
        public string Penyelenggara { get; set; }

        public CalonSiswa ACalonSiswa { get; set; }
    }
}
