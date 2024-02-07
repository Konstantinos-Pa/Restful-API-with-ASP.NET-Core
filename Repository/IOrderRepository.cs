using FoodDeliveryapi.Models;
using FoodDeliveryapi.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FoodDeliveryapi.Repository
{
    public interface IOrderRepository
    {
        Task DeleteOrders(int orderid, SQLConnectionFactory sqlConnectionFactory);
        Task<Order> GetOrderId(int id, SQLConnectionFactory sqlConnectionFactory);
        Task<IReadOnlyList<Order>> GetOrders(SQLConnectionFactory sqlConnectionFactory);
        Task<int> InsertOrders(Order Entity, SQLConnectionFactory sqlConnectionFactory);
        Task<int> UpdateOrders(int Orderid, Order Entity, SQLConnectionFactory sqlConnectionFactory);
    }
}