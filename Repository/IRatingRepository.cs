using FoodDeliveryapi.Models;
using FoodDeliveryapi.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FoodDeliveryapi.Repository
{
    public interface IRatingRepository
    {
        Task DeleteRating(int ratingid, SQLConnectionFactory sqlConnectionFactory);
        Task<Rating> GetRatingId(int id, SQLConnectionFactory sqlConnectionFactory);
        Task<IReadOnlyList<Rating>> GetRatings(SQLConnectionFactory sqlConnectionFactory);
        Task<int> InsertRating(Rating Entity, SQLConnectionFactory sqlConnectionFactory);
        Task<int> UpdateRating(int ratingid, Rating Entity, SQLConnectionFactory sqlConnectionFactory);
    }
}