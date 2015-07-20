using SITech.DTO;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace SITech.Models
{
    interface IAccountRepository<T> where T : class
    {
        Task<CreateAccountDTO> Register(T model, HttpPostedFileBase file = null);
        void CreateRoles();
      
    }
}