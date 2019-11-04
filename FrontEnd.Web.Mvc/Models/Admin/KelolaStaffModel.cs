using BackEnd.Domains;
using System.Collections.Generic;

namespace FrontEnd.Web.Mvc.Models.Admin
{
    public class KelolaStaffModel
    {
        public IEnumerable<Staff> DaftarStaff { set; get; }
    }
}
