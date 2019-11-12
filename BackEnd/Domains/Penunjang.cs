using System;
using System.Collections.Generic;

namespace BackEnd.Domains
{
    public class Penunjang
    {
        public int CalonSiswaId { get; set; }
        public string Pembiaya { get; set; }
        public string StatusTempatTinggal { get; set; }
        public int DayaListrik { get; set; }
        public int JarakTempuh { get; set; }
        public int WaktuTempuh { get; set; }
        public string Transportasi { get; set; }

        public CalonSiswa CalonSiswa { get; set; }
    }
}
