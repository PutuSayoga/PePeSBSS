using BackEnd.Abstraction;
using FrontEnd.Web.Mvc.Models.CalonSiswa;
using FrontEnd.Web.Mvc.Models.PsbPendaftaran;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FrontEnd.Web.Mvc.Controllers
{
    [Authorize]
    public class PrintController : Controller
    {
        private readonly IPendaftaran _pendaftaranService;
        private readonly ICalonSiswa _calonSiswaService;
        public PrintController(IPendaftaran pendaftaranService, ICalonSiswa calonSiswaService)
        {
            _pendaftaranService = pendaftaranService;
            _calonSiswaService = calonSiswaService;
        }

        public IActionResult BuktiPendaftaran(int id)
        {
            var detailAkun = _pendaftaranService.GetAkunPendaftaran(id);
            var model = new BuktiPendaftaranModel()
            {
                NoPendaftaran = detailAkun.NoPendaftaran,
                NamaLengkap = detailAkun.CalonSiswa.NamaLengkap,
                JalurPendaftaran = detailAkun.JalurPendaftaran,
                Password = detailAkun.Password,
                JadwalTes = detailAkun.JadwalTes
                    .ToString("dddd, dd MMMM yyyy", new System.Globalization.CultureInfo("id-ID"))
            };
            return View(model);
        }
        public IActionResult BiodataCalonSiswa()
        {
            string noPendaftaran = User.Identity.Name;
            var model = MapBiodata(noPendaftaran);
            return View(model);
        }
        public BiodataModel MapBiodata(string noPendaftaran)
        {
            var dataDiri = _calonSiswaService.GetDetailDiri(noPendaftaran);
            var dataDiriModel = new KelolaDataDiriModel()
            {
                JalurPendaftaran = dataDiri.JalurPendaftaran,
                NoPendaftaran = dataDiri.NoPendaftaran,
                NamaLengkap = dataDiri.CalonSiswa.NamaLengkap,
                Nik = dataDiri.CalonSiswa.Nik   
            };
            if (dataDiri.CalonSiswa.DataDiri != null)
            {
                dataDiriModel.Agama = dataDiri.CalonSiswa.DataDiri.Agama;
                dataDiriModel.Alamat = dataDiri.CalonSiswa.DataDiri.Alamat;
                dataDiriModel.AnakKe = dataDiri.CalonSiswa.DataDiri.AnakKe;
                dataDiriModel.BeratBadan = dataDiri.CalonSiswa.DataDiri.BeratBadan;
                dataDiriModel.CitaCita = dataDiri.CalonSiswa.DataDiri.CitaCita;
                dataDiriModel.DusunDesaLurah = dataDiri.CalonSiswa.DataDiri.DusunDesaLurah;
                dataDiriModel.Email = dataDiri.CalonSiswa.DataDiri.Email;
                dataDiriModel.GolDarah = dataDiri.CalonSiswa.DataDiri.GolDarah;
                dataDiriModel.Hobi = dataDiri.CalonSiswa.DataDiri.Hobi;
                dataDiriModel.IsPerempuan = dataDiri.CalonSiswa.DataDiri.IsPerempuan;
                dataDiriModel.JumlahSaudara = dataDiri.CalonSiswa.DataDiri.JumlahSaudara;
                dataDiriModel.Kecamatan = dataDiri.CalonSiswa.DataDiri.Kecamatan;
                dataDiriModel.KelainanJasmani = dataDiri.CalonSiswa.DataDiri.KelainanJasmani;
                dataDiriModel.KodePos = dataDiri.CalonSiswa.DataDiri.KodePos;
                dataDiriModel.KotaKabupaten = dataDiri.CalonSiswa.DataDiri.KotaKabupaten;
                dataDiriModel.NamaPanggilan = dataDiri.CalonSiswa.DataDiri.NamaPanggilan;
                dataDiriModel.NoHp = dataDiri.CalonSiswa.DataDiri.NoHp;
                dataDiriModel.NoTelp = dataDiri.CalonSiswa.DataDiri.NoTelp;
                dataDiriModel.RiwayatSakit = dataDiri.CalonSiswa.DataDiri.RiwayatSakit;
                dataDiriModel.Rt = dataDiri.CalonSiswa.DataDiri.Rt;
                dataDiriModel.Rw = dataDiri.CalonSiswa.DataDiri.Rw;
                dataDiriModel.StatusDalamKeluarga = dataDiri.CalonSiswa.DataDiri.StatusDalamKeluarga;
                dataDiriModel.TanggalLahir = dataDiri.CalonSiswa.DataDiri.TanggalLahir;
                dataDiriModel.TempatLahir = dataDiri.CalonSiswa.DataDiri.TempatLahir;
                dataDiriModel.TinggiBadan = dataDiri.CalonSiswa.DataDiri.TinggiBadan;
            }
            var biodata = new BiodataModel()
            {
                DataDiri = dataDiriModel
            };
            return biodata;
        }
    }
}
