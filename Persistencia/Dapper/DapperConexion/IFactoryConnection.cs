using System.Data;

namespace Persistencia.Dapper.DapperConexion
{
    public interface IFactoryConnection
    {
        void CloseConnection();
        IDbConnection GetConnection(Connections? connection = null);
    }
}