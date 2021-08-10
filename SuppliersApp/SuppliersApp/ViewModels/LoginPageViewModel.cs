using Rg.Plugins.Popup.Extensions;
using SuppliersApp.Helpers;
using SuppliersApp.Models;
using SuppliersApp.Services;
using SuppliersApp.Views;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace SuppliersApp.ViewModels
{
    public class LoginPageViewModel : BaseViewModel
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
        public clsResponse response1 = new clsResponse();

        public readonly SupplierLoginApi supplierLoginApi = new SupplierLoginApi();
        public LoginModel LoginModel { get; set; } = new LoginModel();
        public ICommand LoginInCommand { get; }
        private LoginPage loginPage;

        public LoginPageViewModel(LoginPage loginPage)
        {
            supplierLoginApi = new SupplierLoginApi();
            response1 = new clsResponse();
            this.loginPage = loginPage;
            LoginInCommand = new Command(async () => await LoginAsync());

        }
        private async Task LoginAsync()
        {
            //Isbusy = true;
            if (LoginModel.Email != null)
            {
                LoginModel.Email = LoginModel.Email.Trim();
            }
            if (!ValidationHelper.IsFormValid(LoginModel, loginPage)) { return; }
            else
            {
                Isbusy = true;
                response1 = await supplierLoginApi.LoginUserAsync(LoginModel.Email, LoginModel.Password);
                if (response1.Status)
                {
                    Isbusy = false;
                  
                    Application.Current.MainPage=new NavigationPage( new MainPage());

                }
                else
                {
                    Isbusy = false;
                    await Application.Current.MainPage.Navigation.PushPopupAsync(new AlertPage("E", response1.Message));
                }
            }
        }
    }
}
