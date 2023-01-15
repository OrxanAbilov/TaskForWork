using System.ComponentModel.DataAnnotations;

namespace TaskForWork.Models
{
    public class Item
    {
        [Key]
        public int ItemId { get; set; }
        public string? ItemName { get; set; }
        public double ItemPrice { get; set; } = 0;
        public double ItemStock { get; set; } = 0;
    }
}
