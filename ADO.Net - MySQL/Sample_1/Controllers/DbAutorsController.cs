using ADO.Net___Sample_0.Database;
using MySql.Data.MySqlClient; // подключить через NuGet -> MySQL.Data
using System.Data;

namespace ADO.Net___Sample_0.Controllers
{
    /// <summary>
    /// Полное управление над авторами
    /// </summary>
    public class DbAutorsController
    {
        // List<Author> _authors;
        private DbConnection _connection;

        public DbAutorsController()
        {
            _connection = new();
        }

        public DataTable Loading()
        {
            DataTable table = null;

            try
            {
                _connection.OpenConnection();

                string request = "SELECT * FROM `authors`";
                var adapter = new MySqlDataAdapter(request, _connection.GetConnection);
                table = new();

                adapter.Fill(table);

                //dataGridView.DataSource = table;
                //dataGridView.Columns["Id"].Visible = false;
            }
            catch (Exception ex)
            {
                //MessageBox.Show("Ошибка: " + ex.Message, "Внимание!");
                // + работа логера 
                throw new Exception(ex.Message);
                //throw ex;
            }
            finally
            {
                _connection.CloseConnection();
            }

            return table;
        }

        public bool TryInsert(string firstName, string lastName)
        {
            if(string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName))
            {
                // Logger.Log("DbAutorsController.Insert: Не все поля введены!");
                Exception ex = new("Не все поля введены!");
                throw ex;
            }

            bool isDone = false;

            try
            {
                _connection.OpenConnection();

                string sql = $"INSERT INTO Authors (firstName, lastName) VALUES " +
                    $"(@fn, @ln)";

                using (var cmd = new MySqlCommand(sql, _connection.GetConnection))
                {
                    // Замена параметров на значение 
                    cmd.Parameters.AddWithValue("@fn", firstName);
                    cmd.Parameters.AddWithValue("@ln", lastName);

                    int rows = cmd.ExecuteNonQuery();

                    if(rows == 1)
                        isDone = true;

                    //MessageBox.Show($"Строк добавлено: {rows}");
                }

                //textBox_FirstName.Clear();
                //textBox_LastName.Clear();
                //button_Load_Click(sender, e);
            }
            catch (Exception ex)
            {
                //MessageBox.Show("Ошибка: " + ex.Message);
                throw new Exception(ex.Message);
            }
            finally
            {
                _connection.CloseConnection();
            }

            return isDone;
        }

        public bool TryUpdate(int id, string firstName, string lastName)
        {
            if (string.IsNullOrEmpty(firstName) || 
                string.IsNullOrEmpty(lastName) || 
                id == -1)
            {
                // Logger.Log("DbAutorsController.TryUpdate: Не все поля введены!");
                Exception ex = new("Не все поля введены!");
                throw ex;
            }

            bool isDone = false;

            try
            {
                _connection.OpenConnection();

                const string sql =
                    "UPDATE Authors SET firstName = @fn, lastName = @ln WHERE id = @id";

                using (var cmd = new MySqlCommand(sql, _connection.GetConnection))
                {
                    cmd.Parameters.AddWithValue("@fn", firstName);
                    cmd.Parameters.AddWithValue("@ln", lastName);
                    cmd.Parameters.AddWithValue("@id", id);

                    int rows = cmd.ExecuteNonQuery();

                    if(rows == 1)
                        isDone = true;

                    //MessageBox.Show($"Строк обновлено: {rows}");
                }

                //button_Load_Click(sender, e);
            }
            catch (Exception ex)
            {
                //MessageBox.Show("Ошибка: " + ex.Message);
                throw new Exception (ex.Message);
            }
            finally
            {
                _connection.CloseConnection();
            }

            return isDone;
        }

        public bool TryDelete(int id) 
        {
            if (id == -1)
            {
                // Logger.Log("DbAutorsController.TryDelete: Не все поля введены!");
                Exception ex = new("Не все поля введены!");
                throw ex;
            }

            bool isDone = false;

            try
            {
                _connection.OpenConnection();

                const string sql = "DELETE FROM Authors WHERE id = @id";

                using (var cmd = new MySqlCommand(sql, _connection.GetConnection))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    int rows = cmd.ExecuteNonQuery();

                    if(rows == 1)
                        isDone = true;

                    //MessageBox.Show($"Строк удалено: {rows}");
                }

                //button_Load_Click(sender, e);
            }
            catch (Exception ex)
            {
                //MessageBox.Show("Ошибка: " + ex.Message);
                throw new Exception(ex.Message);
            }
            finally
            {
                _connection.CloseConnection();
            }

            return isDone;
        }
    }
}
