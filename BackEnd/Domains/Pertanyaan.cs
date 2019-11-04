using System;
using System.Collections.Generic;

namespace BackEnd.Domains
{
    public partial class Pertanyaan
    {
        public Pertanyaan()
        {
            HasilTesWawancara = new HashSet<HasilTesWawancara>();
        }

        public int SoalId { get; set; }
        public byte IndexPertanyaan { get; set; }
        public string BadanPertanyaan { get; set; }
        public string PathGambar { get; set; }
        public string Pilihan { get; set; }
        public byte? JawabanBenar { get; set; }

        public virtual Soal Soal { get; set; }
        public virtual ICollection<HasilTesWawancara> HasilTesWawancara { get; set; }
    }
}
