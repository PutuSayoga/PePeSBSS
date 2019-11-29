using BackEnd.Domains;
using System;
using System.Collections.Generic;
using System.Text;

namespace BackEnd.Abstraction
{
    public interface IKelas
    {
        IEnumerable<Kelas> GetAllKelas();
        void InsertIntoKelas(int calonSiswaId);

    }
}
