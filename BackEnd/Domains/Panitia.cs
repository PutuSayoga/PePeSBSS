using System;
using System.Collections.Generic;

namespace BackEnd.Domains
{
    public partial class Panitia
    {
        public int? StaffId { get; set; }
        public string Acara { get; set; }
        public string Divisi { get; set; }

        public virtual Staff Staff { get; set; }
    }
}
