using FoodDeliveryapi.Models;
using FoodDeliveryapi.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FoodDeliveryapi.Repository
{
    public interface IMenuRepository
    {
        Task DeleteMenu(int menuid, SQLConnectionFactory sqlConnectionFactory);
        Task<Menu> GetMenuId(int id, SQLConnectionFactory sqlConnectionFactory);
        Task<IReadOnlyList<Menu>> GetMenus(SQLConnectionFactory sqlConnectionFactory);
        Task<int> InsertMenu(Menu Entity, SQLConnectionFactory sqlConnectionFactory);
        Task<int> UpdateMenu(int menuid, Menu Entity, SQLConnectionFactory sqlConnectionFactory);
    }
}