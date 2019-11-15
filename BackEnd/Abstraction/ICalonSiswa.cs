using BackEnd.Domains;
using System;
using System.Collections.Generic;
using System.Text;

namespace BackEnd.Abstraction
{
    public interface ICalonSiswa
    {
        bool IsLogin(string noPendaftaran, string password);
        void SaveDataDiri(string namaLengkap, DataDiri newData);
        void SaveDataAkademikTerakhir(AkademikTerakhir newData);
        void SaveDataPenunjang(Penunjang newData);
        void SaveDataPenanggunjawab(List<Penanggungjawab> newData);
        void SaveDataPrestasi(Prestasi newData);
        void SaveDataRapor(List<Rapor> newData);

        CalonSiswa GetDetail(int akunId);

        string CekStatus(int akunId);
    }
}
