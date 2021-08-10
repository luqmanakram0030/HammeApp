using Rg.Plugins.Popup.Extensions;
using SuppliersApp.Models;
using SuppliersApp.Services;
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
    public class QuestionnaireViewVM:BaseViewModel
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
                    Application.Current.MainPage.Navigation.PushPopupAsync(new IndicatorActity());

                }
                else
                {
                    Application.Current.MainPage.Navigation.PopPopupAsync();

                }

                OnPropertyChanged();
            }
        }

        public Result result;
        public clsSalon clsSalon { get; set; }
        public readonly AddNewSalonService SalonApi;
        public ICommand SalonRegisterCommand { get; }
        public QuestionnaireViewVM(clsSalon clsSalon)
        {
            this.clsSalon = clsSalon;
            SalonApi = new AddNewSalonService();
            SalonRegisterCommand = new Command(async () => await SalonRegisterAsync());
        }
        public async Task SalonRegisterAsync()
        {
            Isbusy = true;

            
                try
                {
                    var request = new GeolocationRequest(GeolocationAccuracy.Medium);
                    var location = await Geolocation.GetLocationAsync(request);
                clsSalon.Latitude = location.Latitude.ToString();
                clsSalon.Longitude = location.Longitude.ToString();
                    result = await SalonApi.RegisterSalonAsync(clsSalon);
                    if (result.Status)
                    {
                        Isbusy = false;

                       // await Application.Current.MainPage.Navigation.PushAsync(new StoreShopImages(result.ShopId));
                    }
                    else
                    {
                        Isbusy = false;
                        await Application.Current.MainPage.Navigation.PushPopupAsync(new AlertPage("E", result.Message));
                    }
                }
                catch (Exception ex)
                {
                    Isbusy = false;
                    await Application.Current.MainPage.Navigation.PushPopupAsync(new AlertPage("E", ex.Message));

                }



            




        }
    }
}
