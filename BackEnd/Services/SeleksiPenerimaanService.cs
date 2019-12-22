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

        public List<AkunPendaftaran> GetAllFinishUjianWithJalur(string jalur)
        {
            var listAkunFinishUjian = GetAllWithJalur(jalur);
            Setmark(jalur, ref listAkunFinishUjian);

            return listAkunFinishUjian;
        }

        public List<AkunPendaftaran> GetAllWithJalur(string jalur)
        {
            string sqlQuery = @"SELECT ap.Id, ap.NoPendaftaran, ap.JalurPendaftaran, cs.NamaLengkap, ra.* 
                FROM CalonSiswa cs JOIN AkunPendaftaran ap ON cs.Id=ap.CalonSiswaId 
                FULL JOIN RangkumanTesAkademik ra ON ap.Id = ra.AkunPendaftaranId
                WHERE JalurPendaftaran=@Jalur AND Status='Sudah Ujian'";
            using (var connection = new SqlConnection(_connectionHelper.GetConnectionString()))
            {
                connection.Open();
                var listAkun = connection.Query<AkunPendaftaran, CalonSiswa, RangkumanTesAkademik, AkunPendaftaran>(
                    sql: sqlQuery,
                    map: (ap, cs, ra) =>
                    {
                        ap.CalonSiswa = cs;
                        ap.Rekap = ra;
                        return ap;
                    },
                    splitOn: "NamaLengkap, AkunPendaftaranId",
                    param: new { Jalur = jalur })
                    .ToList();

                return listAkun;
            }
        }

        public void Setmark(string jalur, ref List<AkunPendaftaran> listAkunSeleksi)
        {
            if (jalur.Equals("Reguler"))
            {
                SetmarkForReguler(ref listAkunSeleksi);
            }
            else if (jalur.Equals("Khusus") || jalur.Equals("Mutasi"))
            {
                SetmarkForKhusus(ref listAkunSeleksi);
            }
        }

        public void SetmarkForReguler(ref List<AkunPendaftaran> listAkunSeleksi)
        {
            foreach (var item in listAkunSeleksi)
            {
                double skorAkhir = ((0.3 * item.Rekap.NilaiMipa) + (0.3 * item.Rekap.NilaiIps) + (0.4 * item.Rekap.NilaiTpa));
                Math.Round(skorAkhir, 2);
                item.Rekap.NilaiAkhir = skorAkhir;
            }
        }

        public void SetmarkForKhusus(ref List<AkunPendaftaran> listAkunSeleksi)
        {
            foreach (var item in listAkunSeleksi)
            {
                double skorAkhir = ((0.3 * item.Rekap.NilaiMipa) + (0.3 * item.Rekap.NilaiIps) + (0.4 * item.Rekap.NilaiTpa));
                bool isPass;
                if (skorAkhir > 50)
                    isPass = true;
                else
                    isPass = false;
                Math.Round(skorAkhir, 2);
                item.Rekap.NilaiAkhir = skorAkhir;
                item.Rekap.IsLolos = isPass;
            }
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
