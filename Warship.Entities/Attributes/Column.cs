using System;
using System.Data;

namespace CustomORM.Attributes
{
    [AttributeUsage(AttributeTargets.All, Inherited = false, AllowMultiple = false )]
    public sealed class ColumnAttribute : Attribute
    {
        private readonly string columnName;
        private DbType fieldType;
        public ColumnAttribute(string columnName, DbType fieldType)
        {
            this.columnName = columnName;
            this.fieldType = fieldType;
        }

        public string ColumnName
        {
            get { return columnName; }
        }

        public DbType FieldType { get { return fieldType; }}
    }
}
