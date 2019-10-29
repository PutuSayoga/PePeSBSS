using System;
using System.Collections.Generic;

namespace BackEnd.Domains
{
    public partial class Tes
    {
        public int? AkunPendaftaranId { get; set; }
        public int? SoalId { get; set; }
        public DateTime TanggalTes { get; set; }

        public virtual AkunPendaftaran AkunPendaftaran { get; set; }
        public virtual Soal Soal { get; set; }
    }
}
