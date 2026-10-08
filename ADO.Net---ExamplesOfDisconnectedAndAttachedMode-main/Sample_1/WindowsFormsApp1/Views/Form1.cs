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

using WindowsFormsApp1.Controller;
using WindowsFormsApp1.Models;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        private DbBooksController _dbBooksController;

        public Form1()
        {
            InitializeComponent();

            _dbBooksController = new DbBooksController();
        }     
        
        private void LoadAndViewBooks(bool isLoad = true)
        {
            try
            {
                if(isLoad)
                    _dbBooksController.LoadData();

                int i = 0;
                dataGridView.Rows.Clear();
                foreach(MBook d in _dbBooksController.Data)
                {
                    dataGridView.Rows.Add(d.Title);
                    dataGridView.Rows[i++].HeaderCell.Value = d.Id.ToString();
                }
            }
            catch(Exception ex)
            {
                throw new Exception($"Form1.LoadAndViewBooks: {ex.Message}");
            }
        }

        private void Add()
        {
            if(textBox_Title.Text == "")
            {
                MessageBox.Show("Ввдите значия!", "Внимание!");
                return;
            }

            try{
                MBook d = new MBook(0, textBox_Title.Text);
               _dbBooksController.AddData(d);

                LoadAndViewBooks();
            }
            catch (Exception ex)
            {
                throw new Exception($"Form1.Add: {ex.Message}");
            }
        }

        private void Edit()
        {
            if (textBox_Title.Text == "")
            {
                MessageBox.Show("Ввдите значия!", "Внимание!");
                return;
            }

            if(dataGridView.CurrentRow.Index == -1)
            {
                MessageBox.Show("Выберите строку!", "Внимание!");
                return;
            }

            try
            {
                MBook d = new MBook(Convert.ToInt32(dataGridView.CurrentRow.HeaderCell.Value),
                    textBox_Title.Text);

                _dbBooksController.EditData(d);
                LoadAndViewBooks(false);
            }
            catch (Exception ex)
            {
                throw new Exception($"Form1.Add: {ex.Message}");
            }
        }

        private void Delete()
        {
            if (dataGridView.CurrentRow.Index == -1)
            {
                MessageBox.Show("Выберите строку!", "Внимание!");
                return;
            }

            try
            {
                int id = Convert.ToInt32(dataGridView.CurrentRow.HeaderCell.Value);
                _dbBooksController.DeleteByIdData(id);
                LoadAndViewBooks(false);
            }
            catch (Exception ex)
            {
                throw new Exception($"Form1.Add: {ex.Message}");
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                LoadAndViewBooks();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Вызвано исключени!");
            }
        }

        private void добавитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Add();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message,"Вызвано исключение!");
            }
        }

        private void изменитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Edit();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Вызвано исключение!");
            }
        }

        private void удалитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Delete();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Вызвано исключение!");
            }
        }

        private void button_Add_Click(object sender, EventArgs e)
        {
            try
            {
                Add();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Вызвано исключение!");
            }
        }

        private void button_Edit_Click(object sender, EventArgs e)
        {
            try
            {
                Edit();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Вызвано исключение!");
            }
        }

        private void button_Delete_Click(object sender, EventArgs e)
        {
            try
            {
                Delete();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Вызвано исключение!");
            }
        }

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
