using System;
using System.Collections.Generic;
using System.Text;
using BackEnd.Domains;
using BackEnd.Abstraction;
using System.Data.SqlClient;
using Dapper;
using System.Linq;

namespace BackEnd.Services
{
    public class CalonSiswaService : ICalonSiswa
    {
        private readonly IDbConnectionHelper _connectionHelper;
        private readonly ISecurityRelate _securityRelateHelper;
        public CalonSiswaService(IDbConnectionHelper connectionHelper, ISecurityRelate securityRelateHelper)
        {
            _connectionHelper = connectionHelper;
            _securityRelateHelper = securityRelateHelper;
        }

        public AkunPendaftaran CekStatus(string noPendaftaran)
        {
            string sqlQuery = @"SELECT Status FROM AkunPendaftaran WHERE NoPendaftaran = @NoPendaftaran";
            using (var connection = new SqlConnection(_connectionHelper.GetConnectionString()))
            {
                connection.Open();

                var result = connection.QueryFirst<AkunPendaftaran>(
                    sql: sqlQuery,
                    param: new { NoPendaftaran = noPendaftaran });

                return result;
            }
        }

        public AkunPendaftaran GetAllDetail(string noPendaftaran)
        {
            throw new NotImplementedException();
        }

        public AkunPendaftaran GetDetailAkademikTerakhir(string noPendaftaran)
        {
            string sqlQuery = @"SELECT ap.JalurPendaftaran, ap.NoPendaftaran, cs.*, at.* 
                                FROM AkunPendaftaran ap FULL JOIN CalonSiswa cs ON ap.CalonSiswaId = cs.Id
                                Full JOIN AkademikTerakhir at ON cs.Id = at.CalonSiswaId
                                WHERE NoPendaftaran = @NoPendaftaran";
            using (var connection = new SqlConnection(_connectionHelper.GetConnectionString()))
            {
                connection.Open();
                var result = connection.Query<AkunPendaftaran, CalonSiswa, AkademikTerakhir, AkunPendaftaran>(
                    sql: sqlQuery,
                    map: (ap, cs, at) =>
                    {
                        ap.ACalonSiswa = cs;
                        cs.AAkademikTerakhir = at;
                        return ap;
                    },
                    splitOn: "Id, CalonSiswaId",
                    param: new { NoPendaftaran = noPendaftaran }).First();

                return result;
            }
        }

        public AkunPendaftaran GetDetailDiri(string noPendaftaran)
        {
            string sqlQuery = @"SELECT ap.JalurPendaftaran, ap.NoPendaftaran, cs.*, dd.* 
                                FROM AkunPendaftaran ap FULL JOIN CalonSiswa cs ON ap.CalonSiswaId = cs.Id
                                Full JOIN DataDiri dd ON cs.Id = dd.CalonSiswaId
                                WHERE NoPendaftaran = @NoPendaftaran";
            using (var connection = new SqlConnection(_connectionHelper.GetConnectionString()))
            {
                connection.Open();
                var result = connection.Query<AkunPendaftaran, CalonSiswa, DataDiri, AkunPendaftaran>(
                    sql: sqlQuery,
                    map: (ap, cs, dd) =>
                    {
                        ap.ACalonSiswa = cs;
                        cs.ADataDiri = dd;
                        return ap;
                    },
                    splitOn: "Id, CalonSiswaId",
                    param: new { NoPendaftaran = noPendaftaran }).First();

                return result;
            }
        }

        public AkunPendaftaran GetDetailPenanggungJawab(string noPendaftaran)
        {
            string sqlQuery = @"SELECT ap.JalurPendaftaran, ap.NoPendaftaran, cs.* 
                                FROM AkunPendaftaran ap FULL JOIN CalonSiswa cs ON ap.CalonSiswaId = cs.Id
                                WHERE NoPendaftaran = @NoPendaftaran
                                SELECT * FROM Penanggungjawab WHERE CalonSiswaId = 
                                (SELECT CalonSiswaId FROM AkunPendaftaran WHERE NoPendaftaran=@NoPendaftaran)";
            using (var connection = new SqlConnection(_connectionHelper.GetConnectionString()))
            {
                connection.Open();
                using (var multiResult = connection.QueryMultiple(sqlQuery, new { NoPendaftaran = noPendaftaran }))
                {
                    var akun = multiResult.Read<AkunPendaftaran, CalonSiswa, AkunPendaftaran>(
                        (ap, cs) =>
                        {
                            ap.ACalonSiswa = cs;
                            return ap;
                        },
                        splitOn: "Id").First();
                    akun.ACalonSiswa.PenanggungjawabS = multiResult.Read<Penanggungjawab>().ToList();

                    return akun;
                }
            }
        }

        public AkunPendaftaran GetDetailPenunjang(string noPendaftaran)
        {
            string sqlQuery = @"SELECT ap.JalurPendaftaran, ap.NoPendaftaran, cs.*, p.* 
                                FROM AkunPendaftaran ap FULL JOIN CalonSiswa cs ON ap.CalonSiswaId = cs.Id
                                Full JOIN Penunjang p ON cs.Id = p.CalonSiswaId
                                WHERE NoPendaftaran = @NoPendaftaran";
            using (var connection = new SqlConnection(_connectionHelper.GetConnectionString()))
            {
                connection.Open();
                var result = connection.Query<AkunPendaftaran, CalonSiswa, Penunjang, AkunPendaftaran>(
                    sql: sqlQuery,
                    map: (ap, cs, p) =>
                    {
                        ap.ACalonSiswa = cs;
                        cs.APenunjang = p;
                        return ap;
                    },
                    splitOn: "Id, CalonSiswaId",
                    param: new { NoPendaftaran = noPendaftaran }).First();

                return result;
            }
        }

