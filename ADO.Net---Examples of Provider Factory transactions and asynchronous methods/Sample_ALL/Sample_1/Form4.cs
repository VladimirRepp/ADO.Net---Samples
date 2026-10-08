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

namespace Sample_1
{
    /// <summary>
    /// Использование callback методов
    /// BeginAction(), EndAction() - начало, окончание работы с дополнительными потоками
    /// </summary>
    public partial class Form4 : Form
    {
        private DataTable _dataTable;

        public Form4()
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

        private void button_Async_Click(object sender, EventArgs e)
        {
            /// Блок 1
            /// Модифицируем строку подключения
            const string AsyncEnabled = "Asynchronous Processing=true";

            // Есть ассинхронный режим?
            if (!DbMyConnection.Instance.ConnеctionString.Contains(AsyncEnabled))
            {
                DbMyConnection.Instance.ConnеctionString = String.Format("{0}; {1}",
                    DbMyConnection.Instance.ConnеctionString,
                    AsyncEnabled);
            }
            ///

            DbMyConnection.Instance.Initialization(DbMyConnection.Instance.ConnеctionString);
            SqlCommand comm = DbMyConnection.Instance.Connection.CreateCommand();

            /// Блок 2
            /// Эмитация длительного запроса к БД
            comm.CommandText = "WAITFOR DELAY '00:00:05'; SELECT* FROM Books; ";
            comm.CommandType = CommandType.Text;
            comm.CommandTimeout = 30;
            ///

            try
            {
                DbMyConnection.Instance.Open();

                /// Блок 3
                /// Создаем делаг
                /// Имя делегата может быть любым, но сигнатура: void Method(IAsyncResult)
                /// По завершению потока делегат вызовется автоматически 
                AsyncCallback callback = new AsyncCallback(GetDataCallback);

                // Создаем нвыой поток и передаем ассинхронный делегат
                comm.BeginExecuteReader(callback, comm);
                MessageBox.Show("Added thread is working...");
                ///
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }



        private void GetDataCallback(IAsyncResult result)
        {
            SqlDataReader reader = null;
            try
            {
                /// Блок 1
                SqlCommand command = (SqlCommand)result.AsyncState;
                ///

                /// Блок 2
                /// End нужно вызвать от того же объекта который вызвал Begin
                reader = command.EndExecuteReader(result);
                ///
                
                _dataTable = new DataTable();
                bool isCreateHead = true;

                do
                {
                    while (reader.Read())
                    {
                        if (isCreateHead)
                        {
                            for (int i = 0; i < reader.FieldCount; i++)
                            {
                                _dataTable.Columns.Add(reader.GetName(i));
                            }

                            isCreateHead = false;
                        }

                        DataRow row = _dataTable.NewRow();
                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            row[i] = reader[i];
                        }
                        _dataTable.Rows.Add(row);
                    }
                }
                while (reader.NextResult());

                // Из доп. потока не можем обратиться к dataGridView
                // По этому обращаемся к нему через внешний метод
                DgvAction();
            }
            catch (Exception ex)
            {
                MessageBox.Show("From Callback 1:" + ex.Message);
            }
            finally
            {
                try
                {
                    if (!reader.IsClosed)
                    {
                        reader.Close();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("From Callback 2:" +
                    ex.Message);
                }
            }
        }

        private void DgvAction()
        {
            if (dataGridView.InvokeRequired)
            {
                dataGridView.Invoke(new Action(DgvAction));
                return;
            }

            dataGridView.DataSource = _dataTable;
        }

        private void Form4_Load(object sender, EventArgs e)
        {

        }
    }
}
