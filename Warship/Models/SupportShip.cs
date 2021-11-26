using Warship.Interfaces.Ships;

namespace Warship.Models
{
    public class SupportShip : Ship, IRepairable
    {
        public int Range { get; set; }
        public void Repair()
        {
            System.Console.WriteLine("Repair");
        }
    }
}
