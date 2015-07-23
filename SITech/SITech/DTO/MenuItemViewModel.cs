namespace SITech.DTO
{
    public class MenuItemViewModel
    {
        public int MenuItemId { get; set; }
        public string CustomerId { get; set; }
        public string ItemName { get; set; }
        public string ItemType { get; set; }
        public double ItemPrice { get; set; }
        public double MenuPrice { get; set; }
        public double Profit { get; set; }
        public bool IsActive { get; set; }
        public string BeverageIds { get; set; }
        public string InventoryIds { get; set; }

    }
}