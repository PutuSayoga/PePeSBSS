using System;
using System.Collections.Generic;

namespace BackEnd.Domains
{
    public partial class HasilTesWawancara
    {
        public int? SoalId { get; set; }
        public byte? IndexPertanyaan { get; set; }
        public int? AkunPendaftaranId { get; set; }
        public string Jawaban { get; set; }

        public virtual AkunPendaftaran AkunPendaftaran { get; set; }
        public virtual Pertanyaan Pertanyaan { get; set; }
    }
}
