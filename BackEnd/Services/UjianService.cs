using BackEnd.Abstraction;
using System;
using System.Collections.Generic;
using System.Text;
using Dapper;
using BackEnd.Domains;
using System.Data.SqlClient;
using System.Linq;

namespace BackEnd.Services
{
    public class UjianService : IUjian
    {
        private readonly IDbConnectionHelper _connectionHelper;
        private readonly IPendaftaran _pendaftaranService;
        private readonly ISoalPenerimaan _soalService;
        public UjianService(IDbConnectionHelper connectionHelper, ISoalPenerimaan soalPenerimaanService, IPendaftaran pendaftaranService)
        {
            _connectionHelper = connectionHelper;
            _soalService = soalPenerimaanService;
            _pendaftaranService = pendaftaranService;
        }

        #region Not Interface Implementation
        
        public void FinishedStatusUjian(int akunId, int soalId)
        {
            string sqlQuery = @"UPDATE Ujian SET IsDone=1 WHERE AkunPendaftaranId = @AkunPendaftaranId AND SoalId = @SoalId";
            using (var connection = new SqlConnection(_connectionHelper.GetConnectionString()))
            {
                connection.Open();
                var ujian = connection.Execute(sql: sqlQuery, param: new { AkunPendaftaranId = akunId, SoalId = soalId });
            }
        }

        public void CheckUjian(int akunId, int soalId)
        {
            var soal = _soalService.GetDetailSoal(soalId);
            var kunciJawaban = soal.ListPertanyaan.ToDictionary(x => x.Id, y => y.Jawaban.ToString());
            var listHasilTes = GetHasilUjian(akunId, soalId);
            for (int i = 0; i < listHasilTes.Count; i++)
            {
                int pertanyaanId = listHasilTes[i].PertanyaanId;
                if (listHasilTes[i].Jawaban.Equals(kunciJawaban[pertanyaanId], StringComparison.OrdinalIgnoreCase))
                    listHasilTes[i].IsCorrect = true;
            }
            string sqlQuery = @"UPDATE HasilTes SET IsCorrect = @IsCorrect
                                WHERE AkunPendaftaranId = @AkunPendaftaranId AND PertanyaanId = @PertanyaanId AND SoalId = @SoalId";
            using (var connection = new SqlConnection(_connectionHelper.GetConnectionString()))
            {
                connection.Open();
                connection.Execute(sql: sqlQuery, param: listHasilTes);
            }
        }

        public List<HasilTes> GetHasilUjian(int akunId, int soalId)
        {
            string sqlQuery = @"SELECT * FROM HasilTes WHERE AkunPendaftaranId=@AkunPendaftaranId AND SoalId=@SoalId";
            using (var connection = new SqlConnection(_connectionHelper.GetConnectionString()))
            {
                connection.Open();
                var listHasilTes = connection
                    .Query<HasilTes>(sql: sqlQuery, param: new { AkunPendaftaranId = akunId, SoalId = soalId })
                    .ToList();
                return listHasilTes;
            }
        }

        public void RecapHasilUjian(int akunPendaftaranId, int soalId)
        {
            var listHasilTes = GetHasilUjian(akunPendaftaranId, soalId);
            var kategori = _soalService.GetSimpleSoal(soalId).Kategori;
            bool isExistInRangkuman = isExistInRangkumanAkademik(akunPendaftaranId);
            double nilai = Mark(listHasilTes.Count(x => x.IsCorrect), listHasilTes.Count());

            string sqlQueryInsertRecap = @"INSERT INTO RangkumanTesAkademik(AkunPendaftaranId) VALUES(@AkunPendaftaranId)";
            string sqlQueryUpdateNilai = $"UPDATE RangkumanTesAkademik SET Nilai{kategori} = @Nilai WHERE AkunPendaftaranId = @AkunPendaftaranId";

            using (var connection = new SqlConnection(_connectionHelper.GetConnectionString()))
            {
                connection.Open();
                if (!isExistInRangkuman)
                {
                    connection.Execute(sql: sqlQueryInsertRecap, param: new { AkunPendaftaranId = akunPendaftaranId });
                }
                connection.Execute(sql: sqlQueryUpdateNilai, param: new { Nilai = nilai, AkunPendaftaranId = akunPendaftaranId });
            }
        }

        public bool isExistInRangkumanAkademik(int akunId)
        {
            string sqlQueryIsExist = @"SELECT 1 FROM RangkumanTesAkademik WHERE AkunPendaftaranId=@AkunPendaftaranId";
            using (var connection = new SqlConnection(_connectionHelper.GetConnectionString()))
            {
                connection.Open();
                bool isExist = connection.QueryFirstOrDefault<bool>(sql: sqlQueryIsExist, param: new { AkunPendaftaranId = akunId });
                return isExist;
            }
        }

        public double Mark(int jawabanBenar, int jumlahPertanyaan)
        {
            double skor = (double)jawabanBenar * (100.0 / (double)jumlahPertanyaan);
            return Math.Round(skor, 2);
        }

        public void UpdateStatusSudahUjian(int akunId)
        {
            string sqlUpdateStatus = @"UPDATE AkunPendaftaran SET Status = 'Sudah Ujian' WHERE Id = @AkunId";
            using (var connection = new SqlConnection(_connectionHelper.GetConnectionString()))
            {
                connection.Open();
                connection.Execute(sql: sqlUpdateStatus, param: new { AkunId = akunId });
            }
        }
        #endregion

