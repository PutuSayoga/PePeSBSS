
using System.ComponentModel.DataAnnotations;

namespace FrontEnd.Web.Mvc.Models.CalonSiswa
{
    public class KelolaDataPenunjangModel
    {
        [Required(ErrorMessage = "Pembiaya tidak boleh kosong")]
        [Display(Name = "Yang Membiayai", Prompt = "Orang yang membiayai keperluan sekolah")]
        public string Pembiaya { get; set; }
        [Display(Name = "Status Tempat Tinggal", Prompt = "Status rumah yang ditempati")]
        public string StatusTempatTinggal { get; set; }
        [Display(Name = "Daya Listrik", Prompt = "Daya listrik rumah yang ditempati")]
        public int? DayaListrik { get; set; }
        [Display(Name = "Jarak Tempuh", Prompt = "Dari rumah ke SMA BSS")]
        public double? JarakTempuh { get; set; }
        [Display(Name = "Waktu Tempuh", Prompt = "Dari rumah ke SMA BSS")]
        public byte? WaktuTempuh { get; set; }
        [Display(Name = "Transportasi", Prompt = "Kendaraan yang digunakan untuk pergi ke sekolah")]
        public string Transportasi { get; set; }
    }
}
