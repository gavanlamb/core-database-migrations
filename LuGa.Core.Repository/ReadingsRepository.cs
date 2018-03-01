using LuGa.Core.Device.Models;
using System.Collections.Generic;
using System.Linq;
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
        public void Add(Reading row)
        {
            using (MySqlConnection dbConnection = Connection)
            {
                const string sQuery = "INSERT INTO Readings (ReadingType, DeviceId, Value, TimeStamp) VALUES(@ReadingType, @DeviceId, @Value, @TimeStamp)";
                dbConnection.OpenAsync();
                dbConnection.Execute(sQuery, row);
            }
        }

        /// <summary>
        /// Retrieve all Readings
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Reading> GetAll()
        {
            using (MySqlConnection dbConnection = Connection)
            {
                const string sQuery = "SELECT * FROM Readings";
                dbConnection.OpenAsync();
                return dbConnection.Query<Reading>(sQuery);
            }
        }

        /// <summary>
        /// Get particular reading
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Reading GetById(int id)
        {
            using (MySqlConnection dbConnection = Connection)
            {
                const string sQuery = "SELECT * FROM Readings WHERE Id = @Id";
                dbConnection.OpenAsync();
                return dbConnection.Query<Reading>(sQuery, new { Id = id }).FirstOrDefault();
            }
        }
    }
}
