using CustomORM.Attributes;
using Entities.Base;
using System.Data;

namespace Entities.Ships
{
    [TableName("Point")]
    public class Point : BaseEntity
    {
        [Column("X", DbType.Int32)]
        public int X { get; set; }
        [Column("Y", DbType.Int32)]
        public int Y { get; set; }
    }
}
