using System.ComponentModel.DataAnnotations;

namespace SupplyChainManagement.ViewModels.Customers
{
    public class CustomerCreateViewModel
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Customer Code is required.")]
        public string CustomerCode { get; set; }

        [Required(ErrorMessage = "Date is required.")]
        public DateTime Date { get; set; }

        [Required(ErrorMessage = "Customer Name is required.")]
        public string CustomerName { get; set; }

        [Required(ErrorMessage = "Contact is required.")]
        public string Contact  { get; set; }

        [Required(ErrorMessage = "Address is required.")]
        public string Address { get; set; }
    }
}
