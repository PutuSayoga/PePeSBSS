using System;
using System.Collections.Generic;

namespace BackEnd.Domains
{
    public class AkademikTerakhir
    {
        public int CalonSiswaId { get; set; }
        public string NamaSekolah { get; set; }
        public string JenisSekolah { get; set; }
        public string StatusSekolah { get; set; }
        public string AlamatSekolah { get; set; }
        public string NoPesertaUn { get; set; }
        public string NoSeriSkhun { get; set; }
        public string NoSeriIjazah { get; set; }

        public CalonSiswa CalonSiswa { get; set; }
    }
}
