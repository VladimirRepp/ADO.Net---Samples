using System.Data;
using MySql.Data.MySqlClient; // подключили через NuGet -> MySQL.Data

namespace ADO.Net___Sample_0
{
    public partial class Form1 : Form
    {
        private const string _connectionString = "" +
            "Server=localhost;" +
            "Port=3306;" +
            "Database=test_db;" +
            "username=root;" +
            "password=;";

        private MySqlConnection _connection;

        public Form1()
        {
            InitializeComponent();
        }

        // Работа с БД
        // ============ ПОДКЛЮЧЕНИЕ К БД ============
        private void OpenConnection()
        {
            if (_connection == null)
                _connection = new MySqlConnection(_connectionString);

            if (_connection.State != ConnectionState.Open)
                _connection.Open();
        }

        // ============ ОТКЛЮЧЕНИЕ ОТ БД ============
        private void CloseConnection()
        {
            if (_connection != null && _connection.State == ConnectionState.Open)
                _connection.Close();
        }

        // ============ ВЫБОРКА ВСЕХ ДАННЫХ ============
        private void button_Load_Click(object sender, EventArgs e)
        {
            try
            {
                OpenConnection();

                string request = "SELECT * FROM `authors`";
                var adapter = new MySqlDataAdapter(request, _connection);
                var table = new DataTable();

                adapter.Fill(table);

                dataGridView.DataSource = table;
                dataGridView.Columns["Id"].Visible = false;
            }
            catch(Exception ex)
            {
                MessageBox.Show("Сообщение: " + ex.Message, "Ошибка!");
            }
            finally
            {
                CloseConnection();
            }
        }

        // ============ ВСТАВКА ============
        private void button_Insert_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox_FirstName.Text) ||
                string.IsNullOrWhiteSpace(textBox_LastName.Text))
            {
                MessageBox.Show("Заполните имя и фамилию.");
                return;
            }

            try
            {
                OpenConnection();

                const string sql =
                    "INSERT INTO Authors (firstName, lastName) VALUES (@fn, @ln)";

                using (var cmd = new MySqlCommand(sql, _connection))
                {
                    cmd.Parameters.AddWithValue("@fn", textBox_FirstName.Text);
                    cmd.Parameters.AddWithValue("@ln", textBox_LastName.Text);

                    int rows = cmd.ExecuteNonQuery();
                    MessageBox.Show($"Добавлено строк: {rows}");
                }

                textBox_FirstName.Clear();
                textBox_LastName.Clear();
                button_Load_Click(sender, e); // обновить таблицу
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка вставки: " + ex.Message);
            }
            finally
            {
                CloseConnection();
            }
        }

        // ============ ОБНОВЛЕНИЕ ============
        private void button_Update_Click(object sender, EventArgs e)
        {
            if (dataGridView.CurrentRow == null)
            {
                MessageBox.Show("Выберите строку для обновления.");
                return;
            }

            if (string.IsNullOrWhiteSpace(textBox_FirstName.Text) ||
                string.IsNullOrWhiteSpace(textBox_LastName.Text))
            {
                MessageBox.Show("Заполните имя и фамилию.");
                return;
            }

            int id = Convert.ToInt32(dataGridView.CurrentRow.Cells["id"].Value);

            try
            {
                OpenConnection();

                const string sql =
                    "UPDATE Authors SET firstName = @fn, lastName = @ln WHERE id = @id";

                using (var cmd = new MySqlCommand(sql, _connection))
                {
                    cmd.Parameters.AddWithValue("@fn", textBox_FirstName.Text);
                    cmd.Parameters.AddWithValue("@ln", textBox_LastName.Text);
                    cmd.Parameters.AddWithValue("@id", id);

                    int rows = cmd.ExecuteNonQuery();
                    MessageBox.Show($"Обновлено строк: {rows}");
                }

                button_Load_Click(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка обновления: " + ex.Message);
            }
            finally
            {
                CloseConnection();
            }
        }

        // ============ УДАЛЕНИЕ ============
        private void button_Delete_Click(object sender, EventArgs e)
        {
            if (dataGridView.CurrentRow == null)
            {
                MessageBox.Show("Выберите строку для обновления.");
                return;
            }

            int id = Convert.ToInt32(dataGridView.CurrentRow.Cells["id"].Value);

            try
            {
                OpenConnection();

                const string sql = "DELETE FROM Authors WHERE id = @id";

                using (var cmd = new MySqlCommand(sql, _connection))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    int rows = cmd.ExecuteNonQuery();
                    MessageBox.Show($"Удалено строк: {rows}");
                }

                button_Load_Click(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка удаления: " + ex.Message);
            }
            finally
            {
                CloseConnection();
            }
        }
    }
}
