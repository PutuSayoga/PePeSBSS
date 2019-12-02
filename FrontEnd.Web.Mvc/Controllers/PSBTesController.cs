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
            var listAkun = _seleksiPenerimaanService.GetAllWithJalur("Khusus");
            var model = new SeleksiModel()
            {
                ListAkun = listAkun.Select(x => new AkunSeleksi()
                {
                    Id = x.Id,
                    NoPendaftaran = x.NoPendaftaran,
                    JalurPendaftaran = x.JalurPendaftaran,
                    NamaLengkap = x.ACalonSiswa.NamaLengkap,
                    NilaiIps = x.ARekapTesAkademik.NilaiIps,
                    NilaiTpa = x.ARekapTesAkademik.NilaiTpa,
                    NilaiMipa = x.ARekapTesAkademik.NilaiMipa
                }).ToList()
            };
            foreach(var item in model.ListAkun)
            {
                item.Keterangan = item.SkorAkhir > 70.0 ? true : false;
            }

            return View(model);
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
                    NamaLengkap = x.ACalonSiswa.NamaLengkap,
                    NilaiMipa = x.ARekapTesAkademik.NilaiMipa,
                    NilaiIps = x.ARekapTesAkademik.NilaiIps,
                    NilaiTpa = x.ARekapTesAkademik.NilaiTpa
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
                    NamaLengkap = x.ACalonSiswa.NamaLengkap,
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
                    NamaLengkap = x.ACalonSiswa.NamaLengkap,
                    JalurPendaftaran = x.JalurPendaftaran,
                    NoPendaftaran = x.NoPendaftaran
                }).ToList()
            };
            return View(model);
        }

        [HttpPost]
        public IActionResult SeleksiNonReguler(int id, string jalur, bool isLolos)
        {
            _seleksiPenerimaanService.SelectionNonReguler(id, isLolos);
            if (jalur.Equals("Khusus"))
                return RedirectToAction(nameof(SeleksiJalurKhusus));

            return View();
        }
    }
}
