using System;
using System.Collections.Generic;

namespace BackEnd.Domains
{
    public partial class Pertanyaan
    {
        public int SoalId { get; set; }
        public byte Id { get; set; }
        public string Isi { get; set; }
        public string OpsiA { get; set; }
        public string OpsiB { get; set; }
        public string OpsiC { get; set; }
        public string OpsiD { get; set; }
        public string OpsiE { get; set; }
        public char Jawaban { get; set; }
    }
}
