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
        private IPendaftaran _pendaftaranService;
        public PrintController(IPendaftaran pendaftaranService)
        {
            _pendaftaranService = pendaftaranService;
        }

        public IActionResult BuktiPendaftaran(int id)
        {
            var detailAkun = _pendaftaranService.GetAkunPendaftaran(id);
            var model = new BuktiPendaftaranModel()
            {
                NoPendaftaran = detailAkun.NoPendaftaran,
                NamaLengkap = detailAkun.CalonSiswa.NamaLengkap,
                JalurPendaftaran = detailAkun.JalurPendaftaran,
                Password = detailAkun.Password,
                JadwalTes = detailAkun.JadwalTes
                    .ToString("dddd, dd MMMM yyyy", new System.Globalization.CultureInfo("id-ID"))
            };
            return View(model);
        }
    }
}
