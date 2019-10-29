using System;
using System.Collections.Generic;

namespace BackEnd.Domains
{
    public partial class Soal
    {
        public Soal()
        {
            Pertanyaan = new HashSet<Pertanyaan>();
            Tes = new HashSet<Tes>();
        }

        public int Id { get; set; }
        public string Kode { get; set; }
        public string Judul { get; set; }
        public string Kategori { get; set; }
        public string Target { get; set; }
        public byte? MaxPertanyaan { get; set; }
        public byte? JumlahPertanyaan { get; set; }
        public byte? BatasWaktu { get; set; }

        public virtual ICollection<Pertanyaan> Pertanyaan { get; set; }
        public virtual ICollection<Tes> Tes { get; set; }
    }
}
