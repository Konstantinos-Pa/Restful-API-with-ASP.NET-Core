using FoodDeliveryapi.Models;
using FoodDeliveryapi.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FoodDeliveryapi.Repository
{
    public interface IAddressRepository
    {
        Task DeleteAddress(int addressid, SQLConnectionFactory sqlConnectionFactory);
        Task<IReadOnlyList<Address>> GetAddresses(SQLConnectionFactory sqlConnectionFactory);
        Task<Address> GetAddressId(int id, SQLConnectionFactory sqlConnectionFactory);
        Task<int> InsertAddress(Address Entity, SQLConnectionFactory sqlConnectionFactory);
        Task<int> UpdateAddress(int addressid, Address Entity, SQLConnectionFactory sqlConnectionFactory);
    }
}