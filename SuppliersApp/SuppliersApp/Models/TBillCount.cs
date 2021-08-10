using SuppliersApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace SuppliersApp.Models
{
    public class TBillCount : BaseViewModel
    {
        private decimal? _TBill;
        public decimal? TBill
        {
            get { return _TBill; }
            set
            {
                _TBill = value;

                OnPropertyChanged();
            }
        }
        private int _TCount;
        public int TCount
        {
            get { return _TCount; }
            set
            {
                _TCount = value;

                OnPropertyChanged();
            }
        }
    }
}
