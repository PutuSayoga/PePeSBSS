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

        public void AddAkunPendaftaran(AkunPendaftaran newAkun)
        {
            // Cek apa calon siswa sudah pernah mendaftar
            if (false)
            {
                // Jika sudah pernah mendaftar
            }
            else
            {
                // Jika belum pernah
                string sqlQuery = @"INSERT INTO CalonSiswa(Nik, Nisn, NamaLengkap) 
                                    VALUES(@Nik, @Nisn, @NamaLengkap)";
                string sqlQuery2 = @"INSERT INTO AkunPendaftaran(CalonSiswaId, NoPendaftaran, Password, JalurPendaftaran, JadwalTes) 
                                     VALUES(@CalonSiswaId, @NoPendaftaran, @Password, @JalurPendaftaran, @JadwalTes)";
                // Create Pass
                newAkun.Password = "1234";
                using (var connection = new SqlConnection(_connectionHelper.GetConnectionString()))
                {
                    connection.Open();
                    using(var trans = connection.BeginTransaction())
                    {
                        try
                        {
                            connection.Execute(sql: sqlQuery, param: newAkun.ACalonSiswa, transaction: trans);
                            newAkun.CalonSiswaId = connection.QueryFirst<int>(
                                sql: "SELECT Id FROM CalonSiswa WHERE Nik = @Nik",
                                param: new { Nik = newAkun.ACalonSiswa.Nik },
                                transaction: trans);
                            newAkun.NoPendaftaran = connection.QueryFirst<string>(
                                sql: @"SELECT MAX(NoPendaftaran)+1 FROM AkunPendaftaran
                                   WHERE JalurPendaftaran = @JalurPendaftaran",
                                param: new { JalurPendaftaran = newAkun.JalurPendaftaran },
                                transaction: trans);
                            connection.Execute(sql: sqlQuery2, param: newAkun, transaction: trans);
                            trans.Commit();
                        }
                        catch
                        {
                            trans.Rollback();
                            throw;
                        }
                    }
                }
            }

        }

        public void Re_Regis(int id)
        {
            throw new NotImplementedException();
        }

        public AkunPendaftaran GetDetailAkunPendaftaran(int id)
        {
            string sqlQuery = @"SELECT a.NoPendaftaran, a.JalurPendaftaran, a.Password, a.JadwalTes, cs.NamaLengkap 
                                FROM AkunPendaftaran a FULL JOIN CalonSiswa cs ON a.CalonSiswaId = cs.Id
                                WHERE a.Id = @Id AND a.Status != 'Admin'";
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
                    splitOn: "NamaLengkap",
                    param: new { Id = id }).First();

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
