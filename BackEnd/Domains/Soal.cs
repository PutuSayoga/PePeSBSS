using System;
using System.Collections.Generic;

namespace BackEnd.Domains
{
    public partial class Soal
    {
        public int Id { get; set; }
        public string Judul { get; set; }
        public string Kategori { get; set; }
        public string Target { get; set; }
        public int JumlahPertanyaan { get; set; }
        public int BatasWaktu { get; set; }
        public string Status { get; set; }

        public ICollection<Pertanyaan> APertanyaanS { get; set; }
    }
}
