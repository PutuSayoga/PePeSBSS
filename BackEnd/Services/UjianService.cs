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
        private readonly ISoalPenerimaan _soalService;
        public UjianService(IDbConnectionHelper connectionHelper, ISoalPenerimaan soalPenerimaanService)
        {
            _connectionHelper = connectionHelper;
            _soalService = soalPenerimaanService;
        }

        public List<int> GetSoalIdPengerjaan(string noPendaftaran)
        {
            //cek jalur pendaftaran
            string sqlQuery = @"SELECT JalurPendaftaran FROM AkunPendaftaran WHERE NoPendaftaran=@NoPendaftaran";
            //get soalId
            List<int> idSoalPengerjaan = new List<int>();
            using (var connection = new SqlConnection(_connectionHelper.GetConnectionString()))
            {
                connection.Open();
                var jalurPendaftaran = connection.QueryFirst<string>(sql: sqlQuery, param: new { NoPendaftaran = noPendaftaran });

                if (jalurPendaftaran.Equals("Reguler"))
                {
                    idSoalPengerjaan.Add(
                        connection.QueryFirst<int>(sql: @"SELECT SoalMipaReguler FROM Pengaturan WHERE Periode = 1"));
                    idSoalPengerjaan.Add(
                        connection.QueryFirst<int>(sql: @"SELECT SoalIpsReguler FROM Pengaturan WHERE Periode = 1"));
                    idSoalPengerjaan.Add(
                        connection.QueryFirst<int>(sql: @"SELECT SoalTpaReguler FROM Pengaturan WHERE Periode = 1"));
                }
                else if (jalurPendaftaran.Equals("Khusus"))
                {
                    idSoalPengerjaan.Add(
                        connection.QueryFirst<int>(sql: @"SELECT SoalMipaKhusus FROM Pengaturan WHERE Periode = 1"));
                    idSoalPengerjaan.Add(
                        connection.QueryFirst<int>(sql: @"SELECT SoalIpsKhusus FROM Pengaturan WHERE Periode = 1"));
                    idSoalPengerjaan.Add(
                        connection.QueryFirst<int>(sql: @"SELECT SoalTpaKhusus FROM Pengaturan WHERE Periode = 1"));
                }
                else if (jalurPendaftaran.Equals("Mutasi"))
                {
                    idSoalPengerjaan.Add(
                        connection.QueryFirst<int>(sql: @"SELECT SoalMipaMutasi FROM Pengaturan WHERE Periode = 1"));
                    idSoalPengerjaan.Add(
                        connection.QueryFirst<int>(sql: @"SELECT SoalIpsMutasi FROM Pengaturan WHERE Periode = 1"));
                    idSoalPengerjaan.Add(
                        connection.QueryFirst<int>(sql: @"SELECT SoalTpaMutasi FROM Pengaturan WHERE Periode = 1"));
                }
            }

            return idSoalPengerjaan;
        }

        public void SaveAnswers(List<HasilTes> listJawaban)
        {
            string sqlQuery = @"INSERT INTO HasilTes(SoalId, PertanyaanId, Jawaban, AkunPendaftaranId) 
                                VALUES(@SoalId, @PertanyaanId, @Jawaban, @AkunPendaftaranId)";
            using (var connection = new SqlConnection(_connectionHelper.GetConnectionString()))
            {
                connection.Execute(sql: sqlQuery, param: listJawaban);
            }
        }

        public void CheckTest(List<HasilTes> listJawaban)
        {
            string sqlQuery = @"UPDATE HasilTes SET Nilai = 1
                                WHERE AkunPendaftaranId = @AkunPendaftaranId AND PertanyaanId = @PertanyaanId AND SoalId = @SoalId
                                AND Jawaban = (SELECT Jawaban FROM Pertanyaan WHERE Id = @PertanyaanId AND SoalId = @SoalId)";
            using (var connection = new SqlConnection(_connectionHelper.GetConnectionString()))
            {
                connection.Execute(sql: sqlQuery, param: listJawaban);
            }
        }

        public void RecapSoal(int akunPendaftaranId, int soalId)
        {
            string sqlQueryJawaban = @"SELECT Nilai FROM HasilTes 
                                       WHERE AkunPendaftaranId = @AkunPendaftaranId AND SoalId = @SoalId";
            string sqlQueryKategori = @"SELECT Kategori FROM Soal WHERE Id = @SoalId";
            string sqlQueryIsExist = @"SELECT AkunPendaftaranId FROM RangkumanTesAkademik WHERE AkunPendaftaranId=@AkunPendaftaranId";
            string sqlQueryInsertRecap = @"INSERT INTO RangkumanTesAkademik(AkunPendaftaranId, NilaiMipa, NilaiIps, NilaiTpa) 
                                           VALUES(@AkunPendaftaranId, @NilaiMipa, @NilaiIps, @NilaiTpa)";
            var rekap = new RangkumanTesAkademik() { AkunPendaftaranId = akunPendaftaranId };

            using (var connection = new SqlConnection(_connectionHelper.GetConnectionString()))
            {
                var kategori = connection.QueryFirst<string>(sql: sqlQueryKategori, param: new { SoalId = soalId });
                var jawaban = connection.Query<bool>(sql: sqlQueryJawaban, param: new { AkunPendaftaranId = akunPendaftaranId, SoalId = soalId }).ToList();
                int row = connection.QueryFirstOrDefault<int>(sql: sqlQueryIsExist, param: new { AkunPendaftaranId = akunPendaftaranId });
                if (row == 0)
                {
                    connection.Execute(sql: sqlQueryInsertRecap, param: rekap);
                }

                if (kategori.Equals("MIPA"))
                {
                    rekap.NilaiMipa = Mark(jawaban.Count(x => x == true), jawaban.Count());
                    connection.Execute(param: rekap,
                        sql: @"UPDATE RangkumanTesAkademik SET NilaiMipa = @NilaiMipa WHERE AkunPendaftaranId = @AkunPendaftaranId");
                }
                else if (kategori.Equals("IPS"))
                {
                    rekap.NilaiIps = Mark(jawaban.Count(x => x == true), jawaban.Count());
                    connection.Execute(param: rekap,
                        sql: @"UPDATE RangkumanTesAkademik SET NilaiIps = @NilaiIps WHERE AkunPendaftaranId = @AkunPendaftaranId");
                }
                else if (kategori.Equals("TPA"))
                {
                    rekap.NilaiTpa = Mark(jawaban.Count(x => x == true), jawaban.Count());
                    connection.Execute(param: rekap,
                        sql: @"UPDATE RangkumanTesAkademik SET NilaiTpa = @NilaiTpa WHERE AkunPendaftaranId = @AkunPendaftaranId");
                }
            }
        }

        public double Mark(int jawabanBenar, int jumlahPertanyaan)
        {
            return jawabanBenar * (100.0 / jumlahPertanyaan);
        }

        public void Submit(List<HasilTes> listJawaban, string noPendataran)
        {
            string sqlQuery = @"SELECT Id FROM AkunPendaftaran WHERE NoPendaftaran = @NoPendaftaran";
            int akunPendaftaranId, soalId;
            using (var connection = new SqlConnection(_connectionHelper.GetConnectionString()))
            {
                akunPendaftaranId = connection.QueryFirst<int>(sql: sqlQuery, param: new { NoPendaftaran = noPendataran });
            }
            List<HasilTes> tempHasilTes = listJawaban.Select(x =>
            {
                x.AkunPendaftaranId = akunPendaftaranId;
                return x;
            }).ToList();
            soalId = tempHasilTes[0].SoalId;
            SaveAnswers(tempHasilTes);
            CheckTest(tempHasilTes);
            RecapSoal(akunPendaftaranId, soalId);
        }

        public Ujian StartTest(int akunPendaftaranId, int soalId)
        {
            var soal = _soalService.GetSimpleSoal(soalId);
            string sqlQuery = @"INSERT INTO Ujian(AkunPendaftaranId, SoalId, WaktuBerakhir) 
                VALUES(@AkunPendaftaranId, @SoalId, @WaktuBerakhir)";

            var ujian = GetUjian(akunPendaftaranId, soalId);
            if (ujian != null)
            {
                return ujian;
            }
            DateTime endTime = DateTime.Now.AddMinutes(soal.BatasWaktu);

            var ujianBaru = new Ujian() { AkunPendaftaranId = akunPendaftaranId, SoalId = soalId, WaktuBerakhir = endTime };
            using (var connection = new SqlConnection(_connectionHelper.GetConnectionString()))
            {
                connection.Open();
                if (ujian == null)
                {

                    connection.Execute(sql: sqlQuery, param: ujianBaru);
                }
            }

            return ujianBaru;
        }

        private Ujian GetUjian(int akunPendaftaranId, int soalId)
        {
            string sqlQuery = @"SELECT * FROM Ujian WHERE AkunPendaftaranId = @AkunPendaftaranId AND SoalId = @SoalId";
            using (var connection = new SqlConnection(_connectionHelper.GetConnectionString()))
            {
                connection.Open();
                var ujian = connection.Query<Ujian>(sql: sqlQuery, param: new { AkunPendaftaranId = akunPendaftaranId, SoalId = soalId }).FirstOrDefault();
                return ujian;
            }
        }

        public int? GetAkademikId(string noPendaftaran, string kategoriSoal)
        {
            string jalurPendaftaran = GetJalurPendaftaran(noPendaftaran);
            string sqlQuery = $"SELECT Soal{kategoriSoal}{jalurPendaftaran} FROM Pengaturan Id = 1";
            using (var connection = new SqlConnection(_connectionHelper.GetConnectionString()))
            {
                connection.Open();
                var soalId = connection.QuerySingle<int>(sql: sqlQuery);
                return soalId;
            }
        }

        public int? GetWawancaraId(string noPendaftaran, string targetSoal)
        {
            string jalurPendaftaran = GetJalurPendaftaran(noPendaftaran);
            string sqlQuery = $"SELECT SoalWawancara{targetSoal}{jalurPendaftaran} FROM Pengaturan Id = 1";
            using (var connection = new SqlConnection(_connectionHelper.GetConnectionString()))
            {
                connection.Open();
                var soalId = connection.QuerySingle<int>(sql: sqlQuery);
                return soalId;
            }
        }

        private string GetJalurPendaftaran(string noPendaftaran)
        {
            string sqlQuery = $"SELECT JalurPendaftaran FROM AkunPendaftaran NoPendaftaran = @NoPendaftaran";
            using (var connection = new SqlConnection(_connectionHelper.GetConnectionString()))
            {
                connection.Open();
                var jalurPendaftaran = connection.QuerySingle<string>(sql: sqlQuery, param: new { NoPendaftaran = noPendaftaran });
                return jalurPendaftaran;
            }
        }
    }
}
