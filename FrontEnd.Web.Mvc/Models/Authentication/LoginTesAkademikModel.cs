using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FrontEnd.Web.Mvc.Models.Authentication
{
    public class LoginTesAkademikModel
    {
        [Required]
        public string NoPendaftaran { get; set; }
        [Required]
        public string Kode { get; set; }
    }
}
