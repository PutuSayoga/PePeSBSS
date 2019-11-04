using BackEnd.Domains;
using System;
using System.Collections.Generic;
using System.Text;

namespace BackEnd.Abstraction
{
    public interface ISoalPenerimaan
    {
        IEnumerable<Soal> GetAllSoalAkademik();
        Soal GetDetailSoalAkademik(int id);
        void AddSoal(Soal newSoal);
        void UpdateSoal(Soal newData);
        void DeleteSoal(int id);

        void AddPertanyaan(int soalId, Pertanyaan newPertanyaan);
        void DeletePertanyaan(int soalId, int id);
        void UpdatePertanyaan(int soalId, Pertanyaan newData);
    }
}
