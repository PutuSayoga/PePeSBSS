using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FrontEnd.Web.Mvc.Models.Admin
{
    public class PengaturanSoalModel
    {
        public List<SelectListItem> ListSoalMipa { get; set; }
        public List<SelectListItem> ListSoalIps { get; set; }
        public List<SelectListItem> ListSoalTpa { get; set; }
        public List<SelectListItem> ListWawancaraCalonSiswa { get; set; }
        public List<SelectListItem> ListWawancaraOrangTua { get; set; }
        [Display(Name = "Mipa Jalur Khusus")]
        public int? SoalMipaKhusus { get; set; }
        [Display(Name = "Ips Jalur Khusus")]
        public int? SoalIpsKhusus { get; set; }
        [Display(Name = "Tpa Jalur Khusus")]
        public int? SoalTpaKhusus { get; set; }
        [Display(Name = "Wawancara Siswa Jalur Khusus")]
        public int? SoalWawancaraCalonSiswaKhusus { get; set; }
        [Display(Name = "Wawancara Orang Tua Jalur Khusus")]
        public int? SoalWawancaraOrangTuaKhusus { get; set; }
        [Display(Name = "Mipa Jalur Reguler")]
        public int? SoalMipaReguler { get; set; }
        [Display(Name = "Mipa Jalur Reguler")]
        public int? SoalIpsReguler { get; set; }
        [Display(Name = "Mipa Jalur Reguler")]
        public int? SoalTpaReguler { get; set; }
        [Display(Name = "Wawancara Siswa Jalur Reguer")]
        public int? SoalWawancaraCalonSiswaReguler { get; set; }
        [Display(Name = "Wawancara Siswa Jalur Reguler")]
        public int? SoalWawancaraOrangTuaReguler { get; set; }
        [Display(Name = "Mipa Jalur Mutasi")]
        public int? SoalMipaMutasi { get; set; }
        [Display(Name = "Mipa Jalur Mutasi")]
        public int? SoalIpsMutasi { get; set; }
        [Display(Name = "Mipa Jalur Mutasi")]
        public int? SoalTpaMutasi { get; set; }
        [Display(Name = "Wawancara Siswa Jalur Mutasi")]
        public int? SoalWawancaraCalonSiswaMutasi { get; set; }
        [Display(Name = "Wawancara Siswa Jalur Mutasi")]
        public int? SoalWawancaraOrangTuaMutasi { get; set; }
        [Display(Name = "Wawancara Siswa Jalur Prestasi")]
        public int? SoalWawancaraCalonSiswaPrestasi { get; set; }
        [Display(Name = "Wawancara Siswa Jalur Prestasi")]
        public int? SoalWawancaraOrangTuaPrestasi { get; set; }
        [Display(Name = "Wawancara Siswa Jalur Mitra")]
        public int? SoalWawancaraCalonSiswaMitra { get; set; }
        [Display(Name = "Wawancara Siswa Jalur Mitra")]
        public int? SoalWawancaraOrangTuaMitra { get; set; }
    }
}





