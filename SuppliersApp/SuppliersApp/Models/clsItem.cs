using SQLite;
using SuppliersApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace SuppliersApp.Models
{
    public class Barcode : BaseViewModel
    {
        private string _barcode;
        public string barcode
        {
            get { return _barcode; }
            set
            {
                _barcode = value;

                OnPropertyChanged();
            }
        }
    }
    public class clsItem : BaseViewModel
    {

       
        [PrimaryKey]
        public int ItemId { get; set; }
        public string ItemName { get; set; }
        public string UrduName { get; set; }
        public string Barcode { get; set; }

        public int? BrandId { get; set; }
        public string BrandName { get; set; }
        public int? CategoryId { get; set; }
        public string CategoryName { get; set; }

        public int? UnitId { get; set; }
        public string UnitName { get; set; }

        public int? Unit2Id { get; set; }
        public string Unit2Name { get; set; }
        public decimal? _CostPrice;
         public decimal? CostPrice
        {
            get { return _CostPrice; }
            set
            {
                _CostPrice = value;

                OnPropertyChanged();
            }
        }
        public decimal? _SalePrice;
        public decimal? SalePrice
        {
            get { return _SalePrice; }
            set
            {
                _SalePrice = value;

                OnPropertyChanged();
            }
        }
        public decimal? MinLevel { get; set; }
        public decimal? MaxLevel { get; set; }
        public decimal? MinOrder { get; set; }

        public decimal? Quantity1 { get; set; }
        private decimal? _Quantity2;
        public decimal? Quantity2
        {
            get { return _Quantity2; }
            set
            {
                if (value > 0)
                {
                    _Quantity2 = value;
                }
                else
                {
                    _Quantity2 = 1;
                }

                OnPropertyChanged();
            }
        }

        public bool? Status { get; set; }
        public string StatusString { get; set; }
        public DateTime? CreatedDate { get; set; }
        public String CreatedByString { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string UpdatedByString { get; set; }
        
        private decimal? _MRP;
        public decimal? MRP
        {
            get { return _MRP; }
            set
            {
                _MRP = value;

                OnPropertyChanged();
            }
        }
        public int? DocumentId { get; set; }
        public string ImagePath { get; set; }
        public int? TaxInvoice { get; set; }
        public string DiscountType { get; set; }
        public decimal? Discount { get; set; }
        public decimal? NetRate => MRP;
        public decimal? TaxValue { get; set; }
        public decimal? ValueExTax { get; set; }
        public decimal? Tax { get; set; }
        public decimal? FixRate { get; set; }
        public decimal? GrossVal { get; set; }


    }


    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
    public class ItemList
    {
     //   public List<object> StockDetail { get; set; }
        [PrimaryKey]
        public int ItemId { get; set; }
        public string ItemName { get; set; }
        public object UrduName { get; set; }
        public string Barcode { get; set; }
        public object Itemcode { get; set; }
        public object DataEntryName { get; set; }
        public object Remarks { get; set; }
        public int BrandId { get; set; }
        public string BrandName { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public object OriginId { get; set; }
        public object OriginName { get; set; }
        public object UnitId { get; set; }
        public object UnitName { get; set; }
        public object BasicUnitId { get; set; }
        public object BasicUnitName { get; set; }
        public object Type { get; set; }
        public object Unit2Id { get; set; }
        public object Unit2Name { get; set; }
        public decimal? CostPrice { get; set; }
        public object SalePrice { get; set; }
        public decimal? MRP { get; set; }
        public object MinLevel { get; set; }
        public object MaxLevel { get; set; }
        public object MinOrder { get; set; }
        public object CTNSize { get; set; }
        public object PendingDA { get; set; }
        public object Quantity1 { get; set; }
        public int? Quantity2 { get; set; }
        public object Stock { get; set; }
        public object ShowDate { get; set; }
        public object FromDate { get; set; }
        public object ToDate { get; set; }
        public object WarehouseID { get; set; }
        public object WarehouseName { get; set; }
        public object ShopTax { get; set; }
        public object Status { get; set; }
        public object StatusString { get; set; }
        public object CreatedDate { get; set; }
        public object CreatedByString { get; set; }
        public object UpdatedDate { get; set; }
        public object UpdatedByString { get; set; }
        public object Weight { get; set; }
        public object Quantity { get; set; }
        public decimal? Rate { get; set; }
        public int? DocumentId { get; set; }
        public string ImagePath { get; set; }
        public int DeleteRoleId { get; set; }
        public int UpdateRoleId { get; set; }
        public string DiscountType { get; set; }
        public decimal? Discount { get; set; }
        public decimal? NetRate { get; set; }
        public decimal? TaxValue { get; set; }
        public decimal? ValueExTax { get; set; }
        public decimal? Tax { get; set; }
    }

    public class clsRootItem
    {
        public bool Status { get; set; }
        public string Message { get; set; }
        public ObservableCollection<ItemList> itemList { get; set; }
    }
    public class ItemListSQLite
    {
        [PrimaryKey]
        public int ItemId { get; set; }
        public string ItemName { get; set; }
       
        public int? Quantity2 { get; set; }
        public string DiscountType { get; set; }
        public decimal? Discount { get; set; }
        public decimal? TaxValue { get; set; }
    }


}
