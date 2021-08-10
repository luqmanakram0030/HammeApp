using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SuppliersApp.Models;
using SuppliersApp.Utlities;
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
    public class GetItemsBySearch
    {
        internal async Task<ObservableCollection<clsItem>> GetItemsAsync(string search)
        {
            ObservableCollection<clsItem> getresponse;
            getresponse = new ObservableCollection<clsItem>();

            try
            {

                var Httpclient = new HttpClient();

                var url = Constants.BaseApiAddress + "MyApi/getitemsbysearch?Search=" + search;

                var uri = new Uri(string.Format(url, string.Empty));


                HttpResponseMessage response = null;

                response = await Httpclient.GetAsync(uri);
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    var jObject = JObject.Parse(responseContent);
                    JArray loi = (JArray)jObject.GetValue("itemList");
                    getresponse = loi.ToObject<ObservableCollection<clsItem>>();

                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("", ex.Message, "Ok");
            }

            return getresponse;

        }
    
    }
}
