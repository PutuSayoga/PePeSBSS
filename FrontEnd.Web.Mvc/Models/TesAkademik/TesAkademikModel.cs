using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FrontEnd.Web.Mvc.Models.TesAkademik
{
    public class TesAkademikModel
    {
        public List<PertanyaanTes> ListPertanyaan { get; set; }
        public int BatasWaktu { get; set; }
    }
    public class PertanyaanTes
    {
        public string Pertanyaan { get; set; }
        public string OpsiA { get; set; }
        public string OpsiB { get; set; }
        public string OpsiC { get; set; }
        public string OpsiD { get; set; }
        public string OpsiE { get; set; }
        public int Id { get; set; }
        public int SoalId { get; set; }
        public string JawabanCalonSiswa { get; set; }
    }
}
