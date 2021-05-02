using Core_Crud_MVC.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Core_Crud_MVC.Services
{
    //public class UserStoreManager : UserStore<UserClass, ApplicationRole, ApplicationDbContext, Guid>
    //{
    //    public UserStoreManager(ApplicationDbContext context, IdentityErrorDescriber describer = null)
    //        : base(context, describer)
    //    {
    //    }
    //}

    public class UserStoreManager<UserClass> : UserStore<UserClass>
        where UserClass : IdentityUser<string>, new()
    {
        public UserStoreManager(ApplicationDbContext context, IdentityErrorDescriber describer = null) : base(context, describer)
        {
        }

        private DbSet<UserClass> UsersSet { get { return Context.Set<UserClass>(); } }

        public override Task<UserClass> FindByNameAsync(string userName, CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();
            return Users.FirstOrDefaultAsync(u => u.UserName == userName, cancellationToken);
        }

        public override IQueryable<UserClass> Users
        {
            get { return UsersSet; }
        }
    }
}
