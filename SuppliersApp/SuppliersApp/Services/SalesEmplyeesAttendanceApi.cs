using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Rg.Plugins.Popup.Extensions;
using SuppliersApp.Models;
using SuppliersApp.Utlities;
using SuppliersApp.Views;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace SuppliersApp.Services
{
    public class SalesEmplyeesAttendanceApi
    {
        internal async Task<Result> AttendanceApi(string attendancebtn)
        {
            Result result = new Result();
            try
            {
                var Httpclient = new HttpClient();
                EAttendance employeeAtt = new EAttendance();
                employeeAtt.SEAttendance = new clsSalesEmployee();
                employeeAtt.SEAttendance.EmployeeID = await Utilty.GetSecureStorageValueFor(Utilty.UserId);
                var request = new GeolocationRequest(GeolocationAccuracy.Medium);
                var location = await Geolocation.GetLocationAsync(request);
                employeeAtt.SEAttendance.Latitude = location.Latitude.ToString();
                employeeAtt.SEAttendance.Longitude = location.Longitude.ToString();
                var url = Constants.BaseApiAddress + "MyApi/InsertUpdateSaleEmplyeesAttendance";

                var uri = new Uri(string.Format(url, string.Empty));
                var json = JsonConvert.SerializeObject(employeeAtt);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = null;

                response = await Httpclient.PostAsync(uri, content);
                if (response.StatusCode == HttpStatusCode.OK)
                {

                    var responseContent = await response.Content.ReadAsStringAsync();
                    var jObject = JObject.Parse(responseContent);
                    string loi = (string)jObject.GetValue("Status");


                }
                if (attendancebtn == "Start Day")
                {
                    await Utilty.SetSecureStorageValue(Utilty.Attendancebtn, "End Day");
                }
                else
                {
                    await Utilty.SetSecureStorageValue(Utilty.Attendancebtn, "Start Day");
                }
                result.Status = false;
            }
            catch (Exception ex)
            {

                result.Status = true;
                result.Message = ex.Message;

            }
            return result;
            //return attendancebtn;
        }
    }
}
