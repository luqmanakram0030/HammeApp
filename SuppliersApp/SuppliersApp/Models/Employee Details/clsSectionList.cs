using System;
using System.Collections.Generic;
using System.Text;

namespace SuppliersApp.Models.Employee_Details
{
    
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
    public class SectionList
    {
        public int EmployeeId { get; set; }
        public int DeleteRoleId { get; set; }
        public int UpdateRoleId { get; set; }
        public object EmployeeName { get; set; }
        public object Mobile1 { get; set; }
        public object Mobile2 { get; set; }
        public object Email { get; set; }
        public object Password { get; set; }
        public object Designation { get; set; }
        public object SectionAllocation { get; set; }
        public object BossId { get; set; }
        public object BossName { get; set; }
        public int SectionId { get; set; }
        public string SectionName { get; set; }
        public object LineStatus { get; set; }
        public object LineStatusString { get; set; }
        public object DistributorId { get; set; }
        public object DistributorName { get; set; }
        public object Remarks { get; set; }
        public object Status { get; set; }
        public object StatusString { get; set; }
        public object CreatedDate { get; set; }
        public object CreatedByString { get; set; }
        public object UpdatedDate { get; set; }
        public object UpdatedByString { get; set; }
        public object DocumentId { get; set; }
        public object NoOfDistributors { get; set; }
        public object ImagePath { get; set; }
        public object ManualCode { get; set; }
    }

    public class clsSectionList
    {
        public List<SectionList> SectionList { get; set; }
    }


}
