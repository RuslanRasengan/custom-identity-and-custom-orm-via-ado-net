using CustomORM.Attributes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;

namespace CustomORM.Extensions
{
    public static class TypeMapper
    {
        public static T Map<T>(this SqlDataReader reader)
        {
            PropertyInfo[] columnsInTable;

            dynamic instance;

            var temp = GetDiscriminator(reader, typeof(T));

            if (temp != null)
            {
                instance = Activator.CreateInstance(temp);

                columnsInTable = temp.GetProperties()
                .Where(f => f.GetCustomAttribute<ColumnAttribute>() != null).ToArray();
            }
            else
            {
                instance = Activator.CreateInstance(typeof(T));

                columnsInTable = typeof(T).GetProperties()
                .Where(f => f.GetCustomAttribute<ColumnAttribute>() != null).ToArray();
            }

            for (var i = 0; i < columnsInTable.Length; i++)
            {
                if (reader.GetName(i) == "Discriminator")
                {
                    continue;
                }
                var columnInTable = columnsInTable.Where(c => c.Name == reader.GetName(i)).FirstOrDefault();
  
                if (reader.GetValue(i) is DBNull)
                {
                    columnInTable.SetValue(instance, null);
                }
                else
                {
                    columnInTable.SetValue(instance, reader.GetValue(i));
                }
            }
            return instance;
        }

        private static Type GetDiscriminator(SqlDataReader reader, Type t)
        {
            try
            {
                var discriminator = reader.GetValue(reader.GetOrdinal("Discriminator")).ToString();

                Type typeDiscriminator = t.Assembly.GetTypes()
                    .Where(type => type.IsSubclassOf(t) && type.Name == discriminator).FirstOrDefault();


                return typeDiscriminator;
            }
            catch (Exception)
            {

                return null;
            }
        }
    }
}