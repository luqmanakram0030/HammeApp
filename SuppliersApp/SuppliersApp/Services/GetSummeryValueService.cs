using Newtonsoft.Json;
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
    public class GetSummeryValueService
    {
        internal async Task<clsSummery> GetValueAsync()
        {
            clsSummery clsSummery = new clsSummery();
            clsSummery.Summary = new Summary();

            try
            {

                var Httpclient = new HttpClient();

                var url = Constants.BaseApiAddress + "MyApi/GetEmployeeSummary/1";

                var uri = new Uri(string.Format(url, string.Empty));


                HttpResponseMessage response = null;

                response = await Httpclient.GetAsync(uri);

               
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();

                    clsSummery = JsonConvert.DeserializeObject<clsSummery>(responseContent); 
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("", ex.Message, "Ok");
            }

            return clsSummery;

        }

    }

}
