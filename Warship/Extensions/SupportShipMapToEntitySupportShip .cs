using Warship.Models;

namespace Warship.Extensions
{
    public static class SupportShipMapToEntitySupportShip
    {
        public static Entities.Ships.SupportShip Map(this SupportShip supportShip, int? areaId = null)
        {
            return new Entities.Ships.SupportShip
            {
                Id = supportShip.Id,
                Length = supportShip.Length,
                Health = supportShip.Health,
                Speed = supportShip.Speed,
                Range = supportShip.Range,
                AreaId = areaId,
                PointId = supportShip.Point?.Id,
                Point = supportShip.Point.Map(),
                Direction = (Entities.Enums.Direction)supportShip.Direction
            };
        }
    }
}
