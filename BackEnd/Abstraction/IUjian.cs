using BackEnd.Domains;
using System;
using System.Collections.Generic;
using System.Text;

namespace BackEnd.Abstraction
{
    public interface IUjian
    {
        bool? IsDone(int akunPendaftaranId, int soalId);
        void StartUjian(int akunId, int soalId);
        void SaveAnswer(HasilTes jawaban);
        string GetAnswer(int akunId, int soalId, int pertanyaanId);
        Ujian GetUjian(int akunId, int soalId);
        void FinishUjian(int akunId, int soalId);
        int GetSoalPengerjaanAkademikId(string noPendaftaran, string kategoriSoal);
        int GetSoalPengerjaanWawancaraId(string noPendaftaran, string targetSoal);
    }
}
