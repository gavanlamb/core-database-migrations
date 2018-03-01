using LuGa.Core.Device.Models;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Dapper;

namespace LuGa.Core.Repository
{
    public class EventsRepository : IRepository<Event>
    {
        private readonly string connectionString;
        
        public EventsRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public IDbConnection Connection => new SqlConnection(connectionString);

        public void Add(Event row)
        {
            using (IDbConnection dbConnection = Connection)
            {
                const string sQuery = "INSERT INTO Events (DeviceId, Action, Zone, Value, TimeStamp) VALUES(@DeviceId, @Action, @Zone, @Value, @TimeStamp)";
                dbConnection.Open();
                dbConnection.Execute(sQuery, row);
            }
        }

        public IEnumerable<Event> GetAll()
        {
            using (IDbConnection dbConnection = Connection)
            {
                const string sQuery = "SELECT * FROM Events";
                dbConnection.Open();
                return dbConnection.Query<Event>(sQuery);
            }
        }

        public Event GetById(int id)
        {
            using (IDbConnection dbConnection = Connection)
            {
                const string sQuery = "SELECT * FROM Events WHERE Id = @Id";
                dbConnection.Open();
                return dbConnection.Query<Event>(sQuery, new { Id = id }).FirstOrDefault();
            }
        }
    }
}
