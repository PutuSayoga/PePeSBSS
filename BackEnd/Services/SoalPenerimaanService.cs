using BackEnd.Domains;
using BackEnd.Abstraction;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Dapper;
using System.Linq;

namespace BackEnd.Services
{
    public class SoalPenerimaanService : ISoalPenerimaan
    {
        private readonly IDbConnectionHelper _connectionHelper;

        public SoalPenerimaanService(IDbConnectionHelper connectionString)
        {
            _connectionHelper = connectionString;
        }

        public IEnumerable<Soal> GetAllSoalAkademik()
        {
            string sqlQuery = @"SELECT * FROM Soal WHERE Kategori != 'Wawancara' AND Status = 'ENABLE'";
            using (var connection = new SqlConnection(_connectionHelper.GetConnectionString()))
            {
                connection.Open();
                var result = connection.Query<Soal>(sql: sqlQuery);

                return result;
            }
        }
        public IEnumerable<Soal> GetAllSoalWawancara()
        {
            string sqlQuery = @"SELECT * FROM Soal WHERE Kategori = 'Wawancara' AND Status = 'ENABLE'";
            using (var connection = new SqlConnection(_connectionHelper.GetConnectionString()))
            {
                connection.Open();
                var result = connection.Query<Soal>(sql: sqlQuery);

                return result;
            }
        }

        public Soal GetDetailSoal(int id)
        {
            string sqlQuery = @"Select * from Soal Where Id = @Id;
                                Select * from Pertanyaan Where SoalId = @Id;";
            using (var connection = new SqlConnection(_connectionHelper.GetConnectionString()))
            {
                connection.Open();
                using(var multiResult = connection.QueryMultiple(sqlQuery, new { Id = id }))
                {
                    var soal = multiResult.Read<Soal>().First();
                    soal.ListPertanyaan = multiResult.Read<Pertanyaan>().ToList();

                    return soal;
                }
            }
        }
        public Soal GetSimpleSoal(int id)
        {
            string sqlQuery = @"SELECT * FROM Soal WHERE Id = @Id";
            using (var connection = new SqlConnection(_connectionHelper.GetConnectionString()))
            {
                connection.Open();
                var soal = connection.Query<Soal>(sql: sqlQuery, param: new { Id = id }).First();
                return soal;
            }
        }
        public void AddSoal(Soal newSoal)
        {
            string sqlQuery = @"INSERT INTO Soal(Judul, Kategori, Jalur, Target, BatasWaktu, Deskripsi) 
                                VALUES(@Judul, @Kategori, @Jalur, @Target, @BatasWaktu, @Deskripsi)";
            using (var connection = new SqlConnection(_connectionHelper.GetConnectionString()))
            {
                connection.Open();
                connection.Execute(sql: sqlQuery, param: newSoal);
            }
        }
        public void DeleteSoal(int id)
        {
            string sqlQuery;
            if (IsUsed(id))
                sqlQuery = @"UPDATE Soal SET Status = 'DISABLE' WHERE Id = @Id";
            else
                sqlQuery = @"DELETE FROM Soal WHERE Id = @Id";

            using (var connection = new SqlConnection(_connectionHelper.GetConnectionString()))
            {
                connection.Open();
                connection.Execute(sql: sqlQuery, param: new { Id = id });
            }
        }
        public void UpdateSoal(Soal newData)
        {
            string sqlQuery = @"UPDATE Soal 
                                SET Judul = @Judul, Kategori = @Kategori, Jalur = @Jalur, Target = @Target,  BatasWaktu = @BatasWaktu, Deskripsi = @Deskripsi
                                WHERE Id = @Id";
            using (var connection = new SqlConnection(_connectionHelper.GetConnectionString()))
            {
                connection.Open();
                connection.Execute(sql: sqlQuery, param: newData);
            }
        }

        public void AddPertanyaan(Pertanyaan newPertanyaan)
        {
            string sqlQuery = @"INSERT INTO Pertanyaan(SoalId, Isi, OpsiA, OpsiB, OpsiC, OpsiD, OpsiE, Jawaban)
                                VALUES(@SoalId, @Isi, @OpsiA, @OpsiB, @OpsiC, @OpsiD, @OpsiE, @Jawaban)";
            string sqlQuery2 = @"UPDATE Soal SET JumlahPertanyaan = (JumlahPertanyaan+1) WHERE Id = @Id";
            using (var connection = new SqlConnection(_connectionHelper.GetConnectionString()))
            {
                connection.Open();
                using (var trans = connection.BeginTransaction())
                {
                    try
                    {
                        connection.Execute(sql: sqlQuery, param: newPertanyaan, transaction: trans);
                        connection.Execute(sql: sqlQuery2, param: new { Id = newPertanyaan.SoalId }, transaction:trans);
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
        public void DeletePertanyaan(int soalId, int id)
        {
            string sqlQuery = @"DELETE FROM Pertanyaan WHERE SoalId = @SoalId AND Id = @Id";
            string sqlQuery2 = @"UPDATE Soal SET JumlahPertanyaan = (JumlahPertanyaan-1) WHERE Id = @SoalId";
            using (var connection = new SqlConnection(_connectionHelper.GetConnectionString()))
            {
                connection.Open();
                using (var trans = connection.BeginTransaction())
                {
                    try
                    {
                        connection.Execute(sql: sqlQuery, param: new { SoalId = soalId, Id = id }, transaction: trans);
                        connection.Execute(sql: sqlQuery2, param: new { SoalId = soalId }, transaction:trans);
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
        public void UpdatePertanyaan(Pertanyaan newData)
        {
            string sqlQuery = @"UPDATE Pertanyaan 
                                SET Isi = @Isi, OpsiA = @OpsiA, OpsiB = @OpsiB, OpsiC = @OpsiC, OpsiD = @OpsiD, OpsiE = @OpsiE, Jawaban = @Jawaban
                                WHERE Id = @Id AND SoalId = @SoalId";
            using (var connection = new SqlConnection(_connectionHelper.GetConnectionString()))
            {
                connection.Open();
                connection.Execute(sql: sqlQuery, param: newData);
            }
        }

        public bool IsUsed(int id)
        {
            string sqlQuery = @"Select IsUsed FROM Soal WHERE Id = @Id";
            using (var connection = new SqlConnection(_connectionHelper.GetConnectionString()))
            {
                connection.Open();
                var result = connection.Execute(sql: sqlQuery, param: new { Id = id });

                return result == 1;
            }
        }

        public Pertanyaan GetPertanyaan(int id, int soalId)
        {
            string sqlQuery = @"SELECT * FROM Pertanyaan WHERE Id = @Id AND SoalId = @SoalId";
            using (var connection = new SqlConnection(_connectionHelper.GetConnectionString()))
            {
                connection.Open();
                var pertanyaan = connection.Query<Pertanyaan>(
                    sql: sqlQuery,
                    param: new { Id = id, SoalId = soalId })
                    .First();
                return pertanyaan;
            }
        }
    }
}
