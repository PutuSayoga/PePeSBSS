using System;
using System.Collections.Generic;

namespace BackEnd.Domains
{
    public class HasilTes
    {
        public int SoalId { get; set; }
        public int PertanyaanId { get; set; }
        public int AkunPendaftaranId { get; set; }
        public string Jawaban { get; set; }
        public bool Nilai { get; set; }

        public AkunPendaftaran AAkunPendaftaran { get; set; }
        public Pertanyaan APertanyaan { get; set; }
    }
}
