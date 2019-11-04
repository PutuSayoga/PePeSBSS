using System.Collections.Generic;

namespace FrontEnd.Web.MvcApp.Models.Domains
{
    public class PertanyaanAkademik
    {
        public int id { get; set; }
        public string Pertanyaan { get; set; }
        public Dictionary<string, bool> Pilihan { get; set; }
    }
}