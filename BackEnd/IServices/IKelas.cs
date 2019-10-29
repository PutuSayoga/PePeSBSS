using BackEnd.Domains;
using System;
using System.Collections.Generic;
using System.Text;

namespace BackEnd.IServices
{
    public interface IKelas
    {
        IEnumerable<Kelas> GetAllKelas();
    }
}
