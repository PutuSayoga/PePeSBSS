using BackEnd.Abstraction;
using FrontEnd.Web.Mvc.Models.PsbPendaftaran;
using Microsoft.AspNetCore.Mvc;
using BackEnd.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace FrontEnd.Web.Mvc.Controllers
{
    [Authorize(Roles = "PSB Pendaftaran, Admin")]
    public class PsbPendaftaranController : Controller
    {
        private IPendaftaran _pendaftaranService;
        public PsbPendaftaranController(IPendaftaran pendaftaranService)
        {
            _pendaftaranService = pendaftaranService;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult DaftarBaru()
            => View();
        [HttpPost]
        public IActionResult DaftarBaru(DaftarBaruModel model)
        {
            if(!model.JalurPendaftaran.Equals("Reguler"))
                if(!((model.JadwalTes >= DateTime.Now) && (model.JadwalTes <= DateTime.Now.AddDays(3))))
                    ModelState.AddModelError(nameof(DaftarBaruModel.JadwalTes),
                        "Jadwal tes maksimal dilaksanakan 3 hari setelah daftar baru");

            if (!ModelState.IsValid)
            {
                ViewBag.Pesan = $"Gagal menambah akun, Data tidak valid";
                return View();
            }
            else
            {
                var newAkun = new AkunPendaftaran()
                {
                    JalurPendaftaran = model.JalurPendaftaran,
                    JadwalTes = model.JadwalTes,
                    CalonSiswa = new CalonSiswa()
                    {
                        Nik = model.Nik,
                        NamaLengkap = model.NamaLengkap,
                        Nisn = model.Nisn
                    }
                };
                int akunId = _pendaftaranService.AddNewAkunPendaftaran(newAkun);
                return RedirectToAction(nameof(BuktiPendaftaran), new { id = akunId });
            }
        }

        public IActionResult ListDaftarBaru()
        {
            var listAkun = _pendaftaranService.GetAllAkunPendaftaran();
            var model = new ListDaftarBaruModel()
            {
                ListAkun = listAkun.Select(x => new AkunDaftarBaru()
                {
                    Id = x.Id,
                    JalurPendaftaran = x.JalurPendaftaran,
                    NamaLengkap = x.CalonSiswa.NamaLengkap,
                    NoPendaftaran = x.NoPendaftaran,
                    Status = x.Status
                })
                .ToList()
            };

            return View(model);
        }

        public IActionResult BuktiPendaftaran(int id)
        {
            var detailAkun = _pendaftaranService.GetAkunPendaftaran(id);
            var model = new BuktiPendaftaranModel()
            {
                Id = detailAkun.Id,
                NoPendaftaran = detailAkun.NoPendaftaran,
                NamaLengkap = detailAkun.CalonSiswa.NamaLengkap,
                JalurPendaftaran = detailAkun.JalurPendaftaran,
                Password = detailAkun.Password,
                JadwalTes = detailAkun.JadwalTes
                    .ToString("dddd, dd MMMM yyyy", new System.Globalization.CultureInfo("id-ID"))
            };
            return View(model);
        }

        public IActionResult DaftarUlang(string noPendaftaran)
        {
            if (noPendaftaran != null)
            {
                var akun = _pendaftaranService.SearchAkunPendaftaran(noPendaftaran);
                if (akun != null)
                {
                    var model = new DaftarUlangModel()
                    {
                        Id = akun.Id,
                        JalurPendaftaran = akun.JalurPendaftaran,
                        NamaLengkap = akun.CalonSiswa.NamaLengkap,
                        NoPendaftaran = akun.NoPendaftaran,
                        Status = akun.Status
                    };
                    return View(model);
                }

                ViewBag.Pesan = "Nomor Pendaftaran tidak ada";
            }

            return View();
        }

        [HttpPost]
        public IActionResult DaftarUlang(int id)
        {
            _pendaftaranService.ReRegist(id);

            return View(null);
        }

        public IActionResult ListDaftarUlang()
        {
            var listAkun = _pendaftaranService.GetAllDaftarUlang();
            var model = new ListDaftarUlangModel()
            {
                ListDaftarUlang = listAkun.Select(x => new AkunDaftarUlang()
                {
                    Id = x.Id,
                    NoPendaftaran = x.NoPendaftaran,
                    AsalSekolah = x.CalonSiswa.AkademikTerakhir.NamaSekolah,
                    JalurPendaftaran = x.JalurPendaftaran,
                    NamaLengkap = x.CalonSiswa.NamaLengkap
                })
                .ToList()
            };

            return View(model);
        }

    }
}
