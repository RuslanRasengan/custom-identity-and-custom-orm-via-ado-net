namespace Warship.Models
{
    public class Point
    {
        public int Id { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public Point()
        {
        }
        public Point(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }
    }
}
