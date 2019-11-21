using BackEnd.Abstraction;
using BackEnd.Domains;
using FrontEnd.Web.Mvc.Models.TesAkademik;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FrontEnd.Web.Mvc.Controllers
{
    [Authorize(Roles = "Calon Siswa, Admin")]
    //[AllowAnonymous]
    public class TesAkademikController : Controller
    {
        ISoalPenerimaan _soalPenerimaanService;
        ITesPenerimaan _tesPenermaanService;
        public TesAkademikController(ISoalPenerimaan soalPenerimaanService, ITesPenerimaan tesPenerimaanService)
        {
            _tesPenermaanService = tesPenerimaanService;
            _soalPenerimaanService = soalPenerimaanService;
        }
        public IActionResult Index()
        {
            var daftarSoal = _tesPenermaanService.GetSoalIdPengerjaan(User.Identity.Name);
            // Mipa, Ips, Tpa
            ViewBag.SoalIdMipa = daftarSoal[0];
            ViewBag.SoalIdIps = daftarSoal[1];
            ViewBag.SoalIdTpa = daftarSoal[2];

            return View();

        }
        [HttpGet]
        public IActionResult Seleksi(int soalId)
        {
            var soal = _soalPenerimaanService.GetDetailSoal(soalId);
            var model = new TesAkademikModel()
            {
                BatasWaktu = soal.BatasWaktu,
                ListPertanyaan = soal.ListPertanyaan.Select(x => new PertanyaanTes()
                {
                    Id = x.Id,
                    SoalId = x.SoalId,
                    OpsiA = x.OpsiA,
                    OpsiB = x.OpsiB,
                    OpsiC = x.OpsiC,
                    OpsiD = x.OpsiD,
                    OpsiE = x.OpsiE,
                    Pertanyaan = x.Isi
                }).ToList()
            };
            return View(model);
        }

        [HttpPost]
        public IActionResult Seleksi(TesAkademikModel model)
        {
            string noPendaftaran = User.Identity.Name;
            var listJawaban = model.ListPertanyaan.Select(x => new HasilTes()
            {
                PertanyaanId = x.Id,
                SoalId = x.SoalId,
                Jawaban = x.JawabanCalonSiswa
            });
            _tesPenermaanService.Submit(listJawaban, noPendaftaran);
            return RedirectToAction(nameof(Index));
        }
    }
}