        public void SaveAnswers(List<HasilTes> listJawaban)
        {
            string sqlQuery = @"INSERT INTO HasilTes(SoalId, PertanyaanId, Jawaban, AkunPendaftaranId) 
                                VALUES(@SoalId, @PertanyaanId, @Jawaban, @AkunPendaftaranId)";
            using (var connection = new SqlConnection(_connectionHelper.GetConnectionString()))
            {
                connection.Open();
                connection.Execute(sql: sqlQuery, param: listJawaban);
            }
        }

        public void SaveAnswer(HasilTes jawaban)
        {
            string sqlQuery = @"UPDATE HasilTes SET Jawaban=@Jawaban 
                WHERE SoalId=@SoalId AND PertanyaanId=@PertanyaanId AND AkunPendaftaranId=@AkunPendaftaranId";
            using (var connection = new SqlConnection(_connectionHelper.GetConnectionString()))
            {
                connection.Execute(sql: sqlQuery, param: jawaban);
            }
        }

        public void StartUjian(int akunPendaftaranId, int soalId)
        {
            string sqlCreateNewUjian = @"INSERT INTO Ujian(AkunPendaftaranId, SoalId, WaktuBerakhir) 
                VALUES(@AkunPendaftaranId, @SoalId, @WaktuBerakhir)";
            string sqlCreateNewHasilTes = @"INSERT INTO HasilTes(AkunPendaftaranId, SoalId, PertanyaanId)
                VALUES(@AkunPendaftaranId, @SoalId, @PertanyaanId)";
            
            var soal = _soalService.GetDetailSoal(soalId);
            var ujianBaru = new Ujian()
            {
                SoalId = soalId,
                AkunPendaftaranId = akunPendaftaranId,
                WaktuBerakhir = DateTime.Now.AddMinutes(soal.BatasWaktu)
            };
            var listHasilTes = soal.ListPertanyaan.Select(x => new HasilTes()
            {
                AkunPendaftaranId = akunPendaftaranId,
                SoalId = x.SoalId,
                PertanyaanId = x.Id
            });

            using (var connection = new SqlConnection(_connectionHelper.GetConnectionString()))
            {
                connection.Open();
                connection.Execute(sql: sqlCreateNewUjian, param: ujianBaru);
                connection.Execute(sql: sqlCreateNewHasilTes, param: listHasilTes);
            }

            UpdateStatusSudahUjian(akunPendaftaranId);
        }

        public bool? IsDone(int akunId, int soalId)
        {
            var ujian = GetUjian(akunId, soalId);

            if (ujian == null)
            {
                return null;
            }
            else if (DateTime.Now < ujian.WaktuBerakhir)
            {
                return ujian.IsDone;
            }
            else
            {
                return true;
            }
        }

        public Ujian GetUjian(int akunId, int soalId)
        {
            string sqlQuery = @"SELECT * FROM Ujian WHERE AkunPendaftaranId = @AkunPendaftaranId AND SoalId = @SoalId";
            using (var connection = new SqlConnection(_connectionHelper.GetConnectionString()))
            {
                connection.Open();
                var ujian = connection.QueryFirstOrDefault<Ujian>(sql: sqlQuery, param: new { AkunPendaftaranId = akunId, SoalId = soalId });
                return ujian;
            }
        }

        public string GetAnswer(int akunId, int soalId, int pertanyaanId)
        {
            string sqlQuery = @"SELECT Jawaban FROM HasilTes WHERE SoalId=@SoalId AND PertanyaanId=@PertanyaanId AND AkunPendaftaranId=@AkunPendaftaranId";
            using (var connection = new SqlConnection(_connectionHelper.GetConnectionString()))
            {
                connection.Open();
                var jawaban = connection.QueryFirstOrDefault<string>(
                    sql: sqlQuery, param: new { AkunPendaftaranId = akunId, SoalId = soalId, PertanyaanId = pertanyaanId });
                return jawaban;
            }
        }

        public void FinishUjian(int akunId, int soalId)
        {
            FinishedStatusUjian(akunId, soalId);
            CheckUjian(akunId, soalId);
            RecapHasilUjian(akunId, soalId);
        }
        
        public int GetSoalPengerjaanAkademikId(string noPendaftaran, string kategoriSoal)
        {
            string jalurPendaftaran = _pendaftaranService.GetJalurPendaftaran(noPendaftaran);
            string sqlQuery = $"SELECT Soal{kategoriSoal}{jalurPendaftaran} FROM Pengaturan WHERE Id = 1";
            using (var connection = new SqlConnection(_connectionHelper.GetConnectionString()))
            {
                connection.Open();
                var soalId = connection.QuerySingle<int>(sql: sqlQuery);
                return soalId;
            }
        }

        public int GetSoalPengerjaanWawancaraId(string noPendaftaran, string targetSoal)
        {
            string jalurPendaftaran = _pendaftaranService.GetJalurPendaftaran(noPendaftaran);
            string sqlQuery = $"SELECT SoalWawancara{targetSoal}{jalurPendaftaran} FROM Pengaturan Id = 1";
            using (var connection = new SqlConnection(_connectionHelper.GetConnectionString()))
            {
                connection.Open();
                var soalId = connection.QuerySingle<int>(sql: sqlQuery);
                return soalId;
            }
        }
    }
}
