using SITech.Models;

namespace SITech.DTO
{
    public class InventoryViewModel
    {
        public int Id { get; set; }

        public int MenuItemId { get; set; }

        public string Vendor { get; set; }

        public double Price { get; set; }

        public string Volume { get; set; }

        public string UnitOfMeasurment { get; set; }

        public string ProductName { get; set; }

        public virtual MenuItem MenuItem { get; set; }

    }
}