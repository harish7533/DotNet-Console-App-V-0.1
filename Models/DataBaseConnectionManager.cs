using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace MusicPlayerSystem_v1_Database.Models
{
    public sealed class DataBaseConnectionManager
    {
        private static readonly Lazy<DataBaseConnectionManager> _dataBaseConnectionManager = new Lazy<DataBaseConnectionManager>(() => new DataBaseConnectionManager());
        private SqlConnection _sqlConnection;
        public static DataBaseConnectionManager dataBaseConnectionManager => _dataBaseConnectionManager.Value;
        public void Initialize(IConfiguration configuration)
        {
            if (_sqlConnection != null)
            {
                throw new InvalidOperationException("DataBaseConnectionManager has already been initialized.");
            }
            string connectionString = configuration.GetConnectionString("DefaultConnectionString");
            _sqlConnection = new(connectionString);
        }
        public SqlConnection Connection
        {
            get
            {
                if (_sqlConnection.State == ConnectionState.Closed)
                {
                    _sqlConnection.Open();
                }
                if (_sqlConnection == null)
                {
                    throw new InvalidOperationException("DataBaseConnectionManager has not been initialized.");
                }
                return _sqlConnection;
            }
        }
        public void CloseConnection()
        {
            if (_sqlConnection.State == ConnectionState.Open && _sqlConnection != null)
            {
                _sqlConnection.Close();
                Console.WriteLine("Connection Closed!");
            }
        }
    }
}