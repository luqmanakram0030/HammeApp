using System;
using System.Collections.Generic;
using System.Text;

namespace SuppliersApp.Models
{
   
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
    public class Summary
    {
        public int EmployeeID { get; set; }
        public string EmployeeName { get; set; }
        public double TargetCTN { get; set; }
        public double TargetQty { get; set; }
        public double TargetValue { get; set; }
        public double OrderQty { get; set; }
        public double OrderValue { get; set; }
        public int NoOfOrders { get; set; }
        public double TodayOrderQty { get; set; }
        public double TodayOrderValue { get; set; }
        public int TodayNoOfOrders { get; set; }
        public float AcheiveQtyPers { get; set; }
        public float AcheiveValuePers { get; set; }
       
        public string CurrentMonth { get; set; }
       
        public int TargetNoOfShops { get; set; }
        public int TargetNoOfOrders { get; set; }
       
       
        public int NoOfShops { get; set; }
        public int MonthNewShop { get; set; }
      
        public int TodayNoOfShops { get; set; }
        public int TodayNewShop { get; set; }
       
        public int NoOfAttend { get; set; }
        public int NoOfVisits { get; set; }
        public int TodayNoOfVisits { get; set; }
    }

    public class clsSummery
    {
        public bool Status { get; set; }
        public string Message { get; set; }
        public Summary Summary { get; set; }
    }


}
