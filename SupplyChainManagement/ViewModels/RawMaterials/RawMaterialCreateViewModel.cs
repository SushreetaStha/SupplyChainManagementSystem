using System.ComponentModel.DataAnnotations;

namespace SupplyChainManagement.ViewModels.RawMaterials
{
    public class RawMaterialCreateViewModel
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Material Code is required.")]
        public string MaterialCode { get; set; }

        [Required(ErrorMessage = "Date is required.")]
        public DateTime Date { get; set; }

        [Required(ErrorMessage = "Material Name is required.")]
        public string MaterialName { get; set; }

        [Required(ErrorMessage = "Quantity is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Quantity cannot be negative.")]
        public int Quantity { get; set; }

        [Required(ErrorMessage = "Cost Price is required.")]
        [Range(1, double.MaxValue, ErrorMessage = "Cost Price cannot be negative.")]
        public double CostPrice { get; set; }
    }
}