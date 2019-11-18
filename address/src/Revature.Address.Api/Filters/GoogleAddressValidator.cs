using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json.Linq;
using Revature.Address.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Revature.Address.Api.Filters
{
  public class GoogleAddressValidator : ActionFilterAttribute
  {
    public override void OnActionExecuting(ActionExecutingContext context)
    {
      var rawQueryString = context.HttpContext.Request.QueryString.ToString();
      rawQueryString = rawQueryString.Substring(1);
      var addressModelProperties = rawQueryString.Split("&");
      int counter = 0;
      if (addressModelProperties[0].Split("=")[0] == "Id"
         || addressModelProperties[0].Split("=")[0] == "id")
      {
        counter += 1;
      }
      string street = addressModelProperties[counter++].Split("=")[1].ToString().Replace("%20", "+");
      var city = addressModelProperties[counter++].Split("=")[1].ToString();
      var state = addressModelProperties[counter++].Split("=")[1].ToString().ToUpper();
      var country = addressModelProperties[counter++].Split("=")[1].ToString().ToUpper();
      var zipcode = addressModelProperties[counter++].Split("=")[1].ToString();
      var http = new HttpClient();

      JObject res = JObject.Parse(CallingGoogleApi(street, city, state, country, zipcode, http).Result);
      JToken formattedAddress = res.SelectToken("results[0].formatted_address");

      string apiAddress = formattedAddress.ToString();

      if (!apiAddress.Contains(street) || !apiAddress.Contains(city) || !apiAddress.Contains(state)
          || !apiAddress.Contains(country) || !apiAddress.Contains(zipcode))
      {
        var response = new JsonResult(new Message()
        {
          MessageType = "Error with Address API not Google API",
          MessageContent = $"Could not validate the given address {street} {city} {state} {zipcode} {country} "
        });
        response.ContentType = "application/json";
        response.StatusCode = 404;
        context.Result = response;
        return;
      }
      return;
    }
    public async Task<string> CallingGoogleApi(string street, string city, string state, string country, string zipcode, HttpClient http)
    {
      string baseUrl = "https://maps.googleapis.com/maps/api/geocode/json?address=";
      baseUrl += $"{street},+{city},+{state},+{country},+{zipcode}&key=AIzaSyBcdxdQRVwksvda1g4tMhjfvEWnpQcrBsA";
      var res = await http.GetAsync(baseUrl);
      return await res.Content.ReadAsStringAsync();
    }
  }

  public class Message
  {
    public string MessageType { get; set; }
    public string MessageContent { get; set; }
  }

}
