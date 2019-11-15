using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FrontEnd.Web.Mvc.Controllers
{
    [Authorize(Roles = "Waka Kesiswaan")]
    public class WakaKesiswaanController : Controller
    {
    }
}
