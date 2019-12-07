using BackEnd.Domains;
using System;
using System.Collections.Generic;
using System.Text;

namespace BackEnd.Abstraction
{
    public interface IUjian
    {
        int? GetAkademikId(string noPendaftaran, string kategoriSoal);
        int? GetWawancaraId(string noPendaftaran, string targetSoal);
        List<int> GetSoalIdPengerjaan(string noPendaftaran);
        void Submit(List<HasilTes> listJawaban, string noPendataran);
        Ujian StartTest(int akunId, int soalId);
    }
}
