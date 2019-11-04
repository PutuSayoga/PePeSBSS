using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FrontEnd.Web.MvcApp.Controllers
{
    public class PSBPendaftaranController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult DaftarBaru()
        {
            return View();
        }

        public IActionResult ListDaftarBaru()
        {
            return View();
        }

        public IActionResult BuktiPendaftaran()
        {
            return View();
        }

        public IActionResult DaftarUlang()
        {
            return View();
        }

        public IActionResult ListDaftarUlang()
        {
            return View();
        }

    }
}
