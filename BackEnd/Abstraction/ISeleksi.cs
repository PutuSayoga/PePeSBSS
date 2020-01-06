using BackEnd.Domains;
using System;
using System.Collections.Generic;
using System.Text;

namespace BackEnd.Abstraction
{
    public interface ISeleksi
    {
        List<AkunPendaftaran> GetAllWithJalur(string jalur);
        string UpdateStatusPendaftar(string noPendaftaran, bool isLolos);
        void UpdateStatusReguler(int totalLolos);
    }
}
