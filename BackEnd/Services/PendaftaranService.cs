using BackEnd.Abstraction;
using BackEnd.Domains;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace BackEnd.Services
{
    public class PendaftaranService : IPendaftaran
    {
        private readonly IDbConnectionHelper _connectionHelper;
        public PendaftaranService(IDbConnectionHelper connectionHelper)
            => _connectionHelper = connectionHelper;

        public int AddNewAkunPendaftaran(AkunPendaftaran newAkun)
        {
            // Cek apa calon siswa sudah pernah mendaftar
            bool exist = IsExistCalonSiswa(newAkun.ACalonSiswa.Nik);
            if (!exist)
            {
                SaveCalonSiswa(newAkun.ACalonSiswa);
            }
            string noPendaftaran = SaveAkunPendaftaran(newAkun);
            return GetIdAkunPendaftaran(noPendaftaran);
        }

        public int GetIdAkunPendaftaran(string noPedaftaran)
        {
            string sqlGetAkunId = @"SELECT Id FROM AkunPendaftaran WHERE NoPendaftaran = @NoPendaftaran";
            using (var connection = new SqlConnection(_connectionHelper.GetConnectionString()))
            {
                connection.Open();
                int akunId = connection.QueryFirstOrDefault<int>(
                    sql: sqlGetAkunId, param: new { NoPendaftaran = noPedaftaran });
                return akunId;
            } 
        }
        public void SaveCalonSiswa(CalonSiswa newCalonSiswa)
        {
            string sqlInsertCalonSIswa = @"INSERT INTO CalonSiswa(Nik, Nisn, NamaLengkap) 
                VALUES(@Nik, @Nisn, @NamaLengkap)";
            using (var connection = new SqlConnection(_connectionHelper.GetConnectionString()))
            {
                connection.Open();
                connection.Execute(sql: sqlInsertCalonSIswa, param: newCalonSiswa);
            }
        }
        public string SaveAkunPendaftaran(AkunPendaftaran newAkun)
        {
            string sqlInsertAkun = @"INSERT INTO AkunPendaftaran(CalonSiswaId, NoPendaftaran, Password, JalurPendaftaran, JadwalTes) 
                VALUES(@CalonSiswaId, @NoPendaftaran, @Password, @JalurPendaftaran, @JadwalTes)";
            string sqlCreateNoPendaftaran = @"SELECT MAX(NoPendaftaran)+1 FROM AkunPendaftaran 
                WHERE JalurPendaftaran = @JalurPendaftaran";
            string sqlGetCalonSiswaId = @"SELECT Id FROM CalonSiswa WHERE Nik = @Nik";

            string passCreated = SecurityRelateService.GeneratePassword();
            newAkun.Password = SecurityRelateService.Encrypt(passCreated);

            using (var connection = new SqlConnection(_connectionHelper.GetConnectionString()))
            {
                connection.Open();
                newAkun.CalonSiswaId = connection.QueryFirst<int>(
                    sql: sqlGetCalonSiswaId, param: new { Nik = newAkun.ACalonSiswa.Nik });
                newAkun.NoPendaftaran = connection.QueryFirst<string>(
                    sql: sqlCreateNoPendaftaran, param: new { JalurPendaftaran = newAkun.JalurPendaftaran });

                connection.Execute(sql: sqlInsertAkun, param: newAkun);

                return newAkun.NoPendaftaran;
            }

        }
        public bool IsExistCalonSiswa(string nik)
        {
            string sqlQuery = @"SELECT Nik FROM CalonSiswa WHERE Nik = @Nik";
            using (var connection = new SqlConnection(_connectionHelper.GetConnectionString()))
            {
                connection.Open();
                var exist = connection.QueryFirstOrDefault<string>(sql: sqlQuery, new { @Nik = nik });
                return exist != null;
            }
        }

        public void Re_Regis(int id)
        {
            throw new NotImplementedException();
        }

        public AkunPendaftaran GetDetailAkunPendaftaran(int id)
        {
            string sqlGetDetailAkun = @"SELECT a.NoPendaftaran, a.JalurPendaftaran, a.Password, a.JadwalTes, cs.NamaLengkap 
                FROM AkunPendaftaran a FULL JOIN CalonSiswa cs ON a.CalonSiswaId = cs.Id
                WHERE a.Id = @Id AND a.Status != 'Admin'";
            using (var connection = new SqlConnection(_connectionHelper.GetConnectionString()))
            {
                connection.Open();
                // Get data from database and map them using dapper
                var result = connection.Query<AkunPendaftaran, CalonSiswa, AkunPendaftaran>(
                    sql: sqlGetDetailAkun,
                    map: (akunPendaftaran, calonSiswa) =>
                    {
                        akunPendaftaran.ACalonSiswa = calonSiswa;
                        return akunPendaftaran;
                    },
                    splitOn: "NamaLengkap",
                    param: new { Id = id }).FirstOrDefault();

                result.Password = SecurityRelateService.Decrypt(result.Password);

                return result;
            }
        }

        public IEnumerable<AkunPendaftaran> GetAllAkunPendaftaran()
        {
            string sqlQuery = @"SELECT a.Id, a.NoPendaftaran, a.JalurPendaftaran, a.Status, cs.NamaLengkap 
                                FROM AkunPendaftaran a FULL JOIN CalonSiswa cs ON a.CalonSiswaId = cs.Id
                                WHERE a.status != 'Admin'";
            using (var connection = new SqlConnection(_connectionHelper.GetConnectionString()))
            {
                connection.Open();

                // Get data from database and map them using dapper
                var result = connection.Query<AkunPendaftaran, CalonSiswa, AkunPendaftaran>(
                    sql: sqlQuery,
                    map: (akunPendaftaran, calonSiswa) =>
                    {
                        akunPendaftaran.ACalonSiswa = calonSiswa;
                        return akunPendaftaran;
                    },
                    splitOn: "NamaLengkap").Distinct().ToList();

                return result;
            }
        }

        public IEnumerable<AkunPendaftaran> GetAllDaftarUlang()
        {
            string sqlQuery = @"SELECT a.Id, a.NoPendaftaran, a.JalurPendaftaran, cs.NamaLengkap, csat.NamaSekolah
                                FROM AkunPendaftaran a FULL JOIN CalonSiswa cs ON a.CalonSiswaId = cs.Id
                                FULL JOIN AkademikTerakhir csat ON cs.Id = csat.CalonSiswaId
                                WHERE a.status = 'Daftar Ulang'";
            using (var connection = new SqlConnection(_connectionHelper.GetConnectionString()))
            {
                connection.Open();

                // Get data from database and map them using dapper
                var result = connection.Query<AkunPendaftaran, CalonSiswa, AkademikTerakhir, AkunPendaftaran>(
                    sql: sqlQuery,
                    map: (akunPendaftaran, calonSiswa, akademikTerakhir) =>
                    {
                        akunPendaftaran.ACalonSiswa = calonSiswa;
                        calonSiswa.AAkademikTerakhir = akademikTerakhir;

                        return akunPendaftaran;
                    },
                    splitOn: "NamaLengkap, NamaSekolah").Distinct().ToList();

                return result;
            }
        }
    }
}
