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

        public string AddNewAkunPendaftaran(AkunPendaftaran newAkun)
        {
            // Cek apa calon siswa sudah pernah mendaftar
            bool exist = IsExistCalonSiswa(newAkun.ACalonSiswa.Nik);
            if (!exist)
            {
                CreateCalonSiswa(newAkun.ACalonSiswa);
            }
            string noPendaftaran = CreateAkunPendaftaran(newAkun);
            return noPendaftaran;
        }

        public int GetAkunPendaftaranId(string noPedaftaran)
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
        private void CreateCalonSiswa(CalonSiswa newCalonSiswa)
        {
            string sqlInsertCalonSIswa = @"INSERT INTO CalonSiswa(Nik, Nisn, NamaLengkap) 
                VALUES(@Nik, @Nisn, @NamaLengkap)";
            using (var connection = new SqlConnection(_connectionHelper.GetConnectionString()))
            {
                connection.Open();
                connection.Execute(sql: sqlInsertCalonSIswa, param: newCalonSiswa);
            }
        }
        private string CreateAkunPendaftaran(AkunPendaftaran newAkun)
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
            }

            if (newAkun.JalurPendaftaran.Equals("Mutasi"))
            {
                CreateAkademikLama(newAkun.CalonSiswaId, newAkun.ACalonSiswa.AAkademikTerakhir.NamaSekolah);
            }

            return newAkun.NoPendaftaran;
        }
        private void CreateAkademikLama(int calonSiswaId, string namaSekolah)
        {
            string sqlQuery = @"INSERT INTO AkademikTerakhir(CalonSiswaId, NamaSekolah) VALUES(@CalonSiswaId, @NamaSekolah)";
            using (var connection = new SqlConnection(_connectionHelper.GetConnectionString()))
            {
                connection.Open();
                connection.Execute(sql: sqlQuery, param: new { CalonSiswaId = calonSiswaId, NamaSekolah = namaSekolah });
            }
        }
        private bool IsExistCalonSiswa(string nik)
        {
            string sqlQuery = @"SELECT 1 FROM CalonSiswa WHERE Nik = @Nik";
            using (var connection = new SqlConnection(_connectionHelper.GetConnectionString()))
            {
                connection.Open();
                var exist = connection.QueryFirstOrDefault<bool>(sql: sqlQuery, new { @Nik = nik });
                return exist;
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
                // Decrypt kalo ada
                if (result != null)
                {
                    result.Password = _securityRelateHelper.Decrypt(result.Password);
                }

                return result;
            }
        }

        public List<AkunPendaftaran> GetAllAkunPendaftaran()
        {
            string sqlQuery = @"SELECT ap.Id, ap.NoPendaftaran, ap.JalurPendaftaran, ap.Status, cs.NamaLengkap 
                FROM AkunPendaftaran ap FULL JOIN CalonSiswa cs ON ap.CalonSiswaId = cs.Id
                WHERE ap.status != 'Admin' AND ap.JalurPendaftaran != 'Mutasi'";
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
                    splitOn: "NamaLengkap")
                    .Distinct()
                    .ToList();

                return result;
            }
        }

        public List<AkunPendaftaran> GetAllDaftarUlang()
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
                    splitOn: "NamaLengkap, NamaSekolah")
                    .Distinct()
                    .ToList();

                return result;
            }
        }

        public List<AkunPendaftaran> GetAllAkunPendaftaranMutasi()
        {
            string sqlQuery = @"SELECT ap.Id, ap.NoPendaftaran, ap.JalurPendaftaran, ap.Status, cs.NamaLengkap, csat.NamaSekolah 
                FROM AkunPendaftaran ap FULL JOIN CalonSiswa cs ON ap.CalonSiswaId = cs.Id
				FULL JOIN AkademikTerakhir csat ON cs.Id = csat.CalonSiswaId
                WHERE ap.JalurPendaftaran = 'Mutasi' AND ap.Status != 'Admin'";
            using (var connection = new SqlConnection(_connectionHelper.GetConnectionString()))
            {
                connection.Open();

                var result = connection.Query<AkunPendaftaran, CalonSiswa, AkademikTerakhir, AkunPendaftaran>(
                    sql: sqlQuery,
                    map: (akunPendaftaran, calonSiswa, akademikTerakhir) =>
                    {
                        akunPendaftaran.ACalonSiswa = calonSiswa;
                        calonSiswa.AAkademikTerakhir = akademikTerakhir;

                        return akunPendaftaran;
                    },
                    splitOn: "NamaLengkap, NamaSekolah")
                    .Distinct()
                    .ToList();

                return result;
            }
        }
    }
}
