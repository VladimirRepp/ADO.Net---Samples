using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;

namespace Sample_2
{
    public partial class Form3 : Form
    {
        private SqlDataAdapter _adapter;            // управление данными 
        private DataSet _dataSet;                   // локальное хранилище
        private SqlCommandBuilder _commandBuilder;

        private string _fileName;                   // название файла
        private string _tableName;                  // название таблицы

        public Form3()
        {
            InitializeComponent();

            DbMyConnector.Instance.Startup();
            _tableName = "Pictures";
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

        private void form2ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();
            form2.Show();
            this.Hide();
        }

        private void загрузитьКартинкуToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Графические файлы |*.bmp; *.jpg; *.gif; *.png";
            openFileDialog.FileName = "";

            if (openFileDialog.ShowDialog() != DialogResult.OK)
                return;

            _fileName = openFileDialog.FileName;
            InsertPicture();
        }

        private void выводИнформацииОКартинкеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int index = dataGridView.CurrentRow.Index;
            if(index == -1)
            {
                MessageBox.Show("Выберите строку!", "Внимание!");
                return;
            }

            try
            {
                int id = -1;
                if (!int.TryParse(dataGridView.Rows[index].Cells[0].Value.ToString(), out id))
                {
                    MessageBox.Show("Неверный формат строки для поля Id(Int)!", "Внимание!");
                    return;
                }

                DbMyConnector.Instance.TryOpenConnection();

                string query = $"select * from {_tableName} where id = @id";
                _adapter = new SqlDataAdapter(query, DbMyConnector.Instance.Connection);
                _dataSet = new DataSet();
                _commandBuilder = new SqlCommandBuilder(_adapter);

                _adapter.SelectCommand.Parameters.AddWithValue("@id", id);

                _adapter.Fill(_dataSet);

                byte[] buffer = (byte[])_dataSet.Tables[0].Rows[0]["Picture"];
                MemoryStream ms = new MemoryStream(buffer);
                pictureBox_Picture.Image = Image.FromStream(ms);
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

        private void выводИнформацииВсехКартинокToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                DbMyConnector.Instance.TryOpenConnection();

                _dataSet = new DataSet();
                string query = $"select * from {_tableName}";

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

        private void InsertPicture()
        {
            if (textBox_BookId.Text == "")
            {
                MessageBox.Show("Введите код книги!");
                return;
            }

            try
            {
                byte[] bytes = CreateCopy();

                DbMyConnector.Instance.TryOpenConnection();

                SqlCommand comm = new SqlCommand(
                    "insert into Pictures(bookid, name, picture) values (@bookid, @name, @picture); ", 
                    DbMyConnector.Instance.Connection
                    );

                int idBook = Convert.ToInt32(textBox_BookId.Text);
                string[] fNames = _fileName.Split('\\');
                string fName = fNames[fNames.Length - 1];

                comm.Parameters.Add("@bookid", SqlDbType.Int).Value = idBook;
                comm.Parameters.Add("@name", SqlDbType.NVarChar, 255).Value = fName;
                comm.Parameters.Add("@picture", SqlDbType.Image, bytes.Length).Value = bytes;
                comm.ExecuteNonQuery();
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

        private byte[] CreateCopy()
        {
            Image img = Image.FromFile(_fileName);
            int maxWidth = 300, maxHeight = 300;

            // Размеры выбраны произвольно
            double ratioX = (double)maxWidth / img.Width;
            double ratioY = (double)maxHeight / img.Height;
            double ratio = Math.Min(ratioX, ratioY);
            
            int newWidth = (int)(img.Width * ratio);
            int newHeight = (int)(img.Height * ratio);
            
            img = new Bitmap(newWidth, newHeight);

            // Рисунок в памяти
            Graphics g = Graphics.FromImage(img);
            g.DrawImage(img, 0, 0, newWidth, newHeight);
            MemoryStream ms = new MemoryStream();
            
            // поток для ввода|вывода байт из памяти
            img.Save(ms, ImageFormat.Jpeg);
            ms.Flush();// выносим в поток все данные 
                       // из буфера

            ms.Seek(0, SeekOrigin.Begin);
            BinaryReader br = new BinaryReader(ms);
            byte[] buf = br.ReadBytes((int)ms.Length);
            return buf;
        }
    }
}
