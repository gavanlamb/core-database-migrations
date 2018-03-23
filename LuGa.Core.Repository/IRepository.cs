using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace LuGa.Core.Repository
{
    /// <summary>
    /// Repository interface
    /// </summary>
    /// <typeparam name="T">Type this repository is for</typeparam>
    public interface IRepository<T>
    {
        /// <summary>
        /// DB connection object
        /// </summary>
        MySqlConnection Connection { get; }

        /// <summary>
        /// Add single item
        /// </summary>
        /// <param name="row"></param>
        Task Add(T row);

        /// <summary>
        /// Retrieve top 100 items ordering by the timestamp
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<T>> GetTopHundred();

        /// <summary>
        /// Get item by the specified Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<T> GetById(int id);

        /// <summary>
        /// Generic query method
        /// </summary>
        /// <typeparam name="R"></typeparam>
        /// <param name="query"></param>
        /// <returns></returns>
        Task<R> Query<R>(string query);
    }
}
