using System;
using System.Collections.Generic;

namespace BackEnd.Domains
{
    public partial class Siswa
    {
        public int Id { get; set; }
        public int? KelasId { get; set; }
        public string Nis { get; set; }
        public string Status { get; set; }

        public virtual Kelas Kelas { get; set; }
        public virtual Jadi Jadi { get; set; }
        public virtual MutasiKeluar MutasiKeluar { get; set; }
    }
}
