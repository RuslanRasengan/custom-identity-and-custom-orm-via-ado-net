using System;

namespace CustomORM.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public sealed class TableNameAttribute : Attribute
    {
        private readonly string value;
        public TableNameAttribute(string value)
        {
            this.value = value;
        }
        public string Value { get { return this.value; } }
    }
}
