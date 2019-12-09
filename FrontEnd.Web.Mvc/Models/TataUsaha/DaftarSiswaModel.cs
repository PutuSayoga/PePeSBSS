using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FrontEnd.Web.Mvc.Models.TataUsaha
{
    public class DaftarSiswaModel
    {
        public List<SiswaView> ListSiswaView { get; set; }
    }

    public class SiswaView
    {
        public int Id { get; set; }
        public string Nis { get; set; }
        public string NamaLengkap { get; set; }
        public string NamaKelas { get; set; }
        public string JenisKelamin { get; set; }
    }
}
