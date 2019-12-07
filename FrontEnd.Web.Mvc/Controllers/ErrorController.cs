using FrontEnd.Web.Mvc.Models.Error;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FrontEnd.Web.Mvc.Controllers
{
    [AllowAnonymous]
    public class ErrorController : Controller
    {
        [Route("/Error/{statusCode}")]
        public IActionResult GlobalErrorHandler(int statusCode)
        {
            var model = new GlobalErrorHandlerModel();
            switch (statusCode)
            {
                case 404:
                    model.StatusCode = 404;
                    model.Message = "Halaman tidak ditemukan";
                    model.ExtraMessage = "Maaf halaman yang anda cari tidak ada";
                    break;
                case 403:
                    model.StatusCode = 403;
                    model.Message = "Tidak memiliki hak akses";
                    model.ExtraMessage = "Maaf anda tidak boleh mengakses halaman ini";
                    break;
                case 500:
                    model.StatusCode = 500;
                    model.Message = "Terjadi kesalahan pada server.\nSilahkan coba lagi nanti";
                    model.ExtraMessage = @"Maaf Terjadi kesalahan pada server saat mengakses halaman ini\n
                        Silahkan Coba lagi nanti";
                    break;
            }
            return View(model);
        }
    }
}
