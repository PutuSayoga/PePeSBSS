using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FrontEnd.Web.Mvc.Models.Seleksi
{
    public class SeleksiAkademikModel
    {
        public List<PertanyaanTest> ListPertanyaan { get; set; }
        public int BatasWaktu { get; set; }
    }
    public class PertanyaanTest
    {
        public string Pertanyaan { get; set; }
        public string OpsiA { get; set; }
        public string OpsiB { get; set; }
        public string OpsiC { get; set; }
        public string OpsiD { get; set; }
        public string OpsiE { get; set; }
        public int PertanyaanId { get; set; }
        public int SoalId { get; set; }
        public string JawabanCalonSiswa { get; set; }
    }
}
