using GoogleApi;
using GoogleApi.Entities.Maps.Geocoding;
using GoogleApi.Entities.Maps.Geocoding.Address.Request;
using Microsoft.Extensions.Logging;
using Revature.Address.Lib.Models.DistanceMatrix;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Revature.Address.Lib.BusinessLogic
{
  public class AddressLogic
  {
    private readonly ILogger _logger;

    // Google's API key
    private const string ApiKey = SecretKey.GApiKey;

    private readonly JsonSerializerOptions _distanceMatrixSerializerOptions = new JsonSerializerOptions
    {
      PropertyNamingPolicy = new JsonSnakeCaseNamingPolicy()
    };

    public AddressLogic(ILogger<AddressLogic> logger = null)
    {
      _logger = logger;
    }

    public async Task<bool> IsInRangeAsync(Address origin, Address destination, int distance)
    {
      // formatted address to be used with Google API
      string formattedOrigin = FormatAddress(origin);
      string formattedDestination = FormatAddress(destination);
      // added parameters to the Google API url
      string googleApiUrlParameter = GetGoogleApiUrl(formattedOrigin, formattedDestination);
      string googleBaseUrl = "https://maps.googleapis.com/maps/api/distancematrix/json";

      Response deserialized;

      using var client = new HttpClient { BaseAddress = new Uri(googleBaseUrl) };
      // Add an Accept header for JSON format.
      client.DefaultRequestHeaders.Accept.Add(
        new MediaTypeWithQualityHeaderValue("application/json"));

      // await for async call and get response.
      using HttpResponseMessage response = await client.GetAsync(googleApiUrlParameter).ConfigureAwait(false);
      if (response.IsSuccessStatusCode)
      {
        deserialized = JsonSerializer.Deserialize<Response>(
          await response.Content.ReadAsStringAsync().ConfigureAwait(false), _distanceMatrixSerializerOptions);
        // Parse the response body.
        string distanceValueString = deserialized.Rows[0].Elements[0].Distance.Text;
        // convert the response to a double
        var distanceValueDouble = double.Parse(distanceValueString[0..^2]);
        if (distanceValueDouble <= distance)
        {
          return true;
        }
        else
        {
          return false;
        }
      }
      else
      {
        _logger.LogWarning("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
        if ((int)response.StatusCode >= 400 && (int)response.StatusCode < 500)
        {
          throw new ArgumentException($"User input error {response.StatusCode}");
        }
        if ((int)response.StatusCode >= 500 && (int)response.StatusCode < 600)
        {
          throw new ArgumentException($"Google API returned internal server-side error {response.StatusCode}");
        }
        throw new ArgumentException($"Google API call was unsuccessful {response.StatusCode}");
      }
    }

    public async Task<bool> IsValidAddress(Address address)
    {
      AddressGeocodeRequest request = new AddressGeocodeRequest();
      request.Address = $"{address.Street} {address.City}, {address.State} {address.ZipCode} {address.Country}";
      request.Key = SecretKey.GApiKey;
      GeocodeResponse response = await GoogleMaps.AddressGeocode.QueryAsync(request);
      var results = response.Results.ToArray();

      if(results.Length != 0)
      {
        return true;
      } else
      {
        return false;
      }
    }

    public static string GetGoogleApiUrl(string origin, string destination)
    {
      // Google distance matrix parameters
      return $"?units=imperial&origins={origin}&destinations={destination}&key=" + ApiKey;
    }

    public static string FormatAddress(Address address)
    {
      // formats address for Google API
      return address.Street.Replace(" ", "+") + "+" + address.City.Replace(" ", "+") + "," + address.State.Replace(" ", "+") + "+" + address.ZipCode;
    }
  }
}
