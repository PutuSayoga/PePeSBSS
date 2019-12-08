using BackEnd.Abstraction;
using BackEnd.Domains;
using System;
using System.Collections.Generic;
using System.Text;
using Dapper;
using System.Data.SqlClient;
using System.Linq;

namespace BackEnd.Services
{
    public class SeleksiPenerimaanService : ISeleksiPenerimaan
    {
        private readonly IDbConnectionHelper _connectionHelper;
        public SeleksiPenerimaanService(IDbConnectionHelper connectionHelper)
            => _connectionHelper = connectionHelper;

        public List<AkunPendaftaran> GetAllWithJalur(string jalur)
        {
            string sqlQuery = @"SELECT ap.Id, ap.NoPendaftaran, ap.JalurPendaftaran, cs.NamaLengkap, ra.* 
                FROM CalonSiswa cs JOIN AkunPendaftaran ap ON cs.Id=ap.CalonSiswaId 
                FULL JOIN RangkumanTesAkademik ra ON ap.Id = ra.AkunPendaftaranId
                WHERE JalurPendaftaran=@Jalur AND Status='Sudah Ujian'";
            List<AkunPendaftaran> akunSeleksi;
            using (var connection = new SqlConnection(_connectionHelper.GetConnectionString()))
            {
                connection.Open();
                akunSeleksi = connection.Query<AkunPendaftaran, CalonSiswa, RangkumanTesAkademik, AkunPendaftaran>(
                    sql: sqlQuery,
                    map: (ap, cs, ra) =>
                    {
                        ap.CalonSiswa = cs;
                        ap.RangkumanTesAkademik = ra;
                        return ap;
                    },
                    splitOn: "NamaLengkap, AkunPendaftaranId",
                    param: new { Jalur = jalur })
                    .ToList();
            }
            if (jalur.Equals("Khusus") || jalur.Equals("Reguler") || jalur.Equals("Mutasi"))
            {
                foreach (var item in akunSeleksi)
                {
                    item.RangkumanTesAkademik.NilaiAkhir = TotalMark(item.RangkumanTesAkademik.NilaiMipa, item.RangkumanTesAkademik.NilaiIps, item.RangkumanTesAkademik.NilaiTpa);
                }
            }
            return akunSeleksi;
        }

        public double TotalMark(double nilaiMipa, double nilaiIps, double nilaiTpa)
        {
            double skorAkhir = ((0.3 * nilaiMipa) + (0.3 * nilaiIps) + (0.4 * nilaiTpa));
            return Math.Round(skorAkhir, 2);
        }

        public void UpdateSelection(int akunId, bool isLolos)
        {
            string sqlUpdateStatus = @"UPDATE AkunPendaftaran SET Status = @Status WHERE Id = @AkunId";
            string status = isLolos ? "Lolos" : "Tidak Lolos";
            using (var connection = new SqlConnection(_connectionHelper.GetConnectionString()))
            {
                connection.Open();
                connection.Execute(sql: sqlUpdateStatus, param: new { Status = status, AkunId = akunId });
            }
        }
    }
}
