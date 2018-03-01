using System.Collections.Generic;
using System.Data;

namespace LuGa.Core.Repository
{
    public interface IRepository<T>
    {
        IDbConnection Connection { get; }

        void Add(T row);

        IEnumerable<T> GetAll();

        T GetById(int id);
    }
}
