using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FrontEnd.Web.Mvc.Models.PsbTes
{
    public class HasilWawancaraModel

    {
        public string Peserta { get; } = "Calon Siswa";
        public List<HasilWawancara> HasilWawancaraCalonSiswa { get; set; }
        public string OrangTua { get; } = "Orang Tua/Wali";
        public List<HasilWawancara> HasilWawancaraOrangTua { get; set; }
    }

    public class HasilWawancara
    {
        public string Pertanyaan { get; set; }
        public string Jawaban { get; set; }
    }
}
