
using Rg.Plugins.Popup.Extensions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using ZXing;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using SuppliersApp.Models;
using SuppliersApp.Services;
using SuppliersApp.Views;
using Result = ZXing.Result;
using SQLite;

namespace SuppliersApp.ViewModels
{
    class BarcodeViewModel : BaseViewModel
    {
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
       
        private ObservableCollection<clsItem> _Items;
        public ObservableCollection<clsItem> Items
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

        public readonly GetItemByBarcode getItemByBarcode;
        private INavigation navigation;
        public Barcode barcode;
        public int id;
        public BarcodeViewModel(INavigation navigation,int id)
        {
            
            this.id = id;
            getItemByBarcode = new GetItemByBarcode();
            barcode = new Barcode();
            this.navigation = navigation;
            IsVisisble = false;
            Total = new TBillCount();
            
           // Total.itemCount = cartVM.Items.Count;
            Total.TBill = 0;
            Items = new ObservableCollection<clsItem>();
            clsItems = new List<clsItem>();

           //  _itemlistapi = new ItemsListApi();
           _connection = Xamarin.Forms.DependencyService.Get<ISQLiteDb>().GetConnection();
           _connection.CreateTableAsync<clsItem>();
           _connection.Table<clsItem>().ToListAsync();
            CountAsync();

          
        }

        private async Task CountAsync()
        {
            clsItems = await _connection.Table<clsItem>().ToListAsync();
            Total.TCount = clsItems.Count();
        }

        private bool _IsVisisble;

        public bool IsVisisble
        {
            get
            {
                return _IsVisisble;
            }
            set
            {
                _IsVisisble = value;


                OnPropertyChanged();
            }
        }
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
                    navigation.PushPopupAsync(new IndicatorActity());

                }
                else
                {
                    navigation.PopPopupAsync();

                }

