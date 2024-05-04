namespace FinalTestRSM.Models
{
    public class SalesByRegionReport
    {
        public string ProductName { get; set; }
        public string ProductCategory { get; set; }
        public decimal TotalSales { get; set; }
        public decimal PercentOfTotalCategorySalesInRegion { get; set; }
        public decimal PercentOfTotalSalesInRegion { get; set; }
        public DateTime OrderDate { get; set; }
        public string TerritoryName { get; set; }
    }
}
