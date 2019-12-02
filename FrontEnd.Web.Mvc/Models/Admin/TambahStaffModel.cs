using System.ComponentModel.DataAnnotations;

namespace FrontEnd.Web.Mvc.Models.Admin
{
    public class TambahStaffModel
    {
        [Required(ErrorMessage = "NIP tidak boleh kosong")]
        [StringLength(18, MinimumLength = 18, ErrorMessage = "Panjang NIK 18 karakter")]
        [Display(Name = "NIP", Prompt = "Nomor Induk Pegawai")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Masukkan NIP yang benar")]
        public string Nip { get; set; }

        [Display(Name = "Nama Lengkap", Prompt ="Nama lengkap pegawai")]
        [Required(ErrorMessage = "Nama lengkap tidak boleh kosong")]
        public string NamaLengkap { get; set; }

        [Display(Name = "E-Mail", Prompt ="Masukkan email pegawai")]
        [Required(ErrorMessage = "E-Mail tidak boleh kosong")]
        [EmailAddress(ErrorMessage = "Masukkan alamat email yang benar")]
        public string Email { get; set; }

        [Display(Name = "Nomor Hp", Prompt ="Masukkan nomor Hp")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Masukkan nomor Hp yang benar ex. 08123456789")]
        public string NoHp { get; set; }

        [Display(Name = "Jabatan", Prompt ="Jabatan pegawai")]
        [Required(ErrorMessage = "Jabatan tidak boleh kosong")]
        public string Jabatan { get; set; }

        [Display(Name = "Username", Prompt ="Masukkan username yang ingin dipakai")]
        [Required(ErrorMessage = "Username tidak boleh kosong")]
        public string Username { get; set; }

        [Display(Name = "Password", Prompt ="Masukkan password yang ingin dipakai")]
        [Required(ErrorMessage = "Password tidak boleh kosong")]
        public string Password { get; set; }

        [Display(Name = "Ulangi Password", Prompt ="Ulangi password")]
        [Compare(nameof(Password), ErrorMessage = "Password yang dimasukkan tidak sama")]
        public string ConfirmPassword { get; set; }
    }
}
