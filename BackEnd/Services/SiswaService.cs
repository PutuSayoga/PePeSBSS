using BackEnd.Abstraction;
using BackEnd.Domains;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using Dapper;
using System.Linq;

namespace BackEnd.Services
{
    public class SiswaService : ISiswa
    {
        private readonly IDbConnectionHelper _connectionHelper;
        public SiswaService(IDbConnectionHelper connectionHelper)
            => _connectionHelper = connectionHelper;

        public void NewMutasiKeluar(MutasiKeluar mutasi)
        {
            string sqlInsertMutasi = @"INSERT INTO MutasiKeluar(SiswaId, Tujuan, Alasan, TanggalKeluar) 
                VALUES(@SiswaId, @Tujuan, @Alasan, @TanggalKeluar)";
            string sqlUpdateStatusSiswa = @"UPDATE Siswa SET Status = 'Keluar' 
                WHERE Id = @SiswaId";
            using (var connection = new SqlConnection(_connectionHelper.GetConnectionString()))
            {
                connection.Open();
                using (var trans = connection.BeginTransaction())
                {
                    try
                    {
                        connection.Execute(sql: sqlInsertMutasi, param: mutasi, transaction: trans);
                        connection.Execute(sql: sqlUpdateStatusSiswa, param: new { SiswaId = mutasi.SiswaId }, transaction: trans);
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

        public List<Siswa> GetAllSiswa()
        {
            string sqlQuery = @"SELECT s.*, cs.NamaLengkap, dd.IsPerempuan, k.NamaKelas 
                FROM Siswa s JOIN CalonSiswa cs ON s.CalonSiswaId = cs.Id
                FULL JOIN DataDiri dd ON s.CalonSiswaId = dd.CalonSiswaId 
				FULL JOIN Kelas k ON s.KelasId = k.Id
                WHERE s.Status = 'Aktif'";
            using (var connection = new SqlConnection(_connectionHelper.GetConnectionString()))
            {
                connection.Open();
                var listSiswa = connection.Query<Siswa, CalonSiswa, DataDiri, Kelas, Siswa>(
                    sql: sqlQuery,
                    map: (s, cs, dd, k) =>
                     {
                         s.CalonSiswa = cs;
                         s.CalonSiswa.DataDiri = dd;
                         s.Kelas = k;
                         return s;
                     },
                    splitOn: "NamaLengkap, IsPerempuan, NamaKelas")
                    .ToList();
                return listSiswa;
            }
        }

        public MutasiKeluar GetMutasiKeluar(int id)
        {
            throw new NotImplementedException();
        }

        public Siswa SearchSiswa(string nis)
        {
            string sqlQuery = @"SELECT s.Id, s.Nis, cs.NamaLengkap, k.NamaKelas 
                FROM Siswa s JOIN CalonSiswa cs ON s.CalonSiswaId = cs.Id
				FULL JOIN Kelas k ON s.KelasId = k.Id
                WHERE s.Status = 'Aktif' AND s.Nis = @Nis";
            using (var connection = new SqlConnection(_connectionHelper.GetConnectionString()))
            {
                connection.Open();
                var listSiswa = connection.Query<Siswa, CalonSiswa, Kelas, Siswa>(
                    sql: sqlQuery,
                    map: (s, cs, k) =>
                    {
                        s.CalonSiswa = cs;
                        s.Kelas = k;
                        return s;
                    },
                    splitOn: "NamaLengkap, NamaKelas",
                    param: new { Nis = nis })
                    .FirstOrDefault();
                return listSiswa;
            }
        }

        public Siswa GetDetailSiswa(int id)
        {
            throw new NotImplementedException();
        }

        public List<Siswa> GetAllMutasiKeluar()
        {
            string sqlQuery = @"SELECT s.Nis, s.Id, mk.Tujuan, mk.Alasan, mk.TanggalKeluar, cs.NamaLengkap
                FROM MutasiKeluar mk JOIN Siswa s ON mk.SiswaId = s.Id
                JOIN CalonSiswa cs ON s.CalonSiswaId = cs.Id";
            using (var connection = new SqlConnection(_connectionHelper.GetConnectionString()))
            {
                connection.Open();
                var mutasiKeluar = connection.Query<Siswa, MutasiKeluar, CalonSiswa, Siswa>(
                    sql: sqlQuery,
                    map: (s, mk, cs) =>
                    {
                        s.MutasiKeluar = mk;
                        s.CalonSiswa = cs;
                        return s;
                    },
                    splitOn: "Tujuan, NamaLengkap")
                    .ToList();
                return mutasiKeluar;
            }
        }
    }
}
