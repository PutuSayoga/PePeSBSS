using BackEnd.Abstraction;

namespace BackEnd.Helper
{
    public class DbConnectionHelper : IDbConnectionHelper
    {
        private readonly string _connectionString;

        public DbConnectionHelper(string connectionString)
            => _connectionString = connectionString;

        public string GetConnectionString() => _connectionString;
    }
}
