using Warship.Interfaces.Ships;

namespace Warship.Models
{
    public class MixedShip : Ship, IAttackable, IRepairable
    {
        public int Range { get; set; }
        public void Attack()
        {
            System.Console.WriteLine("Attack");
        }
        public void Repair()
        {
            System.Console.WriteLine("Repair");
        }
    }
}
