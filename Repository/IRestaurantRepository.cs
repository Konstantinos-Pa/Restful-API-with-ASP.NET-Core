using FoodDeliveryapi.Models;
using FoodDeliveryapi.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FoodDeliveryapi.Repository
{
    public interface IRestaurantRepository
    {
        Task DeleteRestaurants(int restaurantid, SQLConnectionFactory sqlConnectionFactory);
        Task<Restaurant> GetRestaurantId(int id, SQLConnectionFactory sqlConnectionFactory);
        Task<IReadOnlyList<Restaurant>> GetRestaurants(SQLConnectionFactory sqlConnectionFactory);
        Task<int> InstertRestaurants(Restaurant Entity, SQLConnectionFactory sqlConnectionFactory);
        Task<int> UpdateRestaurants(int restaurantid, Restaurant Entity, SQLConnectionFactory sqlConnectionFactory);
    }
}