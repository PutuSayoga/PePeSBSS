using BackEnd.Domains;
using System;
using System.Collections.Generic;
using System.Text;

namespace BackEnd.Abstraction
{
    public interface ISeleksiPenerimaan
    {
        List<AkunPendaftaran> GetAllFinishUjianWithJalur(string jalur);
        void UpdateSelection(int akunId, bool isLolos);
    }
}
