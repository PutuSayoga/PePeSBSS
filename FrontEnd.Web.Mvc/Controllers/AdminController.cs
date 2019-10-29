using Microsoft.AspNetCore.Mvc;
using BackEnd.IServices;
using BackEnd.Domains;
using System.Collections.Generic;
using System.Linq;
using FrontEnd.Web.Mvc.Models.Staff;

namespace FrontEnd.Web.Mvc.Controllers
{
    public class AdminController : Controller
    {
        private readonly IStaff _staffServices;

        public AdminController(IStaff staffServices)
        {
            _staffServices = staffServices;
        }

        public IActionResult Index()
        {
            return View();
        }

        #region Staff
        public IActionResult KelolaStaff(KelolaStaffModel model)
        {
            var results = _staffServices.GetAllStaff();
            List<MinimizeStaff> minimizeStaffs = new List<MinimizeStaff>();
            foreach (var staff in results)
            {
                minimizeStaffs.Add(new MinimizeStaff()
                {
                    Id = staff.Id,
                    Nip = staff.Nip,
                    NamaLengkap = staff.NamaLengkap,
                    Jabatan = staff.Jabatan,
                    Panitia = staff.Panitia == null ?
                        "-" : $"{staff.Panitia.Acara} sie {staff.Panitia.Divisi}"
                });
            }

            model.DaftarStaff = minimizeStaffs;
            return View(model);
        }

        //[HttpGet]
        //public IActionResult TambahStaff()
        //{
        //    return View();
        //}

        //[HttpPost]
        //public IActionResult TambahStaff(Staff staffBaru)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        ViewBag.Pesan = $"Gagal menambah staff\nData tidak valid";
        //        return View();
        //    }
        //    else
        //    {
        //        string result = _model.TambahStaff(staffBaru);
        //        if (!result.Equals("Sukses"))
        //        {
        //            ViewBag.Pesan = $"Gagal menambah staff, {result}";
        //            return View();
        //        }
        //        else
        //        {
        //            ViewBag.Pesan = "Berhasil menambah staff";
        //            return RedirectToAction("KelolaStaff");
        //        }
        //    }
        //}

        //public IActionResult RincianStaff(int id)
        //{
        //    string pesan = TempData["Pesan"] as string;
        //    if (pesan != null)
        //    {
        //        ViewBag.Pesan = pesan;
        //    }
        //    return View(_model.RincianStaff(id));
        //}

        //[HttpPost]
        //public IActionResult UbahStaff(Staff dataBaru)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        TempData["Pesan"] = "Gagal mengubah staff";
        //        return RedirectToAction("RincianStaff", new { id = dataBaru.Id });
        //    }
        //    else
        //    {
        //        if (_model.UbahStaff(dataBaru) != 1)
        //        {
        //            TempData["Pesan"] = "Gagal mengubah staff";
        //            return RedirectToAction("RincianStaff", new { id = dataBaru.Id });
        //        }
        //        else
        //        {
        //            TempData["Pesan"] = "Berhasil mengubah staff";
        //            return RedirectToAction("RincianStaff", new { id = dataBaru.Id });
        //        }
        //    }
        //}

        //[HttpPost]
        //public IActionResult HapusStaff(int id)
        //{
        //    int rowAffected = _model.HapusStaff(id);
        //    if (rowAffected != 1)
        //    {
        //        ViewBag.Pesan = "Gagal menghapus staff";
        //        return RedirectToAction("KelolaStaff");
        //    }
        //    else
        //    {
        //        ViewBag.Pesan = "Berhasil menghapus staff";
        //        return RedirectToAction("KelolaStaff");
        //    }
        //}

        //[HttpPost]
        //public IActionResult TambahPanitia(int id, Panitia panitia, Staff staff)
        //{
        //    if (_model.TambahPanitia(id, panitia) != 1)
        //    {
        //        ViewBag.Pesan = "Gagal menambah panitia";
        //        return RedirectToAction("RincianStaff", new { id = id });
        //    }
        //    else
        //    {
        //        ViewBag.Pesan = "Berhasil menambah panitia";
        //        return RedirectToAction("RincianStaff", new { id = id });
        //    }
        //}

