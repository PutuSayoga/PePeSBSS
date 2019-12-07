using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
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
        public int SoalMipaKhusus { get; set; }
        public int SoalIpsKhusus { get; set; }
        public int SoalTpaKhusus { get; set; }
        public int SoalWawancaraCalonSiswaKhusus { get; set; }
        public int SoalWawancaraOrangTuaKhusus { get; set; }
        public int SoalMipaReguler { get; set; }
        public int SoalIpsReguler { get; set; }
        public int SoalTpaReguler { get; set; }
        public int SoalWawancaraCalonSiswaReguler { get; set; }
        public int SoalWawancaraOrangTuaReguler { get; set; }
        public int SoalMipaMutasi { get; set; }
        public int SoalIpsMutasi { get; set; }
        public int SoalTpaMutasi { get; set; }
        public int SoalWawancaraCalonSiswaMutasi { get; set; }
        public int SoalWawancaraOrangTuaMutasi { get; set; }
        public int SoalWawancaraCalonSiswaPrestasi { get; set; }
        public int SoalWawancaraOrangTuaPrestasi { get; set; }
        public int SoalWawancaraCalonSiswaMitra { get; set; }
        public int SoalWawancaraOrangTuaMitra { get; set; }
    }
}





