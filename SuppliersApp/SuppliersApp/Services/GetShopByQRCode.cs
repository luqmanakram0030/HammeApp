using Newtonsoft.Json.Linq;
using Rg.Plugins.Popup.Extensions;
using SuppliersApp.Models;
using SuppliersApp.Utlities;
using SuppliersApp.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SuppliersApp.Services
{
    public class GetShopByQRCode
    {
        internal async Task<ObservableCollection<clsShops>> GetShopAsync(string qrcode)
        {
            ObservableCollection<clsShops> getresponse = new ObservableCollection<clsShops>();
            try
            {
                var Httpclient = new HttpClient();

                var url = Constants.BaseApiAddress + "MyApi/GetShopByQRCode?qrcode=" + qrcode;

                var uri = new Uri(string.Format(url, string.Empty));


                HttpResponseMessage response = null;

                response = await Httpclient.GetAsync(uri);
                if (response.StatusCode == HttpStatusCode.OK)
                {

                    var responseContent = await response.Content.ReadAsStringAsync();
                    var jObject = JObject.Parse(responseContent);
                    JArray loi = (JArray)jObject.GetValue("Shop");
                    getresponse = loi.ToObject<ObservableCollection<clsShops>>();

                }
            }
            catch(Exception ex)
            {
                await Application.Current.MainPage.Navigation.PushPopupAsync(new AlertPage("E", ex.Message));
            }
            return getresponse;
        }
    }
}
