using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FrontEnd.Web.Mvc.Models.TataUsaha
{
    public class KelolaMutasiKeluarModel
    {
        public CrudMutasiKeluar MutasiKeluar { get; set; }
        public List<CrudMutasiKeluar> ListMutasiKeluar { get; set; }
    }
    public class CrudMutasiKeluar
    {
        public int SiswaId { get; set; }
        [Display(Name ="Nomor Induk Siswa")]
        public string Nis { get; set; }
        [Display(Name ="Nama Lengkap")]
        public string NamaLengkap { get; set; }
        [Display(Name = "Kelas")]
        public string Kelas { get; set; }
        [Display(Name ="Tujuan", Prompt ="Sekolah yang akan dituju siswa")]
        [Required(ErrorMessage ="Tujuan sekolah tidak boleh kosong")]
        public string Tujuan { get; set; }
        [Display(Name ="Alasan", Prompt ="Alasan siswa pindah")]
        [Required(ErrorMessage ="Alasan tidak boleh kosong")]
        public string Alasan { get; set; }
        [Display(Name = "Tanggal Keluar")]
        [Required(ErrorMessage = "Tanggal keluar tidak boleh kosong")]
        [DataType(DataType.Date)]
        public DateTime TanggalKeluar { get; set; } = DateTime.Now;
    }
}
