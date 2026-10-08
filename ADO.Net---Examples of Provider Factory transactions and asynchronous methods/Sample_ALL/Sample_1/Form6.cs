using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Configuration; // подключаем через ссылки

namespace Sample_1
{
    public partial class Form6 : Form
    {
        private DbConnection _connection = null;
        private DbProviderFactory _factory = null;
        private string _connectionString = "";

        public Form6()
        {
            InitializeComponent();
        }

        private void главноеМенюToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MainMenu form = new MainMenu();
            form.Show();
            this.Hide();
        }

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        /// <summary>
        /// При загрузке окна выбираем фабрику для 
        /// поставщика System.Data.SqlClient вызываем
        /// метод для получения строки подключения 
        /// из конфигурациооного файла
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form6_Load(object sender, EventArgs e)
        {
            _factory = DbProviderFactories.GetFactory("System.Data.SqlClient");
            _connection = _factory.CreateConnection();
            _connectionString = GetConnectionStringByProvider("System.Data.SqlClient");

            if (_connectionString == "")
            {
                MessageBox.Show("В конфигурационном файле нет требуемой строки подключения!");
            }
        }

        // Семантика метода async Task Method()
        private async Task LoadDataAsync()
        {
            try
            {
                _connection.ConnectionString = _connectionString;
                await _connection.OpenAsync();

                DbCommand comm = _connection.CreateCommand();
                comm.CommandText = "WAITFOR DELAY '00:00:05';";
                comm.CommandText += textBox_comand.Text.ToString();

                DataTable table = new DataTable();
                using (DbDataReader reader = await comm.ExecuteReaderAsync())
                {
                    int line = 0;
                    do
                    {
                        while (await reader.ReadAsync())
                        {
                            if (line == 0)
                            {
                                for (int i = 0; i < reader.FieldCount; i++)
                                {
                                    table.Columns.Add(reader.
                                   GetName(i));
                                }
                                line++;
                            }

                            DataRow row = table.NewRow();
                            for (int i = 0; i < reader.FieldCount; i++)
                            {
                                row[i] = await reader.
                               GetFieldValueAsync<Object>(i);
                            }
                            table.Rows.Add(row);
                        }
                    } while (reader.NextResult());
                }

                // Выводим результаты запроса
                dataGridView.DataSource = null;
                dataGridView.DataSource = table;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally { 
                _connection.Close(); 
            }
        }

        // async - не забываем
        private async void button_Async1_Click(object sender, EventArgs e)
        {
            await LoadDataAsync();
        }

        /// <summary>
        /// Этот метод по имени поставщика данных
        /// считывает из конфигурационного файла
        /// и возвращает строку подключения, если 
        /// эта строка есть в конфигурационном файле
        /// </summary>
        /// <param name="providerName"></param>
        /// <returns></returns>
        static string GetConnectionStringByProvider(string providerName)
        {
            string returnValue = null;

            // Читаем все строки подключения из App.config
            ConnectionStringSettingsCollection
            settings = ConfigurationManager.
            ConnectionStrings;

            // Ищем и возвращаем строку подключения 
            // для providerName
            if (settings != null)
            {
                foreach (ConnectionStringSettings cs
                in settings)
                {
                    if (cs.ProviderName == providerName)
                    {
                        returnValue = cs.ConnectionString;
                        break;
                    }
                }
            }

            return returnValue;
        }
    }
}
