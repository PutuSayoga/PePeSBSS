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
    [Authorize(Roles = "PSB Tes, Admin")]
    public class PsbTesController : Controller
    {
        private readonly ISeleksiPenerimaan _seleksiPenerimaanService;
        public PsbTesController(ISeleksiPenerimaan seleksiPenerimaanService)
        {
            _seleksiPenerimaanService = seleksiPenerimaanService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
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
                    NilaiIps = x.RangkumanTesAkademik.NilaiIps,
                    NilaiTpa = x.RangkumanTesAkademik.NilaiTpa,
                    NilaiMipa = x.RangkumanTesAkademik.NilaiMipa,
                    SkorAkhir = x.RangkumanTesAkademik.NilaiAkhir
                }).ToList()
            };

            return View(model);
        }
        [HttpPost]
        public IActionResult SeleksiJalurKhusus(int id, bool isLolos)
        {
            _seleksiPenerimaanService.UpdateSelection(id, isLolos);
            return RedirectToAction(nameof(SeleksiJalurKhusus));
        }
        public IActionResult SeleksiJalurReguler()
        {
            var akun = _seleksiPenerimaanService.GetAllWithJalur("Reguler");
            var model = new SeleksiModel()
            {
                ListAkun = akun.Select(x => new AkunSeleksi()
                {
                    Id = x.Id,
                    NoPendaftaran = x.NoPendaftaran,
                    JalurPendaftaran = x.JalurPendaftaran,
                    NamaLengkap = x.CalonSiswa.NamaLengkap,
                    NilaiMipa = x.RangkumanTesAkademik.NilaiMipa,
                    NilaiIps = x.RangkumanTesAkademik.NilaiIps,
                    NilaiTpa = x.RangkumanTesAkademik.NilaiTpa
                })
                .OrderByDescending(x => x.SkorAkhir)
                .ToList()
            };

            return View(model);
        }
        public IActionResult SeleksiJalurMitra()
        {
            var akun = _seleksiPenerimaanService.GetAllWithJalur("Mitra");
            var model = new SeleksiModel()
            {
                ListAkun = akun.Select(x => new AkunSeleksi()
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
            var akun = _seleksiPenerimaanService.GetAllWithJalur("Prestasi");
            var model = new SeleksiModel()
            {
                ListAkun = akun.Select(x => new AkunSeleksi()
                {
                    Id = x.Id,
                    NamaLengkap = x.CalonSiswa.NamaLengkap,
                    JalurPendaftaran = x.JalurPendaftaran,
                    NoPendaftaran = x.NoPendaftaran
                }).ToList()
            };
            return View(model);
        }
    }
}
