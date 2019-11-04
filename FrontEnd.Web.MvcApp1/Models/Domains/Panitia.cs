using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FrontEnd.Web.MvcApp.Models.Domains
{
    public class Panitia
    {
        [Display(Name = "Acara")]
        [Required(ErrorMessage = "Acara tidak boleh kosong")]
        public string Acara { get; set; }
        [Display(Name = "Divisi")]
        [Required(ErrorMessage = "Divisi tidak boleh kosong")]
        public string Divisi { get; set; }
    }
}
