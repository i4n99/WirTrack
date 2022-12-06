using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using System.Data;

namespace Persistencia.Dapper.DapperConexion
{
    public class FactoryConnection : IFactoryConnection
    {
        private IDbConnection _connection;
        private readonly IOptions<ConexionConfiguracion> _configs;
        public FactoryConnection(IOptions<ConexionConfiguracion> configs)
        {
            _configs = configs;
        }
        public void CloseConnection()
        {
            if (_connection != null && _connection.State == ConnectionState.Open)
            {
                _connection.Close();
            }
        }

        public IDbConnection GetConnection(Connections? connection = null)
        {
            if (_connection == null)
            {
                switch (connection)
                {
                    case Connections.DefaultConnection:
                        _connection = new SqlConnection(_configs.Value.DefaultConnection);
                        break;
                    default:
                        _connection = new SqlConnection(_configs.Value.DefaultConnection);
                        break;
                }
            }
            if (_connection.State != ConnectionState.Open)
            {
                _connection.Open();
            }
            return _connection;
        }
    }
}