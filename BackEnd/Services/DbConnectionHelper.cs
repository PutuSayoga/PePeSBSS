using BackEnd.IServices;
using System;
using System.Collections.Generic;
using System.Text;

namespace BackEnd.Services
{
    public class DbConnectionHelper : IDbConnectionHelper
    {
        private readonly string _connectionString;

        public DbConnectionHelper(string connectionString)
        {
            _connectionString = connectionString;
        }

        public string GetConnectionString()
        {
            return _connectionString;
        }
    }
}
