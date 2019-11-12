using System;
using System.Collections.Generic;
using System.Text;
using BackEnd.Domains;
using BackEnd.Abstraction;
using System.Data.SqlClient;
using Dapper;

namespace BackEnd.Services
{
    public class CalonSiswaService : ICalonSiswa
    {
        private readonly IDbConnectionHelper _connectionHelper;
        public CalonSiswaService(IDbConnectionHelper connectionHelper)
            => _connectionHelper = connectionHelper;

        public string CekStatus(int akunId)
        {
            string sqlQuery = @"SELECT Status FROM AkunPendaftaran WHERE Id = @AkunId";
            using (var connection = new SqlConnection(_connectionHelper.GetConnectionString()))
            {
                connection.Open();

                var result = connection.QueryFirst<string>(
                    sql: sqlQuery,
                    param: new { AkunId = akunId });

                return result;
            }
        }

        public CalonSiswa GetDetail(int akunId)
        {

            throw new NotImplementedException();
        }

        public void SaveDataAkademikTerakhir(AkademikTerakhir newData)
        {
            throw new NotImplementedException();
        }

        public void SaveDataDiri(string namaLengkap, DataDiri newData)
        {
            // Cek jika ada
            if (false)
            {
                // update
                string sqlQuery = @"UPDATE Penunjang
                                    SET CalonSiswaId = @CalonSiswaId, NamaPanggilan = @NamaPanggilan, IsPerempuan = @IsPerempuan, TempatLahir = @TempatLahir, TanggalLahir = @TanggalLahir, Alamat = @Alamat, Agama = @Agama, Rt = @Rt, Rw = @Rw,Dusun_Desa_Lurah = @DusunDesaLurah, Kecamatan = @Kecamatan, KotaKabupaten = @KotaKabupaten, KodePos = @KodePos, 
                                        NoTelp = @NoTelp, NoHp = @NoHp, Email = @Email, JumlahSaudara = @JumlahSaudara, AnakKe = @AnakKe, StatusDalamKeluarga = @StatusDalamKeluarga, TinggiBadan = @TinggiBadan, BeratBadan = @BeratBadan, GolDarah = @GolDarah, CitaCita = @CitaCita, Hobi = @Hobi, RiwayatSakit = @RiwayatSakit, KelainanJasmani = @KelainanJasmani 
                                    WHERE CalonSiswaId = @CalonSiswaId";
                string sqlQuery2 = @"UPDATE CalonSiswa SET NamaLengkap = @NamaLengkap WHERE Id = @CalonSiswaId";
                using (var connection = new SqlConnection(_connectionHelper.GetConnectionString()))
                {
                    connection.Open();
                    using (var trans = connection.BeginTransaction())
                    {
                        try
                        {
                            connection.Execute(sql: sqlQuery, param: newData, transaction: trans);
                            connection.Execute(sql: sqlQuery2, param: new { NamaLengkap = namaLengkap, CalonSiswaId = newData.CalonSiswaId }, transaction: trans);
                            trans.Commit();
                        }
                        catch (Exception)
                        {
                            trans.Rollback();
                            throw;
                        }
                    }
                }
            }
            else
            {
                // insert
                string sqlQuery = @"INSERT INTO DataDiri(CalonSiswaId, NamaPanggilan, IsPerempuan, TempatLahir, TanggalLahir, Alamat, Agama, Rt, Rw, Dusun_Desa_Lurah, Kecamatan, Kota_Kabupaten, KodePos, NoTelp, NoHp, Email, JumlahSaudara, AnakKe, StatusDalamKeluarga, TinggiBadan, BeratBadan, GolDarah, CitaCita, Hobi, RiwayatSakit, KelainanJasmani) 
                                    VALUES(@CalonSiswaId, @NamaPanggilan, @IsPerempuan, @TempatLahir, @TanggalLahir, @Alamat, @Agama, @Rt, @Rw, @DusunDesaLurah, @Kecamatan, @KotaKabupaten, @KodePos, @NoTelp, @NoHp, @Email, @JumlahSaudara, @AnakKe, @StatusDalamKeluarga, @TinggiBadan, @BeratBadan, @GolDarah, @CitaCita, @Hobi, @RiwayatSakit, @KelainanJasmani)";
                string sqlQuery2 = @"UPDATE CalonSiswa SET NamaLengkap = @NamaLengkap WHERE Id = @CalonSiswaId";
                using (var connection = new SqlConnection(_connectionHelper.GetConnectionString()))
                {
                    connection.Open();
                    using (var trans = connection.BeginTransaction())
                    {
                        try
                        {
                            connection.Execute(sql: sqlQuery, param: newData, transaction: trans);
                            connection.Execute(sql: sqlQuery2, param: new { NamaLengkap = namaLengkap, CalonSiswaId = newData.CalonSiswaId }, transaction: trans);
                            trans.Commit();
                        }
                        catch (Exception)
                        {
                            trans.Rollback();
                            throw;
                        }
                    }
                }
            }
        }

        public void SaveDataPenanggunjawab(List<Penanggungjawab> newData)
        {
            throw new NotImplementedException();
        }

        public void SaveDataPenunjang(Penunjang newData)
        {
            // Cek jika ada
            if (true)
            {
                // update
                string sqlQuery = @"UPDATE Penunjang
                                    SET Pembiaya = @Pembiaya, StatusTempatTinggal = @StatusTempatTinggal, DayaListrik = @DayaListrik, JarakTempuh = @JarakTempuh, WaktuTempuh = @WaktuTempuh, Transportasi = @Transportasi 
                                    WHERE CalonSiswaId = @CalonSiswaId";
                using (var connection = new SqlConnection(_connectionHelper.GetConnectionString()))
                {
                    connection.Open();
                    connection.Execute(sql: sqlQuery, param: newData);
                }
            }
            else
            {
                // insert
                string sqlQuery = @"INSERT INTO Penunjang(CalonSiswaId, Pembiaya, StatusTempatTinggal, DayaListrik, JarakTempuh, WaktuTempuh, Transportasi) 
                                    VALUES(@CalonSiswaId, @Pembiaya, @StatusTempatTinggal, @DayaListrik, @JarakTempuh, @WaktuTempuh, @Transportasi)";
                using (var connection = new SqlConnection(_connectionHelper.GetConnectionString()))
                {
                    connection.Open();
                    connection.Execute(sql: sqlQuery, param: newData);
                }
            }
        }

        public void SaveDataPrestasi(Prestasi newData)
        {
            throw new NotImplementedException();
        }

        public void SaveDataRapor(List<Rapor> newData)
        {
            throw new NotImplementedException();
        }
    }
}
