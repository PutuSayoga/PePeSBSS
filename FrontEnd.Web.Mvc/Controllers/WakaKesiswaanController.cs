using BackEnd.Abstraction;
using FrontEnd.Web.Mvc.Models.PsbTes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using FrontEnd.Web.Mvc.Models.WakaKesiswaan;
using System.Threading.Tasks;

namespace FrontEnd.Web.Mvc.Controllers
{
    [Authorize(Roles = "Waka Kesiswaan")]
    //[Authorize]
    public class WakaKesiswaanController : Controller
    {
        private readonly ISeleksiPenerimaan _seleksiPenerimaanService;
        private readonly IKelas _kelasService;
        public WakaKesiswaanController(ISeleksiPenerimaan seleksiPenerimaanService, IKelas kelasService)
        {
            _seleksiPenerimaanService = seleksiPenerimaanService;
            _kelasService = kelasService;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult SeleksiMutasiMasuk()
        {
            var listAkun = _seleksiPenerimaanService.GetAllFinishUjianWithJalur("Mutasi");
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
                    SkorAkhir = x.Rekap.NilaiAkhir,
                    IsLolos = x.Rekap.IsLolos
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
            var model = new KelolaKelasModel()
            {
                ListKelas = new List<CrudKelas>()
                {
                    new CrudKelas(){Id=1, JumlahSiswa=0, Kategori="IPA", MaxSiswa=10, NamaKelas="X IPA 1", Tingkat=10}
                }
            };
            return View(model);
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
