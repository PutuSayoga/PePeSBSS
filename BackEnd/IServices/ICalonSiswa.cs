using BackEnd.Domains;
using System;
using System.Collections.Generic;
using System.Text;

namespace BackEnd.IServices
{
    public interface ICalonSiswa
    {
        CalonSiswa GetDetailCalonSiswa(int id);
    }
}
