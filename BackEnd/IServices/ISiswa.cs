using BackEnd.Domains;
using System;
using System.Collections.Generic;
using System.Text;

namespace BackEnd.IServices
{
    public interface ISiswa
    {
        IEnumerable<Siswa> GetAllSiswa();
        int RegisterToKelas(int siswaId, int kelasId);
    }
}
