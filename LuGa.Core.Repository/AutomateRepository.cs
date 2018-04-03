using LuGa.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using MySql.Data.MySqlClient;
using System.Threading.Tasks;
using Dapper;

namespace LuGa.Core.Repository
{
    public class AutomateRepository : IRepository<Automate>
    {
        /// <summary>
        /// Connection string
        /// </summary>
        private readonly string connectionString;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="connectionString">Connection string</param>
        public AutomateRepository(string connectionString)
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
        /// <returns></returns>
        public async Task Add(Automate row)
        {
            using (MySqlConnection dbConnection = Connection)
            {
                const string sQuery = "INSERT INTO Automate (AverageReading, TimeStamp) VALUES(@AverageReading, @TimeStamp)";
                await dbConnection.OpenAsync();
                await dbConnection.ExecuteAsync(sQuery, row);
            }
        }

        /// <summary>
        /// Get particular automate record
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Automate> GetById(int id)
        {
            using (MySqlConnection dbConnection = Connection)
            {
                const string sQuery = "SELECT * FROM Automate WHERE Id = @Id";
                await dbConnection.OpenAsync();
                return await dbConnection.QueryFirstAsync<Automate>(sQuery, new { Id = id }); ;
            }
        }

        /// <summary>
        /// Retrieve top 100 automate records
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Automate>> GetTopHundred()
        {
            using (MySqlConnection dbConnection = Connection)
            {
                const string sQuery = "SELECT * FROM Automate ORDER BY TimeStamp DESC LIMIT 100";
                await dbConnection.OpenAsync();
                return await dbConnection.QueryAsync<Automate>(sQuery);
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
