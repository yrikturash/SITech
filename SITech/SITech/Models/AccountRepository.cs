using SITech.DTO;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace SITech.Models
{
    public class AccountRepository : IAccountRepository<CustomerViewModel>
    {
    private ApplicationDbContext db;
    private UserManager<ApplicationUser> UserManager =
           new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));

    public AccountRepository(ApplicationDbContext context)
    {
        this.db = context;

    }

    public async Task<CreateAccountDTO> Register(CustomerViewModel model, HttpPostedFileBase file = null)
        {
             if (model.MiddleName == null)
                {
                    model.MiddleName = "    ";
                }
                var user = new ApplicationUser() 
                    { 
                    UserName = model.UserName, 
                    FirstName = model.FirstName, 
                    MiddleName = model.MiddleName,
                    LastName = model.LastName,
                    Email = model.Email,
                    EmailConfirmed = true,
                    IsActive = true
                    };
               
             var result = await UserManager.CreateAsync(user, model.Password);
             
            // var userManager = new UserManager<Microsoft.AspNet.Identity.EntityFramework.IdentityUser>(new UserStore<IdentityUser>(new ApplicationDbContext()));

             if (result.Succeeded)
             {
                 //    var roleAdd = await UserManager.AddToRoleAsync(user.Id, "Clerk");
                 //    var code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                 //    var callbackUrl = Url.Action(
                 //       "ConfirmEmail", "Account",
                 //       new { userId = user.Id, code = code },
                 //       protocol: Request.Url.Scheme);

                 //    await UserManager.SendEmailAsync(user.Id,
                 //       "Confirm your account",
                 //       "Please confirm your account by clicking this link: <a href=\""
                 //                                       + callbackUrl + "\">link</a>");
                 //    // ViewBag.Link = callbackUrl;   // Used only for initial demo.
                 //    return View("DisplayEmail");
                 UserManager.AddToRole(user.Id, "Customer");

                 db.SaveChanges();
             }
             return new CreateAccountDTO() { Result = result, User = user };
        }

    public void CreateRoles()
    {
        var roleManager = new RoleManager<Microsoft.AspNet.Identity.EntityFramework.IdentityRole>(new RoleStore<IdentityRole>(new ApplicationDbContext()));
        if (!roleManager.RoleExists("Customer"))
            roleManager.Create(new IdentityRole("Customer"));
        if (!roleManager.RoleExists("PDP"))
            roleManager.Create(new IdentityRole("PDP"));
        if (!roleManager.RoleExists("Admin"))
        {
            roleManager.Create(new IdentityRole("Admin"));

            var user = new ApplicationUser()
            {
                UserName = "admin",
                FirstName = "Admin ",
                LastName = "Admin",
                Email = "admin@email.com",
                EmailConfirmed = true,
                IsActive = true
            };

            UserManager.Create(user, "Password1");

            UserManager.AddToRole(user.Id, "Admin");

        }
    }



    }
}