using BackEnd.Abstraction;
using FrontEnd.Web.Mvc.Models.PsbTes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using FrontEnd.Web.Mvc.Models.WakaKesiswaan;
using System.Threading.Tasks;
using BackEnd.Domains;

namespace FrontEnd.Web.Mvc.Controllers
{
    [Authorize(Roles = "Waka Kesiswaan")]
    //[Authorize]
    public class WakaKesiswaanController : Controller
    {
        private readonly ISeleksi _seleksiPenerimaanService;
        private readonly ISiswa _siswaService;
        private readonly IKelas _kelasService;
        public WakaKesiswaanController(ISeleksi seleksiPenerimaanService, ISiswa siswaService, IKelas kelasService)
        {
            _seleksiPenerimaanService = seleksiPenerimaanService;
            _siswaService = siswaService;
            _kelasService = kelasService;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult SeleksiMutasiMasuk()
        {
            var listAkun = _seleksiPenerimaanService.GetAllWithJalur("Mutasi");
            var model = new SeleksiModel()
            {
                ListAkun = listAkun.Select(x => new AkunSeleksi()
                {
                    Id = x.Id,
                    NoPendaftaran = x.NoPendaftaran,
                    JalurPendaftaran = x.JalurPendaftaran,
                    NamaLengkap = x.CalonSiswa.NamaLengkap,
                    NilaiMipa = x.Rekap.NilaiMipa,
                    NilaiIps = x.Rekap.NilaiIps,
                    NilaiTpa = x.Rekap.NilaiTpa,
                    SkorAkhir = x.Rekap.NilaiAkhir,
                    IsLolos = x.Rekap.IsLolos
                }).ToList()
            };

            return View(model);
        }
        [HttpPost]
        public IActionResult SeleksiMutasiMasuk(string noPendaftaran, bool isLolos)
        {
            string result = _seleksiPenerimaanService.UpdateStatusPendaftar(noPendaftaran, isLolos);
            TempData["Pesan"] = $"Berhasil seleksi, {result}";
            return RedirectToAction(nameof(SeleksiMutasiMasuk));
        }
        public IActionResult KelolaKelas()
        {
            var kelas = _kelasService.GetAllKelas();
            var model = new KelolaKelasModel()
            {
                ListKelas = kelas.Select(x => new CrudKelas()
                {
                    Id = x.Id,
                    JumlahSiswa = x.JumlahSiswa,
                    Kategori = x.Kategori,
                    MaxSiswa = x.MaxSiswa,
                    NamaKelas = x.NamaKelas,
                    Tingkat = x.Tingkat
                }).ToList()
            };
            ViewBag.Pesan = TempData["Pesan"] as string;
            return View(model);
        }
        [HttpPost]
        public IActionResult TambahKelas(KelolaKelasModel model)
        {
            if (!ModelState.IsValid)
            {
                TempData["Pesan"] = "Gagal menambah kelas, Data tidak valid";
            }
            else
            {
                var kelasBaru = new Kelas()
                {
                    JumlahSiswa = model.CrudKelas.JumlahSiswa,
                    MaxSiswa = model.CrudKelas.MaxSiswa,
                    Kategori = model.CrudKelas.Kategori,
                    NamaKelas = model.CrudKelas.NamaKelas,
                    Tingkat = model.CrudKelas.Tingkat,
                };
                _kelasService.CreateNewKelas(kelasBaru);
                TempData["Pesan"] = "Kelas berhasil ditambah";
            }
            return RedirectToAction(nameof(KelolaKelas));
        }
        [HttpPost]
        public IActionResult UbahKelas(KelolaKelasModel model)
        {
            if (!ModelState.IsValid)
            {
                TempData["Pesan"] = $"Gagal mengubah kelas, Data tidak valid";
            }
            else if (model.CrudKelas.MaxSiswa < model.CrudKelas.JumlahSiswa)
            {
                TempData["Pesan"] = $"Gagal mengubah kelas, max siswa tidak boleh lebih kecil dari jumlah siswa";
            }
            else
            {
                var dataBaru = new Kelas()
                {
                    Id = model.CrudKelas.Id,
                    Kategori = model.CrudKelas.Kategori,
                    MaxSiswa = model.CrudKelas.MaxSiswa,
                    NamaKelas = model.CrudKelas.NamaKelas,
                    Tingkat = model.CrudKelas.Tingkat,
                };
                _kelasService.UpdateKelas(dataBaru);
                TempData["Pesan"] = $"Kelas {dataBaru.NamaKelas} berhasil diubah";
            }
            return RedirectToAction(nameof(KelolaKelas));
        }
        public IActionResult RincianKelas(int id)
        {
            var kelas = _kelasService.GetDetailKelas(id);
            var model = new CrudKelas()
            {
                Id = kelas.Id,
                JumlahSiswa = kelas.JumlahSiswa,
                Kategori = kelas.Kategori,
                MaxSiswa = kelas.MaxSiswa,
                NamaKelas = kelas.NamaKelas,
                Tingkat = kelas.Tingkat
            };
            return Json(model);
        }
        [HttpPost]
        public IActionResult HapusKelas(int id, string nama)
        {
            _kelasService.DeleteKelas(id);
            TempData["Pesan"] = $"Berhasil menghapus kelas {nama}";
            return RedirectToAction(nameof(KelolaKelas));
        }
        public IActionResult TestWawancara()
        {
            return View();
        }
        [HttpPost]
        public IActionResult TestWawancara(TestWawancaraModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Pesan = "Data tidak valid";
                return View();
            }
            else
            {
                return RedirectToAction("PendahuluanWawancara", "Ujian", new { noPendaftaran = model.NoPendaftaran, target = model.Target });
            }
        }
        public IActionResult TentukanKelas()
        {
            var listSiswa = _siswaService.GetAllSiswaNotYetGetKelas();
            var model = new TentukanKelasModel();
            model.ListSiswaDitentukan = listSiswa.Select(x => new SiswaDitentukan()
            {
                JalurPendaftaranSucces = x.CalonSiswa.ListAkunPendaftaran[0].JalurPendaftaran,
                NoPendaftaranSucces = x.CalonSiswa.ListAkunPendaftaran[0].NoPendaftaran,
                NamaLengkap = x.CalonSiswa.NamaLengkap,
                Nis = x.Nis
            }).ToList();
            return View(model);
        }
        public IActionResult GetAnggotaKelas(int id)
        {
            var anggota = _kelasService.GetMemberKelas(id);
            var model = anggota.Select(x => new AnggotaKelas()
            {
                NamaLengkap = x.CalonSiswa.NamaLengkap,
                Nis = x.Nis
            }).ToList();
            return Json(model);
        }
    }
}
