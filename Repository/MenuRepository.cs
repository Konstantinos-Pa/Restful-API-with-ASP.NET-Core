using Dapper;
using FoodDeliveryapi.Models;
using FoodDeliveryapi.Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodDeliveryapi.Repository
{
    public class MenuRepository : IMenuRepository
    {

        public MenuRepository()
        {
        }
        public async Task<IReadOnlyList<Menu>> GetMenus(SQLConnectionFactory sqlConnectionFactory)
        {

            using var con = sqlConnectionFactory.Create();
            {
                string queryString = @"SELECT * FROM Menu";
                var result = await con.QueryAsync<Menu>(queryString);
                return result.ToList();
            }
        }
        public async Task<Menu> GetMenuId(int id, SQLConnectionFactory sqlConnectionFactory)
        {

            using var con = sqlConnectionFactory.Create();
            {
                string queryString = @"SELECT * FROM Menu WHERE menu_id=@id";
                var result = await con.QueryAsync<Menu>(queryString, new { id = id });
                return result.FirstOrDefault();
            }
        }
        public async Task<int> InsertMenu(Menu Entity, SQLConnectionFactory sqlConnectionFactory)
        {

            using var con = sqlConnectionFactory.Create();
            {
                string queryString = @"INSERT INTO [dbo].[Menu]
                                           ([restaurant_id]
                                           ,[item_name]
                                           ,[price])
                                     VALUES
                                           (@restaurantid
                                           ,@itemname
                                           ,@price);
                                     SELECT CAST(SCOPE_IDENTITY() AS INT)";
                var Result = (await con.QueryAsync(queryString, Entity)).Single();
                return Result;
            }
        }
        public async Task<int> UpdateMenu(int menuid, Menu Entity, SQLConnectionFactory sqlConnectionFactory)
        {

            using var con = sqlConnectionFactory.Create();
            {
                string queryString = @"UPDATE [dbo].[Menu]
                                       SET [restaurantid] = @restaurantid
                                          ,[item_name] = @itemname
                                          ,[price] = @price
                                     WHERE [menu_id] = @menuid";
                int Result = (await con.QueryAsync(queryString, new { restaurantid = Entity.restaurantid, itemname = Entity.itemname, price = Entity.price, menuid = menuid })).Single();
                return Result;
            }
        }
        public async Task DeleteMenu(int menuid, SQLConnectionFactory sqlConnectionFactory)
        {

            using var con = sqlConnectionFactory.Create();
            {
                string queryString = @"DELETE FROM [dbo].[Menu]
                                       WHERE [menu_id] = @menuid";
                await con.ExecuteAsync(queryString, new { menuid = menuid });
            }
        }
    }
}
