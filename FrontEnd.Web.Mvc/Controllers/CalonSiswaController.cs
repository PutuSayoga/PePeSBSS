using Microsoft.AspNetCore.Mvc;
using BackEnd.Abstraction;
using BackEnd.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FrontEnd.Web.Mvc.Models.CalonSiswa;
using Microsoft.AspNetCore.Authorization;

namespace FrontEnd.Web.Mvc.Controllers
{
    [Authorize(Roles = "Calon Siswa, Admin")]
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
        public IActionResult Biodata()
        {
            var model = new BiodataModel();
            return View(model);
        }
        public IActionResult HasilSeleksi()
        {
            var akun = _calonSiswaService.GetSimpleAkun(User.Identity.Name);
            var model = new HasilSeleksiModel()
            {
                Status = akun.Status,
                TanggalUjian = akun.JadwalTes.ToString("dddd, dd MMMM yyyy", new System.Globalization.CultureInfo("id-ID"))
            };
            return View(model);
        }
        [HttpGet]
        public IActionResult KelolaDataDiri()
        {
            var akun = _calonSiswaService.GetDetailDiri(User.Identity.Name);
            var model = new KelolaDataDiriModel()
            {
                JalurPendaftaran = akun.JalurPendaftaran,
                NoPendaftaran = akun.NoPendaftaran,
                NamaLengkap = akun.CalonSiswa.NamaLengkap
            };
            if (akun.CalonSiswa.DataDiri != null)
            {
                model.Agama = akun.CalonSiswa.DataDiri.Agama;
                model.Alamat = akun.CalonSiswa.DataDiri.Alamat;
                model.AnakKe = akun.CalonSiswa.DataDiri.AnakKe;
                model.BeratBadan = akun.CalonSiswa.DataDiri.BeratBadan;
                model.CitaCita = akun.CalonSiswa.DataDiri.CitaCita;
                model.DusunDesaLurah = akun.CalonSiswa.DataDiri.DusunDesaLurah;
                model.Email = akun.CalonSiswa.DataDiri.Email;
                model.GolDarah = akun.CalonSiswa.DataDiri.GolDarah;
                model.Hobi = akun.CalonSiswa.DataDiri.Hobi;
                model.IsPerempuan = akun.CalonSiswa.DataDiri.IsPerempuan;
                model.JumlahSaudara = akun.CalonSiswa.DataDiri.JumlahSaudara;
                model.Kecamatan = akun.CalonSiswa.DataDiri.Kecamatan;
                model.KelainanJasmani = akun.CalonSiswa.DataDiri.KelainanJasmani;
                model.KodePos = akun.CalonSiswa.DataDiri.KodePos;
                model.KotaKabupaten = akun.CalonSiswa.DataDiri.KotaKabupaten;
                model.NamaPanggilan = akun.CalonSiswa.DataDiri.NamaPanggilan;
                model.NoHp = akun.CalonSiswa.DataDiri.NoHp;
                model.NoTelp = akun.CalonSiswa.DataDiri.NoTelp;
                model.RiwayatSakit = akun.CalonSiswa.DataDiri.RiwayatSakit;
                model.Rt = akun.CalonSiswa.DataDiri.Rt;
                model.Rw = akun.CalonSiswa.DataDiri.Rw;
                model.StatusDalamKeluarga = akun.CalonSiswa.DataDiri.StatusDalamKeluarga;
                model.TanggalLahir = akun.CalonSiswa.DataDiri.TanggalLahir;
                model.TempatLahir = akun.CalonSiswa.DataDiri.TempatLahir;
                model.TinggiBadan = akun.CalonSiswa.DataDiri.TinggiBadan;
            }

            return View(model);
        }
        [HttpGet]
        public IActionResult KelolaDataAkademikTerakhir()
        {
            var akun = _calonSiswaService.GetDetailAkademikTerakhir(User.Identity.Name);
            var model = new KelolaDataAkademikTerakhir();
            if (akun.CalonSiswa.AkademikTerakhir != null)
            {
                model.AlamatSekolah = akun.CalonSiswa.AkademikTerakhir.AlamatSekolah;
                model.JenisSekolah = akun.CalonSiswa.AkademikTerakhir.JenisSekolah;
                model.NamaSekolah = akun.CalonSiswa.AkademikTerakhir.NamaSekolah;
                model.NoPesertaUn = akun.CalonSiswa.AkademikTerakhir.NoPesertaUn;
                model.NoSeriIjazah = akun.CalonSiswa.AkademikTerakhir.NoSeriIjazah;
                model.NoSeriSkhun = akun.CalonSiswa.AkademikTerakhir.NoSeriSkhun;
                model.StatusSekolah = akun.CalonSiswa.AkademikTerakhir.StatusSekolah;
            }
            return View(model);
        }
        [HttpGet]
        public IActionResult KelolaDataRapor()
        {
            var akun = _calonSiswaService.GetDetailRapor(User.Identity.Name);
            var model = new KelolaDataRaporModel();
            if (akun.CalonSiswa.ListNilaiRapor != null)
            {
                model.ListRapor[0] = akun.CalonSiswa.ListNilaiRapor
                    .Where(x => x.MataPelajaran.Equals("Pendidikan Agama"))
                    .Select(x => new CrudRapor()
                    {
                        MataPelajaran = x.MataPelajaran,
                        Semester1 = x.Semester1,
                        Semester2 = x.Semester2,
                        Semester3 = x.Semester3,
                        Semester4 = x.Semester4,
                        Semester5 = x.Semester5,
                    })
                    .FirstOrDefault();
                model.ListRapor[1] = akun.CalonSiswa.ListNilaiRapor
                    .Where(x => x.MataPelajaran.Equals("Pendidikan Kewarganegaraan"))
                    .Select(x => new CrudRapor()
                    {
                        MataPelajaran = x.MataPelajaran,
                        Semester1 = x.Semester1,
                        Semester2 = x.Semester2,
                        Semester3 = x.Semester3,
                        Semester4 = x.Semester4,
                        Semester5 = x.Semester5,
                    })
                    .FirstOrDefault();
                model.ListRapor[2] = akun.CalonSiswa.ListNilaiRapor
                    .Where(x => x.MataPelajaran.Equals("Bahasa Indonesia"))
                    .Select(x => new CrudRapor()
                    {
                        MataPelajaran = x.MataPelajaran,
                        Semester1 = x.Semester1,
                        Semester2 = x.Semester2,
                        Semester3 = x.Semester3,
                        Semester4 = x.Semester4,
                        Semester5 = x.Semester5,
                    })
                    .FirstOrDefault();
                model.ListRapor[3] = akun.CalonSiswa.ListNilaiRapor
                    .Where(x => x.MataPelajaran.Equals("Bahasa Inggris"))
                    .Select(x => new CrudRapor()
                    {
                        MataPelajaran = x.MataPelajaran,
                        Semester1 = x.Semester1,
                        Semester2 = x.Semester2,
                        Semester3 = x.Semester3,
                        Semester4 = x.Semester4,
                        Semester5 = x.Semester5,
                    })
                    .FirstOrDefault();
                model.ListRapor[4] = akun.CalonSiswa.ListNilaiRapor
                    .Where(x => x.MataPelajaran.Equals("Matematika"))
                    .Select(x => new CrudRapor()
                    {
                        MataPelajaran = x.MataPelajaran,
                        Semester1 = x.Semester1,
                        Semester2 = x.Semester2,
                        Semester3 = x.Semester3,
                        Semester4 = x.Semester4,
                        Semester5 = x.Semester5,
                    })
                    .FirstOrDefault();
                model.ListRapor[5] = akun.CalonSiswa.ListNilaiRapor
                    .Where(x => x.MataPelajaran.Equals("Ilmu Pengetahuan Alam"))
                    .Select(x => new CrudRapor()
                    {
                        MataPelajaran = x.MataPelajaran,
                        Semester1 = x.Semester1,
                        Semester2 = x.Semester2,
                        Semester3 = x.Semester3,
                        Semester4 = x.Semester4,
                        Semester5 = x.Semester5,
                    })
                    .FirstOrDefault();
                model.ListRapor[6] = akun.CalonSiswa.ListNilaiRapor
                    .Where(x => x.MataPelajaran.Equals("Ilmu Pengetahuan Sosial"))
                    .Select(x => new CrudRapor()
                    {
                        MataPelajaran = x.MataPelajaran,
                        Semester1 = x.Semester1,
                        Semester2 = x.Semester2,
                        Semester3 = x.Semester3,
                        Semester4 = x.Semester4,
                        Semester5 = x.Semester5,
                    })
                    .FirstOrDefault();
                model.ListRapor[7] = akun.CalonSiswa.ListNilaiRapor
                    .Where(x => x.MataPelajaran.Equals("Seni Budaya"))
                    .Select(x => new CrudRapor()
                    {
                        MataPelajaran = x.MataPelajaran,
                        Semester1 = x.Semester1,
                        Semester2 = x.Semester2,
                        Semester3 = x.Semester3,
                        Semester4 = x.Semester4,
                        Semester5 = x.Semester5,
                    })
                    .FirstOrDefault();
                model.ListRapor[8] = akun.CalonSiswa.ListNilaiRapor
                    .Where(x => x.MataPelajaran.Equals("Pendidikan Jasmani & Olahraga"))
                    .Select(x => new CrudRapor()
                    {
                        MataPelajaran = x.MataPelajaran,
                        Semester1 = x.Semester1,
                        Semester2 = x.Semester2,
                        Semester3 = x.Semester3,
                        Semester4 = x.Semester4,
                        Semester5 = x.Semester5,
                    })
                    .FirstOrDefault();
                model.ListRapor[9] = akun.CalonSiswa.ListNilaiRapor
                    .Where(x => x.MataPelajaran.Equals("Prakarya"))
                    .Select(x => new CrudRapor()
                    {
                        MataPelajaran = x.MataPelajaran,
                        Semester1 = x.Semester1,
                        Semester2 = x.Semester2,
                        Semester3 = x.Semester3,
                        Semester4 = x.Semester4,
                        Semester5 = x.Semester5,
                    })
                    .FirstOrDefault();
                model.ListRapor[10] = akun.CalonSiswa.ListNilaiRapor
                    .Where(x => x.MataPelajaran.Equals("Muatan Lokal (Bahasa Daerah)"))
                    .Select(x => new CrudRapor()
                    {
                        MataPelajaran = x.MataPelajaran,
                        Semester1 = x.Semester1,
                        Semester2 = x.Semester2,
                        Semester3 = x.Semester3,
                        Semester4 = x.Semester4,
                        Semester5 = x.Semester5,
                    })
                    .FirstOrDefault();
            }
            return View(model);
        }
        [HttpGet]
        public IActionResult KelolaDataPrestasi()
        {
            var akun = _calonSiswaService.GetDetailPrestasi(User.Identity.Name);
            var model = new KelolaDataPrestasiModel();
            if (akun.CalonSiswa.Prestasi != null)
            {
                model.Jenis = akun.CalonSiswa.Prestasi.Jenis;
                model.NamaKejuaraan = akun.CalonSiswa.Prestasi.NamaKejuaraan;
                model.Penyelenggara = akun.CalonSiswa.Prestasi.Penyelenggara;
                model.Peringkat = akun.CalonSiswa.Prestasi.Peringkat;
                model.Tanggal = akun.CalonSiswa.Prestasi.Tahun;
                model.Tingkat = akun.CalonSiswa.Prestasi.Tingkat;
            }
            return View(model);
        }
        [HttpGet]
        public IActionResult KelolaDataOrangTua()
        {
            var akun = _calonSiswaService.GetDetailPenanggungJawab(User.Identity.Name);
            var model = new KelolaDataOrangTuaModel();
            if (akun.CalonSiswa.ListPenanggungjawab != null)
            {
                var ayah = akun.CalonSiswa.ListPenanggungjawab
                    .Where(x => x.Sebagai.Equals("Ayah"))
                    .FirstOrDefault();
                if (ayah != null)
                {
                    model.AgamaAyah = ayah.Agama;
                    model.AlamatAyah = ayah.Alamat;
                    model.EmailAyah = ayah.Email;
                    model.KeteranganAyah = ayah.Keterangan;
                    model.NamaLengkapAyah = ayah.NamaLengkap;
                    model.NoHpAyah = ayah.NoHp;
                    model.NoTelpAyah = ayah.NoTelp;
                    model.PekerjaanAyah = ayah.Pekerjaan;
                    model.PendidikanTerakhirAyah = ayah.PendidikanTerakhir;
                    model.PenghasilanAyah = ayah.Penghasilan;
                    model.StatusDalamKeluargaAyah = ayah.StatusDalamKeluarga;
                    model.TanggalLahirAyah = ayah.TanggalLahir;
                    model.TempatLahirAyah = ayah.TempatLahir;
                }
                var ibu = akun.CalonSiswa.ListPenanggungjawab
                    .Where(x => x.Sebagai.Equals("Ibu"))
                    .FirstOrDefault();
                if (ibu != null)
                {
                    model.AgamaIbu = ibu.Agama;
                    model.AlamatIbu = ibu.Alamat;
                    model.EmailIbu = ibu.Email;
                    model.KeteranganIbu = ibu.Keterangan;
                    model.NamaLengkapIbu = ibu.NamaLengkap;
                    model.NoHpIbu = ibu.NoHp;
                    model.NoTelpIbu = ibu.NoTelp;
                    model.PekerjaanIbu = ibu.Pekerjaan;
                    model.PendidikanTerakhirIbu = ibu.PendidikanTerakhir;
                    model.PenghasilanIbu = ibu.Penghasilan;
                    model.StatusDalamKeluargaIbu = ibu.StatusDalamKeluarga;
                    model.TanggalLahirIbu = ibu.TanggalLahir;
                    model.TempatLahirIbu = ibu.TempatLahir;
                }
                var wali = akun.CalonSiswa.ListPenanggungjawab
                    .Where(x => x.Sebagai.Equals("Wali"))
                    .FirstOrDefault();
                if (wali != null)
                {
                    model.AgamaWali = wali.Agama;
                    model.AlamatWali = wali.Alamat;
                    model.EmailWali = wali.Email;
                    model.NamaLengkapWali = wali.NamaLengkap;
                    model.NoHpWali = wali.NoHp;
                    model.NoTelpWali = wali.NoTelp;
                    model.PekerjaanWali = wali.Pekerjaan;
                    model.PendidikanTerakhirWali = wali.PendidikanTerakhir;
                    model.PenghasilanWali = wali.Penghasilan;
                    model.TanggalLahirWali = wali.TanggalLahir;
                    model.TempatLahirWali = wali.TempatLahir;
                }
            }
            return View(model);
        }
        [HttpGet]
        public IActionResult KelolaDataPenunjang()
        {
            var akun = _calonSiswaService.GetDetailPenunjang(User.Identity.Name);
            var model = new KelolaDataPenunjangModel();
            if (akun.CalonSiswa.Penunjang != null)
            {
                model.DayaListrik = akun.CalonSiswa.Penunjang.DayaListrik;
                model.JarakTempuh = akun.CalonSiswa.Penunjang.JarakTempuh;
                model.Pembiaya = akun.CalonSiswa.Penunjang.Pembiaya;
                model.StatusTempatTinggal = akun.CalonSiswa.Penunjang.StatusTempatTinggal;
                model.Transportasi = akun.CalonSiswa.Penunjang.Transportasi;
                model.WaktuTempuh = akun.CalonSiswa.Penunjang.WaktuTempuh;
            }
            return View(model);
        }

