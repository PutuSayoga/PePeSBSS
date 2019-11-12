using System;
using System.Collections.Generic;

namespace BackEnd.Domains
{
    public class Rapor
    {
        public int CalonSiswaId { get; set; }
        public string MataPelajaran { get; set; }
        public double? Semester1 { get; set; }
        public double? Semester2 { get; set; }
        public double? Semester3 { get; set; }
        public double? Semester4 { get; set; }
        public double? Semester5 { get; set; }

        public CalonSiswa ACalonSiswa { get; set; }
    }
}
