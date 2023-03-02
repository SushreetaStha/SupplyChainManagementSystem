using SupplyChainManagement.Models;

namespace SupplyChainManagement.ViewModels.Orders
{
    public class OrderItemsViewModel
    {
        public string CustomerName { get; set; }
        public double GrandTotal { get; set; }

        public IList<OrderItem> OrderItems { get; set; }
    }

    public class OrderItem
    {
        public Guid Id { get; set; }
        public string ProductName { get; set; }
        public int OrderQuantity { get; set; }
        public double SellingPrice { get; set; }
        public int OrderPending { get; set; }
    }
}
