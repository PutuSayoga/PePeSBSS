using BackEnd.Abstraction;
using BackEnd.Domains;
using FrontEnd.Web.Mvc.Models.PsbPendaftaran;
using FrontEnd.Web.Mvc.Models.TataUsaha;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FrontEnd.Web.Mvc.Controllers
{
    [Authorize(Roles ="Tata Usaha, Admin")]
    public class TataUsahaController:Controller
    {
        private readonly IPendaftaran _pendaftaranService;
        private readonly ISiswa _siswaService;
        public TataUsahaController(IPendaftaran pendaftaranService, ISiswa siswaService)
        {
            _pendaftaranService = pendaftaranService;
            _siswaService = siswaService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult KelolaMutasiMasuk()
        {
            ViewBag.Pesan = TempData["Pesan"];
            var akun = _pendaftaranService.GetAllAkunPendaftaranMutasi();
            var model = new KelolaMutasiMasukModel()
            {
                ListAkunMutasi = akun.Select(x => new AkunMutasiMasuk()
                {
                    Id = x.Id,
                    NamaLengkap = x.CalonSiswa.NamaLengkap,
                    NoPendaftaran = x.NoPendaftaran,
                    SekolahAsal = x.CalonSiswa.AkademikTerakhir.NamaSekolah,
                    Status = x.Status
                    
                }).ToList()
            };
            ViewBag.Pesan = TempData["Pesan"];
            return View(model);
        }
        [HttpPost]
        public IActionResult DaftarBaruMutasiMasuk(KelolaMutasiMasukModel model)
        {
            if (!((model.MutasiMasuk.TanggalUjian >= DateTime.Now) &&
                    (model.MutasiMasuk.TanggalUjian <= DateTime.Now.AddDays(3))))
            {
                ModelState.AddModelError(nameof(model.MutasiMasuk.TanggalUjian),
                    "Jadwal tes maksimal dilaksanakan 3 hari setelah daftar baru");
                TempData["Pesan"] = "Jadwal tes maksimal dilaksanakan 3 hari setelah daftar baru. ";
            }

            if (!ModelState.IsValid)
            {
                TempData["Pesan"] += $"Gagal menambah akun, Data tidak valid";
                return RedirectToAction(nameof(KelolaMutasiMasuk));
            }
            else
            {
                var newAkun = new AkunPendaftaran()
                {
                    JalurPendaftaran = model.MutasiMasuk.JalurPendaftaran,
                    JadwalTes = model.MutasiMasuk.TanggalUjian,
                    CalonSiswa = new CalonSiswa()
                    {
                        Nik = model.MutasiMasuk.Nik,
                        NamaLengkap = model.MutasiMasuk.NamaLengkap,
                        Nisn = model.MutasiMasuk.Nisn,
                        AkademikTerakhir = new AkademikTerakhir()
                        {
                            NamaSekolah = model.MutasiMasuk.SekolahAsal
                        }
                    }
                };
                int akunId = _pendaftaranService.NewRegist(newAkun);
                return RedirectToAction(nameof(BuktiPendaftaran), new { id = akunId });
            }
        }
        [HttpPost]
        public IActionResult DaftarUlangMutasi(int id)
        {
            _pendaftaranService.ReRegist(id);
            TempData["Pesan"] = "Daftar ulang berhasil";
            return RedirectToAction(nameof(KelolaMutasiMasuk));
        }
        public IActionResult BuktiPendaftaran(int id)
        {
            var detailAkun = _pendaftaranService.GetAkunPendaftaran(id);
            var model = new BuktiPendaftaranModel()
            {
                Id = detailAkun.Id,
                NoPendaftaran = detailAkun.NoPendaftaran,
                NamaLengkap = detailAkun.CalonSiswa.NamaLengkap,
                JalurPendaftaran = detailAkun.JalurPendaftaran,
                Password = detailAkun.Password,
                JadwalTes = detailAkun.JadwalTes
                    .ToString("dddd, dd MMMM yyyy", new System.Globalization.CultureInfo("id-ID"))
            };
            return View(model);
        }

        public IActionResult KelolaMutasiKeluar()
        {
            var listKeluar = _siswaService.GetAllMutasiKeluar();
            var model = new KelolaMutasiKeluarModel()
            {
                ListMutasiKeluar = listKeluar.Select(x => new CrudMutasiKeluar()
                {
                    Nis = x.Nis,
                    NamaLengkap = x.CalonSiswa.NamaLengkap,
                    Tujuan = x.MutasiKeluar.Tujuan,
                    SiswaId = x.Id
                }).ToList()
            };

            return View(model);
        }
        public IActionResult RincianMutasiKeluar(int id)
        {
            var mutasiKeluar = _siswaService.GetMutasiKeluar(id);
            var model = new CrudMutasiKeluar()
            {
                Alasan = mutasiKeluar.Alasan,
                SiswaId = mutasiKeluar.SiswaId,
                Kelas = mutasiKeluar.Siswa.Kelas.NamaKelas,
                NamaLengkap = mutasiKeluar.Siswa.CalonSiswa.NamaLengkap,
                Nis = mutasiKeluar.Siswa.Nis,
                Tujuan = mutasiKeluar.Tujuan,
                TanggalKeluar = mutasiKeluar.TanggalKeluar
            };
            return Json(model);
        }
        public IActionResult CariSiswa(string nis)
        {
            var siswa = _siswaService.SearchSiswaForMutasiKeluar(nis);
            var model = new CrudMutasiKeluar()
            {
                SiswaId = siswa.Id,
                Kelas = siswa.Kelas == null ? "-" : siswa.Kelas.NamaKelas,
                NamaLengkap = siswa.CalonSiswa.NamaLengkap,
                Nis = siswa.Nis
            };
            return Json(model);
        }
        [HttpPost]
        public IActionResult TambahMutasiKeluar(KelolaMutasiKeluarModel model)
        {
            var mutasiKeluar = new MutasiKeluar()
            {
                Alasan = model.MutasiKeluar.Alasan,
                Tujuan = model.MutasiKeluar.Tujuan,
                TanggalKeluar = DateTime.Now,
                SiswaId = model.MutasiKeluar.SiswaId
            };
            _siswaService.NewMutasiKeluar(mutasiKeluar);
            return RedirectToAction(nameof(KelolaMutasiKeluar));
        }

        public IActionResult DaftarSiswa()
        {
            var siswa = _siswaService.GetAllSiswa();
            var model = new DaftarSiswaModel()
            {
                ListSiswaView = siswa.Select(x => new SiswaView()
                {
                    Id = x.Id,
                    JenisKelamin = x.CalonSiswa.DataDiri == null ? "-" :
                        x.CalonSiswa.DataDiri.IsPerempuan ? "Perempuan" : "Laki-laki",
                    NamaKelas = x.Kelas == null ? "-" : x.Kelas.NamaKelas,
                    NamaLengkap = x.CalonSiswa.NamaLengkap,
                    Nis = x.Nis
                }).ToList()
            };
            return View(model);
        }
    }
}
