using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FrontEnd.Web.Mvc.Models.WakaKesiswaan
{
    public class KelolaKelasModel
    {
        public List<CrudKelas> ListKelas { get; set; }
        public CrudKelas CrudKelas { get; set; }
    }
    public class CrudKelas
    {
        public int Id { get; set; }
        [Display(Name ="Nama Kelas", Prompt ="Nama kelas yang dibuat")]
        [Required(ErrorMessage ="Nama kelas tidak boleh kosong")]
        public string NamaKelas { get; set; }
        [Display(Name ="Kategori", Prompt ="Kategori kelas")]
        [Required(ErrorMessage = "Kategori tidak boleh kosong")]
        public string Kategori { get; set; }
        [Display(Name ="Tingkat", Prompt ="Tingkatan kelas")]
        [Required(ErrorMessage ="Tingkat kelas tidak boleh kosong")]
        public byte Tingkat { get; set; }
        [Display(Name ="Maksimal Siswa", Prompt ="Jumlah maksimal siswa kelas ini")]
        [Required(ErrorMessage ="Jumlah maksimal siswa tidak boleh kosong")]
        [Range(1, byte.MaxValue)]
        public byte? MaxSiswa { get; set; }
        public byte? JumlahSiswa { get; set; }
    }
}
