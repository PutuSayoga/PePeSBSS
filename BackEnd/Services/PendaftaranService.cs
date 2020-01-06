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

        #region Not Interface Implementation
        public void InsertCalonSiswa(CalonSiswa newCalonSiswa)
        {
            string sqlInsertCalonSiswa = @"INSERT INTO CalonSiswa(Nik, Nisn, NamaLengkap) 
                VALUES(@Nik, @Nisn, @NamaLengkap)";
            using (var connection = new SqlConnection(_connectionHelper.GetConnectionString()))
            {
                connection.Open();
                connection.Execute(sql: sqlInsertCalonSiswa, param: newCalonSiswa);
            }
        }
        private void InsertSiswa(Siswa newSiswa)
        {
            string sqlInsertSiswa = @"INSERT INTO Siswa(CalonSiswaId, TanggalMasuk, Nis, Status) 
                VALUES(@CalonSiswaId, @TanggalMasuk, @Nis, @Status)";
            using (var connection = new SqlConnection(_connectionHelper.GetConnectionString()))
            {
                connection.Open();
                connection.Execute(sql: sqlInsertSiswa, param: newSiswa);
            }
        }
        public void InsertAkunPendaftaran(AkunPendaftaran newAkun)
        {
            string sqlInsertAkun = @"INSERT INTO AkunPendaftaran(CalonSiswaId, NoPendaftaran, Password, JalurPendaftaran, JadwalTes) 
                VALUES(@CalonSiswaId, @NoPendaftaran, @Password, @JalurPendaftaran, @JadwalTes)";
            using (var connection = new SqlConnection(_connectionHelper.GetConnectionString()))
            {
                connection.Open();
                connection.Execute(sql: sqlInsertAkun, param: newAkun);
            }

            if (newAkun.JalurPendaftaran.Equals("Mutasi"))
            {
                InsertAkademikLama(newAkun.CalonSiswaId, newAkun.CalonSiswa.AkademikTerakhir.NamaSekolah);
            }
        }
        private void InsertAkademikLama(int calonSiswaId, string namaSekolah)
        {
            string sqlQuery = @"INSERT INTO AkademikTerakhir(CalonSiswaId, NamaSekolah) VALUES(@CalonSiswaId, @NamaSekolah)";
            using (var connection = new SqlConnection(_connectionHelper.GetConnectionString()))
            {
                connection.Open();
                connection.Execute(sql: sqlQuery, param: new { CalonSiswaId = calonSiswaId, NamaSekolah = namaSekolah });
            }
        }
        public AkunPendaftaran CreateAkunPendaftaran(AkunPendaftaran newAkun)
        {
            newAkun.NoPendaftaran = CreateNoPendaftaran(newAkun.JalurPendaftaran);
            newAkun.CalonSiswaId = GetCalonSiswaId(newAkun.CalonSiswa.Nik);
            string passCreated = _securityRelateHelper.GeneratePassword();
            newAkun.Password = _securityRelateHelper.Encrypt(passCreated);

            return newAkun;
        }
        public string CreateNoPendaftaran(string jalurPendaftaran)
        {
            string sqlCreateNoPendaftaran = @"SELECT MAX(NoPendaftaran)+1 FROM AkunPendaftaran 
                WHERE JalurPendaftaran = @JalurPendaftaran";
            using (var connection = new SqlConnection(_connectionHelper.GetConnectionString()))
            {
                connection.Open();
                string noPendaftaran = connection.QueryFirstOrDefault<string>(
                    sql: sqlCreateNoPendaftaran, param: new { JalurPendaftaran = jalurPendaftaran });
                return noPendaftaran;
            }
        }
        private Siswa CreateSiswa(AkunPendaftaran akun)
        {
            var siswa = new Siswa()
            {
                CalonSiswaId = akun.CalonSiswaId,
                TanggalMasuk = DateTime.Now,
                Status = "Aktif",
                Nis = CreateNis()
            };
            return siswa;
        }
        private string CreateNis()
        {
            string sqlCreateNis = @"SELECT MAX(Nis)+1 FROM Siswa";
            using (var connection = new SqlConnection(_connectionHelper.GetConnectionString()))
            {
                connection.Open();
                string nis = connection.QueryFirstOrDefault<string>(sql: sqlCreateNis);
                return nis;
            }
        }
        private void UpdateStatusDaftarUlang(int akunId)
        {
            string sqlReRegist = @"UPDATE AkunPendaftaran SET Status = 'Daftar Ulang' WHERE Id = @Id";
            using (var connection = new SqlConnection(_connectionHelper.GetConnectionString()))
            {
                connection.Open();
                connection.Execute(sql: sqlReRegist, param: new { Id = akunId });
            }
        }
        public bool IsExistCalonSiswa(string nik)
        {
            string sqlQuery = @"SELECT 1 FROM CalonSiswa WHERE Nik = @Nik";
            using (var connection = new SqlConnection(_connectionHelper.GetConnectionString()))
            {
                connection.Open();
                var exist = connection.QueryFirstOrDefault<bool>(sql: sqlQuery, new { @Nik = nik });
                return exist;
            }
        }
        public int GetCalonSiswaId(string nik)
        {
            string sqlGetCalonSiswaId = @"SELECT Id FROM CalonSiswa WHERE Nik = @Nik";
            using (var connection = new SqlConnection(_connectionHelper.GetConnectionString()))
            {
                connection.Open();
                int calonSiswaId = connection.QueryFirstOrDefault<int>(
                    sql: sqlGetCalonSiswaId, param: new { Nik = nik });
                return calonSiswaId;
            }
        }
        #endregion

        public int NewRegist(AkunPendaftaran newAkun)
        {
            // Cek apa calon siswa sudah pernah mendaftar
            bool exist = IsExistCalonSiswa(newAkun.CalonSiswa.Nik);
            if (!exist)
            {
                InsertCalonSiswa(newAkun.CalonSiswa);
            }
            var completeAkun = CreateAkunPendaftaran(newAkun);
            InsertAkunPendaftaran(completeAkun);
            int akunPendaftaranId = GetAkunPendaftaranId(completeAkun.NoPendaftaran);

            return akunPendaftaranId;
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
        public void ReRegist(int akunId)
        {
            UpdateStatusDaftarUlang(akunId);
            var akunPendaftaran = GetAkunPendaftaran(akunId);
            var newSiswa = CreateSiswa(akunPendaftaran);
            InsertSiswa(newSiswa);
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
                        akunPendaftaran.CalonSiswa = calonSiswa;
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
                        akunPendaftaran.CalonSiswa = calonSiswa;
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
                WHERE ap.status = 'Daftar Ulang' AND ap.JalurPendaftaran != 'Mutasi'";
            using (var connection = new SqlConnection(_connectionHelper.GetConnectionString()))
            {
                connection.Open();

                // Get data from database and map them using dapper
                var result = connection.Query<AkunPendaftaran, CalonSiswa, AkademikTerakhir, AkunPendaftaran>(
                    sql: sqlQuery,
                    map: (akunPendaftaran, calonSiswa, akademikTerakhir) =>
                    {
                        akunPendaftaran.CalonSiswa = calonSiswa;
                        calonSiswa.AkademikTerakhir = akademikTerakhir;

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
                        akunPendaftaran.CalonSiswa = calonSiswa;
                        calonSiswa.AkademikTerakhir = akademikTerakhir;

                        return akunPendaftaran;
                    },
                    splitOn: "NamaLengkap, NamaSekolah")
                    .Distinct()
                    .ToList();

                return result;
            }
        }
        public AkunPendaftaran SearchAkunPendaftaran(string noPendaftaran)
        {
            int akunId = GetAkunPendaftaranId(noPendaftaran);
            var akun = GetAkunPendaftaran(akunId);
            return akun;
        }
    }
}
