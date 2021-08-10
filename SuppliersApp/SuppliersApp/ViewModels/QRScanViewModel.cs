using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ZXing;
using Xamarin.Forms;
using SuppliersApp.Views;
using ZXing.Mobile;
using ZXing.Net.Mobile.Forms;
using Rg.Plugins.Popup.Extensions;

namespace SuppliersApp.ViewModels
{
    public class QRScanViewModel : BaseViewModel
    {
        
        private bool _isAnalyzing = true;
        public bool IsAnalyzing
        {
            get { return _isAnalyzing; }
            set
            {
                if (!Equals(_isAnalyzing, value))
                {
                    _isAnalyzing = value;
                }
            }
        }

        private bool _isScanning = true;
        public bool IsScanning
        {
            get { return _isScanning; }
            set
            {
                if (!Equals(_isScanning, value))
                {
                    _isScanning = value;
                }
            }
        }
        public Result result { get; set; }

        public Models.clsShops shopData { get; set; }

        public QRScanViewModel(Models.clsShops clsShops)
        {
            this.shopData = clsShops;
        }
        public Command ShopQRCommand
        {
            get
            {
                return new Command(async () =>
                {
                try
                {
                    IsAnalyzing = false;
                    IsScanning = false;

                    Device.BeginInvokeOnMainThread(()=>
                    {
                        shopData.QRCode = result.Text;
                        Application.Current.MainPage.Navigation.PopModalAsync();
                    });
                      
                    IsAnalyzing = true;
                    IsScanning = true;
                }

                catch (Exception ex)

                {
                   await Application.Current.MainPage.Navigation.PushPopupAsync(new AlertPage("E", ex.Message));
                }

                });
             }
        }
    }
}