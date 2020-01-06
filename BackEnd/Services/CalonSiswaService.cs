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

        #region Not Interface Implementation
        private int GetCalonSiswaId(string noPendaftaran)
        {
            string sqlQuery = @"SELECT CalonSiswaId FROM AkunPendaftaran WHERE NoPendaftaran = @NoPendaftaran";
            using (var connection = new SqlConnection(_connectionHelper.GetConnectionString()))
            {
                connection.Open();
                int calonSiswaId = connection.QueryFirstOrDefault<int>(sql: sqlQuery, param: new { NoPendaftaran = noPendaftaran });

                return calonSiswaId;
            }
        }
        private bool CekExist(int calonSiswaId, string table, string additionalValue = "")
        {
            bool exist;
            string sqlQuery;
            if (additionalValue.Equals(string.Empty))
            {
                sqlQuery = $"SELECT 1 FROM {table} WHERE CalonSiswaId = @CalonSiswaId";
            }
            else
            {
                sqlQuery = $"SELECT 1 FROM {table} WHERE CalonSiswaId = @CalonSiswaId AND Sebagai = @Sebagai";

            }

            using (var connection = new SqlConnection(_connectionHelper.GetConnectionString()))
            {
                connection.Open();
                exist = connection.QueryFirstOrDefault<bool>(sql: sqlQuery, param: new { CalonSiswaId = calonSiswaId, Sebagai = additionalValue });

                return exist;
            }
        }
        #endregion

        public AkunPendaftaran GetSimpleAkun(string noPendaftaran)
        {
            string sqlQuery = @"SELECT * FROM AkunPendaftaran WHERE NoPendaftaran = @NoPendaftaran";
            using (var connection = new SqlConnection(_connectionHelper.GetConnectionString()))
            {
                connection.Open();
                var akun = connection.QueryFirstOrDefault<AkunPendaftaran>(sql: sqlQuery, param: new { NoPendaftaran = noPendaftaran });
                return akun;
            }
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
                        ap.CalonSiswa = cs;
                        cs.AkademikTerakhir = at;
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
                        ap.CalonSiswa = cs;
                        cs.DataDiri = dd;
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
                            ap.CalonSiswa = cs;
                            return ap;
                        },
                        splitOn: "Id").First();
                    akun.CalonSiswa.ListPenanggungjawab = multiResult.Read<Penanggungjawab>().ToList();

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
                        ap.CalonSiswa = cs;
                        cs.Penunjang = p;
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
                        ap.CalonSiswa = cs;
                        cs.Prestasi = p;
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
                            ap.CalonSiswa = cs;
                            return ap;
                        },
                        splitOn: "Id").First();
                    akun.CalonSiswa.ListNilaiRapor = multiResult.Read<Rapor>().ToList();

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
            newData.CalonSiswaId = GetCalonSiswaId(noPendaftaran);
            bool isExist = CekExist(newData.CalonSiswaId, "AkademikTerakhir");
            string sqlQuery;
            if (!isExist)
            {
                sqlQuery = @"INSERT INTO AkademikTerakhir(CalonSiswaId, NamaSekolah, JenisSekolah, StatusSekolah, AlamatSekolah, NoPesertaUn, NoSeriSkhun, NoSeriIjazah) 
                    VALUES(@CalonSiswaId, @NamaSekolah, @JenisSekolah, @StatusSekolah, @AlamatSekolah, @NoPesertaUn, @NoSeriSkhun, @NoSeriIjazah)";
            }
            else
            {
                sqlQuery = @"UPDATE AkademikTerakhir
                    SET NamaSekolah = @NamaSekolah, JenisSekolah = JenisSekolah, StatusSekolah = StatusSekolah, 
                    AlamatSekolah=@AlamatSekolah, NoPesertaUn = @NoPesertaUn, NoSeriSkhun = @NoSeriSkhun, NoSeriIjazah=@NoSeriIjazah
                    WHERE CalonSiswaId = @CalonSiswaId";
            }
            using (var connection = new SqlConnection(_connectionHelper.GetConnectionString()))
            {
                connection.Open();
                connection.Execute(sql: sqlQuery, param: newData);
            }
        }
        public void SaveDataDiri(string noPendaftaran, string namaLengkap, DataDiri newData)
        {
            newData.CalonSiswaId = GetCalonSiswaId(noPendaftaran);
            bool isExist = CekExist(newData.CalonSiswaId, "DataDiri");
            string sqlQuery;
            if (!isExist)
            {
                sqlQuery = @"INSERT INTO DataDiri(CalonSiswaId, NamaPanggilan, IsPerempuan, TempatLahir, TanggalLahir, Alamat, Agama, Rt, Rw, Dusun_Desa_Lurah, Kecamatan, Kota_Kabupaten, KodePos, NoTelp, NoHp, Email, JumlahSaudara, AnakKe, StatusDalamKeluarga, TinggiBadan, BeratBadan, GolDarah, CitaCita, Hobi, RiwayatSakit, KelainanJasmani) 
                    VALUES(@CalonSiswaId, @NamaPanggilan, @IsPerempuan, @TempatLahir, @TanggalLahir, @Alamat, @Agama, @Rt, @Rw, @DusunDesaLurah, @Kecamatan, @KotaKabupaten, @KodePos, @NoTelp, @NoHp, @Email, @JumlahSaudara, @AnakKe, @StatusDalamKeluarga, @TinggiBadan, @BeratBadan, @GolDarah, @CitaCita, @Hobi, @RiwayatSakit, @KelainanJasmani)";
            }
            else
            {
                sqlQuery = @"UPDATE DataDiri
                    SET CalonSiswaId = @CalonSiswaId, NamaPanggilan = @NamaPanggilan, IsPerempuan = @IsPerempuan, TempatLahir = @TempatLahir, TanggalLahir = @TanggalLahir, Alamat = @Alamat, Agama = @Agama, Rt = @Rt, Rw = @Rw, Dusun_Desa_Lurah = @DusunDesaLurah, Kecamatan = @Kecamatan, Kota_Kabupaten = @KotaKabupaten, KodePos = @KodePos,
                    NoTelp = @NoTelp, NoHp = @NoHp, Email = @Email, JumlahSaudara = @JumlahSaudara, AnakKe = @AnakKe, StatusDalamKeluarga = @StatusDalamKeluarga, TinggiBadan = @TinggiBadan, BeratBadan = @BeratBadan, GolDarah = @GolDarah, CitaCita = @CitaCita, Hobi = @Hobi, RiwayatSakit = @RiwayatSakit, KelainanJasmani = @KelainanJasmani
                    WHERE CalonSiswaId = @CalonSiswaId";
            }
            string sqlUpdateNamaLengkap = @"UPDATE CalonSiswa SET NamaLengkap = @NamaLengkap WHERE Id = @CalonSiswaId";
            using (var connection = new SqlConnection(_connectionHelper.GetConnectionString()))
            {
                connection.Open();
                using (var trans = connection.BeginTransaction())
                {
                    try
                    {
                        connection.Execute(sql: sqlQuery, param: newData, transaction: trans);
                        connection.Execute(sql: sqlUpdateNamaLengkap, param: new { NamaLengkap = namaLengkap, CalonSiswaId = newData.CalonSiswaId }, transaction: trans);
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
        public void SaveDataPenanggunjawab(string noPendaftaran, List<Penanggungjawab> newData)
        {
            int calonSiswaId = GetCalonSiswaId(noPendaftaran);
            string sqlQuery;
            for(int i=0; i < newData.Count; i++)
            {
                bool isExist = CekExist(calonSiswaId, "PenanggungJawab", newData[i].Sebagai);
                newData[i].CalonSiswaId = calonSiswaId;
                if (!isExist)
                {
                    sqlQuery = @"INSERT INTO Penanggungjawab(CalonSiswaId, Sebagai, NamaLengkap, TempatLahir, TanggalLahir, Alamat, Agama, PendidikanTerakhir, Pekerjaan, Penghasilan, NoTelp, NoHp, Email, StatusDalamKeluarga, Keterangan) 
                        VALUES(@CalonSiswaId, @Sebagai, @NamaLengkap, @TempatLahir, @TanggalLahir, @Alamat, @Agama, @PendidikanTerakhir, @Pekerjaan, @Penghasilan, @NoTelp, @NoHp, @Email, @StatusDalamKeluarga, @Keterangan)";
                }
                else
                {
                    sqlQuery = @"UPDATE Penanggungjawab
                        SET NamaLengkap = @NamaLengkap, TempatLahir = @TempatLahir, TanggalLahir = @TanggalLahir, Alamat = @Alamat, Agama = @Agama, PendidikanTerakhir = @PendidikanTerakhir, 
                        Pekerjaan = @Pekerjaan, Penghasilan = Penghasilan, NoTelp = @NoTelp, NoHp = @NoHp, Email = @Email, StatusDalamKeluarga = @StatusDalamKeluarga, Keterangan = @Keterangan 
                        WHERE CalonSiswaId = @CalonSiswaId AND Sebagai = @Sebagai";
                }
                using (var connection = new SqlConnection(_connectionHelper.GetConnectionString()))
                {
                    connection.Open();
                    connection.Execute(sql: sqlQuery, param: newData[i]);
                }
            }
        }
        public void SaveDataPenunjang(string noPendaftaran, Penunjang newData)
        {
            newData.CalonSiswaId = GetCalonSiswaId(noPendaftaran);
            bool isExist = CekExist(newData.CalonSiswaId, "Penunjang");
            string sqlQuery;
            if (!isExist)
            {
                sqlQuery = @"INSERT INTO Penunjang(CalonSiswaId, Pembiaya, StatusTempatTinggal, DayaListrik, JarakTempuh, WaktuTempuh, Transportasi) 
                    VALUES(@CalonSiswaId, @Pembiaya, @StatusTempatTinggal, @DayaListrik, @JarakTempuh, @WaktuTempuh, @Transportasi)";
            }
            else
            {
                sqlQuery = @"UPDATE Penunjang
                    SET Pembiaya=@Pembiaya, StatusTempatTinggal=@StatusTempatTinggal, DayaListrik=@DayaListrik, JarakTempuh=@JarakTempuh, WaktuTempuh=@WaktuTempuh, Transportasi=@Transportasi
                    WHERE CalonSiswaId = @CalonSiswaId";
            }
            using (var connection = new SqlConnection(_connectionHelper.GetConnectionString()))
            {
                connection.Open();
                connection.Execute(sql: sqlQuery, param: newData);
            }
        }
        public void SaveDataPrestasi(string noPendaftaran, Prestasi newData)
        {
            newData.CalonSiswaId = GetCalonSiswaId(noPendaftaran);
            bool isExist = CekExist(newData.CalonSiswaId, "Prestasi");
            string sqlQuery;
            if (!isExist)
            {
                sqlQuery = @"INSERT INTO Prestasi(CalonSiswaId, NamaKejuaraan, Jenis, Tingkat, Peringkat, Tahun, Penyelenggara) 
                    VALUES(@CalonSiswaId, @NamaKejuaraan, @Jenis, @Tingkat, @Peringkat, @Tahun, @Penyelenggara)";
            }
            else
            {
                sqlQuery = @"UPDATE Prestasi
                    SET NamaKejuaraan = @NamaKejuaraan, Jenis = @Jenis, Tingkat = @Tingkat, Peringkat = @Peringkat, Tahun = @Tahun, Penyelenggara = @Penyelenggara
                    WHERE CalonSiswaId = @CalonSiswaId";
            }
            using (var connection = new SqlConnection(_connectionHelper.GetConnectionString()))
            {
                connection.Open();
                connection.Execute(sql: sqlQuery, param: newData);
            }
        }
        public void SaveDataRapor(string noPendaftaran, List<Rapor> newData)
        {
            int calonSiswaId = GetCalonSiswaId(noPendaftaran);
            bool isExist = CekExist(calonSiswaId, "Rapor");
            string sqlQuery;
            foreach(var rapor in newData)
            {
                rapor.CalonSiswaId = calonSiswaId;
            }

            if (!isExist)
            {
                sqlQuery = @"INSERT INTO Rapor(CalonSiswaId, MataPelajaran, Semester1, Semester2, Semester3, Semester4, Semester5) 
                    VALUES(@CalonSiswaId, @MataPelajaran, @Semester1, @Semester2, @Semester3, @Semester4, @Semester5)";
            }
            else
            {
                sqlQuery = @"UPDATE Rapor
                    SET Semester1 = @Semester1, Semester2 = @Semester2, Semester3 = @Semester3, Semester4 = @Semester4, Semester5 = @Semester5
                    WHERE CalonSiswaId = @CalonSiswaId AND MataPelajaran = @MataPelajaran";
            }
            using (var connection = new SqlConnection(_connectionHelper.GetConnectionString()))
            {
                connection.Open();
                connection.Execute(sql: sqlQuery, param: newData);
            }
        }
    }
}
