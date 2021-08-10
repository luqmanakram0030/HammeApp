using SuppliersApp.Models;
using SuppliersApp.Services;
using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Forms;
using Rg.Plugins.Popup.Extensions;
using SuppliersApp.Views;
using System.Threading.Tasks;
using System.Linq;
using Xamarin.Essentials;

namespace SuppliersApp.ViewModels
{
    public class ManuallyAddViewModel : BaseViewModel
    {
        #region
        public clsRootItem rootItem { get; set; }
        private bool _Isbusy;

        public bool Isbusy
        {
            get
            {
                return _Isbusy;
            }
            set
            {
                _Isbusy = value;
                if (_Isbusy)
                {
                    Xamarin.Forms.Application.Current.MainPage.Navigation.PushPopupAsync(new IndicatorActity());

                }
                else
                {
                    Xamarin.Forms.Application.Current.MainPage.Navigation.PopPopupAsync();

                }

                OnPropertyChanged();
            }
        }


        private string _searchedText;
        public string search
        {
            get
            {
                return _searchedText;
            }
            set
            {
                _searchedText = value;
                OnPropertyChanged("SearchedText");
                // SearchCommandExecute();
            }
        }


        private SQLiteAsyncConnection _connection;
        

        private ObservableCollection<clsItem> _lst { get; set; }

        public ObservableCollection<clsItem> lst
        {
            get { return _lst; }
            set
            {
                _lst = value;

                OnPropertyChanged();
            }
        }
        public List<clsItem> _clsItems { get; set; }
        public List<clsItem> clsItems
        {
            get
            {
                return _clsItems;
            }
            set
            {
                _clsItems = value;
                OnPropertyChanged();

            }
        }
        public readonly GetItemsBySearch getItemsBySearch;
        #endregion

        public TBillCount Total { get; set; }
        public int id;
        public ManuallyAddViewModel(int id)
        {
            this.id = id;
            
            clsItems = new List<clsItem>();
            Total = new TBillCount();

            lst = new ObservableCollection<clsItem>();
            getItemsBySearch = new GetItemsBySearch();
            _connection = Xamarin.Forms.DependencyService.Get<ISQLiteDb>().GetConnection();

            CountAsync();

        }

        private async Task CountAsync()
        {
            clsItems = await _connection.Table<clsItem>().ToListAsync();
            Total.TCount = clsItems.Count();
        }

        public Command SearchCommand
        {
            get
            {
                return new Command(async () =>
                {
                var current = Connectivity.NetworkAccess;

                    if (current == NetworkAccess.Internet)
                    {
                        Isbusy = true;

                        lst = await getItemsBySearch.GetItemsAsync(search);

                        Isbusy = false;
                        if (lst == null)
                        {
                            await Application.Current.MainPage.Navigation.PushPopupAsync(new AlertPage("W", "This Item Did Not Exist"));
                        }
                    }
                    else
                    {
                        await Application.Current.MainPage.Navigation.PushPopupAsync(new AlertPage("E", "Please connect your device with internet."));

                    }
                });
            }


        }
       
        public Command<clsItem>  AddDisc
        {
            get
            {
                return new Command<clsItem>(async (clsItem c) =>
                {
                    await Application.Current.MainPage.Navigation.PushPopupAsync(new AddDiscountPage(c));

                    MessagingCenter.Subscribe<clsItem>(  this, "GetItemList",async (data) =>
                    {
                        try
                        {
                            if (data.Quantity2 == 1)
                            {
                                data.FixRate = data.MRP;
                            }
                            else
                            {
                                if (data.DiscountType == "%")
                                {
                                    var a = ((data.Discount / 100) * data.FixRate) + data.TaxValue;
                                    data.FixRate = data.FixRate - a;
                                }
                                else
                                {
                                    data.FixRate = (data.FixRate - data.Discount) - data.TaxValue;
                                   
                                }
                                // data.FixRate = data.MRP*data.Quantity2;
                            }
                           

                            var clsitem = new clsItem { ItemId = data.ItemId, ItemName = data.ItemName, CostPrice = data.CostPrice, SalePrice = data.CostPrice, Quantity2 = data.Quantity2, ImagePath = data.ImagePath, Discount = data.Discount, DiscountType = data.DiscountType, TaxValue = data.TaxValue, ValueExTax = data.ValueExTax, MRP = data.MRP,FixRate=data.FixRate };

                            var abc =  _connection.InsertAsync(clsitem);
                            CountAsync();

                        }
                        catch (Exception ex)
                        {
                             Application.Current.MainPage.Navigation.PushPopupAsync(new AlertPage("E", ex.Message));
                        }

                        MessagingCenter.Unsubscribe<clsItem>(this, "GetItemList");
                       
                    });
                   
                });
            }
        }
        
