using FrontEnd.Web.Mvc.Models.Admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FrontEnd.Web.Mvc.Models.Ujian
{
    public class PendahuluanWawancaraModel
    {
        public string NoPendaftaran { get; set; }
        public CrudSoalWawancara SoalWawancara { get; set; }
    }
}
