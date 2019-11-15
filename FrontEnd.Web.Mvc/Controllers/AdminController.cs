using Microsoft.AspNetCore.Mvc;
using BackEnd.Abstraction;
using BackEnd.Domains;
using System.Collections.Generic;
using System.Linq;
using FrontEnd.Web.Mvc.Models.Admin;
using Microsoft.AspNetCore.Authorization;

namespace FrontEnd.Web.Mvc.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly IStaffSma _staffServices;
        private readonly ISoalPenerimaan _soalService;
        public AdminController(IStaffSma staffServices, ISoalPenerimaan soalService)
        {
            _staffServices = staffServices;
            _soalService = soalService;
        }

        public IActionResult Index()
        {
            return View();
        }

        #region Staff
        public IActionResult KelolaStaff()
        {
            ViewBag.Pesan = TempData["Pesan"] as string;
            var listStaff = _staffServices.GetAllStaff();
            var model = new KelolaStaffModel()
            {
                DaftarStaff = listStaff
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
                }
            };
            if (staff.APanitia != null)
            {
                model.Panitia = new TambahPanitiaModel()
                {
                    Acara = staff.APanitia.Acara,
                    Divisi = staff.APanitia.Divisi
                };
            }

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
                var panitiaBaru = new Panitia()
                {
                    StaffId = model.Id,
                    Acara = model.Panitia.Acara,
                    Divisi = model.Panitia.Divisi
                };
                _staffServices.AddPanitiaToStaff(panitiaBaru);

                TempData["Pesan"] = "Berhasil menambahkan panitia";
                return RedirectToAction(nameof(RincianStaff), new { id = model.Id });
            }
        }
        [HttpPost]
        public IActionResult HapusPanitia(int staffId)
        {
            // Service hapus panitia
            _staffServices.DeletePanitiaFromStaff(staffId);

            TempData["Pesan"] = "Berhasil menghapus panitia";
            return RedirectToAction(nameof(RincianStaff), new { id = staffId });
        }
        #endregion

        #region Soal Akademik
        public IActionResult KelolaSoalAkademik()
        {
            ViewBag.Pesan = TempData["Pesan"] as string;

            var listSoalAkademik = _soalService.GetAllSoalAkademik();
            var model = new KelolaSoalAkademikModel()
            {
                ListSoal = listSoalAkademik
            };

            return View(model);
        }
        [HttpPost]
        public IActionResult TambahSoalAkademik(KelolaSoalAkademikModel model)
        {
            if (!ModelState.IsValid)
            {
                TempData["Pesan"] = "Gagal menambah soal\nData tidak valid";
                return RedirectToAction(nameof(KelolaSoalAkademik));
            }
            else
            {
                var soalAkademikBaru = new Soal()
                {
                    Judul = model.SoalAkademik.Judul,
                    BatasWaktu = model.SoalAkademik.BatasWaktu,
                    Kategori = model.SoalAkademik.Kategori,
                    Deskripsi = model.SoalAkademik.Deskripsi,
                };
                _soalService.AddSoal(soalAkademikBaru);
                TempData["Pesan"] = "Soal berhasil ditambah";
                return RedirectToAction(nameof(KelolaSoalAkademik));
            }
        }
        [HttpPost]
        public IActionResult HapusSoalAkademik(int id, string nama)
        {
            _soalService.DeleteSoal(id);
            TempData["Pesan"] = $"Berhasil menghapus soal {nama}";
            return RedirectToAction(nameof(KelolaSoalAkademik));
        }
        [HttpGet]
        public IActionResult UbahSoalAkademik(int id)
        {
            var soal = _soalService.GetSimpleSoal(id);
            var model = new CrudSoalAkademik()
            {
                Judul = soal.Judul,
                Kategori = soal.Kategori,
                BatasWaktu = soal.BatasWaktu,
                Deskripsi = soal.Deskripsi
            };
            return Json(model);
        }
        [HttpPost]
        public IActionResult UbahSoalAkademik(KelolaSoalAkademikModel model)
        {
            if (!ModelState.IsValid)
            {
                TempData["Pesan"] = $"Gagal mengubah soal\nData tidak valid";
                return RedirectToAction(nameof(KelolaSoalAkademik));
            }
            else
            {
                var dataBaru = new Soal()
                {
                    Judul = model.SoalAkademik.Judul,
                    BatasWaktu = model.SoalAkademik.BatasWaktu,
                    Kategori = model.SoalAkademik.Kategori,
                    Deskripsi = model.SoalAkademik.Deskripsi,
                };
                _soalService.UpdateSoal(dataBaru);
                TempData["Pesan"] = $"Soal {dataBaru.Judul} berhasil diubah";
                return RedirectToAction(nameof(KelolaSoalAkademik));
            }
        }
        public IActionResult RincianSoalAkademik(int id)
        {
            var soal = _soalService.GetDetailSoal(id);
            var model = new RincianSoalAkademikModel()
            {
                Id = soal.Id,
                JudulSoal = soal.Judul,
                BatasWaktu = soal.BatasWaktu,
                JumlahPertanyaan = soal.JumlahPertanyaan,
                Kategori = soal.Kategori,
                Deskripsi = soal.Deskripsi,
                ListPertanyaanAkademik = soal.ListPertanyaan
                    .Select(x => new CrudPertanyaanAkademikModel()
                    {
                        Id = x.Id,
                        Isi = x.Isi,
                        OpsiA = x.OpsiA,
                        OpsiB = x.OpsiB,
                        OpsiC = x.OpsiC,
                        OpsiD = x.OpsiD,
                        OpsiE = x.OpsiE,
                        Jawaban = x.Jawaban
                    }).ToList()
            };
            return View(model);
        }
        [HttpPost]
        public IActionResult TambahPertanyaanAkademik(RincianSoalAkademikModel model)
        {
            var newPertanyaan = new Pertanyaan()
            {
                SoalId = model.Id,
                Isi = model.CrudPertanyaanAkademik.Isi,
                OpsiA = model.CrudPertanyaanAkademik.OpsiA,
                OpsiB = model.CrudPertanyaanAkademik.OpsiB,
                OpsiC = model.CrudPertanyaanAkademik.OpsiC,
                OpsiD = model.CrudPertanyaanAkademik.OpsiD,
                OpsiE = model.CrudPertanyaanAkademik.OpsiE,
                Jawaban = model.CrudPertanyaanAkademik.Jawaban
            };
            _soalService.AddPertanyaan(newPertanyaan);
            return RedirectToAction(nameof(RincianSoalAkademik), new { id = model.Id });
        }
        [HttpPost]
        public IActionResult UbahPertanyaanAkademik(RincianSoalAkademikModel model)
        {
            var newData = new Pertanyaan()
            {
                SoalId = model.Id,
                Isi = model.CrudPertanyaanAkademik.Isi,
                OpsiA = model.CrudPertanyaanAkademik.OpsiA,
                OpsiB = model.CrudPertanyaanAkademik.OpsiB,
                OpsiC = model.CrudPertanyaanAkademik.OpsiC,
                OpsiD = model.CrudPertanyaanAkademik.OpsiD,
                OpsiE = model.CrudPertanyaanAkademik.OpsiE,
                Jawaban = model.CrudPertanyaanAkademik.Jawaban
            };
            _soalService.UpdatePertanyaan(newData);
            return RedirectToAction(nameof(RincianSoalAkademik), new { id = model.Id });
        }
        [HttpPost]
        public IActionResult HapusPertanyaanAkademik(int soalId, int id)
        {
            _soalService.DeletePertanyaan(soalId, id);
            return RedirectToAction(nameof(RincianSoalAkademik), new { id = soalId });
        }
        #endregion

        #region Soal Wawancara
        public IActionResult KelolaSoalWawancara()
        {
            ViewBag.Pesan = TempData["Pesan"] as string;

            var listSoalWawancara = _soalService.GetAllSoalWawancara();
            var model = new KelolaSoalWawancaraModel()
            {
                ListSoal = listSoalWawancara
            };

            return View(model);
        }
        [HttpPost]
        public IActionResult TambahSoalWawancara(KelolaSoalWawancaraModel model)
        {
            if (!ModelState.IsValid)
            {
                TempData["Pesan"] = $"Gagal menambah soal\nData tidak valid";
                return RedirectToAction(nameof(KelolaSoalWawancara));
            }
            else
            {
                var soalWawancaraBaru = new Soal()
                {
                    Judul = model.SoalWawancara.Judul,
                    Kategori = model.SoalWawancara.Kategori,
                    Jalur = model.SoalWawancara.Jalur,
                    Target = model.SoalWawancara.Target,
                    Deskripsi = model.SoalWawancara.Deskripsi,
                };
                _soalService.AddSoal(soalWawancaraBaru);
                TempData["Pesan"] = "Soal berhasil ditambah";
                return RedirectToAction(nameof(KelolaSoalWawancara));
            }
        }
        [HttpPost]
        public IActionResult HapusSoalWawancara(int id, string nama)
        {
            _soalService.DeleteSoal(id);
            TempData["Pesan"] = $"Berhasil menghapus soal {nama}";
            return RedirectToAction(nameof(KelolaSoalWawancara));
        }
        [HttpGet]
        public IActionResult UbahSoalWawancara(int id)
        {
            var soal = _soalService.GetSimpleSoal(id);
            var model = new CrudSoalWawancara()
            {
                Judul = soal.Judul,
                Jalur = soal.Jalur,
                Target = soal.Target,
                Deskripsi = soal.Deskripsi
            };
            return Json(model);
        }
        [HttpPost]
        public IActionResult UbahSoalWawancara(KelolaSoalWawancaraModel model)
        {
            if (!ModelState.IsValid)
            {
                TempData["Pesan"] = $"Gagal mengubah soal\nData tidak valid";
                return RedirectToAction(nameof(KelolaSoalWawancara));
            }
            else
            {
                var dataBaru = new Soal()
                {
                    Judul = model.SoalWawancara.Judul,
                    Target = model.SoalWawancara.Target,
                    Jalur = model.SoalWawancara.Jalur,
                    Deskripsi = model.SoalWawancara.Deskripsi,
                };
                _soalService.UpdateSoal(dataBaru);
                TempData["Pesan"] = $"Soal {dataBaru.Judul} berhasil diubah";
                return RedirectToAction(nameof(KelolaSoalWawancara));
            }
        }
        public IActionResult RincianSoalWawancara(int id)
        {
            var soal = _soalService.GetDetailSoal(id);
            var model = new RincianSoalWawancaraModel()
            {
                Id = soal.Id,
                JudulSoal = soal.Judul,
                Jalur = soal.Jalur,
                Target = soal.Target,
                JumlahPertanyaan = soal.JumlahPertanyaan,
                Deskripsi = soal.Deskripsi,
                ListPertanyaanWawancara= soal.ListPertanyaan
                    .Select(x => new CrudPertanyaanWawancaraModel()
                    {
                        Id = x.Id,
                        Isi = x.Isi
                    }).ToList()
            };
            return View(model);
        }
        [HttpPost]
        public IActionResult TambahPertanyaanWawancara(RincianSoalWawancaraModel model)
        {
            var newPertanyaan = new Pertanyaan()
            {
                SoalId = model.Id,
                Isi = model.CrudPertanyaanWawancara.Isi
            };
            _soalService.AddPertanyaan(newPertanyaan);
            return RedirectToAction(nameof(RincianSoalWawancara), new { id = model.Id });
        }
        [HttpPost]
        public IActionResult UbahPertanyaanAkademik(RincianSoalWawancaraModel model)
        {
            var newData = new Pertanyaan()
            {
                SoalId = model.Id,
                Isi = model.CrudPertanyaanWawancara.Isi
            };
            _soalService.UpdatePertanyaan(newData);
            return RedirectToAction(nameof(RincianSoalWawancara), new { id = model.Id });
        }
        [HttpPost]
        public IActionResult HapusPertanyaanWawancara(int soalId, int id)
        {
            _soalService.DeletePertanyaan(soalId, id);
            return RedirectToAction(nameof(RincianSoalWawancara), new { id = soalId });
        }
        #endregion

        #region Siswa
        #endregion
    }
}
