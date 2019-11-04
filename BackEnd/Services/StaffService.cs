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
    public class StaffService : IStaff
    {
        private readonly IDbConnectionHelper _connectionHelper;
        public StaffService(IDbConnectionHelper connectionHelper)
            => _connectionHelper = connectionHelper;

        public void AddPanitiaToStaff(int idStaff, Panitia newPanitia)
        {
            string sqlQuery = @"INSERT INTO Panitia(StaffId, Acara, Divisi) 
                                VALUES(@StaffId, @Acara, @Divisi)";
            using (var connection = new SqlConnection(_connectionHelper.GetConnectionString()))
            {
                connection.Open();
                connection.Execute(sql: sqlQuery, param: new
                {
                    StaffId = idStaff,
                    Acara = newPanitia.Acara,
                    Divisi = newPanitia.Divisi
                });
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
                // Hashing
                //newStaff.Password = Hashing(newStaff.Password);

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
        protected bool IsExistInStaff(string column, string value)
        {
            string sqlQuery = $"SELECT {column} FROM Staff WHERE {column} = @value";
            using (var connection = new SqlConnection(_connectionHelper.GetConnectionString()))
            {
                connection.Open();
                string result = connection.QueryFirstOrDefault<string>(
                    sql: sqlQuery,
                    param: new { value = value });

                return result != null;
            }
        }
        protected string Hashing(string plainText)
        {
            throw new NotImplementedException();
        }
        public void DeletePanitiaFromStaff(int staffId)
        {
            string sqlQuery = @"DELETE FROM Panitia WHERE StaffId = @StaffId";
            using(var connection = new SqlConnection(_connectionHelper.GetConnectionString()))
            {
                connection.Open();
                connection.Execute(sql: sqlQuery, param: new { StaffId = staffId });
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
        public Staff DetailStaff(int id)
        {
            string sqlQuery = @"SELECT * FROM Staff FULL JOIN Panitia 
                                ON Staff.Id = Panitia.StaffId
                                WHERE Staff.Id = @Id";
            using (var connection = new SqlConnection(_connectionHelper.GetConnectionString()))
            {
                connection.Open();

                // Get data from database and map it using dapper
                var result = connection.Query<Staff, Panitia, Staff>(
                   sql: sqlQuery,
                   map: (staff, panitia) =>
                   {
                       staff.APanitia = panitia;

                       return staff;
                   },
                   splitOn: "StaffId",
                   param: new { Id = id }).FirstOrDefault();

                return result;
            }
        }
        public IEnumerable<Staff> GetAllStaff()
        {
            string sqlQuery = @"SELECT * FROM Staff FULL JOIN Panitia 
                                ON Staff.Id = Panitia.StaffId";
            using (var connection = new SqlConnection(_connectionHelper.GetConnectionString()))
            {
                connection.Open();

                // Get data from database and map them using dapper
                var result = connection.Query<Staff, Panitia, Staff>(
                    sql: sqlQuery,
                    map: (staff, panitia) =>
                    {
                        staff.APanitia = panitia;

                        return staff;
                    },
                    splitOn: "StaffId").Distinct().ToList();

                return result;
            }
        }
        public void UpdateStaff(Staff newData)
        {
            string sqlQuery = @"UPDATE Staff 
                                SET NamaLengkap = @NamaLengkap, Jabatan = @Jabatan, NoHp = @NoHp, Email = @Email, Password = @Password 
                                WHERE Id = @Id";
            using (var connection = new SqlConnection(_connectionHelper.GetConnectionString()))
            {
                connection.Open();
                connection.Query(sql: sqlQuery, param: newData);
            }
        }
    }
}
