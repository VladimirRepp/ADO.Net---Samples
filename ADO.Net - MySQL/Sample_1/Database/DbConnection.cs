using MySql.Data.MySqlClient;
using System.Data;

namespace ADO.Net___Sample_0.Database
{
    /// <summary>
    /// Подключение к БД
    /// </summary>
    public class DbConnection
    {
        private MySqlConnection _connection;
        private const string _connectionString = "" +
           "Server=localhost;" +
           "Port=3306;" +
           "Database=test_db;" +
           "username=root;" +
           "password=;";

        public MySqlConnection GetConnection => _connection;

        public DbConnection()
        {
            _connection = new(_connectionString);
        }

        public void OpenConnection()
        {
            if (_connection.State != ConnectionState.Open)
                _connection.Open();
        }

        public void CloseConnection()
        {
            if (_connection != null && _connection.State == ConnectionState.Open)
                _connection.Close();
        }
    }
}
