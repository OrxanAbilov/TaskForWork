using System.ComponentModel.DataAnnotations;

namespace TaskForWork.Models
{
    public class Invoice
    {
        [Key]
        public int InvId { get; set; }
        public int ItemId { get; set; }
        public Item Item { get; set; }
        public double Amount { get; set; }
        public double Price { get; set; } = 0;
        public double Total { get; set; }
        public DateTime SaleDate { get; set; }= DateTime.Now;

    }
}
