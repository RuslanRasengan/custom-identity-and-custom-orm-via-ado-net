using System.Collections.Generic;

namespace DataAccess.Interfaces
{
    public interface IRepository<T>
        where T : class
    {
        List<T> GetAll();
        T GetById(int id);
        void Update(T item);
        void Add(T item);
        void Remove(T item);
    }
}
