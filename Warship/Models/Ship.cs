namespace Warship.Models
{
    public class Ship
    {
        public int Id { get; set; }
        public int Length { get; set; }
        public int Health { get; set; }
        public int Speed { get; set; }
        public Direction Direction { get; set; }
        public Point Point { get; set; }

        public static bool operator ==(Ship firstShip, Ship secondShip)
        {
            if (firstShip.Length == secondShip.Length && firstShip.GetType() == secondShip.GetType())
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static bool operator !=(Ship firstShip, Ship secondShip)
        {
            if (firstShip.Length != secondShip.Length && firstShip.GetType() != secondShip.GetType())
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
