using DataAccess.Interfaces;
using Entities.Area;
using Entities.Ships;
using System.Collections.Generic;

namespace DataAccess.Repositories
{
    public class AreaRepository : IRepository<Area>
    {
        protected readonly Context<Area> db;

        public AreaRepository(string connectionString)
        {
            this.db = new Context<Area>(connectionString);
        }

        public void Add(Area item)
        {
            db.Create(item);
        }

        public List<Area> GetAll()
        {
            return db.GetAll();
        }

        public Area GetById(int id)
        {
            return db.GetById(id);
        }

        public void Remove(Area item)
        {
            db.Delete(item);
        }

        public void Update(Area item)
        {
            db.Update(item);
        }
    }
}
