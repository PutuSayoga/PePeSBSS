using BackEnd.Domains;
using System;
using System.Collections.Generic;
using System.Text;

namespace BackEnd.Abstraction
{
    public interface ISeleksiPenerimaan
    {
        IEnumerable<AkunPendaftaran> GetAllJalurKhusus();
        IEnumerable<AkunPendaftaran> GetAllJalurReguler();
        IEnumerable<AkunPendaftaran> GetAllJalurMitra();
        IEnumerable<AkunPendaftaran> GetAllJalurPrestasi();


    }
}
