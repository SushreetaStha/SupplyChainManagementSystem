namespace SupplyChainManagement.ViewModels.Orders
{
    public class OrderIndexViewModel
    {
        public IList<OrderIndexDetails> OrderDetails { get; set; }
    }

    public class OrderIndexDetails
    {
        public Guid OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public string OrderCode { get; set; }
        public string CustomerName { get; set; }
        public bool IsItemInStock { get; set; }
    }
}
