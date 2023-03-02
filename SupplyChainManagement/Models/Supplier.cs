using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SupplyChainManagement.Models
{
    public class Supplier
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required]
        public string SupplierCode { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public string SupplierName { get; set; }


        [Required]
        public string Contact { get; set; }

        [Required]
        public string Address { get; set; }
    }
}
