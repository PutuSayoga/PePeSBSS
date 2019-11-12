using BackEnd.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FrontEnd.Web.Mvc.Models.PsbPendaftaran
{
    public class ListDaftarUlangModel
    {
        public IEnumerable<AkunPendaftaran> ListDaftarUlang { get; set; }
    }
}
