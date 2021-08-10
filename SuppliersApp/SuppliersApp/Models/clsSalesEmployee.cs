using System;
using System.Collections.Generic;
using System.Text;

namespace SuppliersApp.Models
{
  public  class clsSalesEmployee
    {
       
        public string EmployeeID { get; set; }
         
        public string Longitude { get; set; }
        public string Latitude { get; set; }
        
    }
    public class EAttendance
    {

    public clsSalesEmployee SEAttendance { get; set; }

    }
}
