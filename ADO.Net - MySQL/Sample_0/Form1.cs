using System.Data;
using MySql.Data.MySqlClient; // подключить через NuGet -> MySQL.Data

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

        private void OpenConnection()
        {
            if (_connection == null)
                _connection = new MySqlConnection(_connectionString);

            if (_connection.State != ConnectionState.Open)
                _connection.Open();
        }

        private void CloseConnection()
        {
            if (_connection != null && _connection.State == ConnectionState.Open)
                _connection.Close();
        }

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
                MessageBox.Show("Ошибка: " + ex.Message, "Внимание!");
            }
            finally
            {
                CloseConnection();
            }
        }

        private void button_Insert_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox_FirstName.Text) ||
                string.IsNullOrWhiteSpace(textBox_LastName.Text))
            {
                MessageBox.Show("Не все полян введены!");
                return;
            }

            try
            {
                OpenConnection();

                const string sql =
                    $"INSERT INTO Authors (firstName, lastName) VALUES ('{textBox_FirstName.Text}', '{textBox_LastName.Text}')";
                    // Такой запрос не безопастный 
                    // Попробуйте в textBox_FirstName.Text ввести: 
                    // '), (NULL, 'x', 'x'); DROP TABLE Authors; -- 
                    // Что произошло?
                    
                    // Пример правильной строки запроса (с параметрами/заглушками)
                    // $"INSERT INTO Authors (firstName, lastName) VALUES ({textBox_FirstName.Text}, {textBox_LastName.Text})";

                using (var cmd = new MySqlCommand(sql, _connection))
                {
                    // Замена параметров на значение 
                    //cmd.Parameters.AddWithValue("@fn", textBox_FirstName.Text);
                    //cmd.Parameters.AddWithValue("@ln", textBox_LastName.Text);
                    // А что будет сейчас, если ввести: 
                    // ), (NULL, 'x', 'x'); DROP TABLE Authors; -- 
                    // в поле textBox_FirstName.Text

                    int rows = cmd.ExecuteNonQuery();
                    // Чтобы выполнить команду, необходимо применить один из методов SqlCommand:
                    // - ExecuteNonQuery()/ExecuteNonQueryAsync(): просто выполняет sql-выражение и в
                    // озвращает количество измененных записей. Подходит для sql-выражений INSERT, UPDATE, DELETE, CREATE.
                    // - ExecuteReader()/ExecuteReaderAsync(): выполняет sql-выражение и возвращает строки из таблицы. 
                    // Подходит для sql-выражения SELECT.
                    // - ExecuteScalar()/ExecuteScalarAsync(): выполняет sql-выражение и возвращает одно скалярное значение, 
                    // например, число. Подходит для sql-выражения SELECT в паре с одной из встроенных функций SQL, как например, Min, Max, Sum, Count.
                    
                    MessageBox.Show($"Строк добавлено: {rows}");
                }

                textBox_FirstName.Clear();
                textBox_LastName.Clear();
                button_Load_Click(sender, e); 
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка: " + ex.Message);
            }
            finally
            {
                CloseConnection();
            }
        }

        private void button_Update_Click(object sender, EventArgs e)
        {
            if (dataGridView.CurrentRow == null)
            {
                MessageBox.Show("Выберите строку данных!");
                return;
            }

            if (string.IsNullOrWhiteSpace(textBox_FirstName.Text) ||
                string.IsNullOrWhiteSpace(textBox_LastName.Text))
            {
                MessageBox.Show("Не все данные введены!");
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
                    MessageBox.Show($"Строк обновлено: {rows}");
                }

                button_Load_Click(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка: " + ex.Message);
            }
            finally
            {
                CloseConnection();
            }
        }

        private void button_Delete_Click(object sender, EventArgs e)
        {
            if (dataGridView.CurrentRow == null)
            {
                MessageBox.Show("Выберите даныне.");
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
                    MessageBox.Show($"Строк удалено: {rows}");
                }

                button_Load_Click(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка: " + ex.Message);
            }
            finally
            {
                CloseConnection();
            }
        }
    }
}
