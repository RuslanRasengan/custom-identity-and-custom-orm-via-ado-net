using DataAccess.Interfaces;
using Entities.Ships;
using System.Collections.Generic;

namespace DataAccess.Repositories
{
    public class ShipRepository : IRepository<Ship>
    {
        protected readonly Context<Ship> db;

        public ShipRepository(string connectionString)
        {
            this.db = new Context<Ship>(connectionString);
        }

        public void Add(Ship item)
        {
            db.Create(item);
        }

        public List<Ship> GetAll()
        {
            return db.GetAll();
        }

        public Ship GetById(int id)
        {
            return db.GetById(id);
        }

        public void Remove(Ship item)
        {
            db.Delete(item);
        }

        public void Update(Ship item)
        {
            db.Update(item);
        }
    }
}
