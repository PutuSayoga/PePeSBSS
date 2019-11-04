using BackEnd.Domains;
using System;
using System.Collections.Generic;
using System.Text;

namespace BackEnd.Abstraction
{
    public interface ICalonSiswa
    {
        CalonSiswa GetDetailCalonSiswa(int id);
    }
}
