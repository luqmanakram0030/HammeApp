using System;
using System.Collections.Generic;
using System.Text;

namespace SuppliersApp.Models
{
    public class clsMarketOrder
    {
      
      
        public int? EmployeeId { get; set; }
       
       
        public int? ShopId { get; set; }
       
        public int? ItemId { get; set; }
       
        public string Remarks { get; set; }
        public string Status { get; set; }
        public string Longitude { get; set; }
        public string Latitude { get; set; }
        public int? UnitId { get; set; }
        public decimal? Qty { get; set; }
        public decimal? FOCQty { get; set; }
        public decimal? TotalQty { get; set; }
        public decimal? Rate { get; set; }

        public decimal? Value { get; set; }
        public decimal? GrossTotal { get; set; }
        public List<clsMarketOrder> MarketOrderLineList { get; set; }
        public int? TaxInvoice { get; set; }
        public string DiscType { get; set; }
        public decimal? Discount { get; set; }
        public decimal? NetRate { get; set; }
        public decimal? TaxValue { get; set; }
        public decimal? ValueExTax { get; set; }
        public decimal? Tax { get; set; }
        public string LineDiscType { get; set; }
        public decimal? LineDiscRate { get; set; }

    }
}
