using BackEnd.Abstraction;
using BackEnd.Domains;
using System;
using System.Collections.Generic;
using System.Text;
using Dapper;
using System.Data.SqlClient;

namespace BackEnd.Services
{
    public class SeleksiPenerimaanService : ISeleksiPenerimaan
    {
        private readonly IDbConnectionHelper _connectionHelper;
        public SeleksiPenerimaanService(IDbConnectionHelper connectionHelper)
            => _connectionHelper = connectionHelper;

        public IEnumerable<AkunPendaftaran> GetAllWithJalur(string jalur)
        {
            string sqlQuery = @"SELECT ap.Id, ap.NoPendaftaran, ap.JalurPendaftaran, cs.NamaLengkap, ra.* 
                                FROM CalonSiswa cs JOIN AkunPendaftaran ap ON cs.Id=ap.CalonSiswaId 
                                FULL JOIN RangkumanTesAkademik ra ON ap.Id = ra.AkunPendaftaranId
                                WHERE JalurPendaftaran=@Jalur AND Status='Sudah Ujian'";
            using (var connection = new SqlConnection(_connectionHelper.GetConnectionString()))
            {
                connection.Open();
                var akunSeleksi = connection.Query<AkunPendaftaran, CalonSiswa, RangkumanTesAkademik, AkunPendaftaran>(sql: sqlQuery,
                    map: (ap, cs, ra) =>
                    {
                        ap.ACalonSiswa = cs;
                        ap.ARekapTesAkademik = ra;
                        return ap;
                    },
                    splitOn: "NamaLengkap, AkunPendaftaranId",
                    param: new { Jalur = jalur });
                return akunSeleksi;
            }
        }

        public void SelectionNonReguler(int akunId, bool isLolos)
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
