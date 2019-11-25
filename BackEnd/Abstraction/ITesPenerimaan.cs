using BackEnd.Domains;
using System;
using System.Collections.Generic;
using System.Text;

namespace BackEnd.Abstraction
{
    public interface ITesPenerimaan
    {
        IEnumerable<int> GetSoalIdPengerjaan(string noPendaftaran);
        void Submit(IEnumerable<HasilTes> listJawaban, string noPendataran);
    }
}
