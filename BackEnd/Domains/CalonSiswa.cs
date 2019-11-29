using System;
using System.Collections.Generic;

namespace BackEnd.Domains
{
    public class CalonSiswa
    {
        public int Id { get; set; }
        public string Nik { get; set; }
        public string Nisn { get; set; }
        public string NamaLengkap { get; set; }

        public AkademikTerakhir AAkademikTerakhir { get; set; }
        public DataDiri ADataDiri { get; set; }
        public Penunjang APenunjang { get; set; }
        public Prestasi APrestasi { get; set; }
        public ICollection<AkunPendaftaran> AkunPendaftaranS { get; set; }
        public ICollection<Penanggungjawab> PenanggungjawabS { get; set; }
        public ICollection<Rapor> RaporS { get; set; }
    }
}
