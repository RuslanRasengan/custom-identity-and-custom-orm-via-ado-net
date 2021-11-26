using CustomORM.BusinessLogic;
using DataAccess.Repositories;
using System.Configuration;
using DataAccess;
using Warship.Models;
using Warship.Extensions;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            var connectionString = ConfigurationManager.ConnectionStrings["WarshipDb"].ConnectionString;

            ShipRepository shipRepository = new ShipRepository(connectionString);

            BattleShip bShip = new BattleShip();

            bShip.Range = 1;
            bShip.Length = 1;
            bShip.Health = 1;
            bShip.Speed = 1;
            bShip.Direction = Direction.left;
            bShip.Point = new Point { Id = 1 };

            var bShipEntity = bShip.Map();

            shipRepository.Add(bShipEntity);
            var testGetAllShip = shipRepository.GetAll();
            bShipEntity.Id = 11;
            bShipEntity.Range = 5;

            var testGetByIdShip = shipRepository.GetById(21);
            shipRepository.Update(bShipEntity);
            shipRepository.Remove(bShipEntity);

            AreaRepository areaRepository = new AreaRepository(connectionString);

            Area area = new Area(77, 17);

            var areaEntity = area.Map();

            areaEntity.Id = 3;

            areaRepository.Add(areaEntity);
            var testGetAllArea = areaRepository.GetAll();
            areaEntity.Id = 5;
            var testGetByIdArea = areaRepository.GetById(1);
            areaRepository.Update(areaEntity);
            areaRepository.Remove(areaEntity);

            PointRepository pointRepository = new PointRepository(connectionString);

            Point point = new Point();

            point.X = 11;
            point.Y = 11;

            var pointEntity = point.Map();

            pointRepository.Add(pointEntity);
            var testGetAllPoint = pointRepository.GetAll();
            pointEntity.Id = 12;
            var testGetByIdPoint = pointRepository.GetById(5);
            pointRepository.Update(pointEntity);
            pointRepository.Remove(pointEntity);
        }
    }
}
