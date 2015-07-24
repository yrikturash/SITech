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
        private ApplicationDbContext _db;

        public InventoryRepository(ApplicationDbContext context)
        {
            this._db = context;
        }

        public IEnumerable<InventoryViewModel> GetAll()
        {
            return _db.Inventories.Select(n => new InventoryViewModel()
            {
                Id = n.Id,
                Price = n.Price,
                ProductName = n.ProductName,
                Vendor = n.Vendor,
                UnitOfMeasurment = n.UnitOfMeasurment
            }).AsQueryable();
        }

        public IEnumerable<InventoryViewModel> GetAll(int menuItemId)
        {
            return _db.Inventories.Select(inv => new InventoryViewModel
            {
                Id = inv.Id,
                Price = inv.Price,
                ProductName = inv.ProductName,
                Vendor = inv.Vendor,
                UnitOfMeasurment = inv.UnitOfMeasurment
            }).ToList();
        }

        public List<InventoryViewModel> GetAllById(int id)
        {
            var inventoryViewModels = new List<InventoryViewModel>();
            foreach (var inv in _db.Inventories.Where(item => item.Id == id))
            {
                inventoryViewModels.Add(new InventoryViewModel
                {
                    Id = inv.Id,
                    Price = inv.Price,
                    ProductName = inv.ProductName,
                    Vendor = inv.Vendor,
                    UnitOfMeasurment = inv.UnitOfMeasurment
                });
            }
            return inventoryViewModels;
        }

        public InventoryViewModel Get(int id)
        {
            var inv = _db.Inventories.FirstOrDefault(item => item.Id == id);
            if (inv != null)
            {
                return new InventoryViewModel
                {
                    Id = inv.Id,
                    Price = inv.Price,
                    ProductName = inv.ProductName,
                    Vendor = inv.Vendor,
                    UnitOfMeasurment = inv.UnitOfMeasurment
                };
            }
            return null;
        }

        public Inventory GetById(int id)
        {
            return _db.Inventories.FirstOrDefault(item => item.Id == id);
        }

        public void Create(InventoryViewModel model)
        {
            Inventory inv = new Inventory();
            inv.Price = model.Price;
            inv.ProductName = model.ProductName;
            inv.UnitOfMeasurment = model.UnitOfMeasurment;
            inv.Vendor = model.Vendor;

            _db.Inventories.Add(inv);
            _db.SaveChanges();
        }

        public void Update(InventoryViewModel rate)
        {
            var model = new Inventory()
            {
                Id = rate.Id,
                Price = rate.Price,
                ProductName = rate.ProductName,
                UnitOfMeasurment = rate.UnitOfMeasurment,
                Vendor = rate.Vendor
            };
            _db.Entry(model).State = EntityState.Modified;

            _db.SaveChanges();
        }

        public void Update(Inventory model)
        {
            _db.Entry(model).State = EntityState.Modified;

            _db.SaveChanges();
        }

        public void Delete(int id)
        {
            Inventory rate = _db.Inventories.Find(id);
            if (rate != null)
            {
                _db.Inventories.Remove(rate);
                _db.SaveChanges();
            }
        }
    }
}