using DataAccess.Interfaces;
using Entities.Ships;
using System.Collections.Generic;

namespace DataAccess.Repositories
{
    public class PointRepository : IRepository<Point>
    {
        protected readonly Context<Point> db;

        public PointRepository(string connectionString)
        {
            this.db = new Context<Point>(connectionString);
        }

        public void Add(Point item)
        {
            db.Create(item);
        }

        public List<Point> GetAll()
        {
            return db.GetAll();
        }

        public Point GetById(int id)
        {
            return db.GetById(id);
        }

        public void Remove(Point item)
        {
            db.Delete(item);
        }

        public void Update(Point item)
        {
            db.Update(item);
        }
    }
}
