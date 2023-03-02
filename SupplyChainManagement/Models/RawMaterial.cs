using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SupplyChainManagement.Models
{
    public class RawMaterial
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required]
        public string MaterialCode { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public string MaterialName { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        public double CostPrice { get; set; }
    }
}
