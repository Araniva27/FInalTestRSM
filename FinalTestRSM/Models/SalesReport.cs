using System.Numerics;

namespace FinalTestRSM.Models
{
    public class SalesReport
    {
        public int productID { get; set; }
        public string productName { get; set; }
        public decimal unitPrice { get; set; }
        public int productQuantity { get; set; }
        public string productCategory { get; set; }
        public DateTime orderDate { get; set; }
        public decimal total {  get; set; }


    }
}
