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
    public class FollowupService
    {
        internal async static Task<Result> SaveDataVisitAsync(clsVisit visit)
        {
            Result result = new Result();

            try
            {
                result.Status = false;

                var Httpclient = new HttpClient();

                var url = Constants.BaseApiAddress + "MyApi/InsertUpdateVisit";

                var uri = new Uri(string.Format(url, string.Empty));

                var json = JsonConvert.SerializeObject(visit);

                var content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = null;

                response = await Httpclient.PostAsync(uri, content);


                if (response.StatusCode == HttpStatusCode.OK)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();


                    var jObject = JObject.Parse(responseContent);
                    result.Status = (bool)jObject.GetValue("status");
                    result.Message = jObject.GetValue("message").ToString();
                }

            }
            catch
            {

            }
            return result;
    }

      
    }
}
