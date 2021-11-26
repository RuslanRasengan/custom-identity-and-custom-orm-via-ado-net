using CustomORM.Extensions;
using NUnit.Framework;
using System.Collections.Generic;
using Warship.Entities.Area;
using Warship.Entities.Enums;
using Warship.Entities.Ships;

namespace Warship.UT
{
    public class ShipTest
    {
        [SetUp]
        public void Setup()
        {
            
        }

        [Test]
        public void CheckingAPlungeMovement()
        {
            var area = new Area(9, 10);
            var ship = new Ship(Direction.down, 3, 2);
            var ship2 = new Ship(Direction.left, 4, 2);
            area.AddShip(ship, new Point(4, 4));
            area.AddShip(ship2, new Point(2, 4));
            Assert.IsFalse(area.MoveShip(ship2, 3));
        }

        [Test]
        public void CheckingMovement()
        {
            var area = new Area(9, 10);
            var ship = new Ship(Direction.down, 3, 2);
            var ship2 = new Ship(Direction.left, 4, 2);
            area.AddShip(ship, new Point(4, 4));
            area.AddShip(ship2, new Point(2, 4));
            Assert.IsTrue(area.MoveShip(ship, 3));
        }

        [Test]
        public void ShowShipsPlacement()
        {
            var area = new Area(9, 10);
            area.AddShip(new Ship(), new Point(1, 1));
            var firstShip = area[1, 1, 1];
            Assert.IsNotNull(firstShip);
        }

        [Test]
        public void WrongCoordinate()
        {
            var area = new Area(11, 11);
            var thirdShip = area[2, 1, 1];
            Assert.IsNull(thirdShip);
        }

        [Test]
        public void CheckOverloadingOperatorEqual()
        {
            var ship = new Ship(Direction.down, 5);
            var ship2 = new Ship(Direction.down, 5);

            Assert.IsTrue(ship == ship2);
            
        }

        [Test]
        public void CheckOverloadingOperatorNotEqual()
        {
            var ship = new BattleShip(Direction.down, 5);
            var ship2 = new BattleShip(Direction.down, 5);

            Assert.IsFalse(ship != ship2);

        }

        [Test]
        public void CheckOverloadingOperatorEqualDifLength()
        {
            var ship = new Ship(Direction.down, 4);
            var ship2 = new Ship(Direction.down, 5);

            Assert.IsFalse(ship == ship2);

        }

        [Test]
        public void CheckOverloadingOperatorNotEqualDifShips()
        {
            var ship = new BattleShip(Direction.down, 5);
            var ship2 = new MixedShip(Direction.down, 5);

            Assert.IsFalse(ship != ship2);

        }

        [Test]
        public void CheckOverloadingOperatorEqualDifShips()
        {
            var ship = new BattleShip(Direction.down, 5);
            var ship2 = new MixedShip(Direction.down, 5);

            Assert.IsFalse(ship == ship2);

        }

        [Test]
        public void CheckOverloadingOperatorNotEqualDifLength()
        {
            var ship = new BattleShip(Direction.down, 4);
            var ship2 = new BattleShip(Direction.down, 5);

            Assert.IsFalse(ship != ship2);

        }
    }
}