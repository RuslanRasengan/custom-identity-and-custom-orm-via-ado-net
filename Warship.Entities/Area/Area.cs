using CustomORM.Attributes;
using Entities.Base;
using System.Data;

namespace Entities.Area
{
    [TableName("Area")]
    public sealed class Area : BaseEntity
    {
        [Column("Width", DbType.Int32)]
        public int Width { get; set; }
        [Column("Height", DbType.Int32)]
        public int Height { get; set; }
    }
}