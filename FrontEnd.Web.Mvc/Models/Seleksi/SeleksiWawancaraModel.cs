using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FrontEnd.Web.Mvc.Models.Seleksi
{
    public class SeleksiWawancaraModel
    {
        public List<PertanyaanWawancaraTest> ListPertanyaan { get; set; }
        public string JudulSoal { get; set; }
        public string Target { get; set; }
    }
    public class PertanyaanWawancaraTest
    {
        public string Pertanyaan { get; set; }
        public int PertanyaanId { get; set; }
        public int SoalId { get; set; }
        public string Jawaban { get; set; }
    }
}
