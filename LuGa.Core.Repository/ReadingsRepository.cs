using LuGa.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using MySql.Data.MySqlClient;

namespace LuGa.Core.Repository
{
    /// <summary>
    ///  Repository for Readings.
    /// </summary>
    public class ReadingsRepository : IRepository<Reading>
    {
        /// <summary>
        /// Connection string
        /// </summary>
        private readonly string connectionString;
        
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="connectionString">Connection string </param>
        public ReadingsRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        /// <summary>
        /// Mysql connection client
        /// </summary>
        public MySqlConnection Connection => new MySqlConnection(connectionString);
        
        /// <summary>
        /// Add Reading
        /// </summary>
        /// <param name="row"></param>
        public async Task Add(Reading row)
        {
            using (MySqlConnection dbConnection = Connection)
            {
                const string sQuery = "INSERT INTO Readings (ReadingType, DeviceId, Value, TimeStamp) VALUES(@ReadingType, @DeviceId, @Value, @TimeStamp)";
                await dbConnection.OpenAsync();
                await dbConnection.ExecuteAsync(sQuery, row);
            }
        }

        /// <summary>
        /// Retrieve all Readings
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Reading>> GetTopHundred()
        {
            using (MySqlConnection dbConnection = Connection)
            {
                const string sQuery = "SELECT * FROM Readings ORDER BY TimeStamp DESC LIMIT 100";
                await dbConnection.OpenAsync();
                return await dbConnection.QueryAsync<Reading>(sQuery);
            }
        }

        /// <summary>
        /// Get particular reading
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Reading> GetById(int id)
        {
            using (MySqlConnection dbConnection = Connection)
            {
                const string sQuery = "SELECT * FROM Readings WHERE Id = @Id";
                await dbConnection.OpenAsync();
                return await dbConnection.QueryFirstAsync<Reading>(sQuery, new { Id = id });;  
            }
        }

        /// <summary>
        /// Generic query method
        /// </summary>
        /// <typeparam name="R"></typeparam>
        /// <param name="query"></param>
        public async Task<R> Query<R>(string query)
        {
            using (MySqlConnection dbConnection = Connection)
            {
                await dbConnection.OpenAsync();
                return await dbConnection.QueryFirstAsync<R>(query);
            }
        }
    }
}
