using BackEnd.Domains;
using System;
using System.Collections.Generic;
using System.Text;

namespace BackEnd.Abstraction
{
    public interface ISiswa
    {
        IEnumerable<Siswa> GetAllSiswa();
        void AddSiswa(Siswa newSiswa);
        Siswa CreateNewSiswa(int calonSiswaId);
    }
}
