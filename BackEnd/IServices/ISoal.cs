using BackEnd.Domains;
using System;
using System.Collections.Generic;
using System.Text;

namespace BackEnd.IServices
{
    public interface ISoal
    {
        IEnumerable<Soal> GetAllSoalAkademik();
        Soal GetDetailSoalAkademik(int id);
        string AddSoal(Soal newSoal);
        int UpdateSoal(Soal newData);
        int DeleteSoal(int id);
        IEnumerable<Pertanyaan> GetAllPertanyaan(int idSoal);
        int AddPertanyaan(int soalId, Pertanyaan newPertanyaan);
        int DeletePertanyaan(int soalId, int id);
        int UpdatePertanyaan(int soalId, Pertanyaan newData);
    }
}
