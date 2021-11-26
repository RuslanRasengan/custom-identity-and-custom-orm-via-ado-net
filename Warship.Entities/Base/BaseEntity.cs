using CustomORM.Attributes;
using System.Data;

namespace Entities.Base
{
    public class BaseEntity
    {
        [PrimaryKey]
        [Column("Id", DbType.Int32)]
        public int Id { get; set; }
    }
}
