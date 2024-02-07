using Dapper;
using FoodDeliveryapi.Models;
using FoodDeliveryapi.Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodDeliveryapi.Repository
{
    public class DriverRepository : IDriverRepository
    {

        public DriverRepository()
        {
        }
        public async Task<IReadOnlyList<Driver>> GetDrivers(SQLConnectionFactory sqlConnectionFactory)
        {

            using var con = sqlConnectionFactory.Create();
            {
                string queryString = @"SELECT * FROM Drivers";
                var result = await con.QueryAsync<Driver>(queryString);
                return result.ToList();
            }
        }
        public async Task<Driver> GetDriverId(int id, SQLConnectionFactory sqlConnectionFactory)
        {

            using var con = sqlConnectionFactory.Create();
            {
                string queryString = @"SELECT * FROM Driver WHERE driver_id=@id";
                var result = await con.QueryAsync<Driver>(queryString, new { id = id });
                return result.FirstOrDefault();
            }
        }
        public async Task<int> InsertDrivers(Driver Entity, SQLConnectionFactory sqlConnectionFactory)
        {

            using var con = sqlConnectionFactory.Create();
            {
                string queryString = @"INSERT INTO [dbo].[Drivers]
                                               ([name]
                                               ,[phone]
                                               ,[location]
                                               ,[email])
                                         VALUES
                                               (@name
                                               ,@phone
                                               ,@location
                                               ,@email);
                                        SELECT CAST(SCOPE_IDENTITY() AS INT)";
                int Result = (await con.QueryAsync(queryString, Entity)).Single();
                return Result;
            }
        }
        public async Task<int> UpdateDrivers(int driverid, Driver Entity, SQLConnectionFactory sqlConnectionFactory)
        {

            using var con = sqlConnectionFactory.Create();
            {
                string queryString = @"UPDATE [dbo].[Drivers]
                                       SET [name] = @name
                                          ,[phone] = @phone
                                          ,[location] = @location
                                          ,[email] = @email
                                     WHERE [driver_id] = @driverid";
                var Result = (await con.QueryAsync(queryString, new { name = Entity.name, phone = Entity.phone, location = Entity.location, email = Entity.email, driverid = driverid })).Single();
                return Result;
            }
        }
        public async Task DeleteDrivers(int driverid, SQLConnectionFactory sqlConnectionFactory)
        {

            using var con = sqlConnectionFactory.Create();
            {
                string queryString = @"UPDATE [dbo].[Drivers]
                                       WHERE [driver_id] = @driverid";
                await con.ExecuteAsync(queryString, new { driverid = driverid });
            }
        }
    }
}
