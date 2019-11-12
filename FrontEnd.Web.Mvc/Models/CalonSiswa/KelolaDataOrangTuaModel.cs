using System;
using System.ComponentModel.DataAnnotations;

namespace FrontEnd.Web.Mvc.Models.CalonSiswa
{
    public class KelolaDataOrangTuaModel
    {
        [Required]
        public string NamaLengkapAyah { get; set; }
        [Required]
        public string TempatLahirAyah { get; set; }
        [Required]
        public DateTime TanggalLahirAyah { get; set; }
        [Required]
        public string AlamatAyah { get; set; }
        [Required]
        public string AgamaAyah { get; set; }
        [Required]
        public string PendidikanTerakhirAyah { get; set; }
        public string PekerjaanAyah { get; set; }
        public int PenghasilanAyah { get; set; }
        public string NoTelpAyah { get; set; }
        [Required]
        public string NoHpAyah { get; set; }
        public string EmailAyah { get; set; }
        public string StatusDalamKeluargaAyah { get; set; }
        public string KeteranganAyah { get; set; }

        [Required]
        public string NamaLengkapIbu { get; set; }
        [Required]
        public string TempatLahirIbu { get; set; }
        [Required]
        public DateTime TanggalLahirIbu { get; set; }
        [Required]
        public string AlamatIbu { get; set; }
        [Required]
        public string AgamaIbu { get; set; }
        [Required]
        public string PendidikanTerakhirIbu { get; set; }
        public string PekerjaanIbu { get; set; }
        public int PenghasilanIbu { get; set; }
        public string NoTelpIbu { get; set; }
        [Required]
        public string NoHpIbu { get; set; }
        [Required]
        public string EmailIbu { get; set; }
        public string StatusDalamKeluargaIbu { get; set; }
        public string KeteranganIbu { get; set; }

        public string NamaLengkapWali { get; set; }
        public string TempatLahirWali { get; set; }
        public DateTime TanggalLahirWali { get; set; }
        public string AlamatWali { get; set; }
        public string AgamaWali { get; set; }
        public string PendidikanTerakhirWali { get; set; }
        public string PekerjaanWali { get; set; }
        public int PenghasilanWali { get; set; }
        public string NoTelpWali { get; set; }
        public string NoHpWali { get; set; }
        public string EmailWali { get; set; }
    }
}
