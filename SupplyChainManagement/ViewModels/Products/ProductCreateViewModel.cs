using SupplyChainManagement.Models;
using System.ComponentModel.DataAnnotations;

namespace SupplyChainManagement.ViewModels.Products
{
    public class ProductCreateViewModel
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Product Code is required.")]
        public string ProductCode { get; set; }

        [Required(ErrorMessage = "Date is required.")]
        public DateTime Date { get; set; }

        [Required(ErrorMessage = "Product Name is required.")]
        public string ProductName { get; set; }

        [Required(ErrorMessage = "Category is required.")]
        public Category Category { get; set; }

        [Required(ErrorMessage = "Quantity is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Quantity cannot be negative.")]
        public int Quantity { get; set; }

        [Required(ErrorMessage = "Selling Price is required.")]
        [Range(1, double.MaxValue, ErrorMessage = "Selling Price cannot be negative.")]
        public double SellingPrice { get; set; }
    }
}