        [HttpPost]
        public IActionResult KelolaDataDiri(KelolaDataDiriModel model)
        {
            string namaLengkap = model.NamaLengkap;
            string noPendaftaran = User.Identity.Name;
            var newData = new DataDiri()
            {
                Agama = model.Agama,
                Alamat = model.Alamat,
                AnakKe = model.AnakKe,
                BeratBadan = model.BeratBadan,
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
            _calonSiswaService.SaveDataDiri(noPendaftaran, namaLengkap, newData);
            TempData["Pesan"] = "Berhasil menyimpan";
            return RedirectToAction(nameof(Biodata));
        }
        [HttpPost]
        public IActionResult KelolaDataAkademikTerakhir(KelolaDataAkademikTerakhir model)
        {
            string noPendaftaran = User.Identity.Name;
            var newData = new AkademikTerakhir()
            {
                AlamatSekolah = model.AlamatSekolah,
                JenisSekolah = model.JenisSekolah,
                NamaSekolah = model.NamaSekolah,
                NoPesertaUn = model.NoPesertaUn,
                NoSeriIjazah = model.NoSeriIjazah,
                NoSeriSkhun = model.NoSeriSkhun,
                StatusSekolah = model.StatusSekolah
            };
            _calonSiswaService.SaveDataAkademikTerakhir(noPendaftaran, newData);
            TempData["Pesan"] = "Berhasil menyimpan";
            return RedirectToAction(nameof(Biodata));
        }
        [HttpPost]
        public IActionResult KelolaDataRapor(KelolaDataRaporModel model)
        {
            string noPendaftaran = User.Identity.Name;
            var newData = new List<Rapor>();
            for(int i = 0; i<model.ListRapor.Length; i++)
            {
                newData.Add(new Rapor()
                {
                    MataPelajaran = model.ListRapor[i].MataPelajaran,
                    Semester1 = model.ListRapor[i].Semester1,
                    Semester2 = model.ListRapor[i].Semester2,
                    Semester3 = model.ListRapor[i].Semester3,
                    Semester4 = model.ListRapor[i].Semester4,
                    Semester5 = model.ListRapor[i].Semester5,
                });
            }
            _calonSiswaService.SaveDataRapor(noPendaftaran, newData);
            TempData["Pesan"] = "Berhasil menyimpan";
            return RedirectToAction(nameof(Biodata));
        }
        [HttpPost]
        public IActionResult KelolaDataPrestasi(KelolaDataPrestasiModel model)
        {
            string noPendaftaran = User.Identity.Name;
            var newData = new Prestasi()
            {
                Jenis = model.Jenis,
                NamaKejuaraan = model.NamaKejuaraan,
                Penyelenggara = model.Penyelenggara,
                Peringkat = model.Peringkat,
                Tahun = model.Tanggal,
                Tingkat = model.Tingkat
            };
            _calonSiswaService.SaveDataPrestasi(noPendaftaran, newData);
            TempData["Pesan"] = "Berhasil menyimpan";
            return RedirectToAction(nameof(Biodata));
        }
        [HttpPost]
        public IActionResult KelolaDataOrangTua(KelolaDataOrangTuaModel model)
        {
            string noPendaftaran = User.Identity.Name;
            var newData = new List<Penanggungjawab>()
            {
                // Ayah
                new Penanggungjawab()
                {
                    Agama = model.AgamaAyah,
                    Alamat = model.AlamatAyah,
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
                }
            };
            if (model.NamaLengkapWali != null)
            {
                // Wali
                newData.Add(new Penanggungjawab()
                {
                    Agama = model.AgamaWali,
                    Alamat = model.AlamatWali,
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
                });
            }
            _calonSiswaService.SaveDataPenanggunjawab(noPendaftaran, newData);
            TempData["Pesan"] = "Berhasil menyimpan";
            return RedirectToAction(nameof(Biodata));
        }
        [HttpPost]
        public IActionResult KelolaDataPenunjang(KelolaDataPenunjangModel model)
        {
            string noPendaftaran = User.Identity.Name;
            var newData = new Penunjang()
            {
                DayaListrik = model.DayaListrik,
                JarakTempuh = model.JarakTempuh,
                Pembiaya = model.Pembiaya,
                StatusTempatTinggal = model.StatusTempatTinggal,
                Transportasi = model.Transportasi,
                WaktuTempuh = model.WaktuTempuh
            };
            _calonSiswaService.SaveDataPenunjang(noPendaftaran, newData);
            TempData["Pesan"] = "Berhasil menyimpan";
            return RedirectToAction(nameof(Biodata));
        }
    }
}
