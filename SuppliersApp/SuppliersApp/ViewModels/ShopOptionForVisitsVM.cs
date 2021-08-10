using SuppliersApp.Views;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;


namespace SuppliersApp.ViewModels
{
  
    public class ShopOptionForVisitsVM
    {
        public ICommand ShopSearchCommand { get; }
        public ICommand ShopScanCommand { get; }
        public ShopOptionForVisitsVM()
        {
            ShopSearchCommand = new Command(async () => await ShopSearchAsync());
            ShopScanCommand = new Command(async () => await ShopScanAsync());
        }
        private async Task ShopSearchAsync()
        {
            await Application.Current.MainPage.Navigation.PushAsync(new ShopSearchVisitsPage());
        }
        private async Task ShopScanAsync()
        {
            await Application.Current.MainPage.Navigation.PushAsync(new ScanQRCodeVisitsPage());
        }
    }
}
