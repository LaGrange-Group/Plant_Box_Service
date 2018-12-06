using Newtonsoft.Json.Linq;
using Plant_Box_Service.Models;
using System;
using System.IO;
using System.Linq;
using System.Net;

namespace Trash_Collector.Class
{
    public static class Geocode
    {
        private static ApplicationDbContext db = new ApplicationDbContext();
        public static bool isValidLocation = false;

        public static bool CheckValidAddress(Customer customer)
        {
            try
            {
                string url = SetUrl(customer);
                string result = GET(url);
                JObject o = JObject.Parse(result);
                var locType = o["results"][0]["geometry"]["location_type"];
                string locResult = locType.ToString();
                if (locResult == "ROOFTOP")
                {
                    isValidLocation = true;
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
        private static string SetUrl (Customer customer)
        {
            string address = ReplaceSpaceWithPlus(customer.Address);
            string city = ReplaceSpaceWithPlus(customer.City);
            string state = db.States.Where(s => s.Id == customer.StateId).Select(s => s.Name).Single();
            string urlGeocode = "https://maps.googleapis.com/maps/api/geocode/json?address=" + address + ",+" + city + ",+" + state + "&key=AIzaSyCm51Yofz7jCEmtkvN4mFady9iETRhqm_s";
            return urlGeocode;
        }
        private static string ReplaceSpaceWithPlus(string thisString)
        {
            thisString = thisString.Replace(" ", "+");
            return thisString;
        }
        private static string GET(string url)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            try
            {
                WebResponse response = request.GetResponse();
                using (Stream responseStream = response.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(responseStream, System.Text.Encoding.UTF8);
                    return reader.ReadToEnd();
                }
            }
            catch (WebException ex)
            {
                WebResponse errorResponse = ex.Response;
                using (Stream responseStream = errorResponse.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(responseStream, System.Text.Encoding.GetEncoding("utf-8"));
                    String errorText = reader.ReadToEnd();
                    // log errorText
                }
                throw;
            }
        }
    }
}