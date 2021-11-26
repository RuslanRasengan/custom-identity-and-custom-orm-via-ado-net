using NUnit.Framework;
using System.Collections.Generic;
using Warship.Entities.Area;
using Warship.Entities.Enums;
using Warship.Entities.Ships;

namespace Warship.UT
{
    public class QuadrantTest
    {
        [SetUp]
        public void Setup()
        {
            
        }

        [Test]
        public void CheckFirstQuadrantForAnEvenArea()
        {
            var area = new Area(10, 10);
            area.AddShip(new Ship(), new Point(1, 1));
            var firstShip = area[1, 1, 1];
            Assert.IsNotNull(firstShip);
        }
        [Test]
        public void CheckSecondQuadrantForAnEvenArea()
        {
            var area = new Area(10, 10);
            area.AddShip(new Ship(), new Point(-1, 1));
            var firstShip = area[2, 1, 1];
            Assert.IsNotNull(firstShip);
        }
        [Test]
        public void CheckThirdQuadrantForAnEvenArea()
        {
            var area = new Area(10, 10);
            area.AddShip(new Ship(), new Point(-1, -1));
            var thirdShip = area[3, 1, 1];
            Assert.IsNotNull(thirdShip);
        }
        [Test]
        public void CheckForthQuadrantForAnEvenArea()
        {
            var area = new Area(10, 10);
            area.AddShip(new Ship(), new Point(2, -1));
            var fourthShip = area[4, 1, 1];
            Assert.IsNull(fourthShip);
        }

        [Test]
        public void CheckFirstQuadrantForOddArea()
        {
            var area = new Area(11, 11);
            area.AddShip(new Ship(), new Point(1, 1));
            var firstShip = area[1, 1, 1];
            Assert.IsNotNull(firstShip);

        }
        [Test]
        public void CheckSecondQuadrantForOddArea()
        {
            var area = new Area(11, 11);
            area.AddShip(new Ship(), new Point(-1, 1));
            var firstShip = area[2, 1, 1];
            Assert.IsNotNull(firstShip);
        }
        [Test]
        public void CheckThirdQuadrantForOddArea()
        {
            var area = new Area(11, 11);
            area.AddShip(new Ship(), new Point(-1, -1));
            var thirdShip = area[2, 1, 1];
            Assert.IsNull(thirdShip);
        }

        [Test]
        public void CheckForthQuadrantForOddArea()
        {
            var area = new Area(11, 11);
            area.AddShip(new Ship(), new Point(1, -1));
            var fourthShip = area[4, 1, 1];
            Assert.IsNotNull(fourthShip);
        }
        [Test]
        public void CheckQuadrantForOddAreaCenter()
        {
            var area = new Area(11, 11);
            area.AddShip(new Ship(), new Point(0, 0));
            var fourthShip = area[3, 0, 0];
            Assert.IsNotNull(fourthShip);
        }

        [Test]
        public void CheckFirstQuadrantForAsymmetryArea()
        {
            var area = new Area(10, 11);
            area.AddShip(new Ship(), new Point(1, 1));
            var fourthShip = area[1, 1, 1];
            Assert.IsNotNull(fourthShip);
        }
        [Test]
        public void CheckSecondQuadrantForAsymmetryArea()
        {
            var area = new Area(10, 11);
            area.AddShip(new Ship(), new Point(-1, 1));
            var fourthShip = area[2, 1, 1];
            Assert.IsNotNull(fourthShip);
        }
        [Test]
        public void CheckThirdQuadrantForAsymmetryArea()
        {
            var area = new Area(10, 11);
            area.AddShip(new Ship(), new Point(-1, -1));
            var fourthShip = area[3, 1, 1];
            Assert.IsNotNull(fourthShip);
        }
        [Test]
        public void CheckForthQuadrantForAsymmetryArea()
        {
            var area = new Area(10, 11);
            area.AddShip(new Ship(), new Point(1, -1));
            var fourthShip = area[4, 1, 1];
            Assert.IsNotNull(fourthShip);
        }
        [Test]
        public void CheckCommonLeftQuadrantsForAsymmetryArea()
        {
            var area = new Area(10, 11);
            area.AddShip(new Ship(), new Point(1, 1));
            var fourthShip = area[1, 1, 1];
            Assert.IsNotNull(fourthShip);
        }
        [Test]
        public void CheckCommonRightQuadrantsForAsymmetryArea()
        {
            var area = new Area(10, 11);
            var fourthShip = area[4, 0, 0];
            area.AddShip(new Ship(), new Point(5, 5));
            Assert.IsNull(fourthShip);
        }
    }
}