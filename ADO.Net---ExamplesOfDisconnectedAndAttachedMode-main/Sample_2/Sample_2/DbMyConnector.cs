using System;
using System.Data.SqlClient;
using System.Data;

using System.Configuration; // (!!!) Добавить через ссылки проекта (!!!)

/// <summary>
/// Коннектор к ДБ.
/// Класс Singltone.
/// </summary>
public class DbMyConnector
{
    private static DbMyConnector INSTANCE;

    private SqlConnection _sqlConnection;

    public SqlConnection Connection => _sqlConnection;
    public bool IsConnection => _sqlConnection != null;
    /// <summary>
    /// Вернуть экземпляр/объект класса одиночки
    /// </summary>
    public static DbMyConnector Instance
    {
        get
        {
            if(INSTANCE == null)
                INSTANCE = new DbMyConnector();

            return INSTANCE;
        }
    }

    private DbMyConnector()
    {
    }

    /// <summary>
    /// Инициализация коннектора. Создать подключение.
    /// </summary>
    public void Startup()
    {
        if(!IsConnection)
            CreateConnection();
    }

    /// <summary>
    /// Открыть подключение к БД
    /// </summary>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public bool TryOpenConnection()
    {
        try
        {
            if (_sqlConnection == null)
                return false;

            if (_sqlConnection.State != ConnectionState.Open)
                _sqlConnection.Open();

            return true;
        }
        catch (Exception ex)
        {
            throw new Exception($"DbMyConnector.TryOpenConnection: {ex.Message}");
        }
    }

    /// <summary>
    /// Закрыть подключение к БД
    /// </summary>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public bool TryCloseConnection()
    {
        try
        {
            if (_sqlConnection == null)
                return false;

            if (_sqlConnection.State != ConnectionState.Closed)
                _sqlConnection.Close();

            return true;
        }
        catch (Exception ex)
        {
            throw new Exception($"DbMyConnector.TryCloseConnection: {ex.Message}");
        }
    }

    private void CreateConnection()
    {
        try
        {
            _sqlConnection = new SqlConnection();
            _sqlConnection.ConnectionString =
                @"Data Source = WKS-QTHBM671QNJ\SQLEXPRESS;" +
                "Initial Catalog = ViewDB;" +
                "Integrated Security = SSPI;" +
                "Encrypt = False";

            // Находится в App.config файле проекта
            //_sqlConnection.ConnectionString = ConfigurationManager.
            //ConnectionStrings["MyConnectionString"].ConnectionString;
        }
        catch (Exception ex)
        {
            throw new Exception($"DbMyConnector.CreateConnection: {ex.Message}");
        }
    }
}
