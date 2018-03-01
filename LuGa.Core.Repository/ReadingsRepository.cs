using LuGa.Core.Device.Models;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Dapper;

namespace LuGa.Core.Repository
{
    public class ReadingsRepository : IRepository<Reading>
    {
        
        private readonly string connectionString;
        
        public ReadingsRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public IDbConnection Connection => new SqlConnection(connectionString);
        
        public void Add(Reading row)
        {
            using (IDbConnection dbConnection = Connection)
            {
                const string sQuery = "INSERT INTO Readings (ReadingType, DeviceId, Value, TimeStamp) VALUES(@ReadingType, @DeviceId, @Value, @TimeStamp)";
                dbConnection.Open();
                dbConnection.Execute(sQuery, row);
            }
        }

        public IEnumerable<Reading> GetAll()
        {
            using (IDbConnection dbConnection = Connection)
            {
                const string sQuery = "SELECT * FROM Readings";
                dbConnection.Open();
                return dbConnection.Query<Reading>(sQuery);
            }
        }

        public Reading GetById(int id)
        {
            using (IDbConnection dbConnection = Connection)
            {
                const string sQuery = "SELECT * FROM Readings WHERE Id = @Id";
                dbConnection.Open();
                return dbConnection.Query<Reading>(sQuery, new { Id = id }).FirstOrDefault();
            }
        }
    }
}
