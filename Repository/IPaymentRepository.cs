using FoodDeliveryapi.Models;
using FoodDeliveryapi.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FoodDeliveryapi.Repository
{
    public interface IPaymentRepository
    {
        Task DeletePayment(int paymentid, SQLConnectionFactory sqlConnectionFactory);
        Task<Payment> GetPaymentId(int id, SQLConnectionFactory sqlConnectionFactory);
        Task<IReadOnlyList<Payment>> GetPayments(SQLConnectionFactory sqlConnectionFactory);
        Task<int> InsertPayment(Payment Entity, SQLConnectionFactory sqlConnectionFactory);
        Task<int> UpdatePayment(int paymentid, Payment Entity, SQLConnectionFactory sqlConnectionFactory);
    }
}