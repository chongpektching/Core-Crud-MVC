using Core_Crud_MVC.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core_Crud_MVC.Services
{
    public class LoginManager : SignInManager<ApplicationUser>
    {
        public LoginManager(
            Microsoft.AspNetCore.Identity.UserManager<ApplicationUser> userManager,
            Microsoft.AspNetCore.Http.IHttpContextAccessor contextAccessor,
            Microsoft.AspNetCore.Identity.IUserClaimsPrincipalFactory<ApplicationUser> claimsFactory,
            Microsoft.Extensions.Options.IOptions<Microsoft.AspNetCore.Identity.IdentityOptions> optionsAccessor,
            Microsoft.Extensions.Logging.ILogger<Microsoft.AspNetCore.Identity.SignInManager<ApplicationUser>> logger,
            Microsoft.AspNetCore.Authentication.IAuthenticationSchemeProvider schemes,
            Microsoft.AspNetCore.Identity.IUserConfirmation<ApplicationUser> confirmation)
            : base(userManager, contextAccessor, claimsFactory, optionsAccessor, logger, schemes, confirmation)
        {
        }

       
    }
}
