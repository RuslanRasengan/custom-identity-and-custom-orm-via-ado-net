using System;

namespace CustomORM.Attributes
{
    [AttributeUsage(AttributeTargets.All, Inherited = false, AllowMultiple = false)]
    public sealed class ForeignKeyAttribute : Attribute
    {
        
    }
}
