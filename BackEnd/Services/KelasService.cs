using BackEnd.Abstraction;
using BackEnd.Domains;
using BackEnd.Helper;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace BackEnd.Services
{
    public class KelasService : IKelas
    {
        private readonly IDbConnectionHelper _connectionHelper;
        public KelasService(IDbConnectionHelper connectionHelper)
        {
            _connectionHelper = connectionHelper;
        }

        #region Not Interface Implementation
        #endregion
        public void AddSiswaToKelas(int idSiswa, int idKelas)
        {
            string sqlQueryUpdateSiswa = @"UPDATE Siswa SET KelasId = @KelasId WHERE Id = @SiswaId";
            string sqlQueryUpdateKelas = @"UPDATE Kelas SET JumlahSiswa = (JumlahSiswa+1) WHERE Id = @KelasId";
            using (var connection = new SqlConnection(_connectionHelper.GetConnectionString()))
            {
                connection.Open();
                using (var trans = connection.BeginTransaction())
                {
                    try
                    {
                        connection.Execute(sql: sqlQueryUpdateSiswa, param: new { SiswaId = idSiswa }, transaction: trans);
                        connection.Execute(sql: sqlQueryUpdateKelas, param: new { KelasId = idKelas }, transaction: trans);
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
        public void CreateNewKelas(Kelas newData)
        {
            string sqlQuery = @"INSERT INTO Kelas(NamaKelas, Kategori, Tingkat, MaxSiswa) 
                                VALUES(@NamaKelas, @Kategori, @Tingkat, @MaxSiswa)";
            using (var connection = new SqlConnection(_connectionHelper.GetConnectionString()))
            {
                connection.Open();
                connection.Execute(sql: sqlQuery, param: newData);
            }
        }
        public void DeleteKelas(int idKelas)
        {
            string sqlQueryDeleteKelas = @"DELETE FROM Kelas WHERE Id = @Id";
            string sqlQueryDeleteFromSiswa = @"UPDATE Siswa SET kelasId = NULL WHERE KelasId = @KelasId";
            using (var connection = new SqlConnection(_connectionHelper.GetConnectionString()))
            {
                connection.Open();
                using (var trans = connection.BeginTransaction())
                {
                    try
                    {
                        connection.Execute(sql: sqlQueryDeleteFromSiswa, param: new { KelasId = idKelas }, transaction: trans);
                        connection.Execute(sql: sqlQueryDeleteKelas, param: new { Id = idKelas }, transaction: trans);
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
        public void DeleteSiswaFromKelas(int idSiswa)
        {
            string sqlQueryUpdateSiswa = @"UPDATE Siswa SET KelasId = NULL WHERE Id = @SiswaId";
            string sqlQueryUpdateKelas = @"UPDATE Kelas SET JumlahSiswa = (JumlahSiswa-1) WHERE Id = @KelasId";
            string sqlGetKelasId = @"SELECT KelasId FROM Siswa WHERE Id = @SiswaId";
            using (var connection = new SqlConnection(_connectionHelper.GetConnectionString()))
            {
                connection.Open();
                using (var trans = connection.BeginTransaction())
                {
                    try
                    {
                        int idKelas = connection.ExecuteScalar<int>(sql: sqlGetKelasId, param: new { SiswaId = idSiswa }, transaction:trans);
                        connection.Execute(sql: sqlQueryUpdateSiswa, param: new { SiswaId = idSiswa }, transaction: trans);
                        connection.Execute(sql: sqlQueryUpdateKelas, param: new { KelasId = idKelas }, transaction: trans);
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
        public List<Kelas> GetAllKelas()
        {
            string sqlQuery = @"SELECT * FROM Kelas";
            using (var connection = new SqlConnection(_connectionHelper.GetConnectionString()))
            {
                connection.Open();
                var result = connection.Query<Kelas>(sql: sqlQuery)
                    .ToList();

                return result;
            }
        }
        public Kelas GetDetailKelas(int idKelas)
        {
            string sqlQuery = @"SELECT * FROM Kelas WHERE Id = @Id";
            using (var connection = new SqlConnection(_connectionHelper.GetConnectionString()))
            {
                connection.Open();
                var kelas = connection.Query<Kelas>(
                    sql: sqlQuery,
                    param: new { Id = idKelas })
                    .FirstOrDefault();
                return kelas;
            }
        }
        public List<Siswa> GetMemberKelas(int idKelas)
        {
            string sqlQuery = @"SELECT s.Nis, cs.NamaLengkap FROM Siswa s JOIN CalonSiswa cs ON s.CalonSiswaId = cs.Id WHERE KelasId = @KelasId";
            using (var connection = new SqlConnection(_connectionHelper.GetConnectionString()))
            {
                connection.Open();
                var anggota = connection.Query<Siswa, CalonSiswa, Siswa>(
                    sql: sqlQuery,
                    map: (s, cs) =>
                    {
                        s.CalonSiswa = cs;
                        return s;
                    },
                    splitOn: "NamaLengkap",
                    param: new { KelasId = idKelas })
                    .ToList();
                return anggota;
            }
        }
        public void SetPath()
        {
            throw new Exception();
        }
        public void AutoMapKelas()
        {
            throw new Exception();
        }
        public void UpdateKelas(Kelas updateData)
        {
            string sqlQuery = @"UPDATE Kelas 
                                SET NamaKelas = @NamaKelas, Kategori = @Kategori, Tingkat = @Tingkat, MaxSiswa = @MaxSiswa
                                WHERE Id = @Id";
            using (var connection = new SqlConnection(_connectionHelper.GetConnectionString()))
            {
                connection.Open();
                connection.Execute(sql: sqlQuery, param: updateData);
            }
        }
    }
}
