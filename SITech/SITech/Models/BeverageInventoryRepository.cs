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
        private readonly ApplicationDbContext _db;

        public BeverageInventoryRepository(ApplicationDbContext context)
        {
            this._db = context;
        }

        public IEnumerable<BeverageInventoryViewModel> GetAll()
        {
            List<BeverageInventoryViewModel> inventoryViewModels = new List<BeverageInventoryViewModel>();
            foreach (var inv in _db.BeverageInventories)
            {
                inventoryViewModels.Add(new BeverageInventoryViewModel
                {
                    Id = inv.Id,
                    Price = inv.Price,
                    ProductName = inv.ProductName,
                    Vendor = inv.Vendor,
                    UnitOfMeasurment = inv.UnitOfMeasurment,
                    Age = inv.Age
                });
            }
            return inventoryViewModels;
        }

        public IEnumerable<BeverageInventoryViewModel> GetAll(int menuItemId)
        {
            List<BeverageInventoryViewModel> inventoryViewModels = new List<BeverageInventoryViewModel>();
            foreach (var inv in _db.BeverageInventories)
            {
                inventoryViewModels.Add(new BeverageInventoryViewModel
                {
                    Id = inv.Id,
                    Price = inv.Price,
                    ProductName = inv.ProductName,
                    Vendor = inv.Vendor,
                    Age = inv.Age,
                    UnitOfMeasurment = inv.UnitOfMeasurment
                });
            }
            return inventoryViewModels;
        }

        public BeverageInventory GetById(int id)
        {
            return _db.BeverageInventories.First(item => item.Id == id);
        }

        public BeverageInventoryViewModel Get(int id)
        {
            var inv = _db.BeverageInventories.FirstOrDefault(item => item.Id == id);
            if (inv != null)
            {
                return new BeverageInventoryViewModel
                {
                    Id = inv.Id,
                    Price = inv.Price,
                    ProductName = inv.ProductName,
                    Vendor = inv.Vendor,
                    Age = inv.Age,
                    UnitOfMeasurment = inv.UnitOfMeasurment
                };
            }
            return null;
        }

        public void Create(BeverageInventoryViewModel model)
        {
            BeverageInventory inv = new BeverageInventory
            {
                Price = model.Price,
                ProductName = model.ProductName,
                Age = model.Age,
                UnitOfMeasurment = model.UnitOfMeasurment,
                Vendor = model.Vendor
            };

            _db.BeverageInventories.Add(inv);

            _db.SaveChanges();
        }

        public void Update(BeverageInventoryViewModel rate)
        {
            _db.Entry(rate).State = EntityState.Modified;

            _db.SaveChanges();
        }

        public void Delete(int id)
        {
            BeverageInventory rate = _db.BeverageInventories.Find(id);
            if (rate != null)
            {
                _db.BeverageInventories.Remove(rate);
                _db.SaveChanges();
            }
        }
    }
}