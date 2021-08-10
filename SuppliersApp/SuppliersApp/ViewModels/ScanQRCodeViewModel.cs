
using Rg.Plugins.Popup.Extensions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using ZXing;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using SuppliersApp.Services;
using SuppliersApp.Views;

namespace SuppliersApp.ViewModels
{
    public class ScanQRCodeViewModel:BaseViewModel
    {
        private Models.clsShops _SelectedShop;

        public Models.clsShops SelectedShop
        {
            get { return _SelectedShop; }
            set { _SelectedShop = value;
                OnPropertyChanged();
            }
        }

        private INavigation navigation;
        public readonly GetShopByQRCode qrcodeapi;
        private ObservableCollection<Models.clsShops> _ShopsList { get; set; }
        public ObservableCollection<Models.clsShops> ShopsList
        {
            get
            {
                return _ShopsList;
            }
            set
            {
                _ShopsList = value;
                OnPropertyChanged();
            }
        }
        public ScanQRCodeViewModel(INavigation navigation)
        {
            qrcodeapi = new GetShopByQRCode();
            this.navigation = navigation;
        }
        public Command<Result> AddProductCommand
        {

            get
            {
                return new Command<Result>(async (Result result) =>
                {

                    var duration = TimeSpan.FromSeconds(0.1);
                    Vibration.Vibrate(duration);

                    string qrcode = result.Text;
                    ShopsList = await qrcodeapi.GetShopAsync(qrcode);
                //  await  navigation.PushAsync(new BarcodePage());
                    //bool chk = Items.Where(c => c.Barcode == res).Any(c => c.Barcode == res);
                    //if (!chk)
                    //{

                    //    barcode.barcode = res;
                    //    var a = await getItemByBarcode.GetItemByBarcodeAsync(barcode);
                    //    Items.Add(new clsItem
                    //    {
                    //        ItemId = a[0].ItemId,
                    //        ItemName = a[0].ItemName,
                    //        Barcode = a[0].Barcode,
                    //        CostPrice = a[0].CostPrice,
                    //        SalePrice = a[0].CostPrice,
                    //        Quantity2 = a[0].Quantity2,
                    //        ImagePath = a[0].ImagePath



                    //    });
                    //    Total.TBill = Total.TBill + a[0].CostPrice;
                    //    Total.TCount++;
                    //    var clsitem = new clsItem { ItemId = a[0].ItemId, ItemName = a[0].ItemName, CostPrice = a[0].CostPrice, SalePrice = a[0].CostPrice, Quantity2 = a[0].Quantity2, ImagePath = a[0].ImagePath };

                    //    var abc = await _connection.InsertAsync(clsitem);
                    //}
                    //else
                    //{
                    //    var abc = Items.Where(c => c.Barcode == res).FirstOrDefault();
                    //    abc.Quantity2++;
                    //    abc.SalePrice = abc.CostPrice * abc.Quantity2;
                    //    //abc.LineGrossValue = abc.FRate * abc.Quantity2;
                    //    Total.TBill = Total.TBill + abc.CostPrice;
                    //    var invoiceU = new clsItem { ItemId = abc.ItemId, ItemName = abc.ItemName, CostPrice = abc.CostPrice, SalePrice = abc.SalePrice, Quantity2 = abc.Quantity2, ImagePath = abc.ImagePath };
                    //    await _connection.UpdateAsync(invoiceU);
                    //}
                });
            }
        }
        public Command SelectedShopCmd
        {

            get
            {
                return new Command(async () =>
                {


                  await  navigation.PushAsync(new BarcodePage(SelectedShop.ShopID));
                    
                    
                  
                });
            }
        }
    }
}
