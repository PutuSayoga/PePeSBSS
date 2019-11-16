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
    [Authorize(Roles = "Calon Siswa")]
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
        {
            var akun = _calonSiswaService.GetDetailDiri(User.Identity.Name);
            var model = new KelolaDataDiriModel()
            {
                JalurPendaftaran = akun.JalurPendaftaran,
                NoPendaftaran = akun.NoPendaftaran,
                NamaLengkap = akun.ACalonSiswa.NamaLengkap
            };
            if (akun.ACalonSiswa.ADataDiri != null)
            {
                model.Agama = akun.ACalonSiswa.ADataDiri.Agama;
                model.Alamat = akun.ACalonSiswa.ADataDiri.Alamat;
                model.AnakKe = akun.ACalonSiswa.ADataDiri.AnakKe;
                model.BeratBadan = akun.ACalonSiswa.ADataDiri.BeratBadan;
                model.CitaCita = akun.ACalonSiswa.ADataDiri.CitaCita;
                model.DusunDesaLurah = akun.ACalonSiswa.ADataDiri.DusunDesaLurah;
                model.Email = akun.ACalonSiswa.ADataDiri.Email;
                model.GolDarah = akun.ACalonSiswa.ADataDiri.GolDarah;
                model.Hobi = akun.ACalonSiswa.ADataDiri.Hobi;
                model.IsPerempuan = akun.ACalonSiswa.ADataDiri.IsPerempuan;
                model.JumlahSaudara = akun.ACalonSiswa.ADataDiri.JumlahSaudara;
                model.Kecamatan = akun.ACalonSiswa.ADataDiri.Kecamatan;
                model.KelainanJasmani = akun.ACalonSiswa.ADataDiri.KelainanJasmani;
                model.KodePos = akun.ACalonSiswa.ADataDiri.KodePos;
                model.KotaKabupaten = akun.ACalonSiswa.ADataDiri.KotaKabupaten;
                model.NamaPanggilan = akun.ACalonSiswa.ADataDiri.NamaPanggilan;
                model.NoHp = akun.ACalonSiswa.ADataDiri.NoHp;
                model.NoTelp = akun.ACalonSiswa.ADataDiri.NoTelp;
                model.RiwayatSakit = akun.ACalonSiswa.ADataDiri.RiwayatSakit;
                model.Rt = akun.ACalonSiswa.ADataDiri.Rt;
                model.Rw = akun.ACalonSiswa.ADataDiri.Rw;
                model.StatusDalamKeluarga = akun.ACalonSiswa.ADataDiri.StatusDalamKeluarga;
                model.TanggalLahir = akun.ACalonSiswa.ADataDiri.TanggalLahir;
                model.TempatLahir = akun.ACalonSiswa.ADataDiri.TempatLahir;
                model.TinggiBadan = akun.ACalonSiswa.ADataDiri.TinggiBadan;
            }

            return View(model);
        }
        [HttpGet]
        public IActionResult KelolaDataAkademikTerakhir()
        {
            var akun = _calonSiswaService.GetDetailAkademikTerakhir(User.Identity.Name);
            var model = new KelolaDataAkademikTerakhir();
            if (akun.ACalonSiswa.AAkademikTerakhir != null)
            {
                model.AlamatSekolah = akun.ACalonSiswa.AAkademikTerakhir.AlamatSekolah;
                model.JenisSekolah = akun.ACalonSiswa.AAkademikTerakhir.JenisSekolah;
                model.NamaSekolah = akun.ACalonSiswa.AAkademikTerakhir.NamaSekolah;
                model.NoPesertaUn = akun.ACalonSiswa.AAkademikTerakhir.NoPesertaUn;
                model.NoSeriIjazah = akun.ACalonSiswa.AAkademikTerakhir.NoSeriIjazah;
                model.NoSeriSkhun = akun.ACalonSiswa.AAkademikTerakhir.NoSeriSkhun;
                model.StatusSekolah = akun.ACalonSiswa.AAkademikTerakhir.StatusSekolah;
            }
            return View(model);
        }
        [HttpGet]
        public IActionResult KelolaDataRapor()
        {
            var akun = _calonSiswaService.GetDetailRapor(User.Identity.Name);
            var model = new KelolaDataRaporModel();
            if (akun.ACalonSiswa.RaporS != null)
            {
                model.ListRapor[0] = akun.ACalonSiswa.RaporS
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
                model.ListRapor[1] = akun.ACalonSiswa.RaporS
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
                model.ListRapor[2] = akun.ACalonSiswa.RaporS
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
                model.ListRapor[3] = akun.ACalonSiswa.RaporS
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
                model.ListRapor[4] = akun.ACalonSiswa.RaporS
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
                model.ListRapor[5] = akun.ACalonSiswa.RaporS
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
                model.ListRapor[6] = akun.ACalonSiswa.RaporS
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
                model.ListRapor[7] = akun.ACalonSiswa.RaporS
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
                model.ListRapor[8] = akun.ACalonSiswa.RaporS
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
                model.ListRapor[9] = akun.ACalonSiswa.RaporS
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
                model.ListRapor[10] = akun.ACalonSiswa.RaporS
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
            if (akun.ACalonSiswa.APrestasi != null)
            {
                model.Jenis = akun.ACalonSiswa.APrestasi.Jenis;
                model.NamaKejuaraan = akun.ACalonSiswa.APrestasi.NamaKejuaraan;
                model.Penyelenggara = akun.ACalonSiswa.APrestasi.Penyelenggara;
                model.Peringkat = akun.ACalonSiswa.APrestasi.Peringkat;
                model.Tahun = akun.ACalonSiswa.APrestasi.Tahun;
                model.Tingkat = akun.ACalonSiswa.APrestasi.Tingkat;
            }
            return View(model);
        }
        [HttpGet]
        public IActionResult KelolaDataOrangTua()
        {
            var akun = _calonSiswaService.GetDetailPrestasi(User.Identity.Name);
            var model = new KelolaDataOrangTuaModel();
            if (akun.ACalonSiswa.PenanggungjawabS != null)
            {
                var ayah = akun.ACalonSiswa.PenanggungjawabS
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
                var ibu = akun.ACalonSiswa.PenanggungjawabS
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
                var wali = akun.ACalonSiswa.PenanggungjawabS
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
            if (akun.ACalonSiswa.APenunjang != null)
            {
                model.DayaListrik = akun.ACalonSiswa.APenunjang.DayaListrik;
                model.JarakTempuh = akun.ACalonSiswa.APenunjang.JarakTempuh;
                model.Pembiaya = akun.ACalonSiswa.APenunjang.Pembiaya;
                model.StatusTempatTinggal = akun.ACalonSiswa.APenunjang.StatusTempatTinggal;
                model.Transportasi = akun.ACalonSiswa.APenunjang.Transportasi;
                model.WaktuTempuh = akun.ACalonSiswa.APenunjang.WaktuTempuh;
            }
            return View();
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
            return RedirectToAction(nameof(CetakBiodata));
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
            return RedirectToAction(nameof(CetakBiodata));
        }
        [HttpPost]
        public IActionResult KelolaDataRapor(KelolaDataRaporModel model)
        {
            string noPendaftaran = User.Identity.Name;
            var newData = new List<Rapor>()
            {
                new Rapor()
                {
                    MataPelajaran = model.ListRapor[0].MataPelajaran,
                    Semester1 = model.ListRapor[0].Semester1,
                    Semester2 = model.ListRapor[0].Semester2,
                    Semester3 = model.ListRapor[0].Semester3,
                    Semester4 = model.ListRapor[0].Semester4,
                    Semester5 = model.ListRapor[0].Semester5,
                },
                new Rapor()
                {
                    MataPelajaran = model.ListRapor[1].MataPelajaran,
                    Semester1 = model.ListRapor[1].Semester1,
                    Semester2 = model.ListRapor[1].Semester2,
                    Semester3 = model.ListRapor[1].Semester3,
                    Semester4 = model.ListRapor[1].Semester4,
                    Semester5 = model.ListRapor[1].Semester5,
                },
                new Rapor()
                {
                    MataPelajaran = model.ListRapor[2].MataPelajaran,
                    Semester1 = model.ListRapor[2].Semester1,
                    Semester2 = model.ListRapor[2].Semester2,
                    Semester3 = model.ListRapor[2].Semester3,
                    Semester4 = model.ListRapor[2].Semester4,
                    Semester5 = model.ListRapor[2].Semester5,
                },
                new Rapor()
                {
                    MataPelajaran = model.ListRapor[3].MataPelajaran,
                    Semester1 = model.ListRapor[3].Semester1,
                    Semester2 = model.ListRapor[3].Semester2,
                    Semester3 = model.ListRapor[3].Semester3,
                    Semester4 = model.ListRapor[3].Semester4,
                    Semester5 = model.ListRapor[3].Semester5,
                },
                new Rapor()
                {
                    MataPelajaran = model.ListRapor[4].MataPelajaran,
                    Semester1 = model.ListRapor[4].Semester1,
                    Semester2 = model.ListRapor[4].Semester2,
                    Semester3 = model.ListRapor[4].Semester3,
                    Semester4 = model.ListRapor[4].Semester4,
                    Semester5 = model.ListRapor[4].Semester5,
                },
                new Rapor()
                {
                    MataPelajaran = model.ListRapor[5].MataPelajaran,
                    Semester1 = model.ListRapor[5].Semester1,
                    Semester2 = model.ListRapor[5].Semester2,
                    Semester3 = model.ListRapor[5].Semester3,
                    Semester4 = model.ListRapor[5].Semester4,
                    Semester5 = model.ListRapor[5].Semester5,
                },
                new Rapor()
                {
                    MataPelajaran = model.ListRapor[6].MataPelajaran,
                    Semester1 = model.ListRapor[6].Semester1,
                    Semester2 = model.ListRapor[6].Semester2,
                    Semester3 = model.ListRapor[6].Semester3,
                    Semester4 = model.ListRapor[6].Semester4,
                    Semester5 = model.ListRapor[6].Semester5,
                },
                new Rapor()
                {
                    MataPelajaran = model.ListRapor[7].MataPelajaran,
                    Semester1 = model.ListRapor[7].Semester1,
                    Semester2 = model.ListRapor[7].Semester2,
                    Semester3 = model.ListRapor[7].Semester3,
                    Semester4 = model.ListRapor[7].Semester4,
                    Semester5 = model.ListRapor[7].Semester5,
                },
                new Rapor()
                {
                    MataPelajaran = model.ListRapor[8].MataPelajaran,
                    Semester1 = model.ListRapor[8].Semester1,
                    Semester2 = model.ListRapor[8].Semester2,
                    Semester3 = model.ListRapor[8].Semester3,
                    Semester4 = model.ListRapor[8].Semester4,
                    Semester5 = model.ListRapor[8].Semester5,
                },
                new Rapor()
                {
                    MataPelajaran = model.ListRapor[9].MataPelajaran,
                    Semester1 = model.ListRapor[9].Semester1,
                    Semester2 = model.ListRapor[9].Semester2,
                    Semester3 = model.ListRapor[9].Semester3,
                    Semester4 = model.ListRapor[9].Semester4,
                    Semester5 = model.ListRapor[9].Semester5,
                },
                new Rapor()
                {
                    MataPelajaran = model.ListRapor[10].MataPelajaran,
                    Semester1 = model.ListRapor[10].Semester1,
                    Semester2 = model.ListRapor[10].Semester2,
                    Semester3 = model.ListRapor[10].Semester3,
                    Semester4 = model.ListRapor[10].Semester4,
                    Semester5 = model.ListRapor[10].Semester5,
                }
            };
            _calonSiswaService.SaveDataRapor(noPendaftaran, newData);
            TempData["Pesan"] = "Berhasil menyimpan";
            return RedirectToAction(nameof(CetakBiodata));
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
                Tahun = model.Tahun,
                Tingkat = model.Tingkat
            };
            _calonSiswaService.SaveDataPrestasi(noPendaftaran, newData);
            TempData["Pesan"] = "Berhasil menyimpan";
            return RedirectToAction(nameof(CetakBiodata));
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
            return RedirectToAction(nameof(CetakBiodata));
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
            return RedirectToAction(nameof(CetakBiodata));
        }
        public IActionResult CetakBiodata()
        {
            return View();
        }
        public IActionResult HasilSeleksi()
        {
            var akun = _calonSiswaService.CekStatus(User.Identity.Name);
            var model = new HasilSeleksiModel()
            {
                Status = akun.Status
            };
            return View(model);
        }
    }
}
