using CustomORM.Attributes;
using System.Data;
using Entities.Enums;
using Warship.Interfaces.Ships;

namespace Warship.Models
{
    public class BattleShip : Ship, IAttackable
    {
        public int Range { get; set; }
        public void Attack()
        {
            System.Console.WriteLine("Attack");
        }
    }
}
