using System;
using System.Collections.Generic;
using System.Text;

namespace BackEnd.Abstraction
{
    public interface INotif
    {
        string SendNotif(string noTelp, string message);
    }
}
