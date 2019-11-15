using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FrontEnd.Web.Mvc.Controllers
{
    public class ErrorController : Controller
    {
        [Route("/Error/{statusCode}")]
        public IActionResult GlobalErrorHandler(int statusCode)
        {
            switch (statusCode)
            {
                case 404:
                    ViewBag.Message = "Maaf Halaman tidak ditemukan";
                    break;
                case 403:
                    ViewBag.Message = "Maaf anda tidak bisa mengakses halaman ini";
                    break;
                default:
                    ViewBag.Message = "Terjadi kesalahan pada server.\nSilahkan coba lagi nanti";
                    break;
            }
            return View();
        }
    }
}
