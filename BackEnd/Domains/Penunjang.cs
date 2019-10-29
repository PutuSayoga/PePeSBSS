using System;
using System.Collections.Generic;

namespace BackEnd.Domains
{
    public partial class Penunjang
    {
        public int? CalonSiswaId { get; set; }
        public string Pembiaya { get; set; }
        public string StatusTempatTinggal { get; set; }
        public int? DayaListrik { get; set; }
        public byte? JarakTempuh { get; set; }
        public byte? WaktuTempuh { get; set; }
        public string Transportasi { get; set; }

        public virtual CalonSiswa CalonSiswa { get; set; }
    }
}
