using BackEnd.Domains;
using System;
using System.Collections.Generic;
using System.Text;

namespace BackEnd.Abstraction
{
    public interface IKelas
    {
        List<Kelas> GetAllKelas();
        void CreateNewKelas(Kelas newData);
        void UpdateKelas(Kelas updateData);
        void DeleteKelas(int idKelas);
        Kelas GetDetailKelas(int idSiswa);
        void AddSiswaToKelas(int idSiswa, int idKelas);
        void DeleteSiswaFromKelas(int idSiswa);
        void SetPath();
        void AutoMapKelas();
        List<Siswa> GetMemberKelas(int idKelas);
    }
}
