using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FrontEnd.Web.Mvc.Models.Ujian
{
    public class WawancaraModel
    {
        public List<PertanyaanWawancaraTest> ListPertanyaan { get; set; }
        public int AkunPendaftaranId { get; set; }
        public int SoalId { get; set; }
    }
    public class PertanyaanWawancaraTest
    {
        public string Isi { get; set; }
        public int PertanyaanId { get; set; }
        public string Jawaban { get; set; }
    }
}
