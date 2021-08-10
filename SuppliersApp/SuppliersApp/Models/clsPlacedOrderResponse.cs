using SuppliersApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace SuppliersApp.Models
{
   public class clsPlacedOrderResponse:BaseViewModel
    {

        public bool Status { get; set; }
        public string Message { get; set; }

        public List<clsItem> _ItemList { get; set; }

        public List<clsItem> ItemList
        {
            get { return _ItemList; }
            set
            {
                _ItemList = value;

                OnPropertyChanged();
            }
        }
        private string _OrderNumber { get; set; }

        public string OrderNumber
        {
            get { return _OrderNumber; }
            set
            {
                _OrderNumber = value;

                OnPropertyChanged();
            }
        }
    }
}
