using FrontEnd.Web.Mvc.Models.CalonSiswa;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FrontEnd.Web.Mvc.Models.PsbTes
{
    public class SeleksiJalurPrestasiModel
    {
        public IEnumerable<AkunPrestasi> ListJalurPrestasi { get; set; }
    }
    public class AkunPrestasi
    {
        public int Id { get; set; }
        public string NoPendaftaran { get; set; }
        public string NamaLengkap { get; set; }
        public KelolaDataRaporModel DataRapor { get; set; }
    }
}
