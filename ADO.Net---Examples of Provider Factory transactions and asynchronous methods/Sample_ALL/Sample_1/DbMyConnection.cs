using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration; // (!!!) Добавить через ссылки проекта (!!!)

/// <summary>
/// DbMyConnection - Singltone
/// </summary>
public class DbMyConnection
{
    // Потокобезопасная реализация
    private static readonly DbMyConnection INSTANCE = new DbMyConnection();

    private SqlConnection _sqlConnection = null;
    private string _connectionString = "Data Source=localhost\\SQLEXPRESS;Initial Catalog=ItTop;Integrated Security=True;Encrypt=False";

    public SqlConnection Connection => _sqlConnection;
    public string ConnеctionString {
        get => _connectionString;
        set => _connectionString = value;
    }

    public static DbMyConnection Instance => INSTANCE;

    private DbMyConnection()
    {
        Initialization();
    }

    private void CreateConnection()
    {
        try
        {
            _sqlConnection = new SqlConnection();
            _sqlConnection.ConnectionString = _connectionString;

            // Находится в App.config файле проекта
            //_sqlConnection.ConnectionString = ConfigurationManager.ConnectionStrings["DbTypeSQL"].ConnectionString;
        }
        catch (Exception ex)
        {
            throw new Exception($"DbMyConnection.CreateConnection: {ex.Message}");
        }
    }

    public void Initialization()
    {
        CreateConnection();
    }

    public void Initialization(string connectionString)
    {
        _connectionString = connectionString;
        CreateConnection();
    }

    public bool Open()
    {
        try
        {
            if (_sqlConnection == null)
                CreateConnection();

            if (_sqlConnection.State != ConnectionState.Open)
                _sqlConnection.Open();

            return true;
        }
        catch (Exception ex)
        {
            throw new Exception($"DbMyConnection.Open: {ex.Message}");
        }
    }

    public bool Close()
    {
        try
        {
            if (_sqlConnection == null)
                CreateConnection();

            if (_sqlConnection.State != ConnectionState.Closed)
                _sqlConnection.Close();

            return true;
        }
        catch (Exception ex)
        {
            throw new Exception($"DbMyConnection.Close: {ex.Message}");
        }
    }
}
