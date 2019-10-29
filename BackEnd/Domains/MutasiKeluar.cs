using System;
using System.Collections.Generic;

namespace BackEnd.Domains
{
    public partial class MutasiKeluar
    {
        public int? SiswaId { get; set; }
        public string Tujuan { get; set; }
        public string Alasan { get; set; }
        public string Tanggungan { get; set; }
        public DateTime TanggalKeluar { get; set; }

        public virtual Siswa Siswa { get; set; }
    }
}