        //[HttpPost]
        //public IActionResult HapusPanitia(int id)
        //{
        //    if (_model.HapusPanitia(id) != 1)
        //    {
        //        ViewBag.Pesan = "Gagal menghapus panitia";
        //        return RedirectToAction("RincianStaff", new { id = id });
        //    }
        //    else
        //    {
        //        ViewBag.Pesan = "Berhasil menghapus panitia";
        //        return RedirectToAction("RincianStaff", new { id = id });
        //    }
        //}
        #endregion

        //#region Soal Akademik
        //public IActionResult KelolaSoalAkademik()
        //{

        //    var listSoalAkademik = _model.AmbilSemuaSoalAkademik();

        //    return View(new KelolaSoalAkademikViewModel 
        //    { 
        //        ListSoalAkademik = listSoalAkademik
        //    });
        //}

        //[HttpPost]
        //public IActionResult TambahSoalAkademik(KelolaSoalAkademikViewModel soalAkademikBaru)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        ViewBag.Pesan = $"Gagal menambah staff\nData tidak valid";
        //        return View();
        //    }
        //    else
        //    {
        //        string result = _model.TambahSoalAkademik(soalAkademikBaru.TambahSoalAkademik);
        //        if (!result.Equals("Sukses"))
        //        {
        //            ViewBag.Pesan = $"Gagal menambah staff, {result}";
        //            return View();
        //        }
        //        else
        //        {
        //            ViewBag.Pesan = "Berhasil menambah staff";
        //            return RedirectToAction("KelolaSoalAkademik");
        //        }
        //    }
        //}

        //[HttpPost]
        //public IActionResult HapusSoalAkademik(int id)
        //{
        //    int rowAffected = _model.HapusSoalAkademik(id);
        //    if (rowAffected != 1)
        //    {
        //        ViewBag.Pesan = "Gagal menghapus soal akademik";
        //        return RedirectToAction("KelolaSoalAkademik");
        //    }
        //    else
        //    {
        //        ViewBag.Pesan = "Berhasil menghapus soal akademik";
        //        return RedirectToAction("KelolaSoalAkademik");
        //    }
        //}

        //[HttpPost]
        //public IActionResult UbahSoalAkademik()
        //{
        //    return RedirectToAction("KelolaSoalAkademik");
        //}

        //public IActionResult RincianSoalAkademik(int id)
        //{
        //    var model = _model.RincianSoalAkademik(id);
        //    return View(model);
        //}

        //[HttpPost]
        //public IActionResult TambahPertanyaanAkademik()
        //{
        //    return RedirectToAction("RincianSoalAkademik");
        //}

        //[HttpPost]
        //public IActionResult UbahPertanyaanAkademik()
        //{
        //    return RedirectToAction("RincianSoalAkademik");
        //}

        //[HttpPost]
        //public IActionResult HapusPertanyaanAkademik()
        //{
        //    return RedirectToAction("RincianSoalAkademik");
        //}
        //#endregion

        //#region Soal Wawancara
        //public IActionResult KelolaSoalWawancara()
        //{
        //    return View();
        //}

        //[HttpPost]
        //public IActionResult TambahSoalWawancara()
        //{
        //    return RedirectToAction("KelolaSoalfWawancara");
        //}

        //[HttpPost]
        //public IActionResult HapusSoalWawancara(int id)
        //{
        //    return RedirectToAction("KelolaSoalWawancara");
        //}

        //[HttpPost]
        //public IActionResult UbahSoalWawancara()
        //{
        //    return RedirectToAction("KelolaSoalWawancara");
        //}

        //public IActionResult RincianSoalWawancara(int id)
        //{
        //    return View();
        //}

        //[HttpPost]
        //public IActionResult TambahPertanyaanWawancara()
        //{
        //    return RedirectToAction("RincianSoalWawancara");
        //}

        //[HttpPost]
        //public IActionResult UbahPertanyaanWawancara()
        //{
        //    return RedirectToAction("RincianSoalWawancara");
        //}

        //[HttpPost]
        //public IActionResult HapusPertanyaanWawancara()
        //{
        //    return RedirectToAction("RincianSoalWawancara");
        //}
        //#endregion

        //#region Siswa
        //public IActionResult DaftarSiswa()
        //{
        //    return View();
        //}
        //#endregion
    }
}
