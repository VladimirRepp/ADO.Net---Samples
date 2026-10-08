using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

/*
 * Отсоеденненый режим
 * Табличные данные DataTable
 * Загруженные данные можно сохранить в DataTable, 
 * как мы делали со списком (List<T>)
 */

namespace Sample_2
{
    public partial class Form1 : Form
    {
        private SqlDataReader _sqlDataReader;   // объект, для получения данных
        private DataTable _dataTable;           // локальное хранилище табличных данных

        public Form1()
        {
            InitializeComponent();
            DbMyConnector.Instance.Startup();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void button_Exec_Click(object sender, EventArgs e)
        {

            try
            {
                DbMyConnector.Instance.TryOpenConnection();

                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.CommandText = textBoxCommand.Text;
                sqlCommand.Connection = DbMyConnector.Instance.Connection;

                dataGridView.DataSource = null;

                _dataTable = new DataTable();
                _sqlDataReader = sqlCommand.ExecuteReader();
                bool isCreateHead = true;

                do
                {
                    while (_sqlDataReader.Read())
                    {
                        // Создаем заголовок таблицы
                        if (isCreateHead)
                        {
                            for (int i = 0; i < _sqlDataReader.FieldCount; i++)
                            {
                                _dataTable.Columns.Add(_sqlDataReader.GetName(i));
                            }

                            isCreateHead = false;
                        }

                        // Инициализируем строки данных
                        DataRow row = _dataTable.NewRow();
                        for (int i = 0; i < _sqlDataReader.FieldCount; i++)
                        {
                            row[i] = _sqlDataReader[i];
                        }
                        _dataTable.Rows.Add(row);
                    }
                }
                while (_sqlDataReader.NextResult());

                dataGridView.DataSource = _dataTable;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                DbMyConnector.Instance.TryCloseConnection();
            }
        }

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit(); 
        }

        private void form2ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();
            form2.Show();
            this.Hide();
        }

        private void form3ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form3 form3 = new Form3();  
            form3.Show();
            this.Hide();
        }
    }
}
