using System;
using System.Collections.Generic;

namespace BackEnd.Domains
{
    public class AkunPendaftaran
    {
        public int Id { get; set; }
        public int CalonSiswaId { get; set; }
        public string NoPendaftaran { get; set; }
        public string Password { get; set; }
        public string JalurPendaftaran { get; set; }
        public string Status { get; set; }
        public DateTime JadwalTes { get; set; }

        public CalonSiswa ACalonSiswa { get; set; }
        public ICollection<HasilTesAkademik> HasilTesAkademikS { get; set; }
        public ICollection<HasilTesWawancara> HasilTesWawancaraS { get; set; }
        public ICollection<Tes> TesS { get; set; }
    }
}
