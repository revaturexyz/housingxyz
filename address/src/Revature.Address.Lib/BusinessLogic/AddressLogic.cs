using GoogleApi;
using GoogleApi.Entities.Maps.Geocoding;
using GoogleApi.Entities.Maps.Geocoding.Address.Request;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Revature.Address.Lib.Models.DistanceMatrix;
using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;

namespace Revature.Address.Lib.BusinessLogic
{
  /// <summary>
  /// Contains the logic for making calls to Google Api's
  /// </summary>
  public class AddressLogic
  {
    private readonly ILogger _logger;
    private readonly IConfiguration _configuration;

    // Configures JsonSerializer with a snake case naming policy
    private readonly JsonSerializerOptions _distanceMatrixSerializerOptions = new JsonSerializerOptions
    {
      PropertyNamingPolicy = new JsonSnakeCaseNamingPolicy()
    };

    /// <summary>
    /// Constructor for AddressLogic object
    /// </summary>
    /// <param name="logger"></param>
    public AddressLogic(IConfiguration configuration, ILogger<AddressLogic> logger = null)
    {
      _logger = logger;
      _configuration = configuration;
    }

    /// <summary>
    /// Makes a call to Google's Distance Matrix API to check if two given address
    /// are within a specified distance of each other.
    /// </summary>
    /// <param name="origin"></param>
    /// <param name="destination"></param>
    /// <param name="distance"></param>
    /// <returns>Return true or false</returns>
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
      using HttpResponseMessage response = await client.GetAsync(googleApiUrlParameter + _configuration["GoogleApiKey"]).ConfigureAwait(false);
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

    /// <summary>
    /// Makes a call to Google's Geocode API to check if a given address exists
    /// </summary>
    /// <param name="address"></param>
    /// <returns>Returns true or false</returns>
    public async Task<bool> IsValidAddress(Address address)
    {
      AddressGeocodeRequest request = new AddressGeocodeRequest
      {
        Address = $"{address.Street} {address.City}, {address.State} {address.ZipCode} {address.Country}",
        Key = _configuration["GoogleApiKey"]
      };
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

    /// <summary>
    /// Builds query portion of the url for Distance Matrix API call with a given origin and destination
    /// and API key and sets the units for response data to imperial
    /// </summary>
    /// <param name="origin"></param>
    /// <param name="destination"></param>
    /// <returns>Returns the url query string </returns>
    public static string GetGoogleApiUrl(string origin, string destination)
    {
      // Google distance matrix parameters
      return $"?units=imperial&origins={origin}&destinations={destination}&key=";
    }

    /// <summary>
    /// Replaces white space with '+'s in Address information for proper url integration
    /// </summary>
    /// <param name="address"></param>
    /// <returns>Returns properly formatted address string</returns>
    public static string FormatAddress(Address address)
    {
      // formats address for Google API
      return address.Street.Replace(" ", "+") + "+" + address.City.Replace(" ", "+") + "," + address.State.Replace(" ", "+") + "+" + address.ZipCode;
    }
  }
}
