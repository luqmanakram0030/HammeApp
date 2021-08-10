using SuppliersApp.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace SuppliersApp.ViewModels
{
    public class MenuPageViewModel : BaseViewModel
    {
        public ICommand NewOrderBtn { get; }
        public ICommand NewShopcmd { get; }
        public ICommand VisitsShopCmd { get; }
        public ICommand NewSaloonBtn { get; }
        public ICommand SignOut { get; }
        public MenuPageViewModel()
        {
            NewOrderBtn = new Command(async () => await ShopOptionAsync());
            NewShopcmd = new Command(async () => await NavtoNewShopAsync());
            VisitsShopCmd = new Command(async () => await VisitsShopCmdAsync());
            NewSaloonBtn = new Command(async () => await NavtoNewSaloonAsync());
            SignOut = new Command(async () => await SignOutAsync());
        }
        private async Task ShopOptionAsync()
        {
            await Application.Current.MainPage.Navigation.PushAsync(new ShopOptionPage());
        }
        private async Task NavtoNewSaloonAsync()
        {
              await Application.Current.MainPage.Navigation.PushAsync(new SalonView());
        }
        private async Task VisitsShopCmdAsync()
        {
              await Application.Current.MainPage.Navigation.PushAsync(new ShopOptionForVisitsPage());
        }
        private async Task NavtoNewShopAsync()
        {
              await Application.Current.MainPage.Navigation.PushAsync(new NewShopPage());
        } 
        private async Task SignOutAsync()
        {
            SecureStorage.RemoveAll();

            Application.Current.MainPage = new NavigationPage(new LoginPage());
        }
    }

}
