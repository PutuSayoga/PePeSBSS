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
        private IPendaftaran _calonSiswaService;
        public PsbPendaftaranController(IPendaftaran calonSiswaService)
        {
            _calonSiswaService = calonSiswaService;
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
                ViewBag.Pesan = $"Gagal menambah akun\nData tidak valid";
                return View();
            }
            else
            {
                var newAkun = new AkunPendaftaran()
                {
                    JalurPendaftaran = model.JalurPendaftaran,
                    JadwalTes = model.JadwalTes,
                    ACalonSiswa = new CalonSiswa()
                    {
                        Nik = model.Nik,
                        NamaLengkap = model.NamaLengkap,
                        Nisn = model.Nisn
                    }
                };
                int akunId = _calonSiswaService.AddNewAkunPendaftaran(newAkun);

                return RedirectToAction(nameof(BuktiPendaftaran), new { id = akunId });
            }
        }

        public IActionResult ListDaftarBaru()
        {
            var listAkun = _calonSiswaService.GetAllAkunPendaftaran();
            var model = new ListDaftarBaruModel()
            {
                ListAkun = listAkun.Select(x => new AkunDaftarBaru()
                {
                    Id = x.Id,
                    JalurPendaftaran = x.JalurPendaftaran,
                    NamaLengkap = x.ACalonSiswa.NamaLengkap,
                    NoPendaftaran = x.NoPendaftaran,
                    Status = x.Status
                })
            };

            return View(model);
        }

        public IActionResult BuktiPendaftaran(int id)
        {
            var detailAkun = _calonSiswaService.GetAkunPendaftaran(id);
            var model = new BuktiPendaftaranModel()
            {
                NoPendaftaran = detailAkun.NoPendaftaran,
                NamaLengkap = detailAkun.ACalonSiswa.NamaLengkap,
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
                int id = _calonSiswaService.GetIdAkunPendaftaran(noPendaftaran);
                var akun = _calonSiswaService.GetAkunPendaftaran(id);
                var model = new DaftarUlangModel()
                {
                    Id = akun.Id,
                    JalurPendaftaran = akun.JalurPendaftaran,
                    NamaLengkap = akun.ACalonSiswa.NamaLengkap,
                    NoPendaftaran = akun.NoPendaftaran,
                    Status = akun.Status
                };
                return View(model);
            }

            return View();
        }

        [HttpPost]
        public IActionResult DaftarUlang(int id)
        {
            _calonSiswaService.ReRegist(id);

            return View(null);
        }

        public IActionResult ListDaftarUlang()
        {
            var listAkun = _calonSiswaService.GetAllDaftarUlang();
            var model = new ListDaftarUlangModel()
            {
                ListDaftarUlang = listAkun.Select(x => new AkunDaftarUlang()
                {
                    Id = x.Id,
                    NoPendaftaran = x.NoPendaftaran,
                    AsalSekolah = x.ACalonSiswa.AAkademikTerakhir.NamaSekolah,
                    JalurPendaftaran = x.JalurPendaftaran,
                    NamaLengkap = x.ACalonSiswa.NamaLengkap
                })
            };

            return View(model);
        }

    }
}
