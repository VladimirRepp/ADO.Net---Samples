using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sample_1
{
    /// <summary>
    /// WaitHandle
    /// * WaitOne() блокирующий и наше приложение ждет завершения основного потока
    /// * Главное преимущество - WaitHandle следит за завершением нескольких дополнительных потоков 
    /// * Есть статичные методы WaitAll(), WaitAny()
    /// </summary>

    public partial class Form5 : Form
    {
        public Form5()
        {
            InitializeComponent();
        }

        private void button_Async_Click(object sender, EventArgs e)
        {
            const string AsyncEnabled = "Asynchronous Processing=true";

            // Есть ассинхронный режим?
            if (!DbMyConnection.Instance.ConnеctionString.Contains(AsyncEnabled))
            {
                DbMyConnection.Instance.ConnеctionString = String.Format("{0}; {1}", 
                    DbMyConnection.Instance.ConnеctionString,
                    AsyncEnabled);
            }

            DbMyConnection.Instance.Initialization(DbMyConnection.Instance.ConnеctionString);

            SqlCommand comm = DbMyConnection.Instance.Connection.CreateCommand();
            comm.CommandText = "WAITFOR DELAY '00:00:05'; SELECT* FROM Books; ";
            comm.CommandType = CommandType.Text;
            comm.CommandTimeout = 30;

            try
            {
                DbMyConnection.Instance.Open();

                /// Блок 1
                IAsyncResult iar = comm.BeginExecuteReader();
                ///

                /// Блок 2
                /// Получаем обработчик 
                WaitHandle handle = iar.AsyncWaitHandle;
                /// 

                /// Блок 3
                /// Проверяем, завершилась ли работа в доп. потоке
                if (handle.WaitOne(10000))
                {
                    /// Блок 4
                    GetData(comm, iar);
                    ///
                }
                else
                {
                    MessageBox.Show("Превышен тайм-аут!");
                }
                ///
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void GetData(SqlCommand command, IAsyncResult ia)
        {
            SqlDataReader reader = null;
            try
            {
                reader = command.EndExecuteReader(ia);
                DataTable table = new DataTable();
                dataGridView.DataSource = null;

                int line = 0;
                do
                {
                    while (reader.Read())
                    {
                        if (line == 0)
                        {
                            for (int i = 0; i < reader.FieldCount;
                            i++)
                            {
                                table.Columns.Add(reader.GetName(i));
                            }
                            line++;
                        }
                        DataRow row = table.NewRow();
                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            row[i] = reader[i];
                        }
                        table.Rows.Add(row);
                    }
                } while (reader.NextResult());

                dataGridView.DataSource = table;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"From GetData:{ex.Message}");

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
                catch
                {
                }
            }
        }

        /// <summary>
        /// Оппрос дополнительного потока - холостой ход
        /// </summary>
        private void PollingAnAdditionalStream(SqlCommand comm)
        {
            IAsyncResult iar = comm.BeginExecuteReader();
            while (!iar.IsCompleted)
            {
                Console.WriteLine("Waiting...");
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
