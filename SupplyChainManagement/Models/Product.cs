using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SupplyChainManagement.Models
{
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required]
        public string ProductCode { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public string ProductName { get; set; }

        [Required]
        public Category Category { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        public double SellingPrice { get; set; }
    }
}
