using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Lista3_Crud.Infra.Data.Connection
{
    public class ApplicationDbContext : IDisposable
    {
        private IDbConnection? _connection;
        private IDbTransaction? _transaction;

        public ApplicationDbContext(IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            _connection = new SqlConnection(connectionString);
            _connection.Open();
        }

        public IDbConnection Connection => _connection;
        public IDbTransaction Transaction => _transaction;

        public IDbTransaction BeginTransaction()
        {
            _transaction = _connection.BeginTransaction();
            return _transaction;
        }

        public void SaveChanges()
        {
            if (_transaction == null)
            {
                throw new
                    InvalidOperationException("Transaction have already been already been commited. Check your transaction handling.");
            }
            _transaction.Commit();
            _transaction = null;
        }

        public void Dispose()
        {
            if (_transaction != null)
            {
                _transaction.Rollback();
                _transaction = null;
            }
            if (_connection != null)
            {
                _connection.Close();
                _connection = null;
            }
        }
    }
}