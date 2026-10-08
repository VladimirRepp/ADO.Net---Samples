using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

/*
 * Управление данными в DataSet
    Любую фильтрацию и сортировку локальных данных в DataSet 
    можно выполнить с помощью класса DataViewManager. Управлять 
    фильтрацией и сортировкой данных объект DataViewManager позволяет 
    с помощью своих свойств RowFilter и Sort. Рассмотрим, каким образом 
    это можно сделать
 */

namespace Sample_1
{
    public partial class Form2 : Form
    {
        private SqlDataAdapter _adpter;
        private DataSet _dataSet;
        private SqlCommandBuilder _commandBuilder;

        private string _tableName;

        public Form2()
        {
            InitializeComponent();
            DbMyConnection.Instance.Initialization();
            _tableName = "Books";
        }

        private void button_Fill_Click(object sender, EventArgs e)
        {
            try
            {
                DbMyConnection.Instance.Open();

                _dataSet = new DataSet();
                string query = textBoxCommand.Text;

                _adpter = new SqlDataAdapter(query, DbMyConnection.Instance.Connection);
                dataGridView.DataSource = null;

                _commandBuilder = new SqlCommandBuilder(_adpter);
                _adpter.Fill(_dataSet, _tableName);

                // Создаем класс DataViewManager для управления данными
                DataViewManager dvm = new DataViewManager(_dataSet);

                // Задаем условия отбора и сортировки для требуемой 
                // таблицы в DataSet
                dvm.DataViewSettings[_tableName].RowFilter = "id < 100";
                dvm.DataViewSettings[_tableName].Sort = "Title ASC";

                // Создаем объект содержащий отобранные 
                // и сортированные данные
                // и связываем этот объект с dataGridView
                DataView dataView1 =
                 dvm.CreateDataView(_dataSet.Tables[_tableName]);
                dataGridView.DataSource = dataView1;
            }
            catch (Exception ex)
            {
                throw new Exception($"Form2.button_Fill_Click: {ex.Message}");
            }
            finally
            {
                DbMyConnection.Instance.Close();
            }
        }

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void главноеМенюToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MainMenu form = new MainMenu();
            form.Show();
            this.Hide();
        }
    }
}
