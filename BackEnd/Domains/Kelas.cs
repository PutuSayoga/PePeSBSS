using System;
using System.Collections.Generic;

namespace BackEnd.Domains
{
    public partial class Kelas
    {
        public Kelas()
        {
            Siswa = new HashSet<Siswa>();
        }

        public int Id { get; set; }
        public string NamaKelas { get; set; }
        public string Kategori { get; set; }
        public byte? Tingkat { get; set; }
        public byte? MaxSiswa { get; set; }
        public byte? JumlahSiswa { get; set; }

        public virtual ICollection<Siswa> Siswa { get; set; }
    }
}
