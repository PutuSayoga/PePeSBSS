using Microsoft.AspNetCore.Mvc;
using BackEnd.Abstraction;
using BackEnd.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FrontEnd.Web.Mvc.Models.CalonSiswa;

namespace FrontEnd.Web.Mvc.Controllers
{
    public class CalonSiswaController : Controller
    {
        private readonly ICalonSiswa _calonSiswaService;
        public CalonSiswaController(ICalonSiswa calonSiswaService)
        {
            _calonSiswaService = calonSiswaService;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult KelolaDataDiri() 
            => View();
        [HttpGet]
        public IActionResult KelolaDataAkademikTerakhir()
            => View();
        [HttpGet]
        public IActionResult KelolaDataRapor() 
            => View();
        [HttpGet]
        public IActionResult KelolaDataPrestasi() 
            => View();
        [HttpGet]
        public IActionResult KelolaDataOrangTua() 
            => View();
        [HttpGet]
        public IActionResult KelolaDataPenunjang() 
            => View();
        [HttpPost]
        public IActionResult KelolaDataDiri(KelolaDataDiriModel model)
        {
            string namaLengkap = model.NamaLengkap;
            var newData = new DataDiri()
            {
                Agama = model.Agama,
                Alamat = model.Alamat,
                AnakKe = model.AnakKe,
                BeratBadan = model.BeratBadan,
                CalonSiswaId = 27,
                CitaCita = model.CitaCita,
                DusunDesaLurah = model.DusunDesaLurah,
                Email = model.Email,
                GolDarah = model.GolDarah,
                Hobi = model.Hobi,
                IsPerempuan = model.IsPerempuan,
                JumlahSaudara = model.JumlahSaudara,
                Kecamatan = model.Kecamatan,
                KelainanJasmani = model.KelainanJasmani,
                KodePos = model.KodePos,
                KotaKabupaten = model.KotaKabupaten,
                NamaPanggilan = model.NamaPanggilan,
                NoHp = model.NoHp,
                NoTelp = model.NoTelp,
                RiwayatSakit = model.RiwayatSakit,
                Rt = model.Rt,
                Rw = model.Rw,
                StatusDalamKeluarga = model.StatusDalamKeluarga,
                TanggalLahir = model.TanggalLahir,
                TempatLahir = model.TempatLahir,
                TinggiBadan = model.TinggiBadan
            };
            _calonSiswaService.SaveDataDiri(namaLengkap, newData);
            TempData["Pesan"] = "Berhasil menyimpan data diri";
            return RedirectToAction(nameof(CetakBiodata));
        }
        [HttpPost]
        public IActionResult KelolaDataAkademikTerakhir(KelolaDataAkademikTerakhir model)
        {
            var newData = new AkademikTerakhir()
            {
                AlamatSekolah = model.AlamatSekolah,
                CalonSiswaId = 27,
                JenisSekolah = model.JenisSekolah,
                NamaSekolah = model.NamaSekolah,
                NoPesertaUn = model.NoPesertaUn,
                NoSeriIjazah = model.NoSeriIjazah,
                NoSeriSkhun = model.NoSeriSkhun,
                StatusSekolah = model.StatusSekolah
            };
            _calonSiswaService.SaveDataAkademikTerakhir(newData);
            TempData["Pesan"] = "Berhasil menyimpan data diri";
            return RedirectToAction(nameof(CetakBiodata));
        }
        [HttpPost]
        public IActionResult KelolaDataRapor(KelolaDataRaporModel model)
        {
            var newData = new List<Rapor>()
            {
                new Rapor()
                {
                    CalonSiswaId = 27,
                    MataPelajaran = model.ListRapor[0].MataPelajaran,
                    Semester1 = model.ListRapor[0].Semester1,
                    Semester2 = model.ListRapor[0].Semester2,
                    Semester3 = model.ListRapor[0].Semester3,
                    Semester4 = model.ListRapor[0].Semester4,
                    Semester5 = model.ListRapor[0].Semester5,
                },
                new Rapor()
                {
                    CalonSiswaId = 27,
                    MataPelajaran = model.ListRapor[1].MataPelajaran,
                    Semester1 = model.ListRapor[1].Semester1,
                    Semester2 = model.ListRapor[1].Semester2,
                    Semester3 = model.ListRapor[1].Semester3,
                    Semester4 = model.ListRapor[1].Semester4,
                    Semester5 = model.ListRapor[1].Semester5,
                },
                new Rapor()
                {
                    CalonSiswaId = 27,
                    MataPelajaran = model.ListRapor[2].MataPelajaran,
                    Semester1 = model.ListRapor[2].Semester1,
                    Semester2 = model.ListRapor[2].Semester2,
                    Semester3 = model.ListRapor[2].Semester3,
                    Semester4 = model.ListRapor[2].Semester4,
                    Semester5 = model.ListRapor[2].Semester5,
                },
                new Rapor()
                {
                    CalonSiswaId = 27,
                    MataPelajaran = model.ListRapor[3].MataPelajaran,
                    Semester1 = model.ListRapor[3].Semester1,
                    Semester2 = model.ListRapor[3].Semester2,
                    Semester3 = model.ListRapor[3].Semester3,
                    Semester4 = model.ListRapor[3].Semester4,
                    Semester5 = model.ListRapor[3].Semester5,
                },
                new Rapor()
                {
                    CalonSiswaId = 27,
                    MataPelajaran = model.ListRapor[4].MataPelajaran,
                    Semester1 = model.ListRapor[4].Semester1,
                    Semester2 = model.ListRapor[4].Semester2,
                    Semester3 = model.ListRapor[4].Semester3,
                    Semester4 = model.ListRapor[4].Semester4,
                    Semester5 = model.ListRapor[4].Semester5,
                },
                new Rapor()
                {
                    CalonSiswaId = 27,
                    MataPelajaran = model.ListRapor[5].MataPelajaran,
                    Semester1 = model.ListRapor[5].Semester1,
                    Semester2 = model.ListRapor[5].Semester2,
                    Semester3 = model.ListRapor[5].Semester3,
                    Semester4 = model.ListRapor[5].Semester4,
                    Semester5 = model.ListRapor[5].Semester5,
                },
                new Rapor()
                {
                    CalonSiswaId = 27,
                    MataPelajaran = model.ListRapor[6].MataPelajaran,
                    Semester1 = model.ListRapor[6].Semester1,
                    Semester2 = model.ListRapor[6].Semester2,
                    Semester3 = model.ListRapor[6].Semester3,
                    Semester4 = model.ListRapor[6].Semester4,
                    Semester5 = model.ListRapor[6].Semester5,
                },
                new Rapor()
                {
                    CalonSiswaId = 27,
                    MataPelajaran = model.ListRapor[7].MataPelajaran,
                    Semester1 = model.ListRapor[7].Semester1,
                    Semester2 = model.ListRapor[7].Semester2,
                    Semester3 = model.ListRapor[7].Semester3,
                    Semester4 = model.ListRapor[7].Semester4,
                    Semester5 = model.ListRapor[7].Semester5,
                },
                new Rapor()
                {
                    CalonSiswaId = 27,
                    MataPelajaran = model.ListRapor[8].MataPelajaran,
                    Semester1 = model.ListRapor[8].Semester1,
                    Semester2 = model.ListRapor[8].Semester2,
                    Semester3 = model.ListRapor[8].Semester3,
                    Semester4 = model.ListRapor[8].Semester4,
                    Semester5 = model.ListRapor[8].Semester5,
                },
                new Rapor()
                {
                    CalonSiswaId = 27,
                    MataPelajaran = model.ListRapor[9].MataPelajaran,
                    Semester1 = model.ListRapor[9].Semester1,
                    Semester2 = model.ListRapor[9].Semester2,
                    Semester3 = model.ListRapor[9].Semester3,
                    Semester4 = model.ListRapor[9].Semester4,
                    Semester5 = model.ListRapor[9].Semester5,
                },
                new Rapor()
                {
                    CalonSiswaId = 27,
                    MataPelajaran = model.ListRapor[10].MataPelajaran,
                    Semester1 = model.ListRapor[10].Semester1,
                    Semester2 = model.ListRapor[10].Semester2,
                    Semester3 = model.ListRapor[10].Semester3,
                    Semester4 = model.ListRapor[10].Semester4,
                    Semester5 = model.ListRapor[10].Semester5,
                }
            };
            _calonSiswaService.SaveDataRapor(newData);
            TempData["Pesan"] = "Berhasil menyimpan data diri";
            return RedirectToAction(nameof(CetakBiodata));
        }
        [HttpPost]
        public IActionResult KelolaDataPrestasi(KelolaDataPrestasiModel model)
        {
            var newData = new Prestasi()
            {
                CalonSiswaId = 27,
                Jenis = model.Jenis,
                NamaKejuaraan = model.NamaKejuaraan,
                Penyelenggara = model.Penyelenggara,
                Peringkat = model.Peringkat,
                Tahun = model.Tahun,
                Tingkat = model.Tingkat
            };
            _calonSiswaService.SaveDataPrestasi(newData);
            TempData["Pesan"] = "Berhasil menyimpan data diri";
            return RedirectToAction(nameof(CetakBiodata));
        }
        [HttpPost]
        public IActionResult KelolaDataOrangTua(KelolaDataOrangTuaModel model)
        {
            var newData = new List<Penanggungjawab>()
            {
                // Ayah
                new Penanggungjawab()
                {
                    Agama = model.AgamaAyah,
                    Alamat = model.AlamatAyah,
                    CalonSiswaId = 27,
                    Email = model.EmailAyah,
                    Keterangan = model.KeteranganAyah,
                    NamaLengkap = model.NamaLengkapAyah,
                    NoHp = model.NoHpAyah,
                    NoTelp = model.NoTelpAyah,
                    Pekerjaan = model.PekerjaanAyah,
                    PendidikanTerakhir = model.PendidikanTerakhirAyah,
                    Penghasilan = model.PenghasilanAyah,
                    Sebagai = "Ayah",
                    StatusDalamKeluarga = model.StatusDalamKeluargaAyah,
                    TanggalLahir = model.TanggalLahirAyah,
                    TempatLahir = model.TempatLahirAyah
                },
                // Ibu
                new Penanggungjawab()
                {
                    Agama = model.AgamaIbu,
                    Alamat = model.AlamatIbu,
                    CalonSiswaId = 27,
                    Email = model.EmailIbu,
                    Keterangan = model.KeteranganIbu,
                    NamaLengkap = model.NamaLengkapIbu,
                    NoHp = model.NoHpIbu,
                    NoTelp = model.NoTelpIbu,
                    Pekerjaan = model.PekerjaanIbu,
                    PendidikanTerakhir = model.PendidikanTerakhirIbu,
                    Penghasilan = model.PenghasilanIbu,
                    Sebagai = "Ibu",
                    StatusDalamKeluarga = model.StatusDalamKeluargaIbu,
                    TanggalLahir = model.TanggalLahirIbu,
                    TempatLahir = model.TempatLahirIbu
                },
                // Wali
                new Penanggungjawab()
                {
                    Agama = model.AgamaWali,
                    Alamat = model.AlamatWali,
                    CalonSiswaId = 27,
                    Email = model.EmailWali,
                    NamaLengkap = model.NamaLengkapWali,
                    NoHp = model.NoHpWali,
                    NoTelp = model.NoTelpWali,
                    Pekerjaan = model.PekerjaanWali,
                    PendidikanTerakhir = model.PendidikanTerakhirWali,
                    Penghasilan = model.PenghasilanWali,
                    Sebagai = "Wali",
                    TanggalLahir = model.TanggalLahirWali,
                    TempatLahir = model.TempatLahirWali
                }
            };
            _calonSiswaService.SaveDataPenanggunjawab(newData);
            TempData["Pesan"] = "Berhasil menyimpan data diri";
            return RedirectToAction(nameof(CetakBiodata));
        }
        [HttpPost]
        public IActionResult KelolaDataPenunjang(KelolaDataPenunjangModel model)
        {
            var newData = new Penunjang()
            {
                CalonSiswaId = 27,
                DayaListrik = model.DayaListrik,
                JarakTempuh = model.JarakTempuh,
                Pembiaya = model.Pembiaya,
                StatusTempatTinggal = model.StatusTempatTinggal,
                Transportasi = model.Transportasi,
                WaktuTempuh = model.WaktuTempuh
            };
            _calonSiswaService.SaveDataPenunjang(newData);
            TempData["Pesan"] = "Berhasil menyimpan data diri";
            return RedirectToAction(nameof(CetakBiodata));
        }
        public IActionResult CetakBiodata()
        {
            return View();
        }
        public IActionResult HasilSeleksi()
        {

            return View();
        }
    }
}
