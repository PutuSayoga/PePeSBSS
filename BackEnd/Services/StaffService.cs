using BackEnd.IServices;
using BackEnd.Domains;
using System;
using System.Collections.Generic;
using Dapper;
using System.Data.SqlClient;
using System.Linq;

namespace BackEnd.Services
{
    public class StaffService : IStaff
    {
        private readonly IDbConnectionHelper _connectionHelper;

        public StaffService(IDbConnectionHelper connectionHelper)
        {
            _connectionHelper = connectionHelper;
        }

        public int AddPanitiaToStaff(int idStaff, Panitia newPanitia)
        {
            throw new NotImplementedException();
        }

        public string AddStaff(Staff newStaff)
        {
            throw new NotImplementedException();
        }

        public int DeletePanitiaFromStaff(int idStaff)
        {
            throw new NotImplementedException();
        }

        public int DeleteStaff(int id)
        {
            throw new NotImplementedException();
        }

        public Staff DetailStaff(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Staff> GetAllStaff()
        {
            string sqlQuery = $"SELECT * FROM Staff";
            using(var connection = new SqlConnection(_connectionHelper.GetConnectionString()))
            {
                connection.Open();
                return connection.Query<Staff>(sqlQuery).ToList();
            }
        }

        public int UpdateStaff(Staff newData)
        {
            throw new NotImplementedException();
        }
    }
}
