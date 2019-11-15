using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FrontEnd.Web.Mvc.Controllers
{
    [Authorize(Roles = "Calon Siswa, Admin")]
    public class TesAkademikController : Controller
    {
        public IActionResult TesAkademik()
        {
            return View();
        }
    }
}
