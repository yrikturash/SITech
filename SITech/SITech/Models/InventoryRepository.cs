using SITech.DTO;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace SITech.Models
{
    public class InventoryRepository : IDataRepository<InventoryViewModel>
    {
        private ApplicationDbContext db;

        public InventoryRepository(ApplicationDbContext context)
        {
            this.db = context;
        }
        public IEnumerable<InventoryViewModel> GetAll()
        {
            return db.Inventories.Select(n=> new InventoryViewModel()
            {
                Id = n.Id,
                MenuItemId = n.MenuItemId,
                Price = n.Price,
                ProductName = n.ProductName,
                Vendor = n.Vendor,
                Volume = n.Volume,
                UnitOfMeasurment = n.UnitOfMeasurment

            }).AsQueryable();
        }

        public IEnumerable<InventoryViewModel> GetAll(int menuItemId)
        {
            List<InventoryViewModel> inventoryViewModels = new List<InventoryViewModel>();
            foreach (var inv in db.Inventories)
            {
                inventoryViewModels.Add(new InventoryViewModel
                {
                    Id = inv.Id,
                    MenuItemId = inv.MenuItemId,
                    Price = inv.Price,
                    ProductName = inv.ProductName,
                    Vendor = inv.Vendor,
                    Volume = inv.Volume,
                    UnitOfMeasurment = inv.UnitOfMeasurment
                });
            }
            return inventoryViewModels;
    }

        public List<InventoryViewModel> GetAllByUsername(int MenuItemId)
        {
            var inventoryViewModels = new List<InventoryViewModel>();
            foreach (var inv in db.Inventories.Where(item => item.MenuItemId == MenuItemId))
            {
                inventoryViewModels.Add(new InventoryViewModel
                {
                    Id = inv.Id,
                    Price = inv.Price,
                    ProductName = inv.ProductName,
                    MenuItemId = inv.MenuItemId,
                    Vendor = inv.Vendor,
                    Volume = inv.Volume,
                    UnitOfMeasurment = inv.UnitOfMeasurment
                });
            }
            return inventoryViewModels;
        }

        public InventoryViewModel Get(int id)
    {
        var inv = db.Inventories.Where(item => item.Id == id).FirstOrDefault();
            if(inv != null)
            {
                return new InventoryViewModel
                {
                    Id = inv.Id,
                    Price = inv.Price,
                    ProductName = inv.ProductName,
                    MenuItemId = inv.MenuItemId,
                    Vendor = inv.Vendor,
                    Volume = inv.Volume,
                    UnitOfMeasurment = inv.UnitOfMeasurment
                };
            }
            return null;
    }

        public void Create(InventoryViewModel model)
    {
            Inventory inv = new Inventory();
            inv.MenuItemId = model.MenuItemId;
            inv.Price = model.Price;
            inv.ProductName = model.ProductName;
            inv.UnitOfMeasurment = model.UnitOfMeasurment;
            inv.Vendor = model.Vendor;

            db.Inventories.Add(inv);
            db.SaveChanges();
        }

        public void Update(InventoryViewModel rate)
    {
        db.Entry(rate).State = EntityState.Modified;

            db.SaveChanges();
        }
 
    public void Delete(int id)
    {
        Inventory rate = db.Inventories.Find(id);
            if (rate != null)
            {
                db.Inventories.Remove(rate);
                db.SaveChanges();
            }
        }

    }
}