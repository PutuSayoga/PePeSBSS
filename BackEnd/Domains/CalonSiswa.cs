using System;
using System.Collections.Generic;

namespace BackEnd.Domains
{
    public partial class CalonSiswa
    {
        public CalonSiswa()
        {
            AkunPendaftaran = new HashSet<AkunPendaftaran>();
            Penanggungjawab = new HashSet<Penanggungjawab>();
            Prestasi = new HashSet<Prestasi>();
            Rapor = new HashSet<Rapor>();
        }

        public int Id { get; set; }
        public string Nik { get; set; }
        public string Nisn { get; set; }
        public string NamaLengkap { get; set; }

        public virtual AkademikTerakhir AkademikTerakhir { get; set; }
        public virtual DataDiri DataDiri { get; set; }
        public virtual Jadi Jadi { get; set; }
        public virtual Penunjang Penunjang { get; set; }
        public virtual ICollection<AkunPendaftaran> AkunPendaftaran { get; set; }
        public virtual ICollection<Penanggungjawab> Penanggungjawab { get; set; }
        public virtual ICollection<Prestasi> Prestasi { get; set; }
        public virtual ICollection<Rapor> Rapor { get; set; }
    }
}
