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
                    NamaLengkap = x.ACalonSiswa.NamaLengkap,
                    NilaiMipa = x.ARekapTesAkademik.NilaiMipa ?? 0,
                    NilaiIps = x.ARekapTesAkademik.NilaiIps ?? 0,
                    NilaiTpa = x.ARekapTesAkademik.NilaiTpa ?? 0,
                })
            };
            model.ListAkun.Select(x =>
            {
                x.Keterangan = x.SkorAkhir > 70 ? true : false;
                return x;
            });

            return View(model);
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
