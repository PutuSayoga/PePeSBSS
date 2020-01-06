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
    public class SeleksiService : ISeleksi
    {
        private readonly IDbConnectionHelper _connectionHelper;
        private readonly INotif _zenzivaNotifHelper;
        private readonly ICalonSiswa _calonSiswaService;
        public SeleksiService(IDbConnectionHelper connectionHelper, INotif zenzivaNotifHelper, ICalonSiswa calonSiswaService)
        {
            _connectionHelper = connectionHelper;
            _zenzivaNotifHelper = zenzivaNotifHelper;
            _calonSiswaService = calonSiswaService;
        }

        #region Not Interface Implementation
        private List<AkunPendaftaran> GetAllWith(string jalur)
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
        private void Setmark(string jalur, ref List<AkunPendaftaran> listAkunSeleksi)
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
        private void SetmarkForReguler(ref List<AkunPendaftaran> listAkunSeleksi)
        {
            foreach (var item in listAkunSeleksi)
            {
                double skorAkhir = ((0.3 * item.Rekap.NilaiMipa) + (0.3 * item.Rekap.NilaiIps) + (0.4 * item.Rekap.NilaiTpa));
                Math.Round(skorAkhir, 2);
                item.Rekap.NilaiAkhir = skorAkhir;
            }
        }
        private void SetmarkForKhusus(ref List<AkunPendaftaran> listAkunSeleksi)
        {
            foreach (var item in listAkunSeleksi)
            {
                double skorAkhir = ((0.3 * item.Rekap.NilaiMipa) + (0.3 * item.Rekap.NilaiIps) + (0.4 * item.Rekap.NilaiTpa));
                bool isLolos;
                if (skorAkhir > 50)
                    isLolos = true;
                else
                    isLolos = false;
                Math.Round(skorAkhir, 2);
                item.Rekap.NilaiAkhir = skorAkhir;
                item.Rekap.IsLolos = isLolos;
            }
        }
        private void UpdateSelection(string noPendaftaran, bool isLolos)
        {
            string sqlUpdateStatus = @"UPDATE AkunPendaftaran SET Status = @Status WHERE NoPendaftaran = @NoPendaftaran";
            string status = isLolos ? "Lolos" : "Tidak Lolos";
            using (var connection = new SqlConnection(_connectionHelper.GetConnectionString()))
            {
                connection.Open();
                connection.Execute(sql: sqlUpdateStatus, param: new { Status = status, NoPendaftaran = noPendaftaran });
            }
        }
        private string SetMessage(string noPendaftaran, bool isLolos)
        {
            string message;
            if (isLolos)
            {
                message = $"Selamat! Peserta dengan nomor pendaftaran {noPendaftaran} diterima di SMA Brawijaya Smart School. " +
                    $"Segera periksa akun pendaftaran untuk keterangan lebih lanjut";
            }
            else
            {
                message = $"Mohon maaf, peserta dengan nomor pendaftaran {noPendaftaran} gagal dalam seleksi. " +
                    $"Segera periksa akun pendaftaran untuk keterangan lebih lanjut";
            }
            return message;
        }
        private string SetNumberHandphone(string noPendaftaran)
        {
            try
            {
                Penanggungjawab ayah = _calonSiswaService.GetDetailPenanggungJawab(noPendaftaran)
                    .CalonSiswa
                    .ListPenanggungjawab
                    .Where(x => x.Sebagai.Equals("Ayah"))
                    .FirstOrDefault();
                return ayah.NoHp;
            }
            catch 
            {
                return "";
            }
        }
        #endregion

        public List<AkunPendaftaran> GetAllWithJalur(string jalur)
        {
            var listAkunFinishUjian = GetAllWith(jalur);
            Setmark(jalur, ref listAkunFinishUjian);

            return listAkunFinishUjian;
        }
        public string UpdateStatusPendaftar(string noPendaftaran, bool isLolos)
        {
            UpdateSelection(noPendaftaran, isLolos);
            string noHpAyah = SetNumberHandphone(noPendaftaran);
            string message = SetMessage(noPendaftaran, isLolos);
            string resultNotif = _zenzivaNotifHelper.SendNotif(noHpAyah, message);

            return resultNotif;
        }
        public void UpdateStatusReguler(int totalLolos)
        {
            var listAkun = GetAllWithJalur("Reguler").OrderByDescending(x => x.Rekap.NilaiAkhir).ToList();
            for (int i = 0; i < listAkun.Count; i++)
            {
                string noPendaftaran = listAkun[i].NoPendaftaran;
                bool isLolos;
                if (i < totalLolos)
                {
                    isLolos = true;
                }
                else
                {
                    isLolos = false;
                }
                UpdateSelection(noPendaftaran, isLolos);

            }
        }
    }
}
