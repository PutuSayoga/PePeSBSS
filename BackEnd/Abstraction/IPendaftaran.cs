using BackEnd.Domains;
using System;
using System.Collections.Generic;
using System.Text;

namespace BackEnd.Abstraction
{
    public interface IPendaftaran
    {
        IEnumerable<AkunPendaftaran> GetAllAkunPendaftaran();
        IEnumerable<AkunPendaftaran> GetAllDaftarUlang();
        void Re_Regis(int id);
        int AddNewAkunPendaftaran(AkunPendaftaran newAkun);
        AkunPendaftaran GetDetailAkunPendaftaran(int id);
    }
}
