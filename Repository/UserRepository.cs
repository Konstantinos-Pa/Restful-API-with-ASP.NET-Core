using Dapper;
using FoodDeliveryapi.Models;
using FoodDeliveryapi.Services;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodDeliveryapi.Repository
{
    public class UserRepository : IUserRepository
    {

        public UserRepository()
        {
        }

        public async Task<IReadOnlyList<User>> GetUsers(SQLConnectionFactory sqlConnectionFactory)
        {
            using var con = sqlConnectionFactory.Create();
            {
                string queryString = @"SELECT * FROM Users";
                var result = await con.QueryAsync<User>(queryString);
                return result.ToList();
            }
        }

        public async Task<User> GetUserId(int id, SQLConnectionFactory sqlConnectionFactory)
        {
            using var con = sqlConnectionFactory.Create();
            {
                string queryString = @"SELECT * FROM Users WHERE user_id=@id";
                var result = await con.QueryAsync<User>(queryString, new { id = id });
                return result.FirstOrDefault();
            }
        }
        public async Task<int> InsertUsers(User Entity, SQLConnectionFactory sqlConnectionFactory)
        {
            using var con = sqlConnectionFactory.Create();
            {
                if (Entity.name == null || Entity.email == null || Entity.password == null || Entity.phone == 0)
                {

                }
                string queryString = @"INSERT INTO  Users (name,email,password,phone) VALUES (@name,@email,@password,@phone);
                                SELECT CAST(SCOPE_IDENTITY() AS INT)";

                int Result = (await con.QueryAsync<int>(queryString, Entity)).Single();
                return Result;
            }
        }
        public async Task UpdateUsers(int userid, User Entity, SQLConnectionFactory sqlConnectionFactory)
        {
            using var con = sqlConnectionFactory.Create();
            {
                string queryString = @"UPDATE [dbo].[Users]
                                           SET [name] = @name
                                              ,[email] = @email
                                              ,[password] = @password
                                              ,[phone] = @phone
                                         WHERE [user_id] = @userid";
                await con.ExecuteAsync(queryString, new { name = Entity.name, email = Entity.email, password = Entity.password, phone = Entity.phone, userid = userid });
            }
        }
        public async Task DeleteUsers(int userid, SQLConnectionFactory sqlConnectionFactory)
        {
            using var con = sqlConnectionFactory.Create();
            {
                string queryString = @"DELETE FROM [dbo].[Users]
                                        WHERE [user_id] = @userid";
                await con.ExecuteAsync(queryString, new { userid = userid });
            }
        }
    }
}
