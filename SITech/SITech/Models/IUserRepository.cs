using SITech.DTO;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace SITech.Models
{
    interface IUserRepository<T> where T : class
    {
        IQueryable<UserWrapper> GetAll(bool isCustomer);
        Customer FindById(string id);
        IdentityResult Create(T model, HttpPostedFileBase file = null);
        Task<IdentityResult> Update(EditCustomerViewModel model, HttpPostedFileBase file = null);
        void Delete(int id);
        void ChangeRoles(string UserId, string RoleName);
    }
}