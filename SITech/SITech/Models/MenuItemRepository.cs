using SITech.DTO;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace SITech.Models
{
    public class MenuItemRepository : IDataRepository<MenuItemViewModel>
    {
        private ApplicationDbContext db;

        public MenuItemRepository(ApplicationDbContext context)
        {
            this.db = context;
        }

        public IEnumerable<MenuItemViewModel> GetAll()
        {
            var MenuItemViewModels = new List<MenuItemViewModel>();
            foreach (var inv in db.MenuItems.Where(item => item.IsActive == true))
            {
                MenuItemViewModels.Add(new MenuItemViewModel
                {
                    MenuItemId = inv.MenuItemId,
                    CustomerId = inv.CustomerId,
                    ItemName = inv.ItemName,
                    ItemPrice = inv.ItemPrice,
                    ItemType = inv.ItemType,
                    MenuPrice = inv.MenuPrice,
                    Profit = inv.Profit,
                    IsActive = inv.IsActive,
                    BeverageIds = inv.BeverageIds,
                    InventoryIds = inv.InventoryIds
                });
            }
            return MenuItemViewModels;
        }

        public IEnumerable<MenuItemViewModel> GetByItemType(string itemType, bool isActive = true,
            string customerId = null)
        {
            var menuItemViewModels = new List<MenuItemViewModel>();
            if (customerId != null)
            {
                menuItemViewModels =
                    db.MenuItems.Where(
                        item => item.IsActive == isActive && item.ItemType == itemType && item.CustomerId == customerId)
                        .Select(n =>
                            new MenuItemViewModel
                            {
                                MenuItemId = n.MenuItemId,
                                CustomerId = n.CustomerId,
                                ItemName = n.ItemName,
                                ItemPrice = n.ItemPrice,
                                ItemType = n.ItemType,
                                MenuPrice = n.MenuPrice,
                                Profit = n.Profit,
                                IsActive = n.IsActive,
                                BeverageIds = n.BeverageIds,
                                InventoryIds = n.InventoryIds
                            }).ToList();
            }
            else
            {
                menuItemViewModels =
                    db.MenuItems.Where(item => item.IsActive == isActive && item.ItemType == itemType).Select(n =>
                        new MenuItemViewModel
                        {
                            MenuItemId = n.MenuItemId,
                            CustomerId = n.CustomerId,
                            ItemName = n.ItemName,
                            ItemPrice = n.ItemPrice,
                            ItemType = n.ItemType,
                            MenuPrice = n.MenuPrice,
                            Profit = n.Profit,
                            IsActive = n.IsActive,
                            BeverageIds = n.BeverageIds,
                            InventoryIds = n.InventoryIds
                        }).ToList();
            }
            return menuItemViewModels;
        }

        public IEnumerable<MenuItemViewModel> GetAllByCustomerId(string customerId)
        {
            var MenuItemViewModels = new List<MenuItemViewModel>();
            foreach (var inv in db.MenuItems.Where(item => item.IsActive == true && item.CustomerId == customerId))
            {
                MenuItemViewModels.Add(new MenuItemViewModel
                {
                    MenuItemId = inv.MenuItemId,
                    CustomerId = inv.CustomerId,
                    ItemName = inv.ItemName,
                    ItemPrice = inv.ItemPrice,
                    ItemType = inv.ItemType,
                    MenuPrice = inv.MenuPrice,
                    Profit = inv.Profit,
                    IsActive = inv.IsActive,
                    BeverageIds = inv.BeverageIds,
                    InventoryIds = inv.InventoryIds
                });
            }
            return MenuItemViewModels;
        }

        public IEnumerable<MenuItemViewModel> GetAll(string customerId)
        {
            var menuItemViewModels = new List<MenuItemViewModel>();
            foreach (
                var inv in
                    db.MenuItems.Where(item => customerId != null && (item.IsActive && item.CustomerId == customerId)))
            {
                menuItemViewModels.Add(new MenuItemViewModel
                {
                    MenuItemId = inv.MenuItemId,
                    CustomerId = inv.CustomerId,
                    ItemName = inv.ItemName,
                    ItemPrice = inv.ItemPrice,
                    ItemType = inv.ItemType,
                    MenuPrice = inv.MenuPrice,
                    Profit = inv.Profit,
                    IsActive = inv.IsActive,
                    BeverageIds = inv.BeverageIds,
                    InventoryIds = inv.InventoryIds
                });
            }
            return menuItemViewModels;
        }

        public IEnumerable<MenuItemViewModel> GetAllUnactive(string customerId)
        {
            var MenuItemViewModels = new List<MenuItemViewModel>();
            foreach (
                var inv in
                    db.MenuItems.Where(
                        item => customerId != null && (item.IsActive == false && item.CustomerId == customerId)))
            {
                MenuItemViewModels.Add(new MenuItemViewModel
                {
                    MenuItemId = inv.MenuItemId,
                    CustomerId = inv.CustomerId,
                    ItemName = inv.ItemName,
                    ItemPrice = inv.ItemPrice,
                    ItemType = inv.ItemType,
                    MenuPrice = inv.MenuPrice,
                    Profit = inv.Profit,
                    IsActive = inv.IsActive,
                    BeverageIds = inv.BeverageIds,
                    InventoryIds = inv.InventoryIds
                });
            }
            return MenuItemViewModels;
        }

        public MenuItemViewModel Get(int id)
        {
            var inv = db.MenuItems.FirstOrDefault(item => item.MenuItemId == id);
            if (inv != null)
            {
                return new MenuItemViewModel
                {
                    MenuItemId = inv.MenuItemId,
                    CustomerId = inv.CustomerId,
                    ItemName = inv.ItemName,
                    ItemPrice = inv.ItemPrice,
                    ItemType = inv.ItemType,
                    MenuPrice = inv.MenuPrice,
                    Profit = inv.Profit,
                    IsActive = inv.IsActive,
                    BeverageIds = inv.BeverageIds,
                    InventoryIds = inv.InventoryIds
                };
            }
            return null;
        }

        public MenuItem GetById(int id)
        {
            return db.MenuItems.FirstOrDefault(item => item.MenuItemId == id);
        }

        public void Create(MenuItemViewModel model)
        {
            MenuItem inv = new MenuItem
            {
                MenuItemId = model.MenuItemId,
                CustomerId = model.CustomerId,
                ItemName = model.ItemName,
                ItemPrice = model.ItemPrice,
                ItemType = model.ItemType,
                IsActive = model.IsActive,
                MenuPrice = model.MenuPrice,
                Profit = model.Profit,
                BeverageIds = model.BeverageIds,
                InventoryIds = model.InventoryIds
            };


            db.MenuItems.Add(inv);
            db.SaveChanges();
        }

        public void Update(MenuItemViewModel rate)
        {
            var item = new MenuItem()
            {
                MenuItemId = rate.MenuItemId,
                CustomerId = rate.CustomerId,
                ItemName = rate.ItemName,
                ItemPrice = rate.ItemPrice,
                ItemType = rate.ItemType,
                IsActive = rate.IsActive,
                MenuPrice = rate.MenuPrice,
                Profit = rate.Profit,
                BeverageIds = rate.BeverageIds,
                InventoryIds = rate.InventoryIds
            };
            db.Entry(item).State = EntityState.Modified;

            db.SaveChanges();
        }

        public void Update(MenuItem model)
        {
            db.Entry(model).State = EntityState.Modified;

            db.SaveChanges();
        }

        public void Activate(int Id)
        {
            var menuItem = db.MenuItems.FirstOrDefault(item => item.MenuItemId == Id);

            if (menuItem != null)
            {
                menuItem.IsActive = true;
                db.Entry(menuItem).State = EntityState.Modified;

                db.SaveChanges();
            }
        }

        public void DeActivate(int Id)
        {
            var menuItem = db.MenuItems.FirstOrDefault(item => item.MenuItemId == Id);

            if (menuItem != null)
            {
                menuItem.IsActive = false;
                db.Entry(menuItem).State = EntityState.Modified;

                db.SaveChanges();
            }
        }

        public void Delete(int id)
        {
            MenuItem rate = db.MenuItems.Find(id);
            if (rate != null)
            {
                db.MenuItems.Remove(rate);
                db.SaveChanges();
            }
        }
    }
}