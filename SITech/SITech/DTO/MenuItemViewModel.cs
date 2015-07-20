namespace SITech.DTO
{
    public class MenuItemViewModel
    {
        public int MenuItemId { get; set; }
        public string CustomerId { get; set; }
        public string ItemName { get; set; }
        public string ItemType { get; set; }
        public double ItemPrice { get; set; }
        public bool IsActive { get; set; }
    }
}