using Sample_0.Controller;
using Sample_0.Model;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Sample_0
{
    public partial class Form1 : Form
    {
        private DbAuthorController _authorController;

        public Form1()
        {
            InitializeComponent();
            _authorController = new DbAuthorController();

            dataGridView.TopLeftHeaderCell.Value = "ID";
        }

        private void ViewData()
        {
            dataGridView.Rows.Clear();

            int i = 0;
            foreach(var d in _authorController.Data)
            {
                dataGridView.Rows.Add(d.FirsName, d.LastName);
                dataGridView.Rows[i++].HeaderCell.Value = d.Id.ToString();
            }
        }

        private void button_insert_Click(object sender, EventArgs e)
        {
            try
            {
                Author d = new Author();

                d.Id = Convert.ToInt32(textBox_ID.Text);
                d.FirsName = textBox_FirstName.Text;
                d.LastName = textBox_LastName.Text;

                if(d.FirsName == "" || d.LastName == "")
                {
                    MessageBox.Show("Не все поля введены!", "Внимание!");
                    return;
                }

               if(_authorController.Insert(d) == 1)
                {
                    MessageBox.Show("Данные добавлены!", "Внимание!");
                    ViewData();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button_Update_Click(object sender, EventArgs e)
        {
            try
            {
                Author d = new Author();

                d.Id = Convert.ToInt32(textBox_ID.Text);
                d.FirsName = textBox_FirstName.Text;
                d.LastName = textBox_LastName.Text;

                if (d.FirsName == "" || d.LastName == "")
                {
                    MessageBox.Show("Не все поля введены!", "Внимание!");
                    return;
                }

                if (_authorController.Update(d) == 1)
                {
                    ViewData();
                    MessageBox.Show("Данные обновлены!", "Внимание!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button_Remove_Click(object sender, EventArgs e)
        {
            try
            {
                int id = Convert.ToInt32(textBox_ID.Text);

                if (_authorController.RemoveById(id) == 1)
                {
                    ViewData();
                    MessageBox.Show("Данные удалены!", "Внимание!");
                } 
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button_Load_Click(object sender, EventArgs e)
        {
            try
            {
                if (_authorController.LoadDB())
                {
                    ViewData();
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button_Search_Click(object sender, EventArgs e)
        {

        }
    }
}
