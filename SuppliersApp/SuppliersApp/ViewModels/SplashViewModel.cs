using Rg.Plugins.Popup.Extensions;
using SuppliersApp.Models;
using SuppliersApp.Services;
using SuppliersApp.Utlities;
using SuppliersApp.Views;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace SuppliersApp.ViewModels
{
   
    public class SplashViewModel
    {
        public clsResponse response1 = new clsResponse();
        public readonly SupplierLoginApi supplierLoginApi = new SupplierLoginApi();
        public SplashViewModel()
        {
            
                LoginExist();
            
        }

        public async void LoginExist()
        {
           try
           {
                var username = Utilty.GetSecureStorageValueFor(Utilty.UserName);
                var password = Utilty.GetSecureStorageValueFor(Utilty.Password);
                
                response1 = await supplierLoginApi.LoginUserAsync(username.Result, password.Result);
                if (response1.Status)
                  {
                    Application.Current.MainPage = new NavigationPage(new MainPage());
                   
                  }
                else
                  {
                   Application.Current.MainPage = new NavigationPage(new LoginPage());
                  }
           }

           catch (Exception ex)
           {
           await Application.Current.MainPage.Navigation.PushPopupAsync(new AlertPage("E", ex.Message));
           }
            
        }
    }
}

