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
    [Authorize(Roles = "Waka Kesiswaan, Admin")]
    public class WakaKesiswaanController : Controller
    {
        private readonly ISeleksiPenerimaan _seleksiPenerimaanService;
        public WakaKesiswaanController(ISeleksiPenerimaan seleksiPenerimaanService)
        {
            _seleksiPenerimaanService = seleksiPenerimaanService;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult SeleksiMutasiMasuk()
        {
            var akun = _seleksiPenerimaanService.GetAllWithJalur("Mutasi");
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
                    NilaiTpa = x.RangkumanTesAkademik.NilaiTpa,
                    SkorAkhir = x.RangkumanTesAkademik.NilaiAkhir
                }).ToList()
            };

            return View(model);
        }
        [HttpPost]
        public IActionResult SeleksiMutasiMasuk(int id, bool isLolos)
        {
            _seleksiPenerimaanService.UpdateSelection(id, isLolos);
            return RedirectToAction(nameof(SeleksiMutasiMasuk));
        }
        public IActionResult KelolaKelas()
        {
            return View();
        }
        public IActionResult TestWawancara()
        {
            return View();
        }
    }
}
