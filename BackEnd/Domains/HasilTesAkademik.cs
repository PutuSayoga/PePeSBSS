using System;
using System.Collections.Generic;

namespace BackEnd.Domains
{
    public partial class HasilTesAkademik
    {
        public int? AkunPendaftaranId { get; set; }
        public double? NilaiMipa { get; set; }
        public double? NilaiIps { get; set; }
        public double? NilaiTpa { get; set; }

        public virtual AkunPendaftaran AkunPendaftaran { get; set; }
    }
}
