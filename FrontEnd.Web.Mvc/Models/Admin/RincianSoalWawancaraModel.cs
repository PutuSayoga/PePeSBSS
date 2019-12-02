using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FrontEnd.Web.Mvc.Models.Admin
{
    public class RincianSoalWawancaraModel
    {
        public int Id { get; set; }
        public string JudulSoal { get; set; }
        public int JumlahPertanyaan { get; set; }
        public string Target { get; set; }
        public string Jalur { get; set; }
        public string Deskripsi { get; set; }
        public List<CrudPertanyaanWawancaraModel> ListPertanyaanWawancara { get; set; }
        public CrudPertanyaanWawancaraModel CrudPertanyaanWawancara { get; set; }
    }
    public class CrudPertanyaanWawancaraModel
    {
        public int Id { get; set; }
        public int SoalId { get; set; }
        public string Isi { get; set; }
    }
}