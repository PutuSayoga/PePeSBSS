using BackEnd.Abstraction;
using BackEnd.Domains;
using System;
using System.Collections.Generic;
using Dapper;
using System.Data.SqlClient;
using System.Linq;
using System.Data;

namespace BackEnd.Services
{
    public class StaffSmaService : IStaffSma
    {
        private readonly IDbConnectionHelper _connectionHelper;
        private readonly ISecurityRelate _securityRelateHelper;
        public StaffSmaService(IDbConnectionHelper connectionHelper, ISecurityRelate securityRelateHelper)
        {
            _connectionHelper = connectionHelper;
            _securityRelateHelper = securityRelateHelper;
        }

        #region Not Interface Implementation
        private bool IsExistInStaff(string column, string value)
        {
            string sqlQuery = $"SELECT {column} FROM Staff WHERE {column} = @Value";
            using (var connection = new SqlConnection(_connectionHelper.GetConnectionString()))
            {
                connection.Open();
                string result = connection.QueryFirstOrDefault<string>(sql: sqlQuery, param: new { Value = value });

                return result != null;
            }
        }
        #endregion

        public List<Staff> GetAllStaff()
        {
            string sqlQuery = @"SELECT * FROM Staff FULL JOIN Panitia ON Staff.Id = Panitia.StaffId";
            using (var connection = new SqlConnection(_connectionHelper.GetConnectionString()))
            {
                connection.Open();

                // Get data from database and map them using dapper
                var result = connection.Query<Staff, Panitia, Staff>(
                    sql: sqlQuery,
                    map: (staff, panitia) =>
                    {
                        staff.Panitia = panitia;
                        return staff;
                    },
                    splitOn: "StaffId").Distinct()
                    .ToList();

                return result;
            }
        }
        public string AddStaff(Staff newStaff)
        {
            // Cek NIP
            if (IsExistInStaff(column: "Nip", value: newStaff.Nip))
            {
                return "NIP sudah terdaftar";
            }
            // Cek Username
            else if (IsExistInStaff(column: "Username", value: newStaff.Username))
            {
                return "Username sudah dipakai";
            }
            else
            {
                newStaff.Password = _securityRelateHelper.Encrypt(newStaff.Password);

                // Save
                string sqlQuery = @"INSERT INTO Staff(Nip, NamaLengkap, Email, NoHp, Jabatan, Username, Password) 
                                    VALUES(@Nip, @NamaLengkap, @Email, @NoHp, @Jabatan, @Username, @Password)";
                using (var connection = new SqlConnection(_connectionHelper.GetConnectionString()))
                {
                    connection.Open();
                    connection.Execute(sql: sqlQuery, param: newStaff);

                    return "Sukses";
                }
            }
        }
        public void UpdateStaff(Staff newData)
        {
            newData.Password = _securityRelateHelper.Encrypt(newData.Password);
            string sqlQuery = @"UPDATE Staff 
                SET NamaLengkap = @NamaLengkap, Jabatan = @Jabatan, NoHp = @NoHp, Email = @Email, Password = @Password 
                WHERE Id = @Id";
            using (var connection = new SqlConnection(_connectionHelper.GetConnectionString()))
            {
                connection.Open();
                connection.Query(sql: sqlQuery, param: newData);
            }
        }
        public Staff DetailStaff(int id)
        {
            string sqlQuery = @"SELECT * FROM Staff FULL JOIN Panitia 
                ON Staff.Id = Panitia.StaffId WHERE Staff.Id = @Id";
            using (var connection = new SqlConnection(_connectionHelper.GetConnectionString()))
            {
                connection.Open();

                // Get data from database and map it using dapper
                var result = connection.Query<Staff, Panitia, Staff>(
                   sql: sqlQuery,
                   map: (staff, panitia) =>
                   {
                       staff.Panitia = panitia;
                       return staff;
                   },
                   splitOn: "StaffId",
                   param: new { Id = id }).First();

                return result;
            }
        }
        public void DeleteStaff(int id)
        {
            string sqlQuery = @"DELETE FROM Staff WHERE Id = @Id";
            using (var connection = new SqlConnection(_connectionHelper.GetConnectionString()))
            {
                connection.Open();
                connection.Execute(sql: sqlQuery, param: new { Id = id });
            }
        }
        public void AddPanitiaToStaff(Panitia newPanitia)
        {
            string sqlQuery = @"INSERT INTO Panitia(StaffId, Acara, Divisi) 
                                VALUES(@StaffId, @Acara, @Divisi)";
            using (var connection = new SqlConnection(_connectionHelper.GetConnectionString()))
            {
                connection.Open();
                connection.Execute(sql: sqlQuery, param: newPanitia);
            }
        }
        public void DeletePanitiaFromStaff(int staffId)
        {
            string sqlQuery = @"DELETE FROM Panitia WHERE StaffId = @StaffId";
            using (var connection = new SqlConnection(_connectionHelper.GetConnectionString()))
            {
                connection.Open();
                connection.Execute(sql: sqlQuery, param: new { StaffId = staffId });
            }
        }
        public bool IsLogin(string username, string password, string role)
        {
            password = _securityRelateHelper.Encrypt(password);
            string result, sqlQuery;

            if (role.Contains("PSB"))
            {
                sqlQuery = @"SELECT s.Username FROM Staff s FULL JOIN Panitia p ON s.Id = p.StaffId 
                    WHERE p.Divisi=@Role AND s.Username=@username AND s.Password=@Password";
                role = role.Split(" ")[1];
            }
            else
            {
                sqlQuery = @"SELECT Username FROM Staff 
                    WHERE Jabatan=@Role AND Username=@username AND Password=@Password";
            }

            using (var connection = new SqlConnection(_connectionHelper.GetConnectionString()))
            {
                connection.Open();
                result = connection.QueryFirstOrDefault<string>(
                    sql: sqlQuery,
                    param: new
                    {
                        Username = username,
                        Password = password,
                        Role = role
                    });
            }

            return result != null;
        }
    }
}
