using BackEnd.Abstraction;
using FrontEnd.Web.Mvc.Models.PsbPendaftaran;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FrontEnd.Web.Mvc.Controllers
{
    [Authorize]
    public class PrintController:Controller
    {
        private IPendaftaran _calonSiswaService;
        public PrintController(IPendaftaran calonSiswaService)
        {
            _calonSiswaService = calonSiswaService;
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
    }
}
