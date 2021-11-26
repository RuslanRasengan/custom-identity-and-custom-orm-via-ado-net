using System;
using System.Collections.Generic;
using System.Linq;

namespace Warship.Models
{
    public sealed class Area
    {
        public int Id { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        readonly int MinX;
        readonly int MaxX;
        readonly int MaxY;
        readonly int MinY;

        private List<Ship> ships = new List<Ship>();

        public int Time { get; set; }

        const int radius = 1;

        public Area(int width, int height)
        {
            this.Width = width;
            this.Height = height;
            if (this.Width % 2 == 0)
            {
                this.MaxX = width / 2;
                this.MinX = this.MaxX * -1;
            }

            else
            {
                this.MaxX = (width + 1) / 2;
                this.MinX = (width + 1) / 2 - this.Width;
            }

            if(this.Height % 2 == 0)
            {
                this.MaxY = height / 2;
                this.MinY = this.MaxY * -1;
            }
            else
            {
                this.MaxY = (height + 1) / 2;
                this.MinY = (height + 1) / 2 - this.Height;
            }
        }

        public List<Ship> ShowShipsPlacement()
        {
            return this.ships.OrderBy(s => Math.Sqrt(Math.Pow(s.Point.X, 2) + Math.Pow(s.Point.Y, 2))).ToList();

        }

        public bool AddShip(Ship ship, Point placementPoint)
        {
            if(!IsItPossibleToPlaceShip(placementPoint, ship))
            {
                return false;
            }
            ship.Point = placementPoint;
            this.ships.Add(ship);
            return true;
        }

        public bool MoveShip(Ship ship, int time)
        {
            if(ship.Point == null)
            {
                return false;
            }

            var distance = ship.Speed * time;
            var newPositionShip = new Point();

            this.ships = this.ships.Where(s => s.Point.X != ship.Point.X && s.Point.Y != ship.Point.Y).ToList();

            if (ship.Direction == Direction.up)
            {
                newPositionShip.Y = ship.Point.Y + distance;
            }
            if (ship.Direction == Direction.down)
            {
                newPositionShip.Y = ship.Point.Y - distance;
            }

            if (ship.Direction == Direction.left)
            {
                newPositionShip.X = ship.Point.X - distance;
            }
            if (ship.Direction == Direction.right)
            {
                newPositionShip.X = ship.Point.X + distance;
            }
            if (AddShip(ship, newPositionShip))
            {
                return true;
            }
            this.ships.Add(ship);
            return false;
        }

        private bool IsItPossibleToPlaceShip(Point placementPoint, Ship placeableShip)
        {
            int startPlaceableShip, endPlaceableShip;
            GetBeginningAndEndOfShip(placementPoint, placeableShip.Direction, placeableShip.Length, radius, out startPlaceableShip, out endPlaceableShip);

            if (placementPoint.X < this.MinX || placementPoint.X >= this.MaxX
                || placementPoint.Y < MinY && placementPoint.Y >= this.MaxY)
            {
                return false;
            }

            if (startPlaceableShip + radius < 0
                || (endPlaceableShip - radius <= this.MinX && placeableShip.Direction == Direction.right)
                || (endPlaceableShip + radius >= this.MaxX && placeableShip.Direction == Direction.left)
                || (endPlaceableShip - radius <= this.MinY && placeableShip.Direction == Direction.up)
                || (endPlaceableShip + radius >= this.MaxY && placeableShip.Direction == Direction.down))
            {
                return false;
            }

            var isItPossibleToPlaceShip = true;
            foreach (var ship in this.ships)
            {
                isItPossibleToPlaceShip = IsShipsCross(
                    placementPoint,
                    placeableShip,
                    ship,
                    startPlaceableShip,
                    endPlaceableShip);
                if (!isItPossibleToPlaceShip)
                {
                    break;
                }
            }
            return isItPossibleToPlaceShip;
        }

        private void GetBeginningAndEndOfShip(Point placementPoint, Direction direction, int length, int radius, out int start, out int end)
        {
            if(direction == Direction.left)
            {
                start = placementPoint.X - radius;
                end = placementPoint.X + length;
                return;
            }

            if (direction == Direction.right)
            {
                start = placementPoint.X - length;
                end = placementPoint.X + radius;
                return;
            }

            if (direction == Direction.up)
            {
                start = placementPoint.Y + radius;
                end = placementPoint.Y - length;
                return;
            }

            start = placementPoint.Y + length;
            end = placementPoint.Y - radius;
        }

        private bool IsShipsCross(
            Point placementPoint,
            Ship placeableShip,
            Ship placedShip,
            int startPlaceableShip,
            int endPlaceableShip)
        {
            int startPlacedShip, endPlacedShip;
            GetBeginningAndEndOfShip(placedShip.Point, placedShip.Direction, placedShip.Length, radius, out startPlacedShip, out endPlacedShip);
            if (IsShipsPerpendicular(placeableShip.Direction, placedShip.Direction))
            {
                if (placeableShip.Direction == Direction.up || placeableShip.Direction == Direction.down)
                {
                    return ForPerpendicularShips(
                    placementPoint,
                    placedShip.Point,
                    startPlaceableShip,
                    endPlaceableShip,
                    startPlacedShip,
                    endPlacedShip);
                }
                else
                {
                    return ForPerpendicularShips(
                    placedShip.Point,
                    placementPoint,
                    startPlacedShip,
                    endPlacedShip,
                    startPlaceableShip,
                    endPlaceableShip);
                }
            }

            return ForParallelShips(
            placementPoint,
            placeableShip,
            placedShip,
            startPlaceableShip,
            endPlaceableShip,
            startPlacedShip,
            endPlacedShip);
        }

        private bool IsShipsPerpendicular(Direction firstShipDirection, Direction secondSihpDirection)
        {
            return (firstShipDirection == Direction.down || firstShipDirection == Direction.up)
                && (secondSihpDirection == Direction.left || secondSihpDirection == Direction.right)
                || (secondSihpDirection == Direction.down || secondSihpDirection == Direction.up)
                && (firstShipDirection == Direction.left || firstShipDirection == Direction.right);
        }

        private bool ForParallelShips(
            Point placementPoint,
            Ship placeableShip,
            Ship placedShip,
            int startPlaceableShip,
            int endPlaceableShip,
            int startPlacedShip,
            int endPlacedShip)
        {
            bool isAllowedDistance = false;

            if (placeableShip.Direction == Direction.left || placeableShip.Direction == Direction.right
                &&
                placedShip.Direction == Direction.left || placedShip.Direction == Direction.right
                )
            {
                isAllowedDistance = Math.Abs(placedShip.Point.Y - placementPoint.Y) > radius;
            }
            else
            {
                isAllowedDistance = Math.Abs(placedShip.Point.X - placementPoint.X) > radius;
            }


            if (startPlacedShip > startPlaceableShip && endPlaceableShip > endPlacedShip && !isAllowedDistance)
            {
                return false;
            }
            if (startPlacedShip <= startPlaceableShip && startPlaceableShip <= endPlacedShip && !isAllowedDistance)
            {
                return false;
            }
            if (startPlacedShip <= endPlaceableShip && endPlaceableShip <= endPlacedShip && !isAllowedDistance)
            {
                return false;
            }
            return true;
        }

        private bool ForPerpendicularShips(
            Point verticalShip,
            Point horizontalShip,
            int startVertical,
            int endVertical,
            int startHorizontal,
            int endHorizontal)
        {
            if (startHorizontal > verticalShip.X || verticalShip.X > endHorizontal)
            {
                return true;
            }
            if (startVertical > horizontalShip.Y || horizontalShip.Y > endVertical)
            {
                return true;
            }
            return false;
        }

        private Point CalculateCoordinate(int quadrant, int x, int y)
        {
            if(quadrant == 2)
            {
                return new Point(-x, y);
            }
            if (quadrant == 3)
            {
                return new Point(-x, -y);
            }
            if (quadrant == 4)
            {
                return new Point(x, -y);
            }
            return new Point(x, y);
        }

        public Ship this[int quadrant, int x, int y]
        {
            get
            {
                var coordinates = CalculateCoordinate(quadrant, x, y);

                return this.ships.Where(s => s.Point.X == coordinates.X && s.Point.Y == coordinates.Y).FirstOrDefault();
            }
        }
    }
}