using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using SITech.DTO;
using SITech.Models;

namespace SITech.Controllers
{
    public class WebAppController : Controller
    {
        private readonly UnitOfWork _unitOfWorks;

        public WebAppController()
        {
            _unitOfWorks = new UnitOfWork();
        }

        // GET: WebApp
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult MenuItems()
        {
            IEnumerable<MenuItemViewModel> model = new List<MenuItemViewModel>();
            MenuItemRepository inventoryRepository = new MenuItemRepository(new ApplicationDbContext());
            string customerId = null;
            var firstOrDefault = new ApplicationDbContext().Users.FirstOrDefault(cus => cus.UserName == User.Identity.Name);


            if (firstOrDefault !=
                null)
            {
                customerId = firstOrDefault.Id;
                model = _unitOfWorks.MenuItems.GetAllByCustomerId(customerId);
                return View(model);
            }


            return View(model);
        }

        public ActionResult UnactiveMenuItems()
        {
            IEnumerable<MenuItemViewModel> model = new List<MenuItemViewModel>();
            MenuItemRepository inventoryRepository = new MenuItemRepository(new ApplicationDbContext());
            string customerId = null;
            var firstOrDefault = new ApplicationDbContext().Users.FirstOrDefault(cus => cus.UserName == User.Identity.Name);
            if (firstOrDefault !=
                null)
            {
                customerId = firstOrDefault.Id;
                model = inventoryRepository.GetAllUnactive(customerId);
                return View(model);
            }


            return View(model);
        }

        public ActionResult AddMenuItem()
        {
            var model = new MenuItemViewModel();

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddMenuItem(MenuItemViewModel model)
        {
            if (ModelState.IsValid)
            {
                MenuItemRepository inventoryRepository = new MenuItemRepository(new ApplicationDbContext());
                string customerId = null;
                var firstOrDefault = new ApplicationDbContext().Users.FirstOrDefault(cus => cus.UserName == User.Identity.Name);
                if (firstOrDefault !=
                    null)
                {
                    customerId = firstOrDefault.Id;
                }
                model.CustomerId = customerId;
                model.IsActive = true;
                inventoryRepository.Create(model);
                return RedirectToAction("MenuItems");
            }
            return View(model);
        }

        public void ActivateMenuItems(List<string> menuItemList)
        {
            MenuItemRepository menuItemRepository = new MenuItemRepository(new ApplicationDbContext());

            foreach (var menuItem in menuItemList)
            {
                menuItemRepository.Activate(Convert.ToInt32(menuItem));
            }
        }

        public void DectivateMenuItems(List<string> menuItemList)
        {
            MenuItemRepository menuItemRepository = new MenuItemRepository(new ApplicationDbContext());

            foreach (var menuItem in menuItemList)
            {
                menuItemRepository.DeActivate(Convert.ToInt32(menuItem));
            }
        }
        public ActionResult BeverageInventories()
        {
            BeverageInventoryRepository inventoryRepository = new BeverageInventoryRepository(new ApplicationDbContext());

            var model = inventoryRepository.GetAll();

            return View(model);
        }

        public ActionResult AddBeverageInventory()
        {
            var model = new BeverageInventoryViewModel();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddBeverageInventory(BeverageInventoryViewModel model)
        {

            if (ModelState.IsValid)
            {
                BeverageInventoryRepository inventoryRepository = new BeverageInventoryRepository(new ApplicationDbContext());
                inventoryRepository.Create(model);
                return RedirectToAction("BeverageInventories");
            }
            return View(model);
        }

        public ActionResult Inventories()
        {
            InventoryRepository inventoryRepository = new InventoryRepository(new ApplicationDbContext());

            var model = inventoryRepository.GetAll();

            return View(model);
        }

        public ActionResult AddInventory()
        {
            var model = new InventoryViewModel();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddInventory(InventoryViewModel model)
        {

            if (ModelState.IsValid)
            {
                InventoryRepository inventoryRepository = new InventoryRepository(new ApplicationDbContext());
                inventoryRepository.Create(model);
                return RedirectToAction("Inventories");
            }
            return View(model);
        }
    }
}