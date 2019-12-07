using System;
using System.Collections.Generic;

namespace BackEnd.Domains
{
    public class MutasiKeluar
    {
        public int SiswaId { get; set; }
        public string Tujuan { get; set; }
        public string Alasan { get; set; }
        public DateTime TanggalKeluar { get; set; }

        public Siswa ASiswa { get; set; }
    }
}
