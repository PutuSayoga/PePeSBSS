using BackEnd.Abstraction;
using BackEnd.Helper;
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
        private readonly ISecurityRelate _securityRelateHelper;
        public PendaftaranService(IDbConnectionHelper connectionHelper, ISecurityRelate securityRelateHelper)
        {
            _connectionHelper = connectionHelper;
            _securityRelateHelper = securityRelateHelper;
        }

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
        private void SaveCalonSiswa(CalonSiswa newCalonSiswa)
        {
            string sqlInsertCalonSIswa = @"INSERT INTO CalonSiswa(Nik, Nisn, NamaLengkap) 
                VALUES(@Nik, @Nisn, @NamaLengkap)";
            using (var connection = new SqlConnection(_connectionHelper.GetConnectionString()))
            {
                connection.Open();
                connection.Execute(sql: sqlInsertCalonSIswa, param: newCalonSiswa);
            }
        }
        private string SaveAkunPendaftaran(AkunPendaftaran newAkun)
        {
            string sqlInsertAkun = @"INSERT INTO AkunPendaftaran(CalonSiswaId, NoPendaftaran, Password, JalurPendaftaran, JadwalTes) 
                VALUES(@CalonSiswaId, @NoPendaftaran, @Password, @JalurPendaftaran, @JadwalTes)";
            string sqlCreateNoPendaftaran = @"SELECT MAX(NoPendaftaran)+1 FROM AkunPendaftaran 
                WHERE JalurPendaftaran = @JalurPendaftaran";
            string sqlGetCalonSiswaId = @"SELECT Id FROM CalonSiswa WHERE Nik = @Nik";

            string passCreated = _securityRelateHelper.GeneratePassword();
            newAkun.Password = _securityRelateHelper.Encrypt(passCreated);

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
        private bool IsExistCalonSiswa(string nik)
        {
            string sqlQuery = @"SELECT Nik FROM CalonSiswa WHERE Nik = @Nik";
            using (var connection = new SqlConnection(_connectionHelper.GetConnectionString()))
            {
                connection.Open();
                var exist = connection.QueryFirstOrDefault<string>(sql: sqlQuery, new { @Nik = nik });
                return exist != null;
            }
        }

        public void ReRegist(int akunId)
        {
            string sqlReRegist = @"UPDATE AkunPendaftaran SET Status = 'Daftar Ulang' WHERE Id = @Id";
            using (var connection = new SqlConnection(_connectionHelper.GetConnectionString()))
            {
                connection.Open();
                connection.Execute(sql: sqlReRegist, param: new { Id = akunId });
            }
        }

        public AkunPendaftaran GetAkunPendaftaran(int akunId)
        {
            string sqlGetDetailAkun = @"SELECT ap.*, cs.Nik, cs.Nisn, cs.NamaLengkap 
                FROM AkunPendaftaran ap FULL JOIN CalonSiswa cs ON ap.CalonSiswaId = cs.Id
                WHERE ap.Id = @Id AND ap.Status != 'Admin'";
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
                    splitOn: "Nik",
                    param: new { Id = akunId }).FirstOrDefault();

                result.Password = _securityRelateHelper.Decrypt(result.Password);

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
            string sqlQuery = @"SELECT ap.Id, ap.NoPendaftaran, ap.JalurPendaftaran, cs.NamaLengkap, csat.NamaSekolah
                FROM AkunPendaftaran ap FULL JOIN CalonSiswa cs ON ap.CalonSiswaId = cs.Id
                FULL JOIN AkademikTerakhir csat ON cs.Id = csat.CalonSiswaId
                WHERE ap.status = 'Daftar Ulang'";
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
