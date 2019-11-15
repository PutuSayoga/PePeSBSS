using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FrontEnd.Web.Mvc.Models.PsbPendaftaran
{
    public class DaftarBaruModel
    {
        [Required]
        public string JalurPendaftaran { get; set; }
        [Required]
        public string NamaLengkap { get; set; }
        [Required]
        public string Nik { get; set; }
        [Required]
        public string Nisn { get; set; }
        [Required]
        public DateTime JadwalTes { get; set; }
    }
}
