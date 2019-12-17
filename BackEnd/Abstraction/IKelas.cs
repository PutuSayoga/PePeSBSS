using BackEnd.Domains;
using System;
using System.Collections.Generic;
using System.Text;

namespace BackEnd.Abstraction
{
    public interface IKelas
    {
        List<Kelas> GetAllKelas();
        void AddSiswaToKelas(int calonSiswaId);
        void DeleteSiswaFromKelas();
        void CreateNewKelas();
        void UpdateKelas();
        void DeleteKelas();
        void GetDetailKelas();

    }
}
