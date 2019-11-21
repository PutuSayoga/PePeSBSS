using BackEnd.Domains;
using System;
using System.Collections.Generic;
using System.Text;

namespace BackEnd.Abstraction
{
    public interface ITesPenerimaan
    {
        List<int> GetSoalIdPengerjaan(string noPendaftaran);
        void Submit(IEnumerable<HasilTes> listJawaban, string noPendataran);
    }
}
