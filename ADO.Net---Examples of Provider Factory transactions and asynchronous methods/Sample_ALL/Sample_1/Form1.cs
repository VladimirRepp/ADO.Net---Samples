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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Configuration; // добавить через ССЫДКИ проекта

namespace Sample_1
{
    public partial class Form1 : Form
    {
        private DbConnection _dbConnection = null;
        private DbProviderFactory _dbProviderFactory = null;
        private string _providerName = "";

        public Form1()
        {
            InitializeComponent();
            button_Request.Enabled = false;
        }

        private string GetConnectionStringByProvider(string providerName)
        {
            string returnValue = null;

            // читаем все строки подключения из App.config
            ConnectionStringSettingsCollection settings = ConfigurationManager.ConnectionStrings;

            // ищем и возвращаем строку подключения 
            // для providerName
            if (settings != null)
            {
                foreach (ConnectionStringSettings cs in settings)
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

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button_GetAllProviders_Click(object sender, EventArgs e)
        {
            DataTable t = DbProviderFactories.GetFactoryClasses();
            dataGridView.DataSource = t;
            comboBox_Providers.Items.Clear();

            foreach (DataRow dr in t.Rows)
            {
                comboBox_Providers.Items.Add(dr["InvariantName"]);
            }
        }

        private void comboBox_Providers_SelectedIndexChanged(object sender, EventArgs e)
        {
            _dbProviderFactory = DbProviderFactories.GetFactory(comboBox_Providers.SelectedItem.ToString());
            _dbConnection = _dbProviderFactory.CreateConnection();
            _providerName = GetConnectionStringByProvider(comboBox_Providers.SelectedItem.ToString());
            textBox_ConnectionString.Text = _providerName;
        }

        private void button_Request_Click(object sender, EventArgs e)
        {
            try
            {
                _dbConnection.ConnectionString = textBox_ConnectionString.Text;

                // создаем адаптер из фабрики
                DbDataAdapter adapter = _dbProviderFactory.CreateDataAdapter();
                adapter.SelectCommand = _dbConnection.CreateCommand();
                adapter.SelectCommand.CommandText = textBox_Request.Text;

                // выполняем запрос select из адаптера
                DataTable table = new DataTable();
                adapter.Fill(table);

                // выводим результаты запроса
                dataGridView.DataSource = null;
                dataGridView.DataSource = table;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }            
        }

        private void textBox_Request_TextChanged(object sender, EventArgs e)
        {
            if (textBox_Request.Text.Length > 5)
                button_Request.Enabled = true;
            else
                button_Request.Enabled = false;
        }

        private void главноеМенюToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MainMenu form = new MainMenu();
            form.Show();
            this.Hide();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
