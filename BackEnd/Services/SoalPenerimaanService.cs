using BackEnd.Domains;
using BackEnd.Abstraction;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Dapper;
using System.Text;
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

        public void AddPertanyaan(int soalId, Pertanyaan newPertanyaan)
        {
            throw new NotImplementedException();
        }

        public void AddSoal(Soal newSoal)
        {
            string sqlQuery = @"INSERT INTO Soal(Judul, Kategori, Target, JumlahPertanyaan, BatasWaktu) 
                                VALUES(@Judul, @Kategori, @Target, @JumlahPertanyaan, @BatasWaktu";
            using (var connection = new SqlConnection(_connectionHelper.GetConnectionString()))
            {
                connection.Open();
                connection.Execute(sql: sqlQuery, param: newSoal);
            }
        }

        public void DeletePertanyaan(int soalId, int id)
        {
            string sqlQuery = @"DELETE FROM Pertanyaan WHERE SoalId = @SoalId & IndexPertanyaan = @Id";
            using (var connection = new SqlConnection(_connectionHelper.GetConnectionString()))
            {
                connection.Open();
                connection.Execute(sql: sqlQuery, param: new { SoalId = soalId, Id = id });
            }
        }

        public void DeleteSoal(int id)
        {
            string sqlQuery = @"UPDATE Soal 
                                SET Status = 'DISABLE'
                                WHERE Id = id";
            using (var connection = new SqlConnection(_connectionHelper.GetConnectionString()))
            {
                connection.Open();
                connection.Execute(sql: sqlQuery, param: new { Id = id });
            }
        }

        public IEnumerable<Soal> GetAllSoalAkademik()
        {
            string sqlQuery = @"SELECT * FROM Soal";
            using (var connection = new SqlConnection(_connectionHelper.GetConnectionString()))
            {
                connection.Open();
                var result = connection.Query<Soal>(sql: sqlQuery);

                return result;
            }
        }

        public Soal GetDetailSoalAkademik(int id)
        {
            string sqlQuery = @"SELECT * FROM Soal FULL JOIN Pertanyaan 
                                ON Soal.Id = Pertanyaan.SoalId
                                WHERE Soal.Id = @Id";
            using (var connection = new SqlConnection(_connectionHelper.GetConnectionString()))
            {
                connection.Open();
                var result = connection.Query<Soal, Pertanyaan, Soal>(
                    sql: sqlQuery,
                    map: (soal, pertanyaan) =>
                    {


                        return soal;
                    },
                    splitOn: "SoalId",
                    param: new { Id = id }).FirstOrDefault();

                return result;
            }
        }

        public void UpdatePertanyaan(int soalId, Pertanyaan newData)
        {
            string sqlQuery = @"UPDATE Pertanyaan 
                                SET BadanPertanyaan = @BadanPertanyaan, Pilihan = @Pilihan, JawabanBenar = @JawabanBenar
                                WHERE IndexPertanyaan = @IndexPertanyaan && ";
            using (var connection = new SqlConnection(_connectionHelper.GetConnectionString()))
            {
                connection.Open();
                //connection.Execute(sql: sqlQuery, param: new { Id = id });
            }
        }

        public void UpdateSoal(Soal newData)
        {
            string sqlQuery = @"UPDATE ";
        }
    }
}
