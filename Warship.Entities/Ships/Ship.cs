using CustomORM.Attributes;
using Entities.Base;
using Entities.Enums;
using System.Data;

namespace Entities.Ships
{
    [TableName("Ship")]
    public class Ship : BaseEntity
    {
        [Column("Length", DbType.Int32)]
        public int Length { get; set; }
        [Column("Health", DbType.Int32)]
        public int Health { get; set; }
        [Column("Speed", DbType.Int32)]
        public int Speed { get; set; }
        [Column("Direction", DbType.Int32)]
        public Direction Direction { get; set; }
        [ForeignKey]
        [Column("AreaId", DbType.Int32)]
        public int? AreaId { get; set; }
        [ForeignKey]
        [Column("PointId", DbType.Int32)]
        public int? PointId { get; set; }
        public Point Point { get; set; }
    }
}
