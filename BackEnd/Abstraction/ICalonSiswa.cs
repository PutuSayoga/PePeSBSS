using BackEnd.Domains;
using System;
using System.Collections.Generic;
using System.Text;

namespace BackEnd.Abstraction
{
    public interface ICalonSiswa
    {
        bool IsLogin(string noPendaftaran, string password);
        void SaveDataDiri(string noPendaftaran, string namaLengkap, DataDiri newData);
        void SaveDataAkademikTerakhir(string noPendaftaran, AkademikTerakhir newData);
        void SaveDataPenunjang(string noPendaftaran, Penunjang newData);
        void SaveDataPenanggunjawab(string noPendaftaran, List<Penanggungjawab> newData);
        void SaveDataPrestasi(string noPendaftaran, Prestasi newData);
        void SaveDataRapor(string noPendaftaran, List<Rapor> newData);

        AkunPendaftaran GetAllDetail(string noPendaftaran);
        AkunPendaftaran GetDetailDiri(string noPendaftaran);
        AkunPendaftaran GetDetailPenanggungJawab(string noPendaftaran);
        AkunPendaftaran GetDetailPenunjang(string noPendaftaran);
        AkunPendaftaran GetDetailRapor(string noPendaftaran);
        AkunPendaftaran GetDetailPrestasi(string noPendaftaran);
        AkunPendaftaran GetDetailAkademikTerakhir(string noPendaftaran);

        AkunPendaftaran CekStatus(string noPendaftaran);
    }
}
