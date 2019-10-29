using System;
using System.Collections.Generic;

namespace BackEnd.Domains
{
    public partial class Jadi
    {
        public int? SiswaId { get; set; }
        public int? CalonSiswaId { get; set; }
        public DateTime? TanggalMasuk { get; set; }

        public virtual CalonSiswa CalonSiswa { get; set; }
        public virtual Siswa Siswa { get; set; }
    }
}
