using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace SITech.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public bool IsActive { get; set; }
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<MenuItem> MenuItems { get; set; }
        public DbSet<Inventory> Inventories { get; set; }
        public DbSet<BeverageInventory> BeverageInventories { get; set; }
        public DbSet<Food> Foods { get; set; }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }    
    }

    [Table("MenuItems")]
    public class MenuItem
    {

        [Key]
        public int MenuItemId { get; set; }
        public string ItemName { get; set; }
        public string ItemType { get; set; }
        public double ItemPrice { get; set; }
        public double MenuPrice { get; set; }
        public double Profit { get; set; }
        public string CustomerId { get; set; }
        public bool IsActive { get; set; }

        public string BeverageInventoryIds { get; set; }

        [ForeignKey("CustomerId")]
        public virtual ApplicationUser User { get; set; }
    }

    [Table("Customers")]
    public class Customer
    {
        [Key]
        public string CustomerId { get; set; }

        [ForeignKey("CustomerId")]
        public virtual ApplicationUser User { get; set; }

    }

    [Table("BeverageInventories")]
    public class BeverageInventory
    {
        [Key]
        public int Id { get; set; }

        public int MenuItemId { get; set; }

        public string Vendor { get; set; }

        public string Producer{ get; set; }

        public double Price { get; set; }

        public string Volume { get; set; }

        public string DrinkType { get; set; }

        public string ProductName { get; set; }

        public string UnitOfMeasurment { get; set; }

        public int Age { get; set; }

        //[ForeignKey("MenuItemId")]
        //public virtual MenuItem MenuItem { get; set; }

    }

    [Table("Inventories")]
    public class Inventory
    {
        [Key]
        public int Id { get; set; }

        public int MenuItemId { get; set; }

        public string Vendor { get; set; }

        public double Price { get; set; }

        public string Volume { get; set; }

        public string ProductName { get; set; }

        public string UnitOfMeasurment { get; set; }


        //[ForeignKey("MenuItemId")]
        //public virtual MenuItem MenuItem { get; set; }

    }

    [Table("Foods")]
    public class Food
    {
        [Key]
        public int Id { get; set; }

        public int MenuItemId { get; set; }

        public string Vendor { get; set; }

        public double Price { get; set; }

        public string Volume { get; set; }

        public string ProductName { get; set; }

        [ForeignKey("MenuItemId")]
        public virtual MenuItem MenuItem { get; set; }

    }
   
}