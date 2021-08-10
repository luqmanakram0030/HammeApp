using Rg.Plugins.Popup.Extensions;
using SuppliersApp.Models;
using SuppliersApp.Services;
using SuppliersApp.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace SuppliersApp.ViewModels
{
   
    class ShopSearchVisitsVM : BaseViewModel
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

        private Models.clsShops _SelectedShop;

        public Models.clsShops SelectedShop
        {
            get { return _SelectedShop; }
            set
            {
                _SelectedShop = value;
                OnPropertyChanged();
            }
        }
        public readonly ShopsSearchApi searchApi;
        private INavigation navigation;
        private string _search;
        private ObservableCollection<clsShops> _ShopsList { get; set; }
        public ObservableCollection<clsShops> ShopsList
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
        public string search
        {
            get { return _search; }
            set
            {
                _search = value;
                OnPropertyChanged();
            }
        }

        public ShopSearchVisitsVM(INavigation navigation)
        {
            this.navigation = navigation;
            searchApi = new ShopsSearchApi();
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
                        ShopsList = await searchApi.GetShopsAsync(search);
                        Isbusy = false;
                    }
                    else
                    {
                        await Application.Current.MainPage.Navigation.PushPopupAsync(new AlertPage("E", "Please connect your device with internet."));

                    }
                });
            }


        }

        public Command SelectedShopCmd
        {

            get
            {
                return new Command(async () =>
                {
                    ShopsList.Remove(SelectedShop);
                  
                     await navigation.PushAsync(new VisitsPage(SelectedShop));
                });
            }
        }
    }
}
