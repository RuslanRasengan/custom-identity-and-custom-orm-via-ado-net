using Warship.Models;

namespace Warship.Extensions
{
    public static class BattleShipMapToEntityShip
    {
        public static Entities.Ships.BattleShip Map(this BattleShip battleShip, int? areaId = null)
        {
            return new Entities.Ships.BattleShip
            {
                Id = battleShip.Id,
                Length = battleShip.Length,
                Health = battleShip.Health,
                Speed = battleShip.Speed,
                AreaId = areaId,
                PointId = battleShip.Point?.Id,
                Point = battleShip.Point.Map(),
                Direction = (Entities.Enums.Direction)battleShip.Direction,
                Range = battleShip.Range
            };
        }
    }
}
