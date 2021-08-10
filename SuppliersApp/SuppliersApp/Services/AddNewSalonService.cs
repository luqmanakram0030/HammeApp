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

namespace SuppliersApp.Services
{
    public class AddNewSalonService
    {
        internal async Task<Result> RegisterSalonAsync(clsSalon clsSalon)
        {
            Result result = new Result();
            try
            {
                var Httpclient = new HttpClient();

                var url = Constants.BaseApiAddress + "MyApi/InsertUpdateSalon";

                var uri = new Uri(string.Format(url, string.Empty));

                var json = JsonConvert.SerializeObject(clsSalon);

                var content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = null;

                response = await Httpclient.PostAsync(uri, content);


                if (response.StatusCode == HttpStatusCode.OK)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();

                    var jObject = JObject.Parse(responseContent);
                    result.ShopId = (int)jObject.GetValue("id");
                    result.Status = (bool)jObject.GetValue("status");
                    result.Message = jObject.GetValue("message").ToString();

                }
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
                result.Status = false;
            }
            return result;
        }
    }
}
