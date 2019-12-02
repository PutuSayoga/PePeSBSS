using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FrontEnd.Web.Mvc.Models.CalonSiswa
{
    public class BiodataModel
    {
        public KelolaDataDiriModel DataDiri { get; set; }
        public KelolaDataAkademikTerakhir DataAkademikTerakhir { get; set; }
        public KelolaDataPrestasiModel DataPrestasi { get; set; }
        public KelolaDataRaporModel DataRapor { get; set; }
        public KelolaDataOrangTuaModel DataOrangTua { get; set; }
        public KelolaDataPenunjangModel DataPenunjang { get; set; }
    }
}
