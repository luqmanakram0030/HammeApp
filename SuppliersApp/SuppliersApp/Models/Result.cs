using System;
using System.Collections.Generic;
using System.Text;

namespace SuppliersApp.Models
{
   public class Result
    {
        public int ShopId { get; set; }
        public bool Status { get; set; }
        public string Message { get; set; }
    }
   
    public class Demmy
    {
        public string id { get; set; }
        public string base64image { get; set; }
        public clsDocument doc { get; set; }
    }

}
