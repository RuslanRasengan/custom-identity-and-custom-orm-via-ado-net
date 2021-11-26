using CustomORM.Attributes;
using CustomORM.Extensions;
using Entities.Ships;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;

namespace CustomORM.BusinessLogic
{
    public class DbQueries
    {
        private readonly string connectionString;

        public DbQueries(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public List<T> GetAll<T>()
        {
            var tableName = typeof(T).GetCustomAttribute<TableNameAttribute>().Value;

            string sqlExpression = $"SELECT * FROM {tableName}";

            var list = new List<T>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand(sqlExpression, connection);
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {

                    while (reader.Read())
                    {
                        list.Add(reader.Map<T>());
                    }
                }
                reader.Close();
            }
            return list;
        }
        public T GetById<T>(int id)
        {
            object obj = null;

            var tableName = typeof(T).Name;

            var primaryKey = typeof(T).GetProperties()
                .Where(f => f.GetCustomAttribute<PrimaryKeyAttribute>() != null).FirstOrDefault().Name;

            string sqlExpression = $"SELECT * FROM {tableName} WHERE {primaryKey}={id}";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);

                var reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    reader.Read();
                    obj = reader.Map<T>();
                }
                reader.Close();
            }
            return (T)obj;
        }
        public void AddData<T>(T obj)
        {
            var tableName = typeof(T).Name;

            var columns = String.Join(", ", obj.GetType().GetProperties()
                .Where(f => f.GetCustomAttribute<ColumnAttribute>() != null && f.GetCustomAttribute<PrimaryKeyAttribute>() == null && f.GetValue(obj) != null)
                .Select(x => x.Name));

            var newData = String.Join(", ", obj.GetType().GetProperties()
                .Where(f => f.GetCustomAttribute<ColumnAttribute>() != null && f.GetCustomAttribute<PrimaryKeyAttribute>() == null && f.GetValue(obj) != null)
                .Select(x =>
                {
                    var data = x.GetValue(obj);
                    if(data is string )
                    {
                        return $"'{data}'";
                    }
                    if (data.GetType().IsEnum)
                    {
                        return (int)data;
                    }
                    return data;
                }));

            if (typeof(T) == typeof(Ship))
            {
                if (CheckedDiscriminator<T>())
                {
                    columns += ",Discriminator";

                    newData += $",'{ obj.GetType().Name}'";
                }
            }

            string sqlExpression = $"INSERT INTO {tableName} ({columns}) VALUES ({newData})";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {

                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                command.ExecuteNonQuery();
            }
        }
        public void UpdateData<T>(T obj)
        {
            var tableName = typeof(T).Name;

            var columns = String.Join(", ", obj.GetType().GetProperties()
                .Where(f => f.GetCustomAttribute<ColumnAttribute>() != null && f.GetCustomAttribute<PrimaryKeyAttribute>() == null && f.GetValue(obj) != null)
                .Select(x => x.Name));

            var primaryKey = obj.GetType().GetProperties()
                .Where(f => f.GetCustomAttribute<PrimaryKeyAttribute>() != null).FirstOrDefault();

            var data = obj.GetType().GetProperties()
               .Where(f => f.GetCustomAttribute<ColumnAttribute>() != null && f.GetCustomAttribute<PrimaryKeyAttribute>() == null && f.GetValue(obj) != null)
               .Select(x =>
               {
                   var data = x.GetValue(obj);
                   if (data is string)
                   {
                       return $"'{data}'";
                   }
                   if (data.GetType().IsEnum)
                   {
                       return (int)data;
                   }
                   return data;
               }).ToArray();

            var dataIndex = 0;
            for (var index = 0; index < columns.Length; index++)
            {
                var tempIndex = 0;
                if (columns[index] == ',')
                {
                    columns = columns.Insert(index,string.Concat("=",data[dataIndex]));
                    tempIndex = data[dataIndex].ToString().Length;
                    index = index + tempIndex + 1;
                    dataIndex = dataIndex + 1;
                }

                if (index == columns.Length - 1)
                {
                    columns = columns.Insert(index + 1,string.Concat("=", data[dataIndex]));
                    break;
                }
            }

        string sqlExpression = $"UPDATE {tableName} SET {columns} WHERE {primaryKey.Name}=@{primaryKey.Name}";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                command.Parameters.Add(new SqlParameter($"@{primaryKey.Name}", primaryKey.GetValue(obj)));
                command.ExecuteNonQuery();
            }
        }
        public void Remove<T>(T obj)
        {
            var tableName = typeof(T).Name;

            var primaryKey = typeof(T).GetProperties().Where(f => f.GetCustomAttribute<PrimaryKeyAttribute>() != null).FirstOrDefault();

            string sqlExpression = $"DELETE FROM {tableName} WHERE {primaryKey.Name}=@{primaryKey.Name}";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);

                command.Parameters.Add(new SqlParameter($"@{primaryKey.Name}", primaryKey.GetValue(obj)));

                command.ExecuteNonQuery();
            }
        }
        private bool CheckedDiscriminator<T>()
        {

            var tableName = typeof(T).GetCustomAttribute<TableNameAttribute>().Value;

            string sqlExpression = $"SELECT * FROM {tableName}";

            SqlDataReader reader;

            using (SqlConnection connection = new SqlConnection(this.connectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand(sqlExpression, connection);
                reader = command.ExecuteReader();
                reader.Read();

                try
                {
                    reader.GetOrdinal("Discriminator");

                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            } 
        }
    }
}