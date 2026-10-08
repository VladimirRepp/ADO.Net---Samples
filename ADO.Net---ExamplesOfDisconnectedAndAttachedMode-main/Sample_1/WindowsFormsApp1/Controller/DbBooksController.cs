using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using WindowsFormsApp1.Models;
using WindowsFormsApp1.Database;
using System.Windows.Forms;

namespace WindowsFormsApp1.Controller
{
    public class DbBooksController
    {
        private DbConnection _connection;
        private string _tableName;
        private List<MBook> _data;

        public List<MBook> Data => _data;

        public DbBooksController()
        {
            _connection = new DbConnection();
            _tableName = "Books";
            _data = new List<MBook>();
        }

        public void AddData(MBook d)
        {
            try
            {
                if (_connection.OpenConnection() != true)
                {
                    MessageBox.Show("Сбой открытия подкючеоия к серверу!", "Внимание!");
                    return;
                }

                string query = $"Insert into {_tableName} (Title) Values (@title)";
                SqlCommand sqlCommand = new SqlCommand(query, _connection.Connection);
                sqlCommand.Parameters.AddWithValue("@title", d.Title);

                if (sqlCommand.ExecuteNonQuery() != 1)
                {
                    MessageBox.Show("Сбой добавления!", "Внимание!");
                }

                _connection.CloseConnection();
            }
            catch (Exception ex)
            {
                throw new Exception($"Form1.AddData: {ex.Message}");
            }
        }

        public void EditData(MBook d)
        {
            try
            {
                if (_connection.OpenConnection() != true)
                {
                    MessageBox.Show("Сбой открытия подкючеоия к серверу!", "Внимание!");
                    return;
                }

                string query = $"Update {_tableName} Set Title = @title where Id = @id";
                SqlCommand sqlCommand = new SqlCommand(query, _connection.Connection);
                sqlCommand.Parameters.AddWithValue("@id", d.Id);
                sqlCommand.Parameters.AddWithValue("@title", d.Title);

                if (sqlCommand.ExecuteNonQuery() != 1)
                {
                    MessageBox.Show("Сбой обновления!", "Внимание!");
                }

                _connection.CloseConnection();
            }
            catch (Exception ex)
            {
                throw new Exception($"Form1.AddData: {ex.Message}");
            }
        }

        public void DeleteByIdData(int id)
        {
            try
            {
                if (_connection.OpenConnection() != true)
                {
                    MessageBox.Show("Сбой открытия подкючеоия к серверу!", "Внимание!");
                    return;
                }

                string query = $"Delete from {_tableName} where Id = @id";
                SqlCommand sqlCommand = new SqlCommand(query, _connection.Connection);
                sqlCommand.Parameters.AddWithValue("@id", id);

                if (sqlCommand.ExecuteNonQuery() != 1)
                {
                    MessageBox.Show("Сбой удаления!", "Внимание!");
                }

                _connection.CloseConnection();
            }
            catch (Exception ex)
            {
                throw new Exception($"Form1.DeleteByIdData: {ex.Message}");
            }
        }

        public void LoadData()
        {
            SqlDataReader reader = null;

            try
            {
                if (_connection.OpenConnection() != true)
                {
                    MessageBox.Show("Сбой открытия подкючеоия к серверу!", "Внимание!");
                    return;
                }

                string query = @"Select * from Books";
                SqlCommand sqlCommand = new SqlCommand(query, _connection.Connection);
                reader = sqlCommand.ExecuteReader();

                if (!reader.HasRows)
                {
                    MessageBox.Show("Таблица пуста!", "Внимание!");
                }

                _data.Clear();
                while (reader.Read())
                {
                    MBook d = new MBook(Id: Convert.ToInt32(reader["Id"]),
                        Title: reader["Title"].ToString());

                    _data.Add(d);
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Form1.LoadData: {ex.Message}");
            }
            finally
            {
                if (reader != null)
                    reader.Close();

                _connection.CloseConnection();
            }
        }

    }
}
