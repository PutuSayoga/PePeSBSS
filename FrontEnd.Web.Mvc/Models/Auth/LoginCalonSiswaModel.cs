using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FrontEnd.Web.Mvc.Models.Auth
{
    public class LoginCalonSiswaModel
    {
        [Required]
        [Display(Name ="Nomor Pendaftaran", Prompt ="Masukkan nomor pendaftaran mu disini")]
        public string NoPendaftaran { get; set; }
        [Required]
        [Display(Name ="Password", Prompt ="Masukkan password mu disini")]
        public string Password { get; set; }
    }
}
