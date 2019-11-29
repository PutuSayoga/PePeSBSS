using System;
using System.Collections.Generic;

namespace BackEnd.Domains
{
    public class RangkumanTesAkademik
    {
        public int? AkunPendaftaranId { get; set; }
        public double? NilaiMipa { get; set; }
        public double? NilaiIps { get; set; }
        public double? NilaiTpa { get; set; }

        public AkunPendaftaran AAkunPendaftaran { get; set; }
    }
}
