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
   public class OrderPlacedApi
    {
        internal async Task<clsPlacedOrderResponse> PlaceOrder(clsMarketOrder items)
        {
            clsPlacedOrderResponse response1 = new clsPlacedOrderResponse();
            try
            {

                var Httpclient = new HttpClient();

                var url = Constants.BaseApiAddress + "MyApi/InsertUpdateMarketOrder";

                var uri = new Uri(string.Format(url, string.Empty));

                var json = JsonConvert.SerializeObject(items);

                var content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = null;

                response = await Httpclient.PostAsync(uri, content);


                if (response.StatusCode == HttpStatusCode.OK)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();

                    var jObject = JObject.Parse(responseContent);



                    response1 = new clsPlacedOrderResponse
                    {
                        Status = (bool)jObject.GetValue("status"),

                        Message = jObject.GetValue("message").ToString(),
                        //OrderNumber = jObject.GetValue("orderNumber").ToString(),

                    };



                }
            }
            catch(Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("", ex.Message, "Ok");
            }

            return response1;
        }
    }
}
