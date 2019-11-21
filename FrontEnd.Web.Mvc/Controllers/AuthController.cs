using BackEnd.Abstraction;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using FrontEnd.Web.Mvc.Models.Auth;
using Microsoft.AspNetCore.Authorization;

namespace FrontEnd.Web.Mvc.Controllers
{
    [AllowAnonymous]
    public class AuthController : Controller
    {
        private readonly ICalonSiswa _calonSiswaService;
        private readonly IStaffSma _staffSmaService;
        private readonly ITesPenerimaan _tesAkademikService;

        public AuthController(ICalonSiswa calonSiswaService, IStaffSma staffSmaService, ITesPenerimaan tesAkademikService)
        {
            _calonSiswaService = calonSiswaService;
            _staffSmaService = staffSmaService;
            _tesAkademikService = tesAkademikService;
        }

        public IActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                if (User.IsInRole("Calon Siswa"))
                    return RedirectToAction("Index", "CalonSiswa");
                else if (User.IsInRole("Admin"))
                    return RedirectToAction("Index", "Admin");
                else if (User.IsInRole("Waka Kesiswaan"))
                    return RedirectToAction("Index", "WakaKesiswaan");
                else if (User.IsInRole("Tata Usaha"))
                    return RedirectToAction("Index", "TataUsaha");
                else if (User.IsInRole("PSB Pendaftaran"))
                    return RedirectToAction("Index", "PsbPendaftaran");
                else if (User.IsInRole("PSB Tes"))
                    return RedirectToAction("Index", "PsbTes");
                else
                    throw new Exception();
            }

            return RedirectToAction(nameof(LoginCalonSiswa));
        }
        [HttpGet]
        public IActionResult LoginCalonSiswa(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction(nameof(Index));
            }
            return View();
        }
        [HttpPost]
        public IActionResult LoginCalonSiswa(LoginCalonSiswaModel model, string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            if (!ModelState.IsValid)
            {
                return View();
            }
            else
            {
                bool isLogInSuccess = _calonSiswaService.IsLogin(model.NoPendaftaran, model.Password);
                if (!isLogInSuccess)
                {
                    return View();
                }
                else
                {
                    var userClaims = new List<Claim>()
                    {
                        new Claim(ClaimTypes.Name, model.NoPendaftaran),
                        new Claim(ClaimTypes.Role, "Calon Siswa")
                    };
                    var userIdentity = new ClaimsIdentity(userClaims, "CalonSiswaLogin");
                    var userPrincipal = new ClaimsPrincipal(userIdentity);
                    HttpContext.SignInAsync(
                        CookieAuthenticationDefaults.AuthenticationScheme,
                        userPrincipal);

                    if (returnUrl != null)
                    {
                        return Redirect(returnUrl);
                    }

                    return RedirectToAction(nameof(Index));
                }
            }
        }

        [HttpGet]
        public IActionResult LoginStaff(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction(nameof(Index));
            }
            return View();
        }
        [HttpPost]
        public IActionResult LoginStaff(LoginStaffModel model, string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            if (!ModelState.IsValid)
            {
                return View();
            }
            else
            {
                bool isLogIn = _staffSmaService.IsLogin(model.Username, model.Password, model.Role);
                if (!isLogIn)
                {
                    return View();
                }
                else
                {
                    var userClaims = new List<Claim>()
                    {
                        new Claim(ClaimTypes.Name, model.Username),
                        new Claim(ClaimTypes.Role, model.Role)
                    };
                    var userIdentity = new ClaimsIdentity(userClaims, "StaffLogin");
                    var userPrincipal = new ClaimsPrincipal(userIdentity);
                    HttpContext.SignInAsync(
                        CookieAuthenticationDefaults.AuthenticationScheme,
                        userPrincipal);

                    if (returnUrl != null)
                    {
                        return Redirect(returnUrl);
                    }

                    return RedirectToAction(nameof(Index));
                }
            }
        }

        public IActionResult Logout()
        {
            if (User.IsInRole("Calon Siswa"))
            {
                HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                return RedirectToAction(nameof(LoginCalonSiswa));
            }
            else
            {
                HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                return RedirectToAction(nameof(LoginStaff));
            }
        }
    }
}
