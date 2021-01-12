using Microsoft.AspNetCore.Mvc;
using BackEnd.Abstraction;
using BackEnd.Domains;
using System.Collections.Generic;
using System.Linq;
using FrontEnd.Web.Mvc.Models.Admin;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;
using FrontEnd.Web.Mvc.Models.TataUsaha;

namespace FrontEnd.Web.Mvc.Controllers
{
    [Authorize(Roles = "Admin")]
    //[Authorize]
    public class AdminController : Controller
    {
        private readonly IStaffSma _staffServices;
        private readonly ISoal _soalService;
        private readonly ISiswa _siswaService;
        public AdminController(IStaffSma staffServices, ISoal soalService, ISiswa siswaService)
        {
            _staffServices = staffServices;
            _soalService = soalService;
            _siswaService = siswaService;
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
                DaftarStaff = listStaff.Select(x => new SimpleStaff()
                {
                    Id = x.Id,
                    NamaLengkap = x.NamaLengkap,
                    Nip = x.Nip,
                    Jabatan = x.Jabatan,
                    PanitiaAcara = x.Panitia == null ?
                    "-" : $"{x.Panitia.Acara} sie {x.Panitia.Divisi}"
                }).ToList()
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
                ViewBag.Pesan = "Gagal menambah staff, Data tidak valid";
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
            if (staff.Panitia != null)
            {
                model.Panitia = new TambahPanitiaModel()
                {
                    Acara = staff.Panitia.Acara,
                    Divisi = staff.Panitia.Divisi
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
                TempData["Pesan"] = "Gagal mengubah staff, Data tidak valid";
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
                TempData["Pesan"] = "Gagal menambah panitia, Data tidak valid";
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
                ListSoal = listSoalAkademik.Select(x => new CrudSoalAkademik()
                {
                    BatasWaktu = x.BatasWaktu,
                    Id = x.Id,
                    Judul = x.Judul,
                    JumlahPertanyaan = x.JumlahPertanyaan,
                    Kategori = x.Kategori
                })
                .ToList()
            };

            return View(model);
        }
        [HttpPost]
        public IActionResult TambahSoalAkademik(KelolaSoalAkademikModel model)
        {
            if (!ModelState.IsValid)
            {
                TempData["Pesan"] = "Gagal menambah soal, Data tidak valid";
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
            }
            return RedirectToAction(nameof(KelolaSoalAkademik));
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
                Id = soal.Id,
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
                TempData["Pesan"] = $"Gagal mengubah soal, Data tidak valid";
            }
            else
            {
                var dataBaru = new Soal()
                {
                    Id = model.SoalAkademik.Id,
                    Judul = model.SoalAkademik.Judul,
                    BatasWaktu = model.SoalAkademik.BatasWaktu,
                    Kategori = model.SoalAkademik.Kategori,
                    Deskripsi = model.SoalAkademik.Deskripsi,
                };
                _soalService.UpdateSoal(dataBaru);
                TempData["Pesan"] = $"Soal {dataBaru.Judul} berhasil diubah";
            }
            return RedirectToAction(nameof(KelolaSoalAkademik));
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
                        SoalId = x.SoalId,
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
        [HttpGet]
        public IActionResult TambahPertanyaanAkademik(int soalId)
        {
            var model = new CrudPertanyaanAkademikModel()
            {
                SoalId = soalId
            };
            return View(model);
        }
        [HttpPost]
        public IActionResult TambahPertanyaanAkademik(CrudPertanyaanAkademikModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            else
            {
                var newPertanyaan = new Pertanyaan()
                {
                    SoalId = model.SoalId,
                    Isi = model.Isi,
                    OpsiA = model.OpsiA,
                    OpsiB = model.OpsiB,
                    OpsiC = model.OpsiC,
                    OpsiD = model.OpsiD,
                    OpsiE = model.OpsiE,
                    Jawaban = model.Jawaban
                };
                _soalService.AddPertanyaan(newPertanyaan);
                return RedirectToAction(nameof(RincianSoalAkademik), new { id = model.SoalId });
            }
        }
        [HttpGet]
        public IActionResult UbahPertanyaanAkademik(int id, int soalId)
        {
            var pertanyaanAkademik = _soalService.GetPertanyaan(id, soalId);
            var model = new CrudPertanyaanAkademikModel()
            {
                Id = pertanyaanAkademik.Id,
                SoalId = pertanyaanAkademik.SoalId,
                Isi = pertanyaanAkademik.Isi,
                OpsiA = pertanyaanAkademik.OpsiA,
                OpsiB = pertanyaanAkademik.OpsiB,
                OpsiC = pertanyaanAkademik.OpsiC,
                OpsiD = pertanyaanAkademik.OpsiD,
                OpsiE = pertanyaanAkademik.OpsiE,
                Jawaban = pertanyaanAkademik.Jawaban
            };
            return View(model);
        }
        [HttpPost]
        public IActionResult UbahPertanyaanAkademik(CrudPertanyaanAkademikModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Pesan = "Data tidak valid";
                return View();
            }
            else
            {
                var newData = new Pertanyaan()
                {
                    SoalId = model.SoalId,
                    Id = model.Id,
                    Isi = model.Isi,
                    OpsiA = model.OpsiA,
                    OpsiB = model.OpsiB,
                    OpsiC = model.OpsiC,
                    OpsiD = model.OpsiD,
                    OpsiE = model.OpsiE,
                    Jawaban = model.Jawaban
                };
                _soalService.UpdatePertanyaan(newData);
                return RedirectToAction(nameof(RincianSoalAkademik), new { id = model.SoalId });
            }
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
                ListSoal = listSoalWawancara.Select(x => new CrudSoalWawancara()
                {
                    Deskripsi = x.Deskripsi,
                    Id = x.Id,
                    Jalur = x.Jalur,
                    Judul = x.Judul,
                    JumlahPertanyaan = x.JumlahPertanyaan,
                    Target = x.Target
                }).ToList()
            };

