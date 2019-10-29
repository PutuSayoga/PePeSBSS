using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FrontEnd.Web.Mvc.Models.Staff
{
    public class KelolaStaffModel
    {
        public IEnumerable<MinimizeStaff> DaftarStaff { set; get; }
        public string Pesan { get; set; }
    }
}
