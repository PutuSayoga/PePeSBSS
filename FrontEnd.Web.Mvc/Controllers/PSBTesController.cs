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

        public IActionResult SeleksiJalurKhusus()
        {
            var akun = _seleksiPenerimaanService.GetAllJalurKhusus();
            var model = new SeleksiJalurKhususModel()
            {
                ListJalurKhusus = akun.Select(x => new AkunKhusus()
                {
                    Id = x.Id,
                    NoPendaftaran = x.NoPendaftaran,
                    NamaLengkap = x.ACalonSiswa.NamaLengkap,
                    NilaiMipa = x.ARekapTesAkademik.NilaiMipa ?? 0,
                    NilaiIps = x.ARekapTesAkademik.NilaiIps ?? 0,
                    NilaiTpa = x.ARekapTesAkademik.NilaiTpa ?? 0
                })
            };
            return View(model);
        }
        [HttpPost]
        public IActionResult PerbaruiStatusKhusus(int id)
        {
            return RedirectToAction(nameof(SeleksiJalurKhusus));
        }
        [HttpGet]
        public IActionResult SeleksiJalurReguler()
        {
            return View();
        }
        [HttpPost]
        public IActionResult SeleksiJalurReguler(int id)
        {
            return View();
        }
        [HttpGet]
        public IActionResult SeleksiJalurMitra()
        {
            return View();
        }
        [HttpPost]
        public IActionResult SeleksiJalurMitra(int id)
        {
            return View();
        }
        [HttpGet]
        public IActionResult SeleksiJalurPrestasi()
        {
            return View();
        }
        [HttpPost]
        public IActionResult SeleksiJalurPrestasi(int id)
        {
            return View();
        }
        public IActionResult TestWawancara()
        {
            return View();
        }
    }
}