        public AkunPendaftaran GetDetailPrestasi(string noPendaftaran)
        {
            string sqlQuery = @"SELECT ap.JalurPendaftaran, ap.NoPendaftaran, cs.*, p.* 
                                FROM AkunPendaftaran ap FULL JOIN CalonSiswa cs ON ap.CalonSiswaId = cs.Id
                                Full JOIN Prestasi p ON cs.Id = p.CalonSiswaId
                                WHERE NoPendaftaran = @NoPendaftaran";
            using (var connection = new SqlConnection(_connectionHelper.GetConnectionString()))
            {
                connection.Open();
                var result = connection.Query<AkunPendaftaran, CalonSiswa, Prestasi, AkunPendaftaran>(
                    sql: sqlQuery,
                    map: (ap, cs, p) =>
                    {
                        ap.ACalonSiswa = cs;
                        cs.APrestasi = p;
                        return ap;
                    },
                    splitOn: "Id, CalonSiswaId",
                    param: new { NoPendaftaran = noPendaftaran }).First();

                return result;
            }
        }

        public AkunPendaftaran GetDetailRapor(string noPendaftaran)
        {
            string sqlQuery = @"SELECT ap.JalurPendaftaran, ap.NoPendaftaran, cs.* 
                                FROM AkunPendaftaran ap FULL JOIN CalonSiswa cs ON ap.CalonSiswaId = cs.Id
                                WHERE NoPendaftaran = @NoPendaftaran
                                SELECT * FROM Rapor WHERE CalonSiswaId = 
                                (SELECT CalonSiswaId FROM AkunPendaftaran WHERE NoPendaftaran=@NoPendaftaran)";
            using (var connection = new SqlConnection(_connectionHelper.GetConnectionString()))
            {
                connection.Open();
                using (var multiResult = connection.QueryMultiple(sqlQuery, new { NoPendaftaran = noPendaftaran }))
                {
                    var akun = multiResult.Read<AkunPendaftaran, CalonSiswa, AkunPendaftaran>(
                        (ap, cs) =>
                        {
                            ap.ACalonSiswa = cs;
                            return ap;
                        },
                        splitOn: "Id").First();
                    akun.ACalonSiswa.RaporS = multiResult.Read<Rapor>().ToList();

                    return akun;
                }
            }
        }

        public bool IsLogin(string noPendaftaran, string password)
        {
            string sqlQuery = @"SELECT NoPendaftaran FROM AkunPendaftaran 
                                WHERE NoPendaftaran = @NoPendaftaran AND Password = @Password";
            password = _securityRelateHelper.Encrypt(password);
            using (var connection = new SqlConnection(_connectionHelper.GetConnectionString()))
            {
                connection.Open();
                var result = connection.ExecuteScalar(
                    sql: sqlQuery,
                    param: new { NoPendaftaran = noPendaftaran, Password = password });
                return result != null;
            }
        }

        public void SaveDataAkademikTerakhir(string noPendaftaran, AkademikTerakhir newData)
        {
            throw new NotImplementedException();
        }

        public void SaveDataDiri(string noPendaftaran, string namaLengkap, DataDiri newData)
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
                string sqlQuery = @"SELECT CalonSiswaId FROM AkunPendaftaran WHERE NoPendaftaran = @NoPendaftaran";
                string sqlQuery2 = @"INSERT INTO DataDiri(NamaPanggilan, IsPerempuan, TempatLahir, TanggalLahir, Alamat, Agama, Rt, Rw, Dusun_Desa_Lurah, Kecamatan, Kota_Kabupaten, KodePos, NoTelp, NoHp, Email, JumlahSaudara, AnakKe, StatusDalamKeluarga, TinggiBadan, BeratBadan, GolDarah, CitaCita, Hobi, RiwayatSakit, KelainanJasmani) 
                                    VALUES(@NamaPanggilan, @IsPerempuan, @TempatLahir, @TanggalLahir, @Alamat, @Agama, @Rt, @Rw, @DusunDesaLurah, @Kecamatan, @KotaKabupaten, @KodePos, @NoTelp, @NoHp, @Email, @JumlahSaudara, @AnakKe, @StatusDalamKeluarga, @TinggiBadan, @BeratBadan, @GolDarah, @CitaCita, @Hobi, @RiwayatSakit, @KelainanJasmani)";
                string sqlQuery3 = @"UPDATE CalonSiswa SET NamaLengkap = @NamaLengkap WHERE Id = @CalonSiswaId";

                using (var connection = new SqlConnection(_connectionHelper.GetConnectionString()))
                {
                    connection.Open();
                    newData.CalonSiswaId = connection.QueryFirst(sql: sqlQuery, param: new { NoPendaftaran = noPendaftaran });
                    using (var trans = connection.BeginTransaction())
                    {
                        try
                        {
                            connection.Execute(sql: sqlQuery2, param: newData, transaction: trans);
                            connection.Execute(sql: sqlQuery3, param: new { NamaLengkap = namaLengkap, CalonSiswaId = newData.CalonSiswaId }, transaction: trans);
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

        public void SaveDataPenanggunjawab(string noPendaftaran, List<Penanggungjawab> newData)
        {
            throw new NotImplementedException();
        }

        public void SaveDataPenunjang(string noPendaftaran, Penunjang newData)
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

        public void SaveDataPrestasi(string noPendaftaran, Prestasi newData)
        {
            throw new NotImplementedException();
        }

        public void SaveDataRapor(string noPendaftaran, List<Rapor> newData)
        {
            throw new NotImplementedException();
        }
    }
}
