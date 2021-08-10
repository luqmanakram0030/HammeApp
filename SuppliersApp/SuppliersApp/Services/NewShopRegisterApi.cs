using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Rg.Plugins.Popup.Extensions;
using SuppliersApp.Models;
using SuppliersApp.Utlities;
using SuppliersApp.ViewModels;
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
    public class NewShopRegisterApi
    {
        internal async Task<Result> RegisterShopAsync(clsShops clsShops, ObservableCollection<ImagesSaveBase64> imageObj)
        {
            Result result = new Result();

            try
            {
                var Httpclient = new HttpClient();

                var url = Constants.BaseApiAddress + "MyApi/InsertUpdateShopDetail";

                var uri = new Uri(string.Format(url, string.Empty));

                var json = JsonConvert.SerializeObject(clsShops);

                var content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = null;

                response = await Httpclient.PostAsync(uri, content);


                if (response.StatusCode == HttpStatusCode.OK)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();

                    var jObject = JObject.Parse(responseContent);
                  //  result.ShopId = (int)jObject.GetValue("id");
                    result.Status = (bool)jObject.GetValue("status");
                    result.Message = jObject.GetValue("message").ToString();
                    if (result.Status)
                    {
                        Demmy demmy = new Demmy();
                        demmy.doc = new clsDocument();

                        demmy.doc.TypeId = Convert.ToString((int)jObject.GetValue("id"));
                        demmy.id = await Utilty.GetSecureStorageValueFor(Utilty.UserId);
                        demmy.doc.imagesBase64 = imageObj;


                        var client = new HttpClient();
                        // byte[] array = Encoding.ASCII.GetBytes(image);
                        //var form = new MultipartFormDataContent();

                        //form.Add(new ByteArrayContent(new MemoryStream(array).ToArray()), "image", "image.jpg");
                        var Innercontent = JsonConvert.SerializeObject(demmy);
                        string Innerurl = Constants.BaseApiAddress + "MyApi/SaveImage";
                        var Innerresponse = await client.PostAsync(url, new StringContent(Innercontent, Encoding.UTF8, "application/json"));
                        if (response.StatusCode == HttpStatusCode.OK)
                        {

                          //  await Application.Current.MainPage.Navigation.PushPopupAsync(new AlertPage("S", "Shop Successfully Resgistered"));

                        }
                        else
                        {
                           // await Application.Current.MainPage.Navigation.PushPopupAsync(new AlertPage("E", response.ReasonPhrase.ToString()));
                        }
                    }
                    else
                    {
                      //  await Application.Current.MainPage.Navigation.PushPopupAsync(new AlertPage("S", "Shop already exist."));

                    }
                }
            }
            catch (Exception ex)
            {
               // await Application.Current.MainPage.Navigation.PushPopupAsync(new AlertPage("E", ex.Message));

               
                result.Status = false;
            }
            return result;

        }

    }
}
