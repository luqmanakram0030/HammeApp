using System;
using System.Collections.Generic;
using System.Text;

namespace SuppliersApp.Models
{
    public class clsVisit
    {
        public string VisitId { get; set; }


        public int DeleteRoleId { get; set; }
        public int UpdateRoleId { get; set; }
        public int? EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public int? ShopId { get; set; }
        public string ShopName { get; set; }
        public int? FollowupType { get; set; }
        public DateTime? FollowupDate { get; set; }
        public string IsNeWOrEdit { get; set; }

        public string Remarks { get; set; }
        public string FolloupDateString { get; set; }


        public string Status { get; set; }

        public DateTime? RecordDateTime { get; set; }
        public DateTime? CreatedDate { get; set; }
        public String CreatedByString { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string UpdatedByString { get; set; }

    }
}
