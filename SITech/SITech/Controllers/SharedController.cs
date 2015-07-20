using SITech.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SITech.DTO;
using SITech.Models;

namespace SITech.Controllers
{
    public class SharedController : Controller
    {
        public ActionResult _SideBar()
        {
            IEnumerable<MenuItemViewModel> model = new List<MenuItemViewModel>();
            MenuItemRepository inventoryRepository = new MenuItemRepository(new ApplicationDbContext());
            
            var firstOrDefault = new ApplicationDbContext().Users.FirstOrDefault(cus => cus.UserName == User.Identity.Name);
            if (firstOrDefault !=
                null)
            {
                var customerId = firstOrDefault.Id;
                model = inventoryRepository.GetAll(customerId);
                return PartialView(model);
            }


            return PartialView(model);
        }
    }
}