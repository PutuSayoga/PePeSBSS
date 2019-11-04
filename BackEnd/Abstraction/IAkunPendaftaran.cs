using BackEnd.Domains;
using System;
using System.Collections.Generic;
using System.Text;

namespace BackEnd.Abstraction
{
    interface IAkunPendaftaran
    {
        IEnumerable<AkunPendaftaran> GetAllAkunPendaftaran();
        int AddAkunPendaftaran(AkunPendaftaran newAkun);

        IEnumerable<AkunPendaftaran> GetAllAkunPendaftaranDaftarUlang();
    }
}
