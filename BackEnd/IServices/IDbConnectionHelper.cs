using System;
using System.Collections.Generic;
using System.Text;

namespace BackEnd.IServices
{
    public interface IDbConnectionHelper
    {
        string GetConnectionString();
    }
}
