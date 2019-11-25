using BackEnd.Domains;
using System;
using System.Collections.Generic;
using System.Text;

namespace BackEnd.Abstraction
{
    public interface ISoalPenerimaan
    {
        IEnumerable<Soal> GetAllSoalAkademik();
        IEnumerable<Soal> GetAllSoalWawancara();

        Soal GetDetailSoal(int id);
        Soal GetSimpleSoal(int id);
        void AddSoal(Soal newSoal);
        void UpdateSoal(Soal newData);
        void DeleteSoal(int id);

        Pertanyaan GetPertanyaan(int pertanyaanId, int soalId);
        void AddPertanyaan(Pertanyaan newPertanyaan);
        void DeletePertanyaan(int soalId, int pertanyaanId);
        void UpdatePertanyaan(Pertanyaan newData);
    }
}
