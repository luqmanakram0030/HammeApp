using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SuppliersApp.Models;
using SuppliersApp.Utlities;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SuppliersApp.Services
{
    public class SupplierLoginApi
    {
        internal async Task<clsResponse> LoginUserAsync(string Email, string Password)
        {
            var response1 = new clsResponse
            {
                Message = "Please try again",
                Status = false
            };
            try
            {
              

                bool status = false;
                var Httpclient = new HttpClient();

                var url = Constants.BaseApiAddress + "MyApi/SupplierLogin?Email=" + Email + "&Password=" + Password;

                var uri = new Uri(string.Format(url, string.Empty));





                HttpResponseMessage response = null;

                response = await Httpclient.GetAsync(uri);


                if (response.StatusCode == HttpStatusCode.OK)
                {

                    var responseContent = await response.Content.ReadAsStringAsync();
                    var jObject = JObject.Parse(responseContent);
                    //Isbusy = false;
                    status = (bool)jObject.GetValue("status");
                    var UserId = jObject.GetValue("UserId").ToString();
                    //await Application.Current.MainPage.DisplayAlert("Alert", jObject.GetValue("message").ToString(), "OK");
                    if (status)
                    {
                        try
                        {

                            await Utilty.SetSecureStorageValue(Utilty.UserId, UserId);
                            await Utilty.SetSecureStorageValue(Utilty.UserName, Email);
                            await Utilty.SetSecureStorageValue(Utilty.Password, Password);
                        }
                        catch (Exception ex)
                        {
                            // Possible that device doesn't support secure storage on device.
                        }
                    }
                    response1 = new clsResponse
                    {
                        Message = jObject.GetValue("message").ToString(),
                        Status = status
                    };
                    //if (status)
                    //{
                    //    Isbusy = false;
                    //    Application.Current.MainPage = new NavigationPage(new orderApp.Views.LoginPage());
                    //    await Application.Current.MainPage.DisplayAlert("Alert", jObject.GetValue("message").ToString(), "OK");
                    //}

                }
                else
                {
                    //IsBusy = false;
                }
              
            }
            catch (Exception ex)
            {
                response1.Message = ex.Message;
                response1.Status = false;
            }
            return response1;
        }
    }
}
