using SITech.DTO;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace SITech.Models
{
    public class FoodRepository : IDataRepository<FoodViewModel>
    {
        private ApplicationDbContext db;

        public FoodRepository(ApplicationDbContext context)
    {
        this.db = context;
    }
        public IEnumerable<FoodViewModel> GetAll()
        {
            List<FoodViewModel> inventoryViewModels = new List<FoodViewModel>();
            foreach (var inv in db.Foods)
            {
                inventoryViewModels.Add(new FoodViewModel
                {
                    Id = inv.Id,
                    MenuItemId = inv.MenuItemId,
                    Price = inv.Price,
                    ProductName = inv.ProductName,
                    Vendor = inv.Vendor,
                    Volume = inv.Volume
                });
            }
            return inventoryViewModels;
        }


        public IEnumerable<FoodViewModel> GetAll(int menuItemId)
    {
        return db.Foods.Cast<FoodViewModel>().AsQueryable();
    }

        public List<FoodViewModel> GetAllByUsername(int MenuItemId)
        {
            var FoodViewModels = new List<FoodViewModel>();
            foreach (var inv in db.Foods.Where(item => item.MenuItemId == MenuItemId))
            {
                FoodViewModels.Add(new FoodViewModel
                {
                    Id = inv.Id,
                    MenuItemId = inv.MenuItemId,
                    Price = inv.Price,
                    ProductName = inv.ProductName,
                    Vendor = inv.Vendor,
                    Volume = inv.Volume
                });
            }
            return FoodViewModels;
        }

        public FoodViewModel Get(int id)
    {
        var inv = db.Foods.Where(item => item.Id == id).FirstOrDefault();
            if(inv != null)
            {
                return new FoodViewModel
                {
                    Id = inv.Id,
                    Price = inv.Price,
                    ProductName = inv.ProductName,
                    MenuItemId = inv.MenuItemId,
                    Vendor = inv.Vendor,
                    Volume = inv.Volume
                };
            }
            return null;
    }

        public void Create(FoodViewModel model)
    {
            Food inv = new Food();
            inv.MenuItemId = model.MenuItemId;
            inv.Price = model.Price;
            inv.ProductName = model.ProductName;

            db.Foods.Add(inv);

            db.SaveChanges();
        }

        public void Update(FoodViewModel rate)
    {
        db.Entry(rate).State = EntityState.Modified;

            db.SaveChanges();
        }
 
    public void Delete(int id)
    {
        Food rate = db.Foods.Find(id);
            if (rate != null)
            {
                db.Foods.Remove(rate);
                db.SaveChanges();
            }
        }

    }
}