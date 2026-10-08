using Sample_0.Database;
using Sample_0.Model;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sample_0.Controller
{
    public class DbAuthorController
    {
        private DbConnector _connector;
        private List<Author> _data;

        public List<Author> Data => _data;

        public DbAuthorController()
        {
            _connector = new DbConnector();
            _data = new List<Author>();
        }

        public int Insert(Author d)
        {
            int result = -1;

            try
            {
                _connector.OpenConnection();

                string commandString = $@"insert into Authors (Id, FirstName, LastName) values 
                                        (@id, @firstName, @lastName)";

                SqlCommand command = new SqlCommand();
                command.CommandText = commandString;
                command.Connection = _connector.Connection;

                command.Parameters.AddWithValue("@id", d.Id);
                command.Parameters.AddWithValue("@firstName", d.FirsName);
                command.Parameters.AddWithValue("@lastName", d.LastName);

                result = command.ExecuteNonQuery();

                if (result == 1)
                    _data.Add(d);
            }
            catch (Exception ex)
            {
                throw new Exception("Insert: " + ex.Message);
            }
            finally
            {
                _connector.CloseConnection();
            }

            return result;
        }

        public int Update(Author d)
        {
            int result = -1;

            try
            {
                _connector.OpenConnection();

                string query = @"Update Authors Set FirstName = @firstName, lastName = @lastName where Id = @id";

                SqlCommand command = new SqlCommand();
                command.CommandText = query;
                command.Connection = _connector.Connection;

                command.Parameters.AddWithValue("@id", d.Id);
                command.Parameters.AddWithValue("@firstName", d.FirsName);
                command.Parameters.AddWithValue("@lastName", d.LastName);

                result = command.ExecuteNonQuery();

                if (result == 1)
                {
                    int index = _data.FindIndex(x => x.Id == d.Id);

                    if (index != -1)
                    {
                        _data[index] = d;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Update: " + ex.Message);
            }
            finally
            {
                _connector.CloseConnection();
            }

            return result;
        }

        public int RemoveById(int id)
        {
            int result = -1;

            try
            {
                _connector.OpenConnection();

                string query = @"Delete from Authors where Id = @id";

                SqlCommand command = new SqlCommand();
                command.CommandText = query;
                command.Connection = _connector.Connection;

                command.Parameters.AddWithValue("@id", id);

                result = command.ExecuteNonQuery();

                if (result == 1)
                {
                    int index = _data.FindIndex(x => x.Id == id);

                    if (index != -1)
                    {
                        _data.RemoveAt(index);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Remove: " + ex.Message);
            }
            finally
            {
                _connector.CloseConnection();
            }

            return result;
        }

        // Реализовать данный метод с помощью команды к ДБ
        // Подсказка: использовать выборку (select)
        public Author Search(string firstName)
        {
            return new Author();
        }

        public bool LoadDB()
        {
            SqlDataReader reader = null;

            try
            {
                _connector.OpenConnection();

                SqlCommand sqlCommand = new SqlCommand(@"select * from Authors", _connector.Connection);
                reader = sqlCommand.ExecuteReader();

                _data.Clear();

                if (!reader.HasRows)
                    return false;

                while (reader.Read())
                {
                    Author d = new Author();
                    d.Id = Convert.ToInt32(reader["Id"]);
                    d.FirsName = reader["FirstName"].ToString();
                    d.LastName = reader["LastName"].ToString();

                    _data.Add(d);
                }

            }
            catch (Exception ex)
            {
                throw new Exception("Load: " + ex.Message);
            }
            finally
            {
                reader.Close();
                _connector.CloseConnection();   
            }

            return true;
        }
    }
}
