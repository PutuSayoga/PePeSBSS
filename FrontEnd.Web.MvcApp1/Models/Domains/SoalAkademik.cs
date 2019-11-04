using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FrontEnd.Web.MvcApp.Models.Domains
{
    public class SoalAkademik
    {
        public int Id { get; set; }
        public string KodeSoal { get; set; }
        public string JudulSoal { get; set; }
        public string Kategori { get; set; }
        /// <summary>
        /// Dalam menit
        /// </summary>
        public int WaktuPengerjaan { get; set; }
        public int MaxSoal { get; set; }
        public List<PertanyaanAkademik> ListPertanyann { get; set; } = new List<PertanyaanAkademik>();
    }
}
