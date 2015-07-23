using SITech.Models;

namespace SITech.DTO
{
    public class BeverageInventoryViewModel
    {
        public int Id { get; set; }

        public string Vendor { get; set; }

        public string Producer { get; set; }

        public double Price { get; set; }

        public string Volume { get; set; }

        public string DrinkType { get; set; }

        public string ProductName { get; set; }

        public int Age { get; set; }

        public string UnitOfMeasurment { get; set; }

    }
}