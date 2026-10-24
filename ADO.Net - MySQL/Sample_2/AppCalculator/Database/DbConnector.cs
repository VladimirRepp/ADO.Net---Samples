using System.Data;
using MySql.Data.MySqlClient; // добавлен пакет MySQL.Data через менеджера пакетов

namespace AppCalculator.Database
{
    public class DbConnector
    {
        private MySqlConnection _connection;
        // Так строку подключения лучше не хранить 
        // Лучше убрать ее в окружение
        private string _connectionString = "" +
           "Server=localhost;" +
           "Port=3306;" +
           "Database=test_db;" +
           "username=root;" +
           "password=;";

        public DbConnector(string connectionString = null)
        {
            if (connectionString != null)
                _connectionString = connectionString;

            _connection = new MySqlConnection(_connectionString);
        }

        public MySqlConnection GetConnection() => _connection;

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
