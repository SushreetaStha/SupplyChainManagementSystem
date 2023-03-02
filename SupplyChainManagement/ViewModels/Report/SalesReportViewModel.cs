namespace SupplyChainManagement.ViewModels.Report
{
    public class SalesReportViewModel
    {
        public IList<SalesOrderDetail> SalesOrderDetails { get; set; } = new List<SalesOrderDetail>();
    }

    public class SalesOrderDetail
    {
        public string CustomerName { get; set; }
        public DateTime Date { get; set; }
        public int OrderItemsCount { get; set; }
        public IList<SalesTransactionDetail> SalesTransactionDetails { get; set; } = new List<SalesTransactionDetail>();
    }

    public class SalesTransactionDetail
    {
        public string ProductName { get; set; }
        public string ProductCategory { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
        public double TotalPrice { get; set; }
    }
}
