using Dapper;
using FoodDeliveryapi.Models;
using FoodDeliveryapi.Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodDeliveryapi.Repository
{
    public class AddressRepository : IAddressRepository
    {

        public AddressRepository()
        {
        }
        public async Task<IReadOnlyList<Address>> GetAddresses(SQLConnectionFactory sqlConnectionFactory)
        {
            using var con = sqlConnectionFactory.Create();
            {
                string queryString = @"SELECT * FROM Address";
                var result = await con.QueryAsync<Address>(queryString);
                return result.ToList();
            }
        }
        public async Task<Address> GetAddressId(int id, SQLConnectionFactory sqlConnectionFactory)
        {
            using var con = sqlConnectionFactory.Create();
            {
                string queryString = @"SELECT * FROM Address WHERE addressr_id=@id";
                var result = await con.QueryAsync<Address>(queryString, new { id = id });
                return result.FirstOrDefault();
            }
        }
        public async Task<int> InsertAddress(Address Entity, SQLConnectionFactory sqlConnectionFactory)
        {
            using var con = sqlConnectionFactory.Create();
            {
                string queryString = @"INSERT INTO [dbo].[Address]
                                           ([user_id]
                                           ,[state]
                                           ,[city]
                                           ,[street]
                                           ,[pincode])
                                     VALUES
                                           (@userid
                                           ,@state
                                           ,@city
                                           ,@street
                                           ,@pincode);
                                     SELECT CAST(SCOPE_IDENTITY() AS INT)";
                var Result = (await con.QueryAsync(queryString, Entity)).Single();
                return Result;
            }
        }
        public async Task<int> UpdateAddress(int addressid, Address Entity, SQLConnectionFactory sqlConnectionFactory)
        {
            using var con = sqlConnectionFactory.Create();
            {
                string queryString = @"UPDATE [dbo].[Address]
                                       SET [user_id] = @userid
                                          ,[state] = @state
                                          ,[city] = @city
                                          ,[street] = @street
                                          ,[pincode] = @pincode
                                     WHERE [address_id] = @addressid";
                var Result = (await con.QueryAsync(queryString, new { userid = Entity.userid, state = Entity.state, city = Entity.city, street = Entity.street, pincode = Entity.pincode, addressid = addressid })).Single();
                return Result;
            }
        }
        public async Task DeleteAddress(int addressid, SQLConnectionFactory sqlConnectionFactory)
        {
            using var con = sqlConnectionFactory.Create();
            {
                string queryString = @"DELETE FROM [dbo].[Address]
                                       WHERE [address_id] = @addressid";
                await con.ExecuteAsync(queryString, new { addressid = addressid });
            }
        }
    }
}
