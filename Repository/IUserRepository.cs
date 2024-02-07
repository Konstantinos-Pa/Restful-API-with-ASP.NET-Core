using FoodDeliveryapi.Models;
using FoodDeliveryapi.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FoodDeliveryapi.Repository
{
    public interface IUserRepository
    {
        Task DeleteUsers(int userid, SQLConnectionFactory sqlConnectionFactory);
        Task<User> GetUserId(int id, SQLConnectionFactory sqlConnectionFactory);
        Task<IReadOnlyList<User>> GetUsers(SQLConnectionFactory sqlConnectionFactory);
        Task<int> InsertUsers(User Entity, SQLConnectionFactory sqlConnectionFactory);
        Task UpdateUsers(int userid, User Entity, SQLConnectionFactory sqlConnectionFactory);
    }
}