using System;
using System.Collections.Generic;
using System.Text;

namespace BackEnd.Abstraction
{
    public interface ITesAkademik
    {
        bool IsLogin(string noPendaftaran, string Kode);
    }
}
