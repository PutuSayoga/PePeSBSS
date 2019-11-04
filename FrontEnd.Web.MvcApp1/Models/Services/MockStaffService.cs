using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FrontEnd.Web.MvcApp.Models.Domains;

namespace FrontEnd.Web.MvcApp.Models.Services
{
    public class MockStaffService : IStaffService
    {
        private List<Staff1> _listStaff;
        private List<SoalAkademik> _listSoalAkademik;

        public MockStaffService()
        {
            _listStaff = new List<Staff1>()
            {
                new Staff1() { Id=1, Nip="199705272019101601", NamaLengkap="Putu Gede Sayoga", Email="putugedesayoga@gmail.com", Jabatan="Admin", Username="sayoga", Password="1234"},
                new Staff1() { Id=2, Nip="199812082019101601", NamaLengkap="Albert Amer", Email="albertamer@gmail.com", Jabatan="Tata Usaha", Username="wahyu", Password="1234",
                                Panitia = new Panitia(){ Acara="PPSB", Divisi="Pendaftaran"} }
            };
            _listSoalAkademik = new List<SoalAkademik>()
            {
                new SoalAkademik(){Id=1, JudulSoal="Tes Matematika dasar", Kategori="Matematika", KodeSoal="101", WaktuPengerjaan=1, MaxSoal=5,
                        ListPertanyann = new List<PertanyaanAkademik>(){
                new PertanyaanAkademik()
                {
                    Pertanyaan = @"1+1?",
                    Pilihan = new Dictionary<string, bool>()
                    {
                        {"1", false },
                        {"2", true },
                        {"3", false },
                        {"4", false }
                    }
                },
                new PertanyaanAkademik()
                {
                    Pertanyaan = @"1+2?",
                    Pilihan = new Dictionary<string, bool>()
                    {
                        {"1", false },
                        {"2", false },
                        {"3", true },
                        {"4", false }
                    }
                },
                new PertanyaanAkademik()
                {
                    Pertanyaan = @"1+3?",
                    Pilihan = new Dictionary<string, bool>()
                    {
                        {"1", false },
                        {"2", false },
                        {"3", false },
                        {"4", true }
                    }
                },
                new PertanyaanAkademik()
                {
                    Pertanyaan = @"1+4?",
                    Pilihan = new Dictionary<string, bool>()
                    {
                        {"2", false },
                        {"3", false },
                        {"4", false },
                        {"5", true }
                    }
                }
                        }
            },
                new SoalAkademik(){Id=2, JudulSoal="Tes sejarah dasar", Kategori="Sejarah", KodeSoal="102", WaktuPengerjaan=1, MaxSoal=5,
                        ListPertanyann = new List<PertanyaanAkademik>(){
                new PertanyaanAkademik()
                {
                    Pertanyaan = @"Tahun kemerdekaan Negara Kesatuan Republik Indonesia",
                    Pilihan = new Dictionary<string, bool>()
                    {
                        {"1945", true },
                        {"1946", false },
                        {"1947", false },
                        {"1948", false }
                    }
                },
                new PertanyaanAkademik()
                {
                    Pertanyaan = @"Tanggal dilakukannya sumpah pemuda?",
                    Pilihan = new Dictionary<string, bool>()
                    {
                        {"27 Oktober 1928", false },
                        {"28 Oktober 1928", true },
                        {"27 Oktober 1938", false },
                        {"28 Oktober 1938", false }
                    }
                }
                        }
            }
            };
        }

        public string TambahStaff(Staff1 staffBaru)
        {
            if (_listStaff.Any(staff => staff.Nip == staffBaru.Nip))
            {
                return "NIP sudah terdaftar";
            }
            else if (_listStaff.Any(staff => staff.Username == staffBaru.Username))
            {
                return "Username sudah dipakai";
            }
            else
            {
                _listStaff.Add(staffBaru);
                return "Sukses";
            }
        }

        public int HapusStaff(int id)
        {
            if (_listStaff.Any(staff => staff.Id == id))
            {
                _listStaff.RemoveAll(staff => staff.Id == id);
                return 1;
            }
            return 0;
        }

        public IEnumerable<Staff1> AmbilSemuaStaff()
        {
            return _listStaff;
        }

        public Staff1 RincianStaff(int id)
        {
            return _listStaff.First(staff => staff.Id == id);
        }

        public int UbahStaff(Staff1 dataStaffBaru)
        {
            Staff1 tempStaff = _listStaff.First(staff => staff.Id == dataStaffBaru.Id);
            int index = _listStaff.IndexOf(tempStaff);
            if (index != -1)
            {
                tempStaff.NamaLengkap = dataStaffBaru.NamaLengkap;
                tempStaff.Email = dataStaffBaru.Email;
                tempStaff.NoHp = dataStaffBaru.NoHp;
                tempStaff.Jabatan = dataStaffBaru.Jabatan;
                tempStaff.Password = dataStaffBaru.Password;

                _listStaff[index] = tempStaff;

                return 1;
            }
            else
            {
                return 0;
            }
        }

        public int TambahPanitia(int idStaff, Panitia panitia)
        {
            Staff1 tempStaff = _listStaff.First(staff => staff.Id == idStaff);
            int index = _listStaff.IndexOf(tempStaff);
            if (index != -1)
            {
                tempStaff.Panitia = panitia;

                _listStaff[index] = tempStaff;

                return 1;
            }
            else
            {
                return 0;
            }
        }

        public int HapusPanitia(int id)
        {
            Staff1 tempStaff = _listStaff.First(staff => staff.Id == id);
            int index = _listStaff.IndexOf(tempStaff);
            if (index != -1)
            {
                tempStaff.Panitia = null;

                _listStaff[index] = tempStaff;

                return 1;
            }
            else
            {
                return 0;
            }
        }


        public SoalAkademik RincianSoalAkademik(int id)
        {
            return _listSoalAkademik.First(soalAkademik => soalAkademik.Id == id);
        }

        public IEnumerable<SoalAkademik> AmbilSemuaSoalAkademik()
        {
            return _listSoalAkademik;
        }

        public string TambahSoalAkademik(SoalAkademik soalAkademikBaru)
        {
            if (_listSoalAkademik.Any(staff => staff.KodeSoal == soalAkademikBaru.KodeSoal))
            {
                return "Kode soal sudah terdaftar";
            }
            else
            {
                _listSoalAkademik.Add(soalAkademikBaru);
                return "Sukses";
            }
        }

        public int HapusSoalAkademik(int id)
        {
            if (_listSoalAkademik.Any(soalAkademik => soalAkademik.Id == id))
            {
                _listSoalAkademik.RemoveAll(SoalAkademik => SoalAkademik.Id == id);
                return 1;
            }
            return 0;
        }

        public int UbahSoalAkademik(SoalAkademik dataSoalAkademikBaru)
        {
            throw new NotImplementedException();
        }

        public int TambahPertanyaanAkademik(int idSoal, PertanyaanAkademik pertanyaanBaru)
        {
            throw new NotImplementedException();
        }

        public int UbahPertanyaanAkademik(PertanyaanAkademik dataPertanyaanBaru)
        {
            throw new NotImplementedException();
        }

        public int HapusPertanyaanAkademik(int id)
        {
            throw new NotImplementedException();
        }
    }
}
