using System;
using System.Collections.Generic;
using System.Text;

namespace SuppliersApp.Models.Employee_Details
{
   
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
    public class Employee
    {
        public int EmployeeId { get; set; }
        public int DeleteRoleId { get; set; }
        public int UpdateRoleId { get; set; }
        public string EmployeeName { get; set; }
        public string Mobile1 { get; set; }
        public string Mobile2 { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Designation { get; set; }
        public string SectionAllocation { get; set; }
        public int BossId { get; set; }
        public string BossName { get; set; }
        public object SectionId { get; set; }
        public object SectionName { get; set; }
        public object LineStatus { get; set; }
        public object LineStatusString { get; set; }
        public int DistributorId { get; set; }
        public string DistributorName { get; set; }
        public string Remarks { get; set; }
        public int Status { get; set; }
        public object StatusString { get; set; }
        public object CreatedDate { get; set; }
        public object CreatedByString { get; set; }
        public object UpdatedDate { get; set; }
        public object UpdatedByString { get; set; }
        public int DocumentId { get; set; }
        public int NoOfDistributors { get; set; }
        public string ImagePath { get; set; }
        public object ManualCode { get; set; }
    }

    public class clsEmployee
    {
        public Employee Employee { get; set; }
    }


}
