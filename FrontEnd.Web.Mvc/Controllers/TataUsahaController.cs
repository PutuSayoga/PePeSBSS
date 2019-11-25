using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FrontEnd.Web.Mvc.Controllers
{
    public class TataUsahaController:Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult KelolaMutasiMasuk()
        {
            return View();
        }
        public IActionResult KelolaMutasiKeluar()
        {
            return View();
        }
        public IActionResult DaftarSiswa()
        {
            return View();
        }
    }
}
