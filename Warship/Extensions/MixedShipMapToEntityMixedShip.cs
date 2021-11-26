using Warship.Models;

namespace Warship.Extensions
{
    public static class MixedShipMapToEntityMixedShip
    {
        public static Entities.Ships.MixedShip Map(this MixedShip mixedShip, int? areaId = null)
        {
            return new Entities.Ships.MixedShip
            {
                Id = mixedShip.Id,
                Length = mixedShip.Length,
                Health = mixedShip.Health,
                Speed = mixedShip.Speed,
                Range = mixedShip.Range,
                AreaId = areaId,
                PointId = mixedShip.Point?.Id,
                Point = mixedShip.Point.Map(),
                Direction = (Entities.Enums.Direction)mixedShip.Direction,                
            };
        }
    }
}
