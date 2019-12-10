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
        int NewRegist(AkunPendaftaran newAkun);
        AkunPendaftaran GetAkunPendaftaran(int akunId);
        AkunPendaftaran SearchAkunPendaftaran(string noPendaftaran);
        int GetAkunPendaftaranId(string noPedaftaran);
    }
}
