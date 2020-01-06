using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FrontEnd.Web.Mvc.Models.Auth
{
    public class LoginStaffModel
    {
        [Required]
        [Display(Name = "Username", Prompt = "Masukkan username anda")]
        public string Username { get; set; }
        [Required]
        [Display(Name = "Password", Prompt = "Masukkan password anda")]
        public string Password { get; set; }
        [Required]
        [Display(Name = "Login Sebagai", Prompt = "Pilih peran")]
        public string Role { get; set; }
    }
}
