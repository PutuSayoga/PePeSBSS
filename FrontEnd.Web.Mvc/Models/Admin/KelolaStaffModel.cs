using BackEnd.Domains;
using System.Collections.Generic;

namespace FrontEnd.Web.Mvc.Models.Admin
{
    public class KelolaStaffModel
    {
        public List<SimpleStaff> DaftarStaff { set; get; }
    }
    public class SimpleStaff
    {
        public int Id { get; set; }
        public string NamaLengkap { get; set; }
        public string Nip { get; set; }
        public string Jabatan { get; set; }
        public string PanitiaAcara { get; set; }
    }
}
