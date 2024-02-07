using Dapper;
using FoodDeliveryapi.Models;
using FoodDeliveryapi.Services;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodDeliveryapi.Repository
{
    public class OrderRepository : IOrderRepository
    {

        public OrderRepository(IConfiguration configuration)
        {
        }
        public async Task<IReadOnlyList<Order>> GetOrders(SQLConnectionFactory sqlConnectionFactory)
        {
            using var con = sqlConnectionFactory.Create();
            {
                string queryString = @"SELECT * FROM Orders";
                var result = await con.QueryAsync<Order>(queryString);
                return result.ToList();
            }
        }
        public async Task<Order> GetOrderId(int id, SQLConnectionFactory sqlConnectionFactory)
        {
            using var con = sqlConnectionFactory.Create();
            {
                string queryString = @"SELECT * FROM Order WHERE order_id=@id";
                var result = await con.QueryAsync<Order>(queryString, new { id = id });
                return result.FirstOrDefault();
            }
        }
        public async Task<int> InsertOrders(Order Entity, SQLConnectionFactory sqlConnectionFactory)
        {
            using var con = sqlConnectionFactory.Create();
            {
                string queryString = @"INSERT INTO [dbo].[Orders]
                                           ([user_id]
                                           ,[restaurant_id]
                                           ,[order_total]
                                           ,[delivery_status])
                                     VALUES
                                           (@user_id
                                           ,@restaurant_id
                                           ,@order_total
                                           ,@delivery_status);
                                    SELECT CAST(SCOPE_IDENTITY() AS INT)";
                var Result = (await con.QueryAsync(queryString, Entity)).Single();
                return Result;
            }
        }
        public async Task<int> UpdateOrders(int Orderid, Order Entity, SQLConnectionFactory sqlConnectionFactory)
        {
            using var con = sqlConnectionFactory.Create();
            {
                string queryString = @"UPDATE [dbo].[Orders]
                                       SET [user_id] = @userid
                                          ,[restaurant_id] = @restaurantid
                                          ,[order_total] = @ordertotal
                                          ,[delivery_status] = @deliverystatus
                                     WHERE [order_id] = @orderid";
                int Result = (await con.QueryAsync(queryString, new { userid = Entity.userid, restaurantid = Entity.restaurantid, ordertotal = Entity.ordertotal, deliverystatus = Entity.deliverystatus, Orderid = Orderid })).Single();
                return Result;
            }
        }
        public async Task DeleteOrders(int orderid, SQLConnectionFactory sqlConnectionFactory)
        {
            using var con = sqlConnectionFactory.Create();
            {
                string queryString = @"DELETE FROM [dbo].[Orders]
                                       WHERE [order_id] = @orderid";
                await con.ExecuteAsync(queryString, new { orderid = orderid });
            }
        }
    }
}
