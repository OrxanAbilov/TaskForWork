namespace TaskForWork.Models.DTOs
{
    public class ItemList
    {
        public int ItemId { get; set; }
        public string? ItemName { get; set; }
        public double ItemPrice { get; set; } = 0;
        public double ItemStock { get; set; } = 0;
    }
}
