using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FrontEnd.Web.MvcApp.Models.Domains
{
    public class SoalWawancara
    {
        public int Id { get; set; }
        public string KodeSoal { get; set; }
        public string JudulSoal { get; set; }
        public string JalurPendaftaran { get; set; }
        public string Target { get; set; }
        public int MaxSoal { get; set; }
        public List<PertanyaanWawancara> ListPertanyann { get; set; }
    }
}
