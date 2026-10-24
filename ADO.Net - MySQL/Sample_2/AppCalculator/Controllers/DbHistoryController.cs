using AppCalculator.Database;
using MySql.Data.MySqlClient;

namespace AppCalculator.Controllers
{
    public class DbHistoryController
    {
        private readonly DbConnector _dbConncetor;

        public DbHistoryController(DbConnector dbConnector)
        {
            _dbConncetor = dbConnector;
        }

        public string LoadingHistoryCalculator(string tableName)
        {
            string history = "";

            try
            {
                _dbConncetor.OpenConnection();

                string request = $"SELECT * FROM `{tableName}`";

                using (var cmd = new MySqlCommand(request, _dbConncetor.GetConnection()))
                {
                    var reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            history += reader["history"].ToString() + "\r\n";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                _dbConncetor.CloseConnection();
            }

            return history;
        }

        public void InsertHistoryCalulator(string tableName, string history)
        {
            try
            {
                _dbConncetor.OpenConnection();

                string sql = $"INSERT INTO {tableName} (history) VALUES (@history)";

                using (var cmd = new MySqlCommand(sql, _dbConncetor.GetConnection()))
                {
                    cmd.Parameters.AddWithValue("@history", history);
                    int rows = cmd.ExecuteNonQuery();

                    if (rows != 1)
                    {
                        MessageBox.Show("Проблема при добавление истории!",
                            "Ошибка!");
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                _dbConncetor.CloseConnection();
            }
        }
    }
}
