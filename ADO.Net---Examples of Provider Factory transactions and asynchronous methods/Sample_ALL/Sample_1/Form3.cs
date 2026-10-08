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
    public partial class Form3 : Form
    {
        private string _tableName;

        public Form3()
        {
            InitializeComponent();
            DbMyConnection.Instance.Initialization();
            _tableName = "template";
        }

        private void button_Transaction_Click(object sender, EventArgs e)
        {
            SqlCommand comm = DbMyConnection.Instance.Connection.CreateCommand();
            SqlTransaction tran = null;

            try
            {
                DbMyConnection.Instance.Open();

                tran = DbMyConnection.Instance.Connection.BeginTransaction();
                comm = DbMyConnection.Instance.Connection.CreateCommand();

                comm.Transaction = tran;
                comm.CommandText = $"create table {_tableName}(id int not null identity(1,1) primary key, f1 varchar(20), f2 int)";
                comm.ExecuteNonQuery();

                comm.CommandText = $"insert into {_tableName}(f1, f2) values ('Text value 1', 555)";
                comm.ExecuteNonQuery();

                comm.CommandText = $"insert into {_tableName}(f1, f2) values ('Text value for second row', 777)"; // error: table name!!!
                comm.ExecuteNonQuery();

                tran.Commit();
                MessageBox.Show("Транзация выполнена!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                tran.Rollback();
            }
            finally
            {
                DbMyConnection.Instance.Close();
            }
        }

        private void button_Drop_Click(object sender, EventArgs e)
        {
            SqlCommand comm = DbMyConnection.Instance.Connection.CreateCommand();
            SqlTransaction tran = null;

            try
            {
                DbMyConnection.Instance.Open();

                tran = DbMyConnection.Instance.Connection.BeginTransaction();
                comm = DbMyConnection.Instance.Connection.CreateCommand();

                comm.Transaction = tran;
                comm.CommandText = $"drop table {_tableName}";
                comm.ExecuteNonQuery();

                tran.Commit();
                MessageBox.Show("Транзация выполнена!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                tran.Rollback();
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
