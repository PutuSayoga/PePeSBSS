using BackEnd.Domains;
using System;
using System.Collections.Generic;
using System.Text;

namespace BackEnd.Abstraction
{
    public interface IUjian
    {
        bool? IsDone(int akunPendaftaranId, int soalId);
        Ujian GetUjian(int akunId, int soalId);
        void StartUjianAkademik(int akunId, int soalId);
        void SaveAnswerAkademik(HasilTes jawaban);
        string GetAnswerAkademik(int akunId, int soalId, int pertanyaanId);
        void FinishUjianAkademik(int akunId, int soalId);
        void SaveWawancara(List<HasilTes> listHasil);
        int GetSoalPengerjaanAkademikId(string noPendaftaran, string kategoriSoal);
        int GetSoalPengerjaanWawancaraId(string noPendaftaran, string targetSoal);
    }
}
