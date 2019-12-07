using BackEnd.Domains;
using System;
using System.Collections.Generic;
using System.Text;

namespace BackEnd.Abstraction
{
    public interface ISiswa
    {
        List<Siswa> GetAllSiswa();
        void CreateMutasiKeluar(MutasiKeluar mutasi);
        MutasiKeluar GetMutasiKeluar(int id);
        int GetSiswaId(string nis);
    }
}
