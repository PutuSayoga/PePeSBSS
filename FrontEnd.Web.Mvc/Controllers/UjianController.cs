using BackEnd.Abstraction;
using BackEnd.Domains;
using FrontEnd.Web.Mvc.Models.Seleksi;
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
    public class UjianController : Controller
    {
        ISoalPenerimaan _soalPenerimaanService;
        ITesPenerimaan _tesPenermaanService;
        public UjianController(ISoalPenerimaan soalPenerimaanService, ITesPenerimaan tesPenerimaanService)
        {
            _tesPenermaanService = tesPenerimaanService;
            _soalPenerimaanService = soalPenerimaanService;
        }
        public IActionResult Index()
        {
            var daftarSoal = _tesPenermaanService.GetSoalIdPengerjaan(User.Identity.Name).ToList();
            // Mipa, Ips, Tpa
            ViewBag.SoalIdMipa = daftarSoal[0];
            ViewBag.SoalIdIps = daftarSoal[1];
            ViewBag.SoalIdTpa = daftarSoal[2];

            return View();

        }
        [HttpGet]
        public IActionResult SeleksiAkademik(int soalId)
        {
            var soal = _soalPenerimaanService.GetDetailSoal(soalId);
            var model = new SeleksiAkademikModel()
            {
                BatasWaktu = soal.BatasWaktu,
                ListPertanyaan = soal.ListPertanyaan.Select(x => new PertanyaanTest()
                {
                    PertanyaanId = x.Id,
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
        public IActionResult SeleksiAkademik(SeleksiAkademikModel model)
        {
            string noPendaftaran = User.Identity.Name;
            var listJawaban = model.ListPertanyaan.Select(x => new HasilTes()
            {
                PertanyaanId = x.PertanyaanId,
                SoalId = x.SoalId,
                Jawaban = x.JawabanCalonSiswa
            });
            _tesPenermaanService.Submit(listJawaban, noPendaftaran);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult SeleksiWawancara(string noPendaftaran, string target)
        {
            var soal = _soalPenerimaanService.GetDetailSoal(16);
            var model = new SeleksiWawancaraModel()
            {
                JudulSoal = soal.Judul,
                Target = soal.Target,
                ListPertanyaan = soal.ListPertanyaan.Select(x => new PertanyaanWawancaraTest()
                {
                    PertanyaanId = x.Id,
                    SoalId = x.SoalId,
                    Pertanyaan = x.Isi
                }).ToList()
            };
            return View(model);
        }
        public IActionResult Peraturan()
        {
            return View();
        }
    }
}
