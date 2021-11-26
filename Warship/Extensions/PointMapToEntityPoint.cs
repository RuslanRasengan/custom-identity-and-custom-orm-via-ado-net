using Warship.Models;

namespace Warship.Extensions
{
    public static class PointMapToEntityPoint
    {
        public static Entities.Ships.Point Map(this Point point)
        {
            return new Entities.Ships.Point
            {
                X = point.X,
                Y = point.Y
            };
        }
    }
}
