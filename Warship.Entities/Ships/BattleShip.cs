using CustomORM.Attributes;
using System.Data;

namespace Entities.Ships
{
    public class BattleShip : Ship
    {
        [Column("Range", DbType.Int32)]
        public int Range { get; set; }
    }
}
