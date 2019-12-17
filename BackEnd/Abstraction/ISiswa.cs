using BackEnd.Domains;
using System;
using System.Collections.Generic;
using System.Text;

namespace BackEnd.Abstraction
{
    public interface ISiswa
    {
        List<Siswa> GetAllSiswa();
        List<Siswa> GetAllMutasiKeluar();
        void NewMutasiKeluar(MutasiKeluar mutasi);
        MutasiKeluar GetMutasiKeluar(int id);
        Siswa SearchSiswa(string nis);
        Siswa GetDetailSiswa(int id);
    }
}
