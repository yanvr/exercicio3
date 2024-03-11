using Dapper;
using Lista3_Crud.Domain.Entities;
using Lista3_Crud.Domain.Interfaces;
using Lista3_Crud.Infra.Data.Connection;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Reflection;
using System.Text;
using System.Text.Json.Serialization;

namespace Lista3_Crud.Infra.Data.Repositories
{
    public class GenericRepository<T>(ApplicationDbContext context) : IGenericRepository<T> where T : Entity
    {
        private readonly ApplicationDbContext _context = context;

        public async Task<T?> GetById(int id)
        {
            T result;
            try
            {
                string tableName = GetTableName();
                string keyColumn = GetKeyColumnName();
                string query = $"SELECT {GetColumnsAsProperties()} FROM {tableName} WHERE {keyColumn} = '{id}'";

                result = await _context.Connection.QueryFirstOrDefaultAsync<T>(query);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching a record from db: ${ex.Message}");
                throw new Exception("Unable to fetch data. Please contact the administrator.");
            }

            return result;
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            IEnumerable<T> result;
            try
            {
                string tableName = GetTableName();
                string query = $"SELECT {GetColumnsAsProperties()} FROM {tableName}";

                result = await _context.Connection!.QueryAsync<T>(query);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching records from db: ${ex.Message}");
                throw new Exception("Unable to fetch data. Please contact the administrator.");
            }

            return result;
        }

        public async Task<int> Add(T entity)
        {
            try
            {
                string tableName = GetTableName();
                string columns = GetColumns(excludeKey: true);
                string properties = GetPropertyNames(excludeKey: true);
                string query = $"INSERT INTO {tableName} ({columns}) VALUES ({properties}) SELECT SCOPE_IDENTITY()";

                var result = await _context.Connection.ExecuteScalarAsync(query, entity, _context.Transaction);
                return Convert.ToInt32(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding a record to db: ${ex.Message}");
            }

            return 0;
        }

        public async Task Update(T entity)
        {
            try
            {
                string? tableName = GetTableName();
                string? keyColumn = GetKeyColumnName();
                string? keyProperty = GetKeyPropertyName();

                StringBuilder query = new StringBuilder();
                query.Append($"UPDATE {tableName} SET ");

                foreach (var property in GetProperties(true))
                {
                    var columnAttribute = property.GetCustomAttribute<ColumnAttribute>();

                    string propertyName = property.Name;
                    string columnName = columnAttribute?.Name ?? "";

                    query.Append($"{columnName} = @{propertyName},");
                }

                query.Remove(query.Length - 1, 1);

                query.Append($" WHERE {keyColumn} = @{keyProperty}");

                await _context.Connection.ExecuteAsync(query.ToString(), entity, transaction: _context.Transaction);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating a record in db: ${ex.Message}");
            }
        }

        public async Task Delete(T entity)
        {
            try
            {
                string? tableName = GetTableName();
                string? keyColumn = GetKeyColumnName();
                string? keyProperty = GetKeyPropertyName();
                string query = $"DELETE FROM {tableName} WHERE {keyColumn} = @{keyProperty}";

                await _context.Connection.ExecuteAsync(query, entity, transaction: _context.Transaction);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting a record in db: ${ex.Message}");
            }
        }

        private string GetTableName()
        {
            var type = typeof(T);
            var tableAttribute = type.GetCustomAttribute<TableAttribute>();
            if (tableAttribute != null)
                return tableAttribute.Name;

            return type.Name;
        }

        private static string? GetKeyColumnName()
        {
            PropertyInfo[] properties = typeof(T).GetProperties();

            foreach (PropertyInfo property in properties)
            {
                object[] keyAttributes = property.GetCustomAttributes(typeof(KeyAttribute), true);

                if (keyAttributes != null && keyAttributes.Length > 0)
                {
                    object[] columnAttributes = property.GetCustomAttributes(typeof(ColumnAttribute), true);

                    if (columnAttributes != null && columnAttributes.Length > 0)
                    {
                        ColumnAttribute columnAttribute = (ColumnAttribute)columnAttributes[0];
                        return columnAttribute?.Name ?? "";
                    }
                    else
                    {
                        return property.Name;
                    }
                }
            }

            return null;
        }

        private string GetColumns(bool excludeKey = false)
        {
            var type = typeof(T);
            var columns = string.Join(", ", type.GetProperties()
                .Where(p => (!excludeKey || !p.IsDefined(typeof(KeyAttribute))) && !p.IsDefined(typeof(JsonIgnoreAttribute)))
                .Select(p =>
                {
                    var columnAttribute = p.GetCustomAttribute<ColumnAttribute>();
                    return columnAttribute != null ? columnAttribute.Name : p.Name;
                }));

            return columns;
        }

        private string GetColumnsAsProperties(bool excludeKey = false)
        {
            var type = typeof(T);
            var columnsAsProperties = string.Join(", ", type.GetProperties()
                .Where(p => (!excludeKey || !p.IsDefined(typeof(KeyAttribute))) && !p.IsDefined(typeof(JsonIgnoreAttribute)))
                .Select(p =>
                {
                    var columnAttribute = p.GetCustomAttribute<ColumnAttribute>();
                    return columnAttribute != null ? $"{columnAttribute.Name} AS {p.Name}" : p.Name;
                }));

            return columnsAsProperties;
        }

        private string GetPropertyNames(bool excludeKey = false)
        {
            var properties = typeof(T)
                .GetProperties()
                .Where(p => (!excludeKey || p.GetCustomAttribute<KeyAttribute>() == null)
                    && p.GetCustomAttribute<JsonIgnoreAttribute>() == null);

            var values = string.Join(", ", properties.Select(p => $"@{p.Name}"));

            return values;
        }

        private IEnumerable<PropertyInfo> GetProperties(bool excludeKey = false)
        {
            var properties = typeof(T).GetProperties()
                .Where(p => (!excludeKey || p.GetCustomAttribute<KeyAttribute>() == null) && p.GetCustomAttribute<JsonIgnoreAttribute>() == null);

            return properties;
        }

        private string? GetKeyPropertyName()
        {
            var properties = typeof(T).GetProperties()
                .Where(p => p.GetCustomAttribute<KeyAttribute>() != null).ToList();

            if (properties.Any())
                return properties?.FirstOrDefault()?.Name ?? null;

            return null;
        }
    }
}