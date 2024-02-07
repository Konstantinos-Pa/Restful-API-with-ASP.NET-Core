using Dapper;
using FoodDeliveryapi.Models;
using FoodDeliveryapi.Services;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodDeliveryapi.Repository
{
    public class RestaurantRepository : IRestaurantRepository
    {

        public RestaurantRepository()
        {
        }
        public async Task<IReadOnlyList<Restaurant>> GetRestaurants(SQLConnectionFactory sqlConnectionFactory)
        {
            using var con = sqlConnectionFactory.Create();
            {
                string queryString = @"SELECT * FROM Restaurants";
                var result = await con.QueryAsync<Restaurant>(queryString);
                return result.ToList();
            }
        }
        public async Task<Restaurant> GetRestaurantId(int id, SQLConnectionFactory sqlConnectionFactory)
        {
            using var con = sqlConnectionFactory.Create();
            {
                string queryString = @"SELECT * FROM Restaurant WHERE restaurant_id=@id";
                var result = await con.QueryAsync<Restaurant>(queryString, new { id = id });
                return result.FirstOrDefault();
            }
        }
        public async Task<int> InstertRestaurants(Restaurant Entity, SQLConnectionFactory sqlConnectionFactory)
        {
            using var con = sqlConnectionFactory.Create();
            {
                string queryString = @"INSERT INTO Restaurants (name,adress,phone) VALUES (@name,@adress,@phone);
                                        SELECT CAST(SCOPE_IDENTITY() AS INT)";
                int Result = (await con.QueryAsync(queryString, Entity)).Single();
                return Result;
            }
        }
        public async Task<int> UpdateRestaurants(int restaurantid, Restaurant Entity, SQLConnectionFactory sqlConnectionFactory)
        {
            using var con = sqlConnectionFactory.Create();
            {
                string queryString = @"UPDATE [dbo].[Restaurants]
                                       SET [name] = @name
                                          ,[address] = @address
                                          ,[phone] = @phone
                                     WHERE [restaurant_id] = @restaurantid";
                int Result = (await con.QueryAsync(queryString, new { name = Entity.name, adress = Entity.address, phone = Entity.phone, restaurantid = restaurantid })).Single();
                return Result;
            }
        }
        public async Task DeleteRestaurants(int restaurantid, SQLConnectionFactory sqlConnectionFactory)
        {
            using var con = sqlConnectionFactory.Create();
            {
                string queryString = @"DELETE FROM [dbo].[Restaurants]
                                       WHERE [restaurant_id] = @restaurantid";
                await con.ExecuteAsync(queryString, new { restaurantid = restaurantid });
            }
        }
    }
}
