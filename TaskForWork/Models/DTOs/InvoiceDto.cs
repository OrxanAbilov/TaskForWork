namespace TaskForWork.Models.DTOs
{
    public class InvoiceDto
    {
        public int InvId { get; set; }
        public string ItemName { get; set; }
        public double Amount { get; set; }
        public double Price { get; set; } = 0;
        public double Total { get; set; }
        public string SaleDate { get; set; }
    }
}
