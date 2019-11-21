using BackEnd.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FrontEnd.Web.Mvc.Models.Admin
{
    public class KelolaSoalWawancaraModel
    {
        public IEnumerable<Soal> ListSoal { get; set; }
        public CrudSoalWawancara SoalWawancara { get; set; }
    }
    public class CrudSoalWawancara
    {
        public string Judul { get; set; }
        public string Jalur { get; set; }
        public string Target { get; set; }
        public string Deskripsi { get; set; }
    }
}
