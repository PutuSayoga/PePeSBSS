using System;
using System.Collections.Generic;

namespace BackEnd.Domains
{
    public partial class Staff
    {
        public int Id { get; set; }
        public string Nip { get; set; }
        public string NamaLengkap { get; set; }
        public string Email { get; set; }
        public string NoHp { get; set; }
        public string Jabatan { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public virtual Panitia Panitia { get; set; }
    }
}
