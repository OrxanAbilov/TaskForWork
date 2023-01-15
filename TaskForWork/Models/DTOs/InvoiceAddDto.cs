namespace TaskForWork.Models.DTOs
{
    public class InvoiceAddDto
    {
        public ItemComboDto Item { get; set; }
        public double Amount { get; set; }
        public double Price { get; set; } = 0;
        public double Total { get; set; }
    }
}
