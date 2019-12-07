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
        List<AkunPendaftaran> GetAllAkunPendaftaranMutasi();
        void ReRegist(int akunId);
        string AddNewAkunPendaftaran(AkunPendaftaran newAkun);
        AkunPendaftaran GetAkunPendaftaran(int akunId);
        int GetAkunPendaftaranId(string noPedaftaran);
    }
}
