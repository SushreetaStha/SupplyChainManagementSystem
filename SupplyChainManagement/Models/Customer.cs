using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SupplyChainManagement.Models
{
    public class Customer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required]
        public string CustomerCode { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public string CustomerName { get; set; }


        [Required]
        public string Contact { get; set; }

        [Required]
        public string Address { get; set; }
    }
}
