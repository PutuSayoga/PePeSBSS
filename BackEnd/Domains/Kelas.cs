using System;
using System.Collections.Generic;

namespace BackEnd.Domains
{
    public class Kelas
    {
        public int Id { get; set; }
        public string NamaKelas { get; set; }
        public string Kategori { get; set; }
        public byte? Tingkat { get; set; }
        public byte? MaxSiswa { get; set; }
        public byte? JumlahSiswa { get; set; }

        public ICollection<Siswa> SiswaS { get; set; }
    }
}
