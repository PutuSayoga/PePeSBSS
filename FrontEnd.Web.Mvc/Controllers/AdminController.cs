using Microsoft.AspNetCore.Mvc;
using BackEnd.Abstraction;
using BackEnd.Domains;
using System.Collections.Generic;
using System.Linq;
using FrontEnd.Web.Mvc.Models.Admin;

namespace FrontEnd.Web.Mvc.Controllers
{
    public class AdminController : Controller
    {
        private readonly IStaff _staffServices;
        private readonly ISoalPenerimaan _soalPenerimaanService;
        public AdminController(IStaff staffServices, ISoalPenerimaan soalPenerimaanService)
        {
            _staffServices = staffServices;
            _soalPenerimaanService = soalPenerimaanService;
        }

        public IActionResult Index()
        {
            return View();
        }

        #region Staff
        public IActionResult KelolaStaff()
        {
            ViewBag.Pesan = TempData["Pesan"] as string;
            var model = new KelolaStaffModel()
            {
                DaftarStaff = _staffServices.GetAllStaff()
            };
            return View(model);
        }
        [HttpGet]
        public IActionResult TambahStaff() => View();
        [HttpPost]
        public IActionResult TambahStaff(TambahStaffModel model)
        {
            // Cek valid
            if (!ModelState.IsValid)
            {
                ViewBag.Pesan = "Gagal menambah staff. Data tidak valid";
                return View();
            }
            else
            {
                var newStaff = new Staff()
                {
                    Nip = model.Nip,
                    NamaLengkap = model.NamaLengkap,
                    Email = model.Email,
                    NoHp = model.NoHp,
                    Jabatan = model.Jabatan,
                    Username = model.Username,
                    Password = model.Password
                };
                string result = _staffServices.AddStaff(newStaff);

                if (!result.Equals("Sukses"))
                {
                    ViewBag.Pesan = $"Gagal menambah staff. {result}";
                    return View();
                }
                else
                {
                    TempData["Pesan"] = $"Berhasil menambah staff dengan NIP {newStaff.Nip}";
                    return RedirectToAction(nameof(KelolaStaff));
                }
            }
        }
        public IActionResult RincianStaff(int id)
        {
            ViewBag.Pesan = TempData["Pesan"] as string;

            var staff = _staffServices.DetailStaff(id);
            var model = new RincianStaffModel()
            {
                Id = staff.Id,
                Staff = new TambahStaffModel()
                {
                    Nip = staff.Nip,
                    NamaLengkap = staff.NamaLengkap,
                    Jabatan = staff.Jabatan,
                    Email = staff.Email,
                    NoHp = staff.NoHp,
                    Username = staff.Username
                },
                Panitia = new TambahPanitiaModel()
                {
                    Acara = staff.APanitia.Acara,
                    Divisi = staff.APanitia.Divisi
                }
            };

            return View(model);
        }
        [HttpPost]
        public IActionResult UbahStaff(RincianStaffModel model)
        {
            // Cek valid
            if (!ModelState.IsValid)
            {
                TempData["Pesan"] = "Gagal mengubah staff. Data tidak valid";
                return RedirectToAction(nameof(RincianStaff), new { id = model.Id });
            }
            else
            {
                // Petakan dari model
                var dataBaru = new Staff()
                {
                    Id = model.Id,
                    NamaLengkap = model.Staff.NamaLengkap,
                    Email = model.Staff.Email,
                    NoHp = model.Staff.NoHp,
                    Jabatan = model.Staff.Jabatan,
                    Password = model.Staff.Password
                };

                // Service ubah staff
                _staffServices.UpdateStaff(dataBaru);

                TempData["Pesan"] = "Berhasil mengubah staff";
                return RedirectToAction(nameof(RincianStaff), new { id = model.Id });
            }
        }
        [HttpPost]
        public IActionResult HapusStaff(int id, string nip)
        {
            // Service hapus staff
            _staffServices.DeleteStaff(id);

            TempData["Pesan"] = $"Berhasil menghapus staff dengan NIP {nip}";
            return RedirectToAction(nameof(KelolaStaff));
        }
        [HttpPost]
        public IActionResult TambahPanitia(RincianStaffModel model)
        {
            // Cek valid
            if (!ModelState.IsValid)
            {
                TempData["Pesan"] = "Gagal menambah panitia. Data tidak valid";
                return RedirectToAction(nameof(RincianStaff), new { id = model.Id });
            }
            else
            {
                var id = model.Id;
                var panitiaBaru = new Panitia()
                {
                    Acara = model.Panitia.Acara,
                    Divisi = model.Panitia.Divisi
                };
                _staffServices.AddPanitiaToStaff(id, panitiaBaru);

                TempData["Pesan"] = "Berhasil menambahkan panitia";
                return RedirectToAction(nameof(RincianStaff), new { id = model.Id });
            }
        }
        [HttpPost]
        public IActionResult HapusPanitia(int idStaff)
        {
            // Service hapus panitia
            _staffServices.DeletePanitiaFromStaff(idStaff);

            ViewBag.Pesan = "Berhasil menghapus panitia";
            return RedirectToAction(nameof(RincianStaff), new { id = idStaff });
        }
        #endregion

        #region Soal Akademik
        public IActionResult KelolaSoalAkademik()
        {
            ViewBag.Pesan = TempData["Pesan"] as string;

            var listSoalAkademik = _soalPenerimaanService.GetAllSoalAkademik();

            return View();
        }

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
        #endregion

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
