using BackEnd.Domains;
using System;
using System.Collections.Generic;
using System.Text;

namespace BackEnd.Abstraction
{
    public interface ISiswa
    {
        List<Siswa> GetAllSiswa();
        Siswa GetDetailSiswa(int id);
        List<Siswa> GetAllMutasiKeluar();
        MutasiKeluar GetMutasiKeluar(int id);
        void NewMutasiKeluar(MutasiKeluar mutasi);
        Siswa SearchSiswaForMutasiKeluar(string nis);
        List<Siswa> GetAllSiswaNotYetGetKelas();
    }
}
