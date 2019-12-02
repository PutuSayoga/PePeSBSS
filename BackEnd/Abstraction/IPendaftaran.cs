using BackEnd.Domains;
using System;
using System.Collections.Generic;
using System.Text;

namespace BackEnd.Abstraction
{
    public interface IPendaftaran
    {
        List<AkunPendaftaran> GetAllAkunPendaftaran();
        List<AkunPendaftaran> GetAllDaftarUlang();
        void ReRegist(int akunId);
        int AddNewAkunPendaftaran(AkunPendaftaran newAkun);
        AkunPendaftaran GetAkunPendaftaran(int akunId);
        int GetIdAkunPendaftaran(string noPedaftaran);
    }
}
