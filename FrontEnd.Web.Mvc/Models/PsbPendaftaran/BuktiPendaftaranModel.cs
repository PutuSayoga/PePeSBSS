using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FrontEnd.Web.Mvc.Models.PsbPendaftaran
{
    public class BuktiPendaftaranModel
    {
        public int Id { get; set; }
        [Display(Name ="Nomor Pendaftaran")]
        public string NoPendaftaran { get; set; }
        [Display(Name ="Nama Lengkap")]
        public string NamaLengkap { get; set; }
        [Display(Name ="Jalur Pendaftaran")]
        public string JalurPendaftaran { get; set; }
        [Display(Name ="Status Pembayaran")]
        public bool isLunas { get { return true; } }
        [Display(Name ="Password")]
        public string Password { get; set; }
        [Display(Name ="Jadwal Tes")]
        public string JadwalTes { get; set; }
    }
}
