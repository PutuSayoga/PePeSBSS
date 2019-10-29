using System;
using System.Collections.Generic;

namespace BackEnd.Domains
{
    public partial class AkunPendaftaran
    {
        public AkunPendaftaran()
        {
            HasilTesAkademik = new HashSet<HasilTesAkademik>();
            HasilTesWawancara = new HashSet<HasilTesWawancara>();
            Tes = new HashSet<Tes>();
        }

        public int Id { get; set; }
        public int? CalonSiswaId { get; set; }
        public string NoPendaftaran { get; set; }
        public string Password { get; set; }
        public string JenisPendaftaran { get; set; }
        public string Status { get; set; }
        public DateTime JadwalTes { get; set; }

        public virtual CalonSiswa CalonSiswa { get; set; }
        public virtual ICollection<HasilTesAkademik> HasilTesAkademik { get; set; }
        public virtual ICollection<HasilTesWawancara> HasilTesWawancara { get; set; }
        public virtual ICollection<Tes> Tes { get; set; }
    }
}
