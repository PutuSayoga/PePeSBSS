using BackEnd.Abstraction;
using BackEnd.Domains;
using FrontEnd.Web.Mvc.Models.Ujian;
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
        IUjian _tesPenermaanService;
        IPendaftaran _pendaftaranService;
        public UjianController(ISoalPenerimaan soalPenerimaanService, IUjian tesPenerimaanService, IPendaftaran pendaftaranService)
        {
            _tesPenermaanService = tesPenerimaanService;
            _soalPenerimaanService = soalPenerimaanService;
            _pendaftaranService = pendaftaranService;
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
        public IActionResult JawabSoalAkademik(int soalId, int qid)
        {
            int akunPendaftaranId = _pendaftaranService.GetAkunPendaftaranId(User.Identity.Name);
            var detailUjian = _tesPenermaanService.StartTest(akunPendaftaranId, soalId);
            if (detailUjian.WaktuBerakhir < DateTime.Now)
            {
                return RedirectToAction("Index");
            }
            var soal = _soalPenerimaanService.GetDetailSoal(soalId);
            var listPertanyaan = soal.PertanyaanS.Select(x => x.Id).ToArray();
            var index = Array.FindIndex(listPertanyaan, x => x == qid);

            if (qid == 0)
            {
                return RedirectToAction("JawabSoalAkademik", new { soalId, qid = listPertanyaan[0] });
            }

            var pertanyaan = _soalPenerimaanService.GetPertanyaan(qid, soalId);
            var model = new JawabSoalAkademikModel()
            {
                BatasWaktu = detailUjian.WaktuBerakhir,
                Pertanyaan = new PertanyaanTest()
                {
                    PertanyaanId = pertanyaan.Id,
                    SoalId = pertanyaan.SoalId,
                    OpsiA = pertanyaan.OpsiA,
                    OpsiB = pertanyaan.OpsiB,
                    OpsiC = pertanyaan.OpsiC,
                    OpsiD = pertanyaan.OpsiD,
                    OpsiE = pertanyaan.OpsiE,
                    Pertanyaan = pertanyaan.Isi
                },
                NextId = index + 1 < listPertanyaan.Length ? listPertanyaan[index + 1] : -1,
                PrevId = index - 1 > -1 ? listPertanyaan[index - 1] : -1,
            };
            return View(model);
        }

        [HttpPost]
        public IActionResult JawabSoalAkademik(JawabSoalAkademikModel model)
        {
            string noPendaftaran = User.Identity.Name;
            //var listJawaban = model.ListPertanyaan.Select(x => new HasilTes()
            //{
            //    PertanyaanId = x.PertanyaanId,
            //    SoalId = x.SoalId,
            //    Jawaban = x.JawabanCalonSiswa
            //});
            //_tesPenermaanService.Submit(listJawaban, noPendaftaran);
            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles =("Waka Kesiswaan, Psb Tes"))]
        public IActionResult SeleksiWawancara(string noPendaftaran, string target)
        {
            var soal = _soalPenerimaanService.GetDetailSoal(13);
            var model = new SeleksiWawancaraModel()
            {
                JudulSoal = soal.Judul,
                Target = soal.Target,
                ListPertanyaan = soal.PertanyaanS.Select(x => new PertanyaanWawancaraTest()
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