        public Command<clsItem> AddCartItem
        {

            get
            {
                
                return new Command<clsItem>(async (clsItem data) =>
                {
                    try
                    {
                       
                        var clsitem = new clsItem { ItemId = data.ItemId, ItemName = data.ItemName, CostPrice = data.CostPrice, SalePrice = data.CostPrice, Quantity2 = data.Quantity2, ImagePath = data.ImagePath, Discount = data.Discount, DiscountType = data.DiscountType, Tax = data.Tax, ValueExTax = data.ValueExTax, MRP = data.MRP, FixRate = data.FixRate };

                        var abc = await _connection.InsertAsync(clsitem);
                        CountAsync();
                    }
                    catch (Exception ex)
                    {
                        Application.Current.MainPage.Navigation.PushPopupAsync(new AlertPage("E", ex.Message));
                    }
                });
            }
        }

        public Command<clsItem> IncreaseQtyCommand
        {

            get
            {
                
                return new Command<clsItem>(async (clsItem product) =>
                {
                    product.Quantity2 += 1;
                    //product.LineGrossValue = product.Quantity2 * product.FRate;
                    //product.MRP = product.FixRate;
                    product.MRP = product.FixRate * product.Quantity2;

                    var invoiceU = new clsItem { ItemId = product.ItemId, ItemName = product.ItemName, CostPrice = product.CostPrice, SalePrice = product.SalePrice, Quantity2 = product.Quantity2, ImagePath = product.ImagePath ,MRP= product.MRP,FixRate=product.FixRate};
                    var abc = await _connection.UpdateAsync(invoiceU);
                    //Total.TBill = product.Sum(s => s.MRP);
                });
            }
        }



        public Command<clsItem> DecreaseQtyCommand
        {
            get
            {
                return new Command<clsItem>(async (clsItem product) =>
                {
                    product.Quantity2 -= 1;
                    if (product.Quantity2 == 1)
                    {
                        product.MRP = product.FixRate;
                        //Total.TBill = Items.Sum(s => s.SRate);
                    }
                    else
                    {
                        // Total.TBill = Total.TBill - product.FRate;

                        // product.LineGrossValue = product.SRate - product.FRate;
                        product.MRP = product.MRP - product.FixRate;
                        // Total.TBill = Items.Sum(s => s.SRate);
                    }
                    var invoiceU = new clsItem { ItemId = product.ItemId, ItemName = product.ItemName, CostPrice = product.CostPrice, SalePrice = product.SalePrice, Quantity2 = product.Quantity2, ImagePath = product.ImagePath, MRP = product.MRP, FixRate = product.FixRate };
                    var abc = await _connection.UpdateAsync(invoiceU);
                });
            }
        }


        public Command ViewCart
        {
            get
            {
                return new Command( () =>
                {
                     Application.Current.MainPage.Navigation.PushAsync(new CartViewPage(id));
                    MessagingCenter.Subscribe<clsInvoice>(this, "CountList", async (data) =>
                    {
                        clsItems = await _connection.Table<clsItem>().ToListAsync();
                        Total.TCount = clsItems.Count();
                        if (Total.TCount == 0)
                        {
                            MessagingCenter.Unsubscribe<clsInvoice>(this, "CountList");
                        }
                    });
                });
            }
        }

    }
}
