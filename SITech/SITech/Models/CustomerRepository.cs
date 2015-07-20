using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using SITech.App_Start;
using SITech.Models;
using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;
using SITech.DTO;

namespace SITech.Models
{
    public class CustomerRepository : IUserRepository<CustomerViewModel>
    {

    private ApplicationDbContext db;
    private UserManager<ApplicationUser> UserManager =
           new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));

    public CustomerRepository(ApplicationDbContext context)
    {
        this.db = context;

    }


    public IQueryable<UserWrapper> GetAll(bool isCustomer = false)
    {
        string RoleName = "PDP";
        List<Customer> customers = db.Customers.Where(c => !c.User.IsActive).AsParallel().ToList();

        if (isCustomer)
        {
            RoleName = "Customer";
            customers = db.Customers.Where(c => c.User.IsActive).AsParallel().ToList();
        }

        var usersWrap = new List<UserWrapper>();
        foreach (Customer customer in customers)
        {
            IList<string> rolesForUser = UserManager.GetRoles(customer.User.Id);
            usersWrap.AddRange(from role in rolesForUser
                               where UserManager.IsInRole(customer.User.Id, RoleName)
                               select new UserWrapper
                               {
                                   UserId = customer.User.Id,
                                   UserName = customer.User.UserName,
                                   MiddleName = customer.User.MiddleName,
                                   FirstName = customer.User.FirstName,
                                   LastName = customer.User.LastName,
                                   Email = customer.User.Email,
                                   RoleName = role,
                                   IsActive = customer.User.IsActive
                               });
        }

        return usersWrap.AsQueryable();
    }

    public Customer FindById(string id)
    {
        return db.Customers.Find(id);
    }

    public IdentityResult Create(CustomerViewModel model, HttpPostedFileBase file)
    {
        if (model.MiddleName == null)
        {
            model.MiddleName = "    ";
        }

        var user = new ApplicationUser
        {
            UserName = model.UserName,
            FirstName = model.FirstName,
            MiddleName = model.MiddleName,
            LastName = model.LastName,
            Email = model.Email,
            EmailConfirmed = true,
            IsActive = (model.RoleName != "Customer") ? false : true
            //IsActive = false
        };


        IdentityResult result = UserManager.Create(user, model.Password);

        if (result.Succeeded)
        {
            IdentityResult roleAdd = UserManager.AddToRole(user.Id, model.RoleName);

            
                db.Customers.Add(
                    new Customer
                    {
                        CustomerId = user.Id

                    });
                db.SaveChanges();

        }
        return result;
    }

    public async Task<IdentityResult> Update(EditCustomerViewModel model, HttpPostedFileBase file)
    {
               ApplicationUser user = UserManager.FindById(model.CustomerId);

                user.UserName = model.UserName;
                user.FirstName = model.FirstName;
                user.MiddleName = model.MiddleName;
                user.LastName = model.LastName;
                user.Email = model.Email;
                user.IsActive = (model.RoleName != "Customer") ? false : true;

                IdentityResult result = await UserManager.UpdateAsync(user);

                if (result.Succeeded)
                {
                    if (!UserManager.IsInRole(user.Id, model.RoleName))
                    {
                        IList<string> rolesForUser = await UserManager.GetRolesAsync(user.Id);

                        if (rolesForUser.Any())
                        {
                            foreach (string item in rolesForUser)
                            {
                                // item should be the name of the role
                                IdentityResult remove = await UserManager.RemoveFromRoleAsync(user.Id, item);
                            }
                            IdentityResult roleAdd = await UserManager.AddToRoleAsync(user.Id, model.RoleName);
                        }
                    }
                      var customer = db.Customers.Find(user.Id);


                        db.Entry(customer).State = EntityState.Modified;
                        db.SaveChanges();
                    
                }
                return result;
    }

    public string GetRole(string Id) 
    {
        return UserManager.GetRoles(Id).First();    
    }
 
    public void Delete(int id)
    {

    }

    #region return Json

    public void ChangeRoles(string UserId, string RoleName)
    {
        var customer = db.Customers.FirstOrDefault(c => c.CustomerId == UserId);
        if (customer.User.IsActive)
        {
            customer.User.IsActive = false;
            IdentityResult result = UserManager.RemoveFromRole(customer.CustomerId, RoleName);
            if (result.Succeeded)
            {
                UserManager.AddToRole(customer.CustomerId, "PDP");

            }
        }
        else
        {
            customer.User.IsActive = true;
            IdentityResult result = UserManager.RemoveFromRole(customer.CustomerId, RoleName);
            if (result.Succeeded)
            {
                UserManager.AddToRole(customer.CustomerId, "Customer");

            }

        }
        db.SaveChanges();
    }

    #endregion

    }
}