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
            var akun = _pendaftaranService.GetAllAkunPendaftaranMutasi();
            var model = new KelolaMutasiMasukModel()
            {
                ListAkunMutasi = akun.Select(x => new AkunMutasiMasuk()
                {
                    Id = x.Id,
                    NamaLengkap = x.ACalonSiswa.NamaLengkap,
                    NoPendaftaran = x.NoPendaftaran,
                    SekolahAsal = x.ACalonSiswa.AAkademikTerakhir.NamaSekolah,
                    Status = x.Status
                    
                }).ToList()
            };
            ViewBag.Pesan = TempData["Pesan"];
            return View(model);
        }
        public IActionResult CariSiswa(string nis)
        {
            var model = new CrudMutasiKeluar()
            {
                SiswaId = 1,
                Alasan = "Gak Kuat",
                Kelas = "X IPA 1",
                NamaLengkap = "Test",
                Nis = "000001",
                Tujuan = "SMA 1 Test"
            };
            return Json(model);
        }

        public IActionResult KelolaMutasiKeluar()
        {
            var model = new KelolaMutasiKeluarModel()
            {
                ListMutasiKeluar = new List<CrudMutasiKeluar>()
                {
                    new CrudMutasiKeluar(){NamaLengkap="tes"}
                }
            };
            return View(model);
        }
        public IActionResult DaftarSiswa()
        {
            return View();
        }
        [HttpPost]
        public IActionResult DaftarBaruAkunMutasi(KelolaMutasiMasukModel model)
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
                    ACalonSiswa = new CalonSiswa()
                    {
                        Nik = model.MutasiMasuk.Nik,
                        NamaLengkap = model.MutasiMasuk.NamaLengkap,
                        Nisn = model.MutasiMasuk.Nisn,
                        AAkademikTerakhir = new AkademikTerakhir()
                        {
                            NamaSekolah = model.MutasiMasuk.SekolahAsal
                        }
                    }
                };
                string noPendaftaran = _pendaftaranService.AddNewAkunPendaftaran(newAkun);
                int akunId = _pendaftaranService.GetAkunPendaftaranId(noPendaftaran);
                return RedirectToAction(nameof(BuktiPendaftaran), new { id = akunId });
            }
        }
        public IActionResult BuktiPendaftaran(int id)
        {
            var detailAkun = _pendaftaranService.GetAkunPendaftaran(id);
            var model = new BuktiPendaftaranModel()
            {
                Id = detailAkun.Id,
                NoPendaftaran = detailAkun.NoPendaftaran,
                NamaLengkap = detailAkun.ACalonSiswa.NamaLengkap,
                JalurPendaftaran = detailAkun.JalurPendaftaran,
                Password = detailAkun.Password,
                JadwalTes = detailAkun.JadwalTes
                    .ToString("dddd, dd MMMM yyyy", new System.Globalization.CultureInfo("id-ID"))
            };
            return View(model);
        }

        public IActionResult DetailMutasiKeluar(int id)
        {
            var mutasiKeluar = _siswaService.GetMutasiKeluar(id);
            var model = new CrudMutasiKeluar()
            {
                Alasan = mutasiKeluar.Alasan,
                SiswaId = mutasiKeluar.SiswaId,
                Kelas = mutasiKeluar.ASiswa.AKelas.NamaKelas,
                NamaLengkap = mutasiKeluar.ASiswa.ACalonSiswa.NamaLengkap,
                Nis = mutasiKeluar.ASiswa.Nis,
                Tujuan = mutasiKeluar.Tujuan,
                TanggalKeluar = mutasiKeluar.TanggalKeluar
            };
            return Json(model);
        }

        public IActionResult TambahMutasiKeluar(KelolaMutasiKeluarModel model)
        {
            int siswaId = _siswaService.GetSiswaId(model.MutasiKeluar.Nis);
            var mutasiKeluar = new MutasiKeluar()
            {
                Alasan = model.MutasiKeluar.Alasan,
                Tujuan = model.MutasiKeluar.Tujuan,
                TanggalKeluar = DateTime.Now,
                SiswaId = siswaId
            };
            _siswaService.CreateMutasiKeluar(mutasiKeluar);
            return RedirectToAction(nameof(KelolaMutasiKeluar));
        }
    }
}
