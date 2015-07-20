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
                    ItemType = inv.ItemType
                });
            }
            return MenuItemViewModels;
        }

        public IEnumerable<MenuItemViewModel> GetAll(string customerId)
        {
            var menuItemViewModels = new List<MenuItemViewModel>();
            foreach (var inv in db.MenuItems.Where(item => customerId != null && (item.IsActive && item.CustomerId == customerId)))
            {
                menuItemViewModels.Add(new MenuItemViewModel
                {
                    MenuItemId = inv.MenuItemId,
                    CustomerId = inv.CustomerId,
                    ItemName = inv.ItemName,
                    ItemPrice = inv.ItemPrice,
                    ItemType = inv.ItemType
                });
            }
            return menuItemViewModels;
        }

        public IEnumerable<MenuItemViewModel> GetAllUnactive(string customerId)
        {
            var MenuItemViewModels = new List<MenuItemViewModel>();
            foreach (var inv in db.MenuItems.Where(item => customerId != null && (item.IsActive == false && item.CustomerId == customerId)))
            {
                MenuItemViewModels.Add(new MenuItemViewModel
                {
                    MenuItemId = inv.MenuItemId,
                    CustomerId = inv.CustomerId,
                    ItemName = inv.ItemName,
                    ItemPrice = inv.ItemPrice,
                    ItemType = inv.ItemType
                });
            }
            return MenuItemViewModels;
        }
        
        public MenuItemViewModel Get(int id)
    {
        var inv = db.MenuItems.Where(item => item.MenuItemId == id).FirstOrDefault();
            if(inv != null)
            {
                return new MenuItemViewModel
                {
                    MenuItemId = inv.MenuItemId,
                    CustomerId = inv.CustomerId,
                    ItemName = inv.ItemName,
                    ItemPrice = inv.ItemPrice,
                    ItemType = inv.ItemType
                };
            }
            return null;
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
                IsActive = model.IsActive
            };


            db.MenuItems.Add(inv);
            db.SaveChanges();
        }

        public void Update(MenuItemViewModel rate)
    {
        db.Entry(rate).State = EntityState.Modified;

            db.SaveChanges();
        }

        public void Activate(int Id)
        {
            var menuItem = db.MenuItems.Where(item => item.MenuItemId == Id).FirstOrDefault();

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