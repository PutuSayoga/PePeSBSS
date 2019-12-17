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
        private readonly ISeleksiPenerimaan _seleksiPenerimaanService;
        public PsbTesController(ISeleksiPenerimaan seleksiPenerimaanService, IPendaftaran pendaftaranService)
        {
            _seleksiPenerimaanService = seleksiPenerimaanService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult SeleksiJalurKhusus()
        {
            var listAkun = _seleksiPenerimaanService.GetAllFinishUjianWithJalur("Khusus");
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

            return View(model);
        }
        public IActionResult SeleksiJalurReguler()
        {
            var listAkun = _seleksiPenerimaanService.GetAllFinishUjianWithJalur("Reguler");
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

            return View(model);
        }
        public IActionResult SeleksiJalurMitra()
        {
            var listAkun = _seleksiPenerimaanService.GetAllFinishUjianWithJalur("Mitra");
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
            return View(model);
        }
        public IActionResult SeleksiJalurPrestasi()
        {
            var listAkun = _seleksiPenerimaanService.GetAllFinishUjianWithJalur("Prestasi");
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
            return View(model);
        }
        [HttpPost]
        public IActionResult Seleksi(int id, bool isLolos)
        {
            _seleksiPenerimaanService.UpdateSelection(id, isLolos);
            return RedirectToAction(nameof(SeleksiJalurKhusus));
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
