using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SITech.Models
{
    public class UnitOfWork : IDisposable
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private CustomerRepository customerRepository;
        private InventoryRepository inventoryRepository;
        private AccountRepository accountRepository;
        private BeverageInventoryRepository beverageInventoryRepository;
        private MenuItemRepository _menuItemRepository;

        public MenuItemRepository MenuItems
        {
            get
            {
                if (_menuItemRepository == null)
                    _menuItemRepository = new MenuItemRepository(db);
                return _menuItemRepository;
            }
        }
        public CustomerRepository Customers
        {
            get
            {
                if (customerRepository == null)
                    customerRepository = new CustomerRepository(db);
                return customerRepository;
            }
        }

        public InventoryRepository Inventories
        {
            get
            {
                if (inventoryRepository == null)
                    inventoryRepository = new InventoryRepository(db);
                return inventoryRepository;
            }
        }

        public BeverageInventoryRepository BeverageInventories
        {
            get
            {
                if (beverageInventoryRepository == null)
                    beverageInventoryRepository = new BeverageInventoryRepository(db);
                return beverageInventoryRepository;
            }
        }

        public AccountRepository Accounts
        {
            get
            {
                if (accountRepository == null)
                    accountRepository = new AccountRepository(db);
                return accountRepository;
            }
        }

        public void Save()
        {
            db.SaveChanges();
        }

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}