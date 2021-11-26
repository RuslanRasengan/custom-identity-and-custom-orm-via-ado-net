using CustomORM.BusinessLogic;
using System.Collections.Generic;

namespace DataAccess
{
    public class Context<T>
    {
        private readonly DbQueries dbQueries;

        public Context(string connectionString)
        {
            this.dbQueries = new DbQueries(connectionString);
        }
        public void Create(T item)
        {
            dbQueries.AddData(item);
        }
        public void Delete(T item)
        {
            dbQueries.Remove(item);
        }

        public List<T> GetAll()
        {
            return dbQueries.GetAll<T>();
        }

        public T GetById(int id)
        {
            return dbQueries.GetById<T>(id);
        }

        public void Update(T item)
        {
            dbQueries.UpdateData(item);
        }
    }
}
