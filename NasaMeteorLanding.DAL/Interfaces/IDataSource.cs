using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NasaMeteorLanding.DAL.Interfaces
{
    public interface IDataSource<T> where T : class
    {
        Task<IEnumerable<T>> Data { get; }
    }
}
