using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Generic CRUD contract shared by all repositories
// Deliberately kept minimal — domain-specific queries belong on the specific interfaces below so this stays reusable for any entity type

namespace Evidence_Locker.Core.Interfaces
{
    public interface IRepository<T> where T : class
    {
        T? GetById(int id);
        IEnumerable<T> GetAll();
        void Add(T entity);
        void Update(T entity);
        void Delete(int id);
    }
}
