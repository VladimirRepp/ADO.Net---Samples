using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;   

namespace WindowsFormsApp1.Database
{
    public class DbConnection
    {
        private SqlConnection _sqlConnection;

        public SqlConnection Connection => _sqlConnection;

        public DbConnection() {             
            CreateConnection(); 
        }

        private void CreateConnection()
        {
            try
            {
                _sqlConnection = new SqlConnection();
                _sqlConnection.ConnectionString = "Data Source=localhost\\SQLEXPRESS" +
                    ";Initial Catalog=ItTop;Integrated Security=True;Encrypt=False";
            }
            catch (Exception ex)
            {
                throw new Exception($"DbConnection.CreateConnection: {ex.Message}");
            }
        }

        public bool OpenConnection()
        {
            try
            {
                if (_sqlConnection == null)
                    return false;

                if (_sqlConnection.State != ConnectionState.Open)
                    _sqlConnection.Open();

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"DbConnection.OpenConnection: {ex.Message}");
            }
        }

        public bool CloseConnection()
        {
            try
            {
                if (_sqlConnection == null)
                    return false;

                if (_sqlConnection.State != ConnectionState.Closed)
                    _sqlConnection.Close();

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"DbConnection.CloseConnection: {ex.Message}");
            }
        }
    }
}
