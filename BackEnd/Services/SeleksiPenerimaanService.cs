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
        {
            _connectionHelper = connectionHelper;
        }
        public IEnumerable<AkunPendaftaran> GetAllJalurKhusus()
        {
            string sqlQuery = @"SELECT ap.Id, ap.NoPendaftaran, cs.NamaLengkap, ra.* 
                                FROM CalonSiswa cs JOIN AkunPendaftaran ap ON cs.Id=ap.CalonSiswaId 
                                JOIN RangkumanTesAkademik ra ON ap.Id = ra.AkunPendaftaranId
                                WHERE JalurPendaftaran='Khusus' AND Status='Sudah Ujian'";
            using(var connection = new SqlConnection(_connectionHelper.GetConnectionString()))
            {
                connection.Open();
                var akunSeleksi = connection.Query<AkunPendaftaran, CalonSiswa, RangkumanTesAkademik, AkunPendaftaran>(sql: sqlQuery,
                    map: (ap, cs, ra) =>
                    {
                        ap.ACalonSiswa = cs;
                        ap.ARekapTesAkademik = ra;
                        return ap;
                    },
                    splitOn: "NamaLengkap, AkunPendaftaranId");
                return akunSeleksi;
            }

        }

        public IEnumerable<AkunPendaftaran> GetAllJalurMitra()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<AkunPendaftaran> GetAllJalurPrestasi()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<AkunPendaftaran> GetAllJalurReguler()
        {
            throw new NotImplementedException();
        }
    }
}
