using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FrontEnd.Web.Mvc.Models.Admin
{
    public class RincianStaffModel
    {
        public int Id { get; set; }
        public TambahStaffModel Staff { get; set; }
        public TambahPanitiaModel Panitia { get; set; }
    }
    public class TambahPanitiaModel
    {

        [Display(Name = "Acara")]
        [Required(ErrorMessage = "Acara tidak boleh kosong")]
        public string Acara { get; set; }
        [Display(Name = "Divisi")]
        [Required(ErrorMessage = "Divisi tidak boleh kosong")]
        public string Divisi { get; set; }

    }
}
