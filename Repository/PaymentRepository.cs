using Dapper;
using FoodDeliveryapi.Models;
using FoodDeliveryapi.Services;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodDeliveryapi.Repository
{
    public class PaymentRepository : IPaymentRepository
    {

        public PaymentRepository()
        {
        }
        public async Task<IReadOnlyList<Payment>> GetPayments(SQLConnectionFactory sqlConnectionFactory)
        {
            using var con = sqlConnectionFactory.Create();
            {
                string queryString = @"SELECT * FROM Payment";
                var result = await con.QueryAsync<Payment>(queryString);
                return result.ToList();
            }
        }
        public async Task<Payment> GetPaymentId(int id, SQLConnectionFactory sqlConnectionFactory)
        {
            using var con = sqlConnectionFactory.Create();
            {
                string queryString = @"SELECT * FROM Payment WHERE payment_id=@id";
                var result = await con.QueryAsync<Payment>(queryString, new { id = id });
                return result.FirstOrDefault();
            }
        }
        public async Task<int> InsertPayment(Payment Entity, SQLConnectionFactory sqlConnectionFactory)
        {
            using var con = sqlConnectionFactory.Create();
            {
                string queryString = @"INSERT INTO [dbo].[Payment]
                                           ([order_id]
                                           ,[payment_method]
                                           ,[amount]
                                           ,[status])
                                     VALUES
                                           (@orderid
                                           ,@paymentmethod
                                           ,@amount
                                           ,@status);
                                     SELECT CAST(SCOPE_IDENTITY() AS INT)";
                int Result = (await con.QueryAsync(queryString, Entity)).Single();
                return Result;
            }
        }
        public async Task<int> UpdatePayment(int paymentid, Payment Entity, SQLConnectionFactory sqlConnectionFactory)
        {
            using var con = sqlConnectionFactory.Create();
            {
                string queryString = @"UPDATE [dbo].[Payment]
                                       SET [order_id] = @orderid
                                          ,[payment_method] = @paymentmethod
                                          ,[amount] = @amount
                                          ,[status] = @status
                                     WHERE [payment_id] = @paymentid";
                int Result = (await con.QueryAsync(queryString, new { orderid = Entity.orderid, paymentmethod = Entity.paymentmethod, amount = Entity.amount, status = Entity.status, paymentid = paymentid })).Single();
                return Result;
            }
        }
        public async Task DeletePayment(int paymentid, SQLConnectionFactory sqlConnectionFactory)
        {
            using var con = sqlConnectionFactory.Create();
            {
                string queryString = @"UPDATE [dbo].[Payment]
                                       WHERE [payment_id] = @paymentid";
                await con.ExecuteAsync(queryString, new { paymentid = paymentid });
            }
        }
    }
}
