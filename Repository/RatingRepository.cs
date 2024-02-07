using Dapper;
using FoodDeliveryapi.Models;
using FoodDeliveryapi.Services;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodDeliveryapi.Repository
{
    public class RatingRepository : IRatingRepository
    {

        public RatingRepository(IConfiguration configuration)
        {
        }
        public async Task<IReadOnlyList<Rating>> GetRatings(SQLConnectionFactory sqlConnectionFactory)
        {
            using var con = sqlConnectionFactory.Create();
            {
                string queryString = @"SELECT * FROM Rating";
                var result = await con.QueryAsync<Rating>(queryString);
                return result.ToList();
            }
        }
        public async Task<Rating> GetRatingId(int id, SQLConnectionFactory sqlConnectionFactory)
        {
            using var con = sqlConnectionFactory.Create();
            {
                string queryString = @"SELECT * FROM Rating WHERE rating_id=@id";
                var result = await con.QueryAsync<Rating>(queryString, new { id = id });
                return result.FirstOrDefault();
            }
        }
        public async Task<int> InsertRating(Rating Entity, SQLConnectionFactory sqlConnectionFactory)
        {
            using var con = sqlConnectionFactory.Create();
            {
                string queryString = @"INSERT INTO [dbo].[Rating]
                                           ([user_id]
                                           ,[restaurant_id]
                                           ,[rating])
                                     VALUES
                                           (@userid
                                           ,@restaurantid
                                           ,@rating);
                                    SELECT CAST(SCOPE_IDENTITY() AS INT)";
                int Result = (await con.QueryAsync(queryString, Entity)).Single();
                return Result;
            }
        }
        public async Task<int> UpdateRating(int ratingid, Rating Entity, SQLConnectionFactory sqlConnectionFactory)
        {
            using var con = sqlConnectionFactory.Create();
            {
                string queryString = @"UPDATE [dbo].[Rating]
                                       SET [user_id] = @userid
                                          ,[restaurant_id] = @restaurantid
                                          ,[rating] = @rating
                                     WHERE [rating_id] = @ratingid";
                int Result = (await con.QueryAsync(queryString, new { userid = Entity.userid, restaurantid = Entity.restaurantid, rating = Entity.rating, ratingid = ratingid })).Single();
                return Result;
            }
        }
        public async Task DeleteRating(int ratingid, SQLConnectionFactory sqlConnectionFactory)
        {
            using var con = sqlConnectionFactory.Create();
            {
                string queryString = @"DELETE FROM [dbo].[Rating]
                                       WHERE [rating_id] = @ratingid";
                await con.ExecuteAsync(queryString, new { ratingid = ratingid });
            }
        }
    }
}
