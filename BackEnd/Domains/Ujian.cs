using System;
using System.Collections.Generic;
using System.Text;

namespace BackEnd.Domains
{
    public class Ujian
    {
        public int AkunPendaftaranId { get; set; }
        public int SoalId { get; set; }
        public DateTime WaktuBerakhir { get; set; }
        public bool IsDone { get; set; }
    }
}
