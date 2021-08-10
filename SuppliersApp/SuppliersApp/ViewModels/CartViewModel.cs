using SuppliersApp.Models;
using SuppliersApp.Services;
using SuppliersApp.Views;
using Rg.Plugins.Popup.Extensions;
using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace SuppliersApp.ViewModels
{
    class CartViewModel : BaseViewModel
    {
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
        public TBillCount total { get; set; }
        public ObservableCollection<clsInvoice> _Items { get; set; }
        public ObservableCollection<clsInvoice> Items
        {
            get
            {
                return _Items;
            }
            set
            {
                _Items = value;
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
        private readonly OrderPlacedApi _InvoiceApi;
        public ObservableCollection<clsInvoice> CartItems { get; set; } = new ObservableCollection<clsInvoice>();

        public INavigation navigation1;
        private SQLiteAsyncConnection _connection;
        public int id;
        public CartViewModel(INavigation navigation, int id)
        {
            this.id = id;
            total = new TBillCount();
            Items = new ObservableCollection<clsInvoice>();
            _InvoiceApi = new OrderPlacedApi();

            this.navigation1 = navigation;
            _connection = Xamarin.Forms.DependencyService.Get<ISQLiteDb>().GetConnection();

            _ = GetCartItem();

        }

        private async Task GetCartItem()
        {
            var g = await SecureStorage.GetAsync("UserId");
            int m = Convert.ToInt32(g);
            clsItems = await _connection.Table<clsItem>().ToListAsync();
            foreach (var item in clsItems)
            {
                Items.Add(new clsInvoice
                {
                    ItemId = item.ItemId,
                    ItemName = item.ItemName,
                    WarehouseId = 5009,
                    AccountId = m,
                    LineDiscRate = 0,

                    MRP=item.MRP,
                   // FRate = item.NetRate,
                    FixRate = item.FixRate,
                    DiscType=item.DiscountType,
                    DiscAmount=item.Discount,
                    Tax=item.TaxValue,
                    ValueExTax =item.ValueExTax,


                    Barcode = item.Barcode,
                    Quantity2 = item.Quantity2,
                    Rate = item.SalePrice,
                    FRate = item.CostPrice,
                    SRate = item.SalePrice,
                    LineGrossValue = item.CostPrice * item.Quantity2,
                    LineNetAmount = item.CostPrice,
                    ImagePath = item.ImagePath


                });
            }

            total.TBill = Items.Sum(s => s.MRP);
        }

        public Command<clsInvoice> RemoveItem
        {
            get
            {
                return new Command<clsInvoice>( async(clsInvoice product) =>
                {
                    var invoiceU = new clsItem { ItemId = product.ItemId, ItemName = product.ItemName, CostPrice = product.FRate, SalePrice = product.SRate, Quantity2 = 1, ImagePath = product.ImagePath,MRP = product.MRP };
                    await _connection.DeleteAsync(invoiceU);
                   
                    Items.Remove(product);
                    total.TBill = total.TBill - product.MRP;
                    total.TCount--;
                    MessagingCenter.Send<clsInvoice>(product, "CountList");
                });
            }
        }
        public Command<clsInvoice> IncreaseQtyCommand
        {

            get
            {

                return new Command<clsInvoice>(async(clsInvoice product) =>
                {
                    product.Quantity2 += 1;
                    product.LineGrossValue = product.Quantity2 * product.FixRate;
                    total.TBill = total.TBill + product.FixRate;
                    product.MRP = product.FixRate * product.Quantity2;

                    var invoiceU = new clsItem { ItemId = product.ItemId, ItemName = product.ItemName, CostPrice = product.FRate, SalePrice = product.SRate, Quantity2 = product.Quantity2, ImagePath = product.ImagePath, MRP = product.MRP };
                    await _connection.UpdateAsync(invoiceU);
                });
            }
        }
        public Command<clsInvoice> DecreaseQtyCommand
        {

            get
            {
                return new Command<clsInvoice>(async(clsInvoice product) =>
                {

                    product.Quantity2 -= 1;
                    if (product.Quantity2 == 1)
                    {
                        product.MRP = product.FixRate;
                        total.TBill = Items.Sum(s => s.MRP);
                        //TBill = TBill - product.FRate;

                    }
                    else
                    {
                        // TBill = TBill - product.FRate;

                        product.LineGrossValue = product.MRP - product.FixRate;
                        product.MRP = product.MRP - product.FixRate;
                        total.TBill = Items.Sum(s => s.MRP);
                        // product.TotalAmount += product.LineGrossValue;
                    }
                    var invoiceU = new clsItem { ItemId = product.ItemId, ItemName = product.ItemName, CostPrice = product.FRate, SalePrice = product.SRate, Quantity2 = product.Quantity2, ImagePath = product.ImagePath, MRP = product.MRP };
                    await _connection.UpdateAsync(invoiceU);
                });
            }
        }
        public Command PlaceOrder
        {
            get
            {
                return new Command(async () =>
                {
                var connect = Connectivity.NetworkAccess;

                    if (connect == NetworkAccess.Internet)
                    {
                        var g = await SecureStorage.GetAsync("UserId");
                        int m = Convert.ToInt32(g);
                        clsMarketOrder marketOrder = new clsMarketOrder();
                        marketOrder.EmployeeId = m;
                        marketOrder.ShopId = id;
                        var request = new GeolocationRequest(GeolocationAccuracy.Medium);
                        var location = await Geolocation.GetLocationAsync(request);
                        marketOrder.Latitude = location.Latitude.ToString();
                        marketOrder.Longitude = location.Longitude.ToString();
                        marketOrder.GrossTotal = total.TBill;
                        marketOrder.MarketOrderLineList = new List<clsMarketOrder>();
                        foreach (var item in Items)
                        {
                            marketOrder.MarketOrderLineList.Add(new clsMarketOrder()
                            {
                                ItemId = item.ItemId,
                                Qty = item.Quantity2,
                                FOCQty = 0,
                                TotalQty = 0 + item.Quantity2,
                                Rate = item.FixRate,
                                Value = item.MRP,
                                LineDiscType = item.DiscType,
                                LineDiscRate = item.DiscAmount,
                                //Discount = item.DiscAmount,
                                Tax = item.Tax,
                                ValueExTax = item.ValueExTax,

                            });
                        }
                        //clsInvoice list = new clsInvoice();
                        //list.TotalAmount = total.TBill;
                        //list.AccountId = m;
                        //list.SaleLineList = Items.ToList();
                        Isbusy = true;
                        var current = Connectivity.NetworkAccess;

                        if (current == NetworkAccess.Internet)
                        {
                            var res = await _InvoiceApi.PlaceOrder(marketOrder);
                            Isbusy = false;
                            if (res.Status)
                            {
                                total.TBill = 0;
                                Items.Clear();
                                await _connection.DropTableAsync<clsItem>();
                                await Xamarin.Forms.Application.Current.MainPage.Navigation.PushAsync(new OrderAcceptedPage(res.OrderNumber));
                            }
                        }
                        else
                        {
                            await Xamarin.Forms.Application.Current.MainPage.DisplayAlert("", "Please Connect with Internet.", "ok");
                            Isbusy = false;
                        }
                    }
                    else
                    {
                        await Application.Current.MainPage.Navigation.PushPopupAsync(new AlertPage("E", "Please connect your device with internet."));

                    }
                });
            }
        }
    }
}
