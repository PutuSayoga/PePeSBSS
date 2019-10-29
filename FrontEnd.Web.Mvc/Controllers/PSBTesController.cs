using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FrontEnd.Web.Mvc.Controllers
{
    public class PSBTesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
