using BackEnd.Abstraction;
using BackEnd.Domains;
using FrontEnd.Web.Mvc.Models.Admin;
using FrontEnd.Web.Mvc.Models.Ujian;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace FrontEnd.Web.Mvc.Controllers
{
    [Authorize(Roles = "Calon Siswa, Admin")]
    //[AllowAnonymous]
    public class UjianController : Controller
    {
        ISoalPenerimaan _soalPenerimaanService;
        IUjian _ujianService;
        public UjianController(ISoalPenerimaan soalPenerimaanService, IUjian ujianService)
        {
            _ujianService = ujianService;
            _soalPenerimaanService = soalPenerimaanService;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult MulaiMengerjakan(int soalId)
        {
            int akunId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            _ujianService.StartUjian(akunId, soalId);
            return RedirectToAction(nameof(JawabSoalAkademik), new { soalId });
        }
        public IActionResult PendahuluanUjian(string kategori)
        {
            int akunId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            int soalPengerjaanId = _ujianService.GetSoalPengerjaanAkademikId(User.Identity.Name, kategori);
            bool? isDone = _ujianService.IsDone(akunId, soalPengerjaanId);
            if (isDone == null)
            {
                var soal = _soalPenerimaanService.GetSimpleSoal(soalPengerjaanId);
                var model = new CrudSoalAkademik()
                {
                    BatasWaktu = soal.BatasWaktu,
                    Deskripsi = soal.Deskripsi,
                    Id = soal.Id,
                    Judul = soal.Judul,
                    JumlahPertanyaan = soal.JumlahPertanyaan,
                    Kategori = soal.Kategori
                };
                return View(model);
            }
            else if ((bool)isDone)
            {
                return View();
            }
            else
            {
                return RedirectToAction(nameof(JawabSoalAkademik), new { soalId = soalPengerjaanId });
            }
        }
        public IActionResult SelesaiUjianAkademik()
        {
            return View();
        }
        [HttpPost]
        public IActionResult SelesaiUjianAkademik(JawabSoalAkademikModel model)
        {
            int akunId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var jawaban = new HasilTes()
            {
                AkunPendaftaranId = akunId,
                SoalId = model.SoalId,
                PertanyaanId = model.PertanyaanId,
                Jawaban = model.JawabanCalonSiswa
            };
            _ujianService.SaveAnswer(jawaban);
            _ujianService.FinishUjian(akunId, model.SoalId);
            return View();
        }
        [HttpGet]
        public IActionResult JawabSoalAkademik(int soalId, int? qid)
        {
            int akunId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            bool? isDone = _ujianService.IsDone(akunId, soalId);
            if (isDone == null)
            {
                return RedirectToAction(nameof(Index));
            }
            else if ((bool)isDone)
            {
                return RedirectToAction(nameof(SelesaiUjianAkademik));
            }
            else
            {
                var detailUjian = _ujianService.GetUjian(akunId, soalId);
                var listPertanyaan = _soalPenerimaanService.GetDetailSoal(soalId).ListPertanyaan.ToList();
                int pertanyaanIndex;
                if (qid == null)
                    pertanyaanIndex = 0;
                else
                    pertanyaanIndex = listPertanyaan.FindIndex(x => x.Id == qid);

                var pertanyaan = listPertanyaan[pertanyaanIndex];
                var model = new JawabSoalAkademikModel()
                {
                    NoPertanyaan = pertanyaanIndex + 1,
                    MapPertanyaan = listPertanyaan.Select(x => x.Id).ToList(),
                    BatasWaktu = detailUjian.WaktuBerakhir,
                    PertanyaanId = pertanyaan.Id,
                    SoalId = pertanyaan.SoalId,
                    OpsiA = pertanyaan.OpsiA,
                    OpsiB = pertanyaan.OpsiB,
                    OpsiC = pertanyaan.OpsiC,
                    OpsiD = pertanyaan.OpsiD,
                    OpsiE = pertanyaan.OpsiE,
                    Pertanyaan = pertanyaan.Isi,
                    NextId = (pertanyaanIndex + 1) < listPertanyaan.Count ? listPertanyaan[pertanyaanIndex + 1].Id : -1,
                    PrevId = (pertanyaanIndex - 1) > -1 ? listPertanyaan[pertanyaanIndex - 1].Id : -1,
                    JawabanCalonSiswa = _ujianService.GetAnswer(akunId, soalId, pertanyaan.Id)
                };
                return View(model);
            }
        }
        [HttpPost]
        public IActionResult JawabSoalAkademik(JawabSoalAkademikModel model)
        {
            int akunId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var jawaban = new HasilTes()
            {
                AkunPendaftaranId = akunId,
                SoalId = model.SoalId,
                PertanyaanId = model.PertanyaanId,
                Jawaban = model.JawabanCalonSiswa
            };
            _ujianService.SaveAnswer(jawaban);
            return RedirectToAction("JawabSoalAkademik", "Ujian", new { soalId = model.SoalId, qid = model.Tujuan });
        }

        [Authorize(Roles = ("Waka Kesiswaan, Psb Tes"))]
        public IActionResult SeleksiWawancara(string noPendaftaran, string target)
        {
            var soal = _soalPenerimaanService.GetDetailSoal(13);
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
    }
}
