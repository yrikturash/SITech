using SITech.DTO;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace SITech.Models
{
    public class BeverageInventoryRepository : IDataRepository<BeverageInventoryViewModel>
    {
        private ApplicationDbContext db;

        public BeverageInventoryRepository(ApplicationDbContext context)
    {
        this.db = context;
    }

        public IEnumerable<BeverageInventoryViewModel> GetAll()
        {
            List<BeverageInventoryViewModel> inventoryViewModels = new List<BeverageInventoryViewModel>();
            foreach (var inv in db.BeverageInventories)
            {
                inventoryViewModels.Add(new BeverageInventoryViewModel
                {
                    Id = inv.Id,
                    MenuItemId = inv.MenuItemId,
                    Price = inv.Price,
                    ProductName = inv.ProductName,
                    Vendor = inv.Vendor,
                    Volume = inv.Volume,
                    Age = inv.Age
                    
                });
            }
            return inventoryViewModels;
        }

        public IEnumerable<BeverageInventoryViewModel> GetAll(int menuItemId)
    {
        List<BeverageInventoryViewModel> inventoryViewModels = new List<BeverageInventoryViewModel>();
        foreach (var inv in db.BeverageInventories)
        {
            inventoryViewModels.Add(new BeverageInventoryViewModel
            {
                Id = inv.Id,
                MenuItemId = inv.MenuItemId,
                Price = inv.Price,
                ProductName = inv.ProductName,
                Vendor = inv.Vendor,
                Volume = inv.Volume,
                Age = inv.Age
            });
        }
        return inventoryViewModels;
    }

        public List<BeverageInventoryViewModel> GetAllByUsername(int MenuItemId)
        {
            var inventoryViewModels = new List<BeverageInventoryViewModel>();
            foreach (var inv in db.BeverageInventories.Where(item => item.MenuItemId == MenuItemId))
            {
                inventoryViewModels.Add(new BeverageInventoryViewModel
                {
                    Id = inv.Id,
                    MenuItemId = inv.MenuItemId,
                    Price = inv.Price,
                    ProductName = inv.ProductName,
                    Vendor = inv.Vendor,
                    Volume = inv.Volume,
                    Age = inv.Age
                });
            }
            return inventoryViewModels;
        }

        public BeverageInventoryViewModel Get(int id)
    {
        var inv = db.BeverageInventories.Where(item => item.Id == id).FirstOrDefault();
            if(inv != null)
            {
                return new BeverageInventoryViewModel
                {
                    Id = inv.Id,
                    MenuItemId = inv.MenuItemId,
                    Price = inv.Price,
                    ProductName = inv.ProductName,
                    Vendor = inv.Vendor,
                    Volume = inv.Volume,
                    Age = inv.Age
                };
            }
            return null;
    }

        public void Create(BeverageInventoryViewModel model)
        {
            BeverageInventory inv = new BeverageInventory
            {
                MenuItemId = model.MenuItemId,
                DrinkType = model.DrinkType,
                Price = model.Price,
                Producer = model.Producer,
                ProductName = model.ProductName,
                Age = model.Age
            };

            db.BeverageInventories.Add(inv);

            db.SaveChanges();
        }

        public void Update(BeverageInventoryViewModel rate)
        {
            db.Entry(rate).State = EntityState.Modified;

            db.SaveChanges();
        }
 
    public void Delete(int id)
    {
        BeverageInventory rate = db.BeverageInventories.Find(id);
            if (rate != null)
            {
                db.BeverageInventories.Remove(rate);
                db.SaveChanges();
            }
        }

    }
}