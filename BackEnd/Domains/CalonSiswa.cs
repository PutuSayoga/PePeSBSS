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

        public AkademikTerakhir AkademikTerakhir { get; set; }
        public DataDiri DataDiri { get; set; }
        public Penunjang Penunjang { get; set; }
        public Prestasi Prestasi { get; set; }
        public List<AkunPendaftaran> ListAkunPendaftaran { get; set; }
        public List<Penanggungjawab> ListPenanggungjawab { get; set; }
        public List<Rapor> ListNilaiRapor { get; set; }
        public Siswa Siswa { get; set; }
    }
}
