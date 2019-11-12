
using System.ComponentModel.DataAnnotations;

namespace FrontEnd.Web.Mvc.Models.CalonSiswa
{
    public class KelolaDataPenunjangModel
    {
        [Required]
        public string Pembiaya { get; set; }
        public string StatusTempatTinggal { get; set; }
        public int DayaListrik { get; set; }
        public byte JarakTempuh { get; set; }
        public byte WaktuTempuh { get; set; }
        public string Transportasi { get; set; }
    }
}
