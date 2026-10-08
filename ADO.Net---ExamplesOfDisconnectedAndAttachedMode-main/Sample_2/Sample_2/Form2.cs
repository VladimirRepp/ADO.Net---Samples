using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

/*
 * Отсоедененный режим
 * Локальное хранение данных - DataSet
 * Подключение к БД + управление данных - DbDataAdapter
 * 
 * Основные свойтсва типа DbCommand у DbDataAdapter
 * 1. SelectCommand
 * 2. InsertCommand
 * 3. UpdateCommand
 * 4. DeleteCommand
 * ----------------
 * 5. Fill()    - заполнение
 * 6. Update()  - обновление сервера => можно добавить новые данные в таблицу,
 * вызвать данный метод, и данные подгрузятся локально за счет того, что локально
 * данные храняться в DataSet и когда нужно Adapter обновит сервер относительно 
 * актуальных данных в DataSet
 */

namespace Sample_2
{
    public partial class Form2 : Form
    {
        private SqlDataAdapter _adapter;            // объект, класс которого управляет переносом данных в БД и обратно  
        private DataSet _dataSet;                   // локальное хранилище данных
        private SqlCommandBuilder _commandBuilder;  // создает запросы 

        private string _tableName;

        public Form2()
        {
            InitializeComponent();
            DbMyConnector.Instance.Startup();
            _tableName = "Authors";
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void button_Fill_Click(object sender, EventArgs e)
        {
            try
            {
                DbMyConnector.Instance.TryOpenConnection();

                _dataSet = new DataSet();
                string query = textBoxCommand.Text;

                _adapter = new SqlDataAdapter(query, DbMyConnector.Instance.Connection);
                dataGridView.DataSource = null;

                _commandBuilder = new SqlCommandBuilder(_adapter);
                _adapter.Fill(_dataSet, _tableName); // заносит результат выполнения команды выборки (чтения) в _dataSet из БД
                 
                dataGridView.DataSource = _dataSet.Tables[_tableName];
            }
            catch (Exception ex)
            {
                throw new Exception($"Form2.button_Fill_Click: {ex.Message}");
            }
            finally
            {
                DbMyConnector.Instance.TryCloseConnection();
            }
        }

        private void button_Update_Click(object sender, EventArgs e)
        {
            try
            {
                if(_adapter != null)
                    _adapter.Update(_dataSet, _tableName); // обновляет данные в БД относительно _dataSet 
            }
            catch( Exception ex)
            {
                throw new Exception($"Form2.button_Update_Click: {ex.Message}");
            }
        }

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void form1ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();
            form1.Show();
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
