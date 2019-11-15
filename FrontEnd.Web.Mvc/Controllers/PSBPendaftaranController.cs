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
    [Authorize(Roles = "PSB Pendaftaran")]
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
        {
            return View();
        }
        [HttpPost]
        public IActionResult DaftarBaru(DaftarBaruModel model)
        {
            if(!model.JalurPendaftaran.Equals("Reguler"))
                if(!((model.JadwalTes >= DateTime.Now) && 
                    (model.JadwalTes <= DateTime.Now.AddDays(3))))
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
                _calonSiswaService.AddAkunPendaftaran(newAkun);
                return View();
            }
        }

        public IActionResult ListDaftarBaru()
        {
            var listAkun = _calonSiswaService.GetAllAkunPendaftaran();
            var model = new ListDaftarBaruModel()
            {
                ListAkun = listAkun
            };

            return View(model);
        }

        public IActionResult BuktiPendaftaran(int id)
        {
            var result = _calonSiswaService.GetDetailAkunPendaftaran(id);
            var model = new BuktiPendaftaranModel()
            {
                NoPendaftaran = result.NoPendaftaran,
                NamaLengkap = result.ACalonSiswa.NamaLengkap,
                JalurPendaftaran = result.JalurPendaftaran,
                Password = result.Password,
                JadwalTes = result.JadwalTes
                    .ToString("dddd, dd MMMM yyyy", new System.Globalization.CultureInfo("id-ID"))
            };
            return View(model);
        }

        public IActionResult DaftarUlang()
        {
            return View();
        }

        public IActionResult ListDaftarUlang()
        {
            var listAkun = _calonSiswaService.GetAllDaftarUlang();
            var model = new ListDaftarUlangModel()
            {
                ListDaftarUlang = listAkun
            };

            return View(model);
        }

    }
}
