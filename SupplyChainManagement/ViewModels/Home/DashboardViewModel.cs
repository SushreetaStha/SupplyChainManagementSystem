namespace SupplyChainManagement.ViewModels.Home
{
    public class DashboardViewModel
    {
        public double Profit { get; set; }
        public int TotalOrders { get; set; }
        public int TotalSuppliers { get; set; }
        public int TotalCustomers { get; set; }
        public ProductPieChart ProductPieChart { get; set; } = new ProductPieChart();
        public OrderBarChart OrderBarChart { get; set; } = new OrderBarChart();
    }

    public class ProductPieChart
    {
        public List<string> ProductNames { get; set; } = new List<string>();
        public List<int> ProductQuantities { get; set; } = new List<int>();
    }

    public class OrderBarChart
    {
        public List<string> Months { get; set; } = new List<string>
        {
            "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December"
        };
        public List<int> OrdersCount { get; set; } = new List<int>();
    }
}
