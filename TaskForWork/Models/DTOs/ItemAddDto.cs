using System.ComponentModel.DataAnnotations;

namespace TaskForWork.Models.DTOs
{
    public class ItemAddDto
    {
        [Required]
        public string? ItemName { get; set; }
        public double ItemPrice { get; set; } = 0;
        public double ItemStock { get; set; } = 0;
    }
}
