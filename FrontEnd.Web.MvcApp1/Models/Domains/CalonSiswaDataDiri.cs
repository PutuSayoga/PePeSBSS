using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FrontEnd.Web.MvcApp.Models.Domains
{
    public class CalonSiswaDataDiri
    {
        public int Id { get; set; }
        public string NIK { get; set; }
        public string NamaLengkap { get; set; }
        public string NamaPanggilan { get; set; }
        public bool IsPerempuan { get; set; }
        public string TempatLahir { get; set; }
        public DateTime TanggalLahir { get; set; }
        public string Agama { get; set; }
        public string Alamat { get; set; }
        public int RT { get; set; }
        public int RW { get; set; }
        public string Desa { get; set; }
        public string Kecamatan { get; set; }
        public string Kota { get; set; }
        public string KodePos { get; set; }
        public string NoTelp { get; set; }
        public string NoHp { get; set; }
        public string Email { get; set; }
        public int JumlahSaudara { get; set; }
        public int AnakKe { get; set; }
        public string StatusDalamKeluarga { get; set; }
        /// <summary>
        /// Dalam Cm
        /// </summary>
        public int TinggiBadan { get; set; }
        /// <summary>
        /// Dalam Kg
        /// </summary>
        public int BeratBadan { get; set; }
        public string GolDarah { get; set; }
        public string CitaCita { get; set; }
        public List<string> ListHobi { get; set; }
        public List<string> ListRiwayatSakit { get; set; }
        public List<string> ListKelainanJasmani { get; set; }
        public List<CalonSiswaPrestasi> ListPrestasi { get; set; }
        public List<CalonSiswaNilaiRapor> NilaiRaporSMA { get; set; }
        public List<AkunPendaftaran> ListAkun { get; set; }
        public CalonSiswaPenunjang Penunjang { get; set; }
        public CalonSiswaAkademikLama AkademikLama { get; set; }
        public List<CalonSiswaPenanggungJawab> ListPenanggungungJawab { get; set; }
    }
}
