using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FrontEnd.Web.Mvc.Models.PsbTes
{
    public class TestWawancaraModel
    {
        [Required(ErrorMessage = "Nomor pendaftaran tidak boleh kosong")]
        [Display(Name = "Nomor Pendaftaran", Prompt = "Masukkan nomor pendaftaran")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Masukkan nomor pendaftaran yang benar")]
        public string NoPendaftaran { get; set; }

        [Required(ErrorMessage = "Siapa yang melakukan test wawancara?")]
        [Display(Name = "Target", Prompt = "Orang yang melakukan test wawancara")]
        public string Target { get; set; }
    }
}
