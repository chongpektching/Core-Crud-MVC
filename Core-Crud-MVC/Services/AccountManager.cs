using Core_Crud_MVC.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Configuration;
using Dapper;

namespace Core_Crud_MVC.Services
{
    public class AccountManager : UserManager<ApplicationUser>
    {
        private ConnectionStringsConfig _connectionStrings;

        public AccountManager(
            IUserStore<ApplicationUser> store,
            IOptions<IdentityOptions> optionsAccessor,
            IPasswordHasher<ApplicationUser> passwordHasher,
            IEnumerable<IUserValidator<ApplicationUser>> userValidators,
            IEnumerable<IPasswordValidator<ApplicationUser>> passwordValidators,
            ILookupNormalizer keyNormalizer,
            IdentityErrorDescriber errors,
            IServiceProvider services,
            ILogger<UserManager<ApplicationUser>> logger,
            IOptionsSnapshot<ConnectionStringsConfig> connectionStrings)
            : base(store, optionsAccessor, passwordHasher, userValidators, passwordValidators, keyNormalizer, errors, services, logger)
        {
            _connectionStrings = connectionStrings?.Value ?? throw new ArgumentNullException(nameof(connectionStrings));
        }

        public override async Task<IdentityResult> CreateAsync(ApplicationUser user)
        {
            using (SqlConnection con = new SqlConnection(_connectionStrings.DefaultConnection))
            {
                await con.OpenAsync();

                string sql = "INSERT INTO [dbo].[User] " +
                "VALUES (@id, @UserName, @PasswordHash, @Email)";

                int rows = await con.ExecuteAsync(sql, new { user.Id, user.Email, user.EmailConfirmed, user.PasswordHash, user.UserName });

                if (rows > 0)
                {
                    return IdentityResult.Success;
                }
                return IdentityResult.Failed(new IdentityError { Description = $"Could not insert user {user.Email}." });
            }
        }

        public override async Task<string> GetUserNameAsync(ApplicationUser user)
        {
            using (SqlConnection con = new SqlConnection(_connectionStrings.DefaultConnection))
            {
                await con.OpenAsync();

                var parameters = new DynamicParameters();

                parameters.Add("UserName", user.UserName);
                parameters.Add("Password", user.PasswordHash);

                string sql = "SELECT UserName FROM [dbo].[User] WHERE UserName = @UserName AND PasswordHash = @Password;";

                var result = await con.QueryAsync<string>(sql, parameters);

                return result.FirstOrDefault();
            }
        }
    }
}
