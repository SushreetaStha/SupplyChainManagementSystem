using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SupplyChainManagement.Models
{
    public class Order
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required]
        public string OrderCode { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [ForeignKey("Customer")]
        public Guid CustomerId { get; set; }

        [Required]
        public bool IsCompleted { get; set; }

        [Required]
        public double GrandTotal { get; set; }

        public Customer Customer { get; set; }

        public IEnumerable<Transaction> Transactions { get; set; }
    }
}
