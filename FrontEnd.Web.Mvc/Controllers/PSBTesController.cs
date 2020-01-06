using BackEnd.Abstraction;
using FrontEnd.Web.Mvc.Models.PsbTes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FrontEnd.Web.Mvc.Controllers
{
    [Authorize(Roles = "PSB Tes")]
    //[Authorize]
    public class PsbTesController : Controller
    {
        private readonly ISeleksi _seleksiPenerimaanService;
        public PsbTesController(ISeleksi seleksiPenerimaanService, IPendaftaran pendaftaranService)
        {
            _seleksiPenerimaanService = seleksiPenerimaanService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult SeleksiJalurKhusus()
        {
            var listAkun = _seleksiPenerimaanService.GetAllWithJalur("Khusus");
            var model = new SeleksiModel()
            {
                ListAkun = listAkun.Select(x => new AkunSeleksi()
                {
                    Id = x.Id,
                    NoPendaftaran = x.NoPendaftaran,
                    JalurPendaftaran = x.JalurPendaftaran,
                    NamaLengkap = x.CalonSiswa.NamaLengkap,
                    NilaiIps = x.Rekap.NilaiIps,
                    NilaiTpa = x.Rekap.NilaiTpa,
                    NilaiMipa = x.Rekap.NilaiMipa,
                    SkorAkhir = x.Rekap.NilaiAkhir,
                    IsLolos = x.Rekap.IsLolos
                }).ToList()
            };
            ViewBag.Pesan = TempData["Pesan"] as string;

            return View(model);
        }
        [HttpPost]
        public IActionResult SeleksiJalurKhusus(string noPendaftaran, bool isLolos)
        {
            string result = _seleksiPenerimaanService.UpdateStatusPendaftar(noPendaftaran, isLolos);
            TempData["Pesan"] = $"Berhasil seleksi, {result}";
            return RedirectToAction(nameof(SeleksiJalurKhusus));
        }
        public IActionResult SeleksiJalurReguler()
        {
            var listAkun = _seleksiPenerimaanService.GetAllWithJalur("Reguler");
            var model = new SeleksiModel()
            {
                ListAkun = listAkun.Select(x => new AkunSeleksi()
                {
                    Id = x.Id,
                    NoPendaftaran = x.NoPendaftaran,
                    JalurPendaftaran = x.JalurPendaftaran,
                    NamaLengkap = x.CalonSiswa.NamaLengkap,
                    NilaiMipa = x.Rekap.NilaiMipa,
                    NilaiIps = x.Rekap.NilaiIps,
                    NilaiTpa = x.Rekap.NilaiTpa,
                    SkorAkhir = x.Rekap.NilaiAkhir
                })
                .OrderByDescending(x => x.SkorAkhir)
                .ToList()
            };
            ViewBag.Pesan = TempData["Pesan"] as string;

            return View(model);
        }
        [HttpPost]
        public IActionResult SeleksiJalurReguler(int banyakLolos, int total)
        {
            if (banyakLolos < 0)
            {
                TempData["Pesan"] = "Nilai yang dimasukkan tidak boleh negatif";
            }
            else if (total < banyakLolos)
            {
                TempData["Pesan"] = "Jumlah peserta lebih sedikit dari banyak siswa yang diinginkan";
            }
            else
            {
                _seleksiPenerimaanService.UpdateStatusReguler(banyakLolos);
                TempData["Pesan"] = "Berhasil menyeleksi, data akan diekspor menjadi excel";
            }
            return RedirectToAction(nameof(SeleksiJalurReguler));
        }
        public IActionResult SeleksiJalurMitra()
        {
            var listAkun = _seleksiPenerimaanService.GetAllWithJalur("Mitra");
            var model = new SeleksiModel()
            {
                ListAkun = listAkun.Select(x => new AkunSeleksi()
                {
                    Id = x.Id,
                    NamaLengkap = x.CalonSiswa.NamaLengkap,
                    JalurPendaftaran = x.JalurPendaftaran,
                    NoPendaftaran = x.NoPendaftaran
                }).ToList()
            };
            ViewBag.Pesan = TempData["Pesan"] as string;
            return View(model);
        }
        [HttpPost]
        public IActionResult SeleksiJalurMitra(string noPendaftaran, bool isLolos)
        {
            string result = _seleksiPenerimaanService.UpdateStatusPendaftar(noPendaftaran, isLolos);
            TempData["Pesan"] = $"Berhasil seleksi, {result}";
            return RedirectToAction(nameof(SeleksiJalurMitra));
        }
        public IActionResult SeleksiJalurPrestasi()
        {
            var listAkun = _seleksiPenerimaanService.GetAllWithJalur("Prestasi");
            var model = new SeleksiModel()
            {
                ListAkun = listAkun.Select(x => new AkunSeleksi()
                {
                    Id = x.Id,
                    NamaLengkap = x.CalonSiswa.NamaLengkap,
                    JalurPendaftaran = x.JalurPendaftaran,
                    NoPendaftaran = x.NoPendaftaran
                }).ToList()
            };
            ViewBag.Pesan = TempData["Pesan"] as string;
            return View(model);
        }
        [HttpPost]
        public IActionResult SeleksiJalurPrestasi(string noPendaftaran, bool isLolos)
        {
            string result = _seleksiPenerimaanService.UpdateStatusPendaftar(noPendaftaran, isLolos);
            TempData["Pesan"] = $"Berhasil seleksi, {result}";
            return RedirectToAction(nameof(SeleksiJalurPrestasi));
        }
        public IActionResult TestWawancara()
        {
            return View();
        }
        [HttpPost]
        public IActionResult TestWawancara(TestWawancaraModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Pesan = "Data tidak valid";
                return View();
            }
            else
            {
                return RedirectToAction("PendahuluanWawancara", "Ujian", new { noPendaftaran = model.NoPendaftaran, target = model.Target });
            }
        }
    }
}