            return View(model);
        }
        [HttpPost]
        public IActionResult TambahSoalWawancara(KelolaSoalWawancaraModel model)
        {
            if (!ModelState.IsValid)
            {
                TempData["Pesan"] = $"Gagal menambah soal, Data tidak valid";
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
            }
            return RedirectToAction(nameof(KelolaSoalWawancara));
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
                Id = soal.Id,
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
                TempData["Pesan"] = $"Gagal mengubah soal, Data tidak valid";
            }
            else
            {
                var dataBaru = new Soal()
                {
                    Id = model.SoalWawancara.Id,
                    Kategori = model.SoalWawancara.Kategori,
                    Judul = model.SoalWawancara.Judul,
                    Target = model.SoalWawancara.Target,
                    Jalur = model.SoalWawancara.Jalur,
                    Deskripsi = model.SoalWawancara.Deskripsi,
                };
                _soalService.UpdateSoal(dataBaru);
                TempData["Pesan"] = $"Soal {dataBaru.Judul} berhasil diubah";
            }
            return RedirectToAction(nameof(KelolaSoalWawancara));
        }
        public IActionResult RincianSoalWawancara(int id)
        {
            ViewBag.Pesan = TempData["Pesan"] as string;
            var soal = _soalService.GetDetailSoal(id);
            var model = new RincianSoalWawancaraModel()
            {
                Id = soal.Id,
                JudulSoal = soal.Judul,
                Jalur = soal.Jalur,
                Target = soal.Target,
                JumlahPertanyaan = soal.JumlahPertanyaan,
                Deskripsi = soal.Deskripsi,
                ListPertanyaanWawancara = soal.ListPertanyaan
                    .Select(x => new CrudPertanyaanWawancaraModel()
                    {
                        Id = x.Id,
                        Isi = x.Isi,
                        SoalId = x.SoalId
                    }).ToList()
            };
            return View(model);
        }
        [HttpPost]
        public IActionResult TambahPertanyaanWawancara(RincianSoalWawancaraModel model)
        {
            if (!ModelState.IsValid)
            {
                TempData["Pesan"] = "Data tidak valid";
            }
            else
            {
                var newPertanyaan = new Pertanyaan()
                {
                    SoalId = model.Id,
                    Isi = model.CrudPertanyaanWawancara.Isi
                };
                _soalService.AddPertanyaan(newPertanyaan);
            }
            return RedirectToAction(nameof(RincianSoalWawancara), new { id = model.Id });
        }
        [HttpGet]
        public IActionResult UbahPertanyaanWawancara(int id, int soalId)
        {
            var pertanyaan = _soalService.GetPertanyaan(id, soalId);
            var model = new CrudPertanyaanWawancaraModel()
            {
                Id = pertanyaan.Id,
                Isi = pertanyaan.Isi,
                SoalId = pertanyaan.SoalId
            };
            return Json(model);
        }
        [HttpPost]
        public IActionResult UbahPertanyaanWawancara(RincianSoalWawancaraModel model)
        {
            if (!ModelState.IsValid)
            {
                TempData["Pesan"] = "Data tidak valid";
            }
            else
            {
                var newData = new Pertanyaan()
                {
                    Id = model.CrudPertanyaanWawancara.Id,
                    SoalId = model.CrudPertanyaanWawancara.SoalId,
                    Isi = model.CrudPertanyaanWawancara.Isi
                };
                _soalService.UpdatePertanyaan(newData);
            }
            return RedirectToAction(nameof(RincianSoalWawancara), new { id = model.CrudPertanyaanWawancara.SoalId });
        }
        [HttpPost]
        public IActionResult HapusPertanyaanWawancara(int soalId, int id)
        {
            _soalService.DeletePertanyaan(soalId, id);
            return RedirectToAction(nameof(RincianSoalWawancara), new { id = soalId });
        }
        #endregion

        #region Siswa
        public IActionResult ListDaftarSiswa()
        {
            var siswa = _siswaService.GetAllSiswa();
            var model = new ListDaftarSiswaModel()
            {
                ListSiswaView = siswa.Select(x => new SiswaView()
                {
                    Id = x.Id,
                    JenisKelamin = x.CalonSiswa.DataDiri == null ? "-" :
                        x.CalonSiswa.DataDiri.IsPerempuan ? "Perempuan" : "Laki-laki",
                    NamaKelas = x.Kelas == null ? "-" : x.Kelas.NamaKelas,
                    NamaLengkap = x.CalonSiswa.NamaLengkap,
                    Nis = x.Nis
                }).ToList()
            };
            return View(model);
        }
        #endregion

        public IActionResult PengaturanSoal()
        {
            ViewBag.Pesan = TempData["Pesan"] as string;
            var soalAkademik = _soalService.GetAllSoalAkademik();
            var soalWawancara = _soalService.GetAllSoalWawancara();
            var pengaturanSoal = _soalService.GetPengaturanSoal();

            var model = new PengaturanSoalModel()
            {
                ListSoalMipa = soalAkademik
                .Where(x => x.Kategori.Equals("MIPA"))
                .Select(y => new SelectListItem()
                {
                    Text = y.Judul,
                    Value = y.Id.ToString()
                }).ToList(),
                ListSoalIps = soalAkademik
                .Where(x => x.Kategori.Equals("IPS"))
                .Select(y => new SelectListItem()
                {
                    Text = y.Judul,
                    Value = y.Id.ToString()
                }).ToList(),
                ListSoalTpa = soalAkademik
                .Where(x => x.Kategori.Equals("TPA"))
                .Select(y => new SelectListItem()
                {
                    Text = y.Judul,
                    Value = y.Id.ToString()
                }).ToList(),
                ListWawancaraCalonSiswa = soalWawancara
                .Where(x => x.Target.Equals("Calon Siswa"))
                .Select(y => new SelectListItem()
                {
                    Text = y.Judul,
                    Value = y.Id.ToString()
                }).ToList(),
                ListWawancaraOrangTua = soalWawancara
                .Where(x => x.Target.Equals("Orang Tua"))
                .Select(y => new SelectListItem()
                {
                    Text = y.Judul,
                    Value = y.Id.ToString()
                }).ToList(),

                SoalMipaKhusus = pengaturanSoal.SoalMipaKhusus,
                SoalIpsKhusus = pengaturanSoal.SoalIpsKhusus,
                SoalTpaKhusus = pengaturanSoal.SoalTpaKhusus,
                SoalWawancaraCalonSiswaKhusus = pengaturanSoal.SoalWawancaraCalonSiswaKhusus,
                SoalWawancaraOrangTuaKhusus = pengaturanSoal.SoalWawancaraOrangTuaKhusus,

                SoalMipaReguler = pengaturanSoal.SoalMipaReguler,
                SoalIpsReguler = pengaturanSoal.SoalIpsReguler,
                SoalTpaReguler = pengaturanSoal.SoalTpaReguler,
                SoalWawancaraCalonSiswaReguler = pengaturanSoal.SoalWawancaraCalonSiswaReguler,
                SoalWawancaraOrangTuaReguler = pengaturanSoal.SoalWawancaraOrangTuaReguler,

                SoalMipaMutasi = pengaturanSoal.SoalMipaMutasi,
                SoalIpsMutasi = pengaturanSoal.SoalIpsMutasi,
                SoalTpaMutasi = pengaturanSoal.SoalTpaMutasi,
                SoalWawancaraCalonSiswaMutasi = pengaturanSoal.SoalWawancaraCalonSiswaMutasi,
                SoalWawancaraOrangTuaMutasi = pengaturanSoal.SoalWawancaraOrangTuaMutasi,

                SoalWawancaraCalonSiswaPrestasi = pengaturanSoal.SoalWawancaraCalonSiswaPrestasi,
                SoalWawancaraOrangTuaPrestasi = pengaturanSoal.SoalWawancaraOrangTuaPrestasi,

                SoalWawancaraCalonSiswaMitra = pengaturanSoal.SoalWawancaraCalonSiswaMitra,
                SoalWawancaraOrangTuaMitra = pengaturanSoal.SoalWawancaraOrangTuaMitra,
            };

            return View(model);
        }
        [HttpPost]
        public IActionResult SimpanPengaturanSoal(PengaturanSoalModel model)
        {
            var pengaturan = new Pengaturan()
            {
                SoalMipaMutasi = model.SoalMipaMutasi??0,
                SoalIpsMutasi = model.SoalIpsMutasi??0,
                SoalTpaMutasi = model.SoalTpaMutasi??0,
                SoalWawancaraCalonSiswaMutasi = model.SoalWawancaraCalonSiswaMutasi??0,
                SoalWawancaraOrangTuaMutasi = model.SoalWawancaraOrangTuaMutasi??0,
                SoalMipaKhusus = model.SoalMipaKhusus??0,
                SoalIpsKhusus = model.SoalIpsKhusus??0,
                SoalTpaKhusus = model.SoalTpaKhusus??0,
                SoalWawancaraCalonSiswaKhusus = model.SoalWawancaraCalonSiswaKhusus??0,
                SoalWawancaraOrangTuaKhusus = model.SoalWawancaraOrangTuaKhusus??0,
                SoalMipaReguler = model.SoalMipaReguler??0,
                SoalIpsReguler = model.SoalIpsReguler??0,
                SoalTpaReguler = model.SoalTpaReguler??0,
                SoalWawancaraCalonSiswaReguler = model.SoalWawancaraCalonSiswaReguler??0,
                SoalWawancaraOrangTuaReguler = model.SoalWawancaraOrangTuaReguler??0,
                SoalWawancaraCalonSiswaMitra = model.SoalWawancaraCalonSiswaMitra??0,
                SoalWawancaraOrangTuaMitra = model.SoalWawancaraOrangTuaMitra??0,
                SoalWawancaraCalonSiswaPrestasi = model.SoalWawancaraCalonSiswaPrestasi??0,
                SoalWawancaraOrangTuaPrestasi = model.SoalWawancaraOrangTuaPrestasi??0,
            };
            _soalService.SavePengaturanSoal(pengaturan);
            TempData["Pesan"] = "Berhasil menyimpan";
            return RedirectToAction(nameof(PengaturanSoal));
        }
    }
}
