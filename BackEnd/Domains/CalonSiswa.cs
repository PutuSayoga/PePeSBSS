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
        public DataDiri DataDiri { get; set; }
        public Jadi Jadi { get; set; }
        public Penunjang Penunjang { get; set; }
        public ICollection<AkunPendaftaran> AkunPendaftaran { get; set; }
        public ICollection<Penanggungjawab> Penanggungjawab { get; set; }
        public ICollection<Prestasi> Prestasi { get; set; }
        public ICollection<Rapor> Rapor { get; set; }
    }
}