                OnPropertyChanged();
            }
        }
        public TBillCount Total { get; set; }
        private List<clsItem> _lst { get; set; }

        public List<clsItem> lst
        {
            get { return _lst; }
            set
            {
                _lst = value;

                OnPropertyChanged();
            }
        }
       // private readonly ItemsListApi _itemlistapi;
        private readonly SQLiteAsyncConnection _connection;
      
        public Command<Result> AddProductCommand
        {

            get
            {
                return new Command<Result>(async (Result result) =>
                {
                var current = Connectivity.NetworkAccess;

                    if (current == NetworkAccess.Internet)
                    {
                        try
                        {
                            var duration = TimeSpan.FromSeconds(0.1);
                            Vibration.Vibrate(duration);

                            var res = result.Text;
                            bool chk = Items.Where(c => c.Barcode == res).Any(c => c.Barcode == res);
                            if (!chk)
                            {

                                barcode.barcode = res;
                                var a = await getItemByBarcode.GetItemByBarcodeAsync(barcode);
                                Items.Add(new clsItem
                                {
                                    ItemId = a[0].ItemId,
                                    ItemName = a[0].ItemName,
                                    Barcode = a[0].Barcode,
                                    CostPrice = a[0].CostPrice,
                                    SalePrice = a[0].CostPrice,
                                    Quantity2 = a[0].Quantity2,
                                    ImagePath = a[0].ImagePath,
                                    MRP = a[0].MRP,
                                    FixRate = a[0].MRP

                                });

                                Total.TBill = Total.TBill + a[0].MRP;

                                //var clsitem = new clsItem { ItemId = a[0].ItemId, ItemName = a[0].ItemName, CostPrice = a[0].CostPrice, SalePrice = a[0].CostPrice, Quantity2 = a[0].Quantity2, ImagePath = a[0].ImagePath };

                                //var abc = await _connection.InsertAsync(clsitem);
                            }
                            else
                            {
                                var abc = Items.Where(c => c.Barcode == res).FirstOrDefault();
                                abc.Quantity2++;
                                abc.MRP = abc.MRP * abc.Quantity2;
                                //abc.LineGrossValue = abc.FRate * abc.Quantity2;
                                Total.TBill = Total.TBill + abc.MRP;
                                var invoiceU = new clsItem { ItemId = abc.ItemId, ItemName = abc.ItemName, CostPrice = abc.CostPrice, SalePrice = abc.SalePrice, Quantity2 = abc.MRP, ImagePath = abc.ImagePath, MRP = abc.MRP };
                                await _connection.UpdateAsync(invoiceU);
                            }
                        }
                        catch (Exception ex)
                        {
                            await Application.Current.MainPage.Navigation.PushPopupAsync(new AlertPage("S", ex.Message));
                        }
                    }
                    else
                    {
                        await Application.Current.MainPage.Navigation.PushPopupAsync(new AlertPage("E", "Please connect your device with internet."));

                    }
                });
            }
        }
        public Command<clsItem> RemoveItem
        {
            get
            {
                return new Command<clsItem>((clsItem product) =>
               {
                    // await _connection.DeleteAsync(product);
                    Items.Remove(product);
                    Total.TBill = Total.TBill - product.MRP;
                    Total.TCount--;
               });
            }
        }
        public Command<clsItem> AddDisc
        {
            get
            {
                return new Command<clsItem>(async (clsItem c) =>
                {
                    await Application.Current.MainPage.Navigation.PushPopupAsync(new AddDiscountPage(c));

                   MessagingCenter.Subscribe<clsItem>(this, "GetItemList", (data) =>
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

                           
                              
                            var clsitem = new clsItem { ItemId = data.ItemId, ItemName = data.ItemName, CostPrice = data.CostPrice, SalePrice = data.CostPrice, Quantity2 = data.Quantity2, ImagePath = data.ImagePath, Discount = data.Discount, DiscountType = data.DiscountType, TaxValue = data.TaxValue, ValueExTax = data.ValueExTax, MRP = data.MRP, FixRate = data.FixRate };

                            var abc = _connection.InsertAsync(clsitem);
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
                    //bool chk = Items.Where(c => c.Barcode == data.Barcode).Any(c => c.Barcode == data.Barcode);
                    //    if (!chk)
                    //    {
                    try
                    {
                        
                       
                        var clsitem = new clsItem { ItemId = data.ItemId, ItemName = data.ItemName, CostPrice = data.CostPrice, SalePrice = data.CostPrice, Quantity2 = data.Quantity2, ImagePath = data.ImagePath, Discount = data.Discount, DiscountType = data.DiscountType, TaxValue = data.TaxValue, ValueExTax = data.ValueExTax, MRP = data.MRP, FixRate = data.FixRate };

                        var abc = _connection.InsertAsync(clsitem);
                        CountAsync();
                    }
                    catch (Exception ex)
                    {
                        Application.Current.MainPage.Navigation.PushPopupAsync(new AlertPage("E", ex.Message));
                    }
                    // }
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
                    Total.TBill = Items.Sum(s => s.MRP);
                    var invoiceU = new clsItem { ItemId = product.ItemId, ItemName = product.ItemName, CostPrice = product.CostPrice, SalePrice = product.SalePrice, Quantity2 = product.Quantity2, ImagePath = product.ImagePath, MRP = product.MRP, FixRate = product.FixRate };
                    var abc = await _connection.UpdateAsync(invoiceU);
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
                        Total.TBill = Items.Sum(s => s.MRP);
                    }
                    else
                    {
                        Total.TBill = Total.TBill - product.FixRate;

                        //product.LineGrossValue = product.SRate - product.FRate;
                        product.MRP = product.MRP - product.FixRate;
                        Total.TBill = Items.Sum(s => s.MRP);
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
                return new Command(async() =>
                {
                   await navigation.PushAsync(new CartViewPage(id));
                    MessagingCenter.Subscribe<clsInvoice>(this, "CountList",async ( data) =>
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
        public Command ManuallyAddPage
        {
            get
            {
                return new Command(async() =>
                {
                    await navigation.PushAsync(new ManuallyAddPage(id));
                });
            }
        }
    }
}
