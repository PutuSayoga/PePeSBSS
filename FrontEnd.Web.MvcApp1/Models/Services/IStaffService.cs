using FrontEnd.Web.MvcApp.Models.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FrontEnd.Web.MvcApp.Models.Services
{
    public interface IStaffService
    {
        IEnumerable<Staff1> AmbilSemuaStaff();
        Staff1 RincianStaff(int id);
        string TambahStaff(Staff1 staffBaru);
        int HapusStaff(int id);
        int UbahStaff(Staff1 dataStaffBaru);
        int TambahPanitia(int idStaff, Panitia panitia);
        int HapusPanitia(int id);

        IEnumerable<SoalAkademik> AmbilSemuaSoalAkademik();
        SoalAkademik RincianSoalAkademik(int id);
        string TambahSoalAkademik(SoalAkademik soalAkademikBaru);
        int HapusSoalAkademik(int id);
        int UbahSoalAkademik(SoalAkademik dataSoalAkademikBaru);
        int TambahPertanyaanAkademik(int idSoal, PertanyaanAkademik pertanyaanBaru);
        int UbahPertanyaanAkademik(PertanyaanAkademik dataPertanyaanBaru);
        int HapusPertanyaanAkademik(int id);
    }
}
