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
    //[AllowAnonymous]
    [Authorize]
    public class UjianController : Controller
    {
        private readonly ISoal _soalPenerimaanService;
        private readonly IPendaftaran _pendaftaranService;
        private readonly IUjian _ujianService;
        public UjianController(ISoal soalPenerimaanService, IPendaftaran pendaftaranService, IUjian ujianService)
        {
            _ujianService = ujianService;
            _pendaftaranService = pendaftaranService;
            _soalPenerimaanService = soalPenerimaanService;
        }

        [Authorize(Roles = "Calon Siswa")]
        public IActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = "Calon Siswa")]
        public IActionResult MulaiMengerjakanAkademik(int soalId, string kategori)
        {
            int akunId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            bool? isDone = _ujianService.IsDone(akunId, soalId);
            if (isDone == null)
            {
                _ujianService.StartUjianAkademik(akunId, soalId);
                return RedirectToAction(nameof(JawabSoalAkademik), new { soalId });
            }
            else if ((bool)isDone)
            {
                TempData["Pesan"] = "Sudah dikerjakan";
                return RedirectToAction(nameof(PendahuluanAkademik), new { kategori });
            }
            else
            {
                return RedirectToAction(nameof(JawabSoalAkademik), new { soalId });
            }
        }
        
        [Authorize(Roles = "Calon Siswa")]
        public IActionResult PendahuluanAkademik(string kategori)
        {
            int akunId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            int soalPengerjaanId = _ujianService.GetSoalPengerjaanAkademikId(User.Identity.Name, kategori);
            bool? isDone = _ujianService.IsDone(akunId, soalPengerjaanId);
            ViewBag.Pesan = TempData["Pesan"] as string;
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
        
        [HttpGet]
        [Authorize(Roles = "Calon Siswa")]
        public IActionResult SelesaiUjianAkademik()
        {
            return View();
        }
        
        [HttpPost]
        [Authorize(Roles = "Calon Siswa")]
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
            _ujianService.SaveAnswerAkademik(jawaban);
            _ujianService.FinishUjianAkademik(akunId, model.SoalId);
            return View();
        }
        
        [HttpGet]
        [Authorize(Roles = "Calon Siswa")]
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
                    JawabanCalonSiswa = _ujianService.GetAnswerAkademik(akunId, soalId, pertanyaan.Id)
                };
                return View(model);
            }
        }
        
        [HttpPost]
        [Authorize(Roles = "Calon Siswa")]
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
            _ujianService.SaveAnswerAkademik(jawaban);
            return RedirectToAction("JawabSoalAkademik", "Ujian", new { soalId = model.SoalId, qid = model.Tujuan });
        }

        [Authorize(Roles = ("Waka Kesiswaan, PSB Tes"))]
        public IActionResult PendahuluanWawancara(string noPendaftaran, string target)
        {
            int akunId = _pendaftaranService.GetAkunPendaftaranId(noPendaftaran);
            int soalPengerjaanId = _ujianService.GetSoalPengerjaanWawancaraId(noPendaftaran, target);
            bool? isDone = _ujianService.IsDone(akunId, soalPengerjaanId);
            if (isDone == null)
            {
                var soal = _soalPenerimaanService.GetSimpleSoal(soalPengerjaanId);
                var model = new PendahuluanWawancaraModel()
                {
                    SoalWawancara = new CrudSoalWawancara()
                    {
                        Deskripsi = soal.Deskripsi,
                        Id = soal.Id,
                        Jalur = soal.Jalur,
                        Judul = soal.Judul,
                        Target = soal.Target,
                        JumlahPertanyaan = soal.JumlahPertanyaan
                    },
                    NoPendaftaran = noPendaftaran
                };

                return View(model);
            }
            else
            {
                return RedirectToAction("SelesaiWawancara", "Ujian");
            }
        }

        [Authorize(Roles = ("Waka Kesiswaan, PSB Tes"))]
        public IActionResult Wawancara(string noPendaftaran, int soalId)
        {
            int akunId = _pendaftaranService.GetAkunPendaftaranId(noPendaftaran);
            var soalPengerjaan = _soalPenerimaanService.GetDetailSoal(soalId);
            var model = new WawancaraModel()
            {
                AkunPendaftaranId = akunId,
                SoalId = soalId,
                ListPertanyaan = soalPengerjaan.ListPertanyaan
                    .Select(x => new PertanyaanWawancaraTest()
                    {
                        Isi = x.Isi,
                        PertanyaanId = x.Id
                    }).ToList()
            };
            return View(model);
        }

        [HttpGet]
        [Authorize(Roles = ("Waka Kesiswaan, PSB Tes"))]
        public IActionResult SelesaiWawancara()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = ("Waka Kesiswaan, PSB Tes"))]
        public IActionResult SelesaiWawancara(WawancaraModel model)
        {
            int akunId = model.AkunPendaftaranId, soalId = model.SoalId;
            var listHasilTes = model.ListPertanyaan
                .Select(x => new HasilTes()
                {
                    AkunPendaftaranId = akunId,
                    SoalId = soalId,
                    PertanyaanId = x.PertanyaanId,
                    Jawaban = x.Jawaban
                }).ToList();
            _ujianService.SaveWawancara(listHasilTes);
            return View();
        }
    }
}
