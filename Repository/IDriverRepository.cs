using FoodDeliveryapi.Models;
using FoodDeliveryapi.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FoodDeliveryapi.Repository
{
    public interface IDriverRepository
    {
        Task DeleteDrivers(int driverid, SQLConnectionFactory sqlConnectionFactory);
        Task<Driver> GetDriverId(int id, SQLConnectionFactory sqlConnectionFactory);
        Task<IReadOnlyList<Driver>> GetDrivers(SQLConnectionFactory sqlConnectionFactory);
        Task<int> InsertDrivers(Driver Entity, SQLConnectionFactory sqlConnectionFactory);
        Task<int> UpdateDrivers(int driverid, Driver Entity, SQLConnectionFactory sqlConnectionFactory);
    }
}