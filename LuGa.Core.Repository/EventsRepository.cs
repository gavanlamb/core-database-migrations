using LuGa.Core.Device.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using MySql.Data.MySqlClient;

namespace LuGa.Core.Repository
{
    /// <summary>
    /// Repository for Events.
    /// </summary>
    public class EventsRepository : IRepository<Event>
    {
        /// <summary>
        /// Connection string
        /// </summary>
        private readonly string connectionString;
        
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="connectionString">Connection string</param>
        public EventsRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        /// <summary>
        /// Mysql connection client
        /// </summary>
        public MySqlConnection Connection => new MySqlConnection(connectionString);

        /// <summary>
        /// Add Event
        /// </summary>
        /// <param name="row">Event object to save</param>
        public async Task Add(Event row)
        {
            using (MySqlConnection dbConnection = Connection)
            {
                const string sQuery = "INSERT INTO Events (DeviceId, Action, Zone, Value, TimeStamp) VALUES(@DeviceId, @Action, @Zone, @Value, @TimeStamp)";
                await dbConnection.OpenAsync();
                await dbConnection.ExecuteAsync(sQuery, row);
            }
        }

        /// <summary>
        /// Retrieve all Events
        /// </summary>
        /// <returns>List of Events</returns>
        public async Task<IEnumerable<Event>> GetTopHundred()
        {
            using (MySqlConnection dbConnection = Connection)
            {
                const string sQuery = "SELECT * FROM events ORDER BY TimeStamp DESC LIMIT 100";
                await dbConnection.OpenAsync();
                return await dbConnection.QueryAsync<Event>(sQuery);
            }
        }

        /// <summary>
        /// Get particular event
        /// </summary>
        /// <param name="id">Id of event to find</param>
        /// <returns>Found event or null</returns>
        public async Task<Event> GetById(int id)
        {
            using (MySqlConnection dbConnection = Connection)
            {
                const string sQuery = "SELECT * FROM Events WHERE Id = @Id";
                await dbConnection.OpenAsync();
                return await dbConnection.QueryFirstAsync<Event>(sQuery, new { Id = id });
            }
        }
    }
}
