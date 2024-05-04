namespace FinalTestRSM.Models
{
    public class SalesByCustomerReport
    {
        public string CustomerName { get; set; }
        public string CategoryName { get; set; }
        public string ProductName { get; set; }
        public int Sales { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalSales { get; set; }

    }
}
