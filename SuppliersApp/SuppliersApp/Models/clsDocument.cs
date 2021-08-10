using SuppliersApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace SuppliersApp.Models
{
   public class clsDocument
    {
       
        public string TypeId { get; set; }
        public string Remarks { get; set; }
        public DateTime? Date { get; set; }
        public string DocumentExtension => "jpg";
        public string DocumentType => "Shop";
        public ObservableCollection<ImagesSaveBase64> imagesBase64 { get; set; }
    }
}
