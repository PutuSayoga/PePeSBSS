using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FrontEnd.Web.MvcApp.Models.Domains
{
    public class Staff1
    {
        public int Id { get; set; }

        [Display(Name = "NIP")]
        [Required(ErrorMessage = "NIP tidak boleh kosong")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Masukkan NIP yang benar")]
        public string Nip { get; set; }

        [Display(Name = "Nama Lengkap")]
        [Required(ErrorMessage = "Nama lengkap tidak boleh kosong")]
        public string NamaLengkap { get; set; }

        [Display(Name = "E-Mail")]
        [Required(ErrorMessage = "E-Mail tidak boleh kosong")]
        [EmailAddress(ErrorMessage = "Masukkan alamat email yang benar")]
        public string Email { get; set; }

        [Display(Name = "Nomor Hp")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Masukkan nomor Hp yang benar")]
        public string NoHp { get; set; }

        [Display(Name = "Jabatan")]
        [Required(ErrorMessage = "Jabatan tidak boleh kosong")]
        public string Jabatan { get; set; }

        [Display(Name = "Username")]
        [Required(ErrorMessage = "Username tidak boleh kosong")]
        public string Username { get; set; }

        [Display(Name = "Password")]
        [Required(ErrorMessage = "Password tidak boleh kosong")]
        public string Password { get; set; }

        [Display(Name = "Ulangi Password")]
        [Compare(nameof(Password), ErrorMessage = "Password yang dimasukkan tidak sama")]
        public string ConfirmPassword { get; set; }


        public Panitia Panitia { get; set; }
    }
}
