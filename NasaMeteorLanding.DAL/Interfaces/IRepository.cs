using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NasaMeteorLanding.DAL.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAll();
        //T Get(int id);
        //IEnumerable<T> Find(Func<T, Boolean> predicate);
        //void Create(T item);
        //void Update(T item);
        //void Delete(int id);
    }
}
