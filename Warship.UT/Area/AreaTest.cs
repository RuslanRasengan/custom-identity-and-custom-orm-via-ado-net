using NUnit.Framework;
using System.Collections.Generic;
using Warship.Entities.Area;
using Warship.Entities.Enums;
using Warship.Entities.Ships;

namespace Warship.UT
{
    public class AreaTest
    {
        [SetUp]
        public void Setup()
        {
            
        }

        [Test]
        public void BorderCrossingCheckUp()
        {
            var area = new Area(10, 9);
            var ship = new Ship(Direction.up, 3);
            Assert.IsFalse(area.AddShip(ship, new Point(9, 8)));
        }

        [Test]
        public void BorderCrossingCheckDown()
        {
            var area = new Area(10, 9);
            var ship = new Ship(Direction.down, 3);
            Assert.IsFalse(area.AddShip(ship, new Point(1, 5)));
        }

        [Test]
        public void BorderCrossingCheckLeft()
        {
            var area = new Area(10, 9);
            var ship = new Ship(Direction.left, 3);
            Assert.IsFalse(area.AddShip(ship, new Point(9, 8)));
        }

        [Test]
        public void BorderCrossingCheckRight()
        {
            var area = new Area(10, 9);
            var ship = new Ship(Direction.right, 3);
            Assert.IsFalse(area.AddShip(ship, new Point(-4, 1)));
        }

        [Test]
        public void TestAddStartPositionUp()
        {
            var area = new Area(10, 9);
            var ship = new Ship(Direction.up, 3);
            Assert.IsTrue(area.AddShip(ship, new Point(1, 1)));
        }

        [Test]
        public void TestAddStartPositionDown()
        {
            var area = new Area(10, 9);
            var ship = new Ship(Direction.down, 3);
            Assert.IsFalse(area.AddShip(ship, new Point(5, 5)));
        }

        [Test]
        public void TestAddStartPositionLeft()
        {
            var area = new Area(10, 9);
            var ship = new Ship(Direction.left, 3);
            Assert.IsTrue(area.AddShip(ship, new Point(0, 0)));
        }

        [Test]
        public void TestAddStartPositionRight()
        {
            var area = new Area(10, 9);
            var ship = new Ship(Direction.right, 3);
            Assert.IsFalse(area.AddShip(ship, new Point(0, 0)));
        }

        [Test]
        public void TestAddWithTouch()
        {
            var area = new Area(10, 9);
            var ship = new Ship(Direction.left, 3);
            var ship2 = new Ship(Direction.right, 3);
            area.AddShip(ship, new Point(4, 4));
            Assert.IsFalse(area.AddShip(ship2, new Point(5, 4)));
        }

        [Test]
        public void TestAddParalellTouch()
        {
            var area = new Area(10, 9);
            var ship = new Ship(Direction.right, 3);
            var ship2 = new Ship(Direction.right, 3);
            area.AddShip(ship, new Point(4, 4));
            Assert.IsFalse(area.AddShip(ship2, new Point(4, 5)));
        }

        [Test]
        public void TestAddPerpendicularTouch()
        {
            var area = new Area(10, 9);
            var ship = new Ship(Direction.right, 3);
            var ship2 = new Ship(Direction.down, 3);
            area.AddShip(ship, new Point(4, 4));
            Assert.IsFalse(area.AddShip(ship2, new Point(4, 7)));
        }

        [Test]
        public void TestAddWitoutTouch()
        {
            var area = new Area(10, 9);
            var ship = new Ship(Direction.right, 2);
            var ship2 = new Ship(Direction.left, 2);
            area.AddShip(ship, new Point(1, 1));
            Assert.IsTrue(area.AddShip(ship2, new Point(1, 3)));
        }

        [Test]
        public void TestAddParalellNotTouch()
        {
            var area = new Area(10, 9);
            var ship = new Ship(Direction.right, 3);
            var ship2 = new Ship(Direction.right, 3);
            area.AddShip(ship, new Point(4, 4));
            Assert.IsTrue(area.AddShip(ship2, new Point(4, 6)));
        }

        [Test]
        public void TestAddPerpendicularNotTouch()
        {
            var area = new Area(10, 9);
            var ship = new Ship(Direction.right, 3);
            var ship2 = new Ship(Direction.down, 3);
            area.AddShip(ship, new Point(1, 1));
            Assert.IsTrue(area.AddShip(ship2, new Point(1, 2)));
        }

        [Test]
        public void Have—ommonHead()
        {
            var area = new Area(15, 15);
            var ship = new Ship(Direction.right, 3);
            var ship2 = new Ship(Direction.left, 3);
            area.AddShip(ship, new Point(3, 3));
            Assert.IsFalse(area.AddShip(ship2, new Point(3, 3)));
        }

        [Test]
        public void VerticallyTouch()
        {
            var area = new Area(15, 15);
            var ship = new Ship(Direction.up, 3);
            var ship2 = new Ship(Direction.left, 5);
            area.AddShip(ship, new Point(4, 4));
            Assert.IsFalse(area.AddShip(ship2, new Point(5, 5)));
        }

        [Test]
        public void VerticallyNotTouch()
        {
            var area = new Area(10, 10);
            var ship = new Ship(Direction.up, 5);
            var ship2 = new Ship(Direction.down, 5);
            area.AddShip(ship, new Point(1, 3));
            Assert.IsTrue(area.AddShip(ship2, new Point(3, 1)));
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
    }
}