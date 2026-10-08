using System.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace Sample_0.Database
{
    public class DbConnector
    {
        private SqlConnection _connection;

        public SqlConnection Connection => _connection;

        public DbConnector()
        {
            CreateConnection();
        }

        private void CreateConnection()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["MyConnString"].ConnectionString;
            _connection = new SqlConnection(connectionString);

            _connection.Open();
        }

        public bool OpenConnection()
        {
            if (_connection.State != ConnectionState.Open)
                _connection.Open();

            return _connection.State == ConnectionState.Open;
        }

        public bool CloseConnection()
        {
            if (_connection.State != ConnectionState.Closed)
                _connection.Close();

            return _connection.State == ConnectionState.Closed;
        }
    }
}
