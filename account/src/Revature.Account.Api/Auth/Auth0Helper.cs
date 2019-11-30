using RestSharp;
using System.Text.Json;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.IdentityModel.Tokens.Jwt;
using Auth0.ManagementApi;

namespace Revature.Account.Api
{
  public class Auth0Helper
  {
    private static string _domain;
    private static string _audience;
    private static string _clientId;
    private static string _secret;
    private static string _claimsDomain = "https://revature.com/";

    public static readonly string CoordinatorRole = "coordinator";
    public static readonly string UnapprovedProviderRole = "unapproved_provider";
    public static readonly string ApprovedProviderRole = "approved_provider";

    public ManagementApiClient Client { get; private set; }
    public string Email { get; private set; }
    public string[] Roles { get; private set; }
    public JsonElement AppMetadata { get; private set; }

    public static string Domain
    {
      get
      {
        return _domain;
      }
    }

    public static string Audience
    {
      get
      {
        return _audience;
      }
    }

    public static string ClientId
    {
      get
      {
        return _clientId;
      }
    }

    public static string Secret
    {
      get
      {
        return _secret;
      }
    }

    public static void SetSecretValues(string domain, string audience, string clientId, string secret)
    {
      _domain = domain;
      _audience = audience;
      _clientId = clientId;
      _secret = secret;
    }

    public Auth0Helper(HttpRequest request)
    {
      string jwt = request.Headers["Authorization"];
      // Remove 'Bearer '
      jwt = jwt.Substring(7, jwt.Length - 7);
      var handler = new JwtSecurityTokenHandler();
      var token = handler.ReadJwtToken(jwt);

      Email = (string)token.Payload[_claimsDomain + "email"];
      Roles = JsonSerializer.Deserialize<string[]>(token.Payload[_claimsDomain + "roles"].ToString());
      // Will only need the id field from the app metadata
      AppMetadata = JsonSerializer.Deserialize<dynamic>(token.Payload[_claimsDomain + "app_metadata"].ToString());

      var managementToken = GetManagementToken();
      Client = new ManagementApiClient(managementToken, _domain);
    }

    public string GetManagementToken()
    {
      var client = new RestClient($"https://{_domain}/oauth/token");
      var request = new RestRequest(Method.POST);

      /*request.AddHeader("content-type", "application/x-www-form-urlencoded");
      request.AddParameter("application/x-www-form-urlencoded",
        $"client_id={_clientId}&client_secret={_secret}&audience=https%3A%2F%2F{_domain}%2Fapi%2Fv2%2F&grant_type=client_credentials",
        ParameterType.RequestBody);*/

      request.AddHeader("content-type", "application/json");
      request.AddParameter("application/json", $"{{\"client_id\":\"{_clientId}\",\"client_secret\":\"{_secret}\",\"audience\":\"https://{_domain}/api/v2/\",\"grant_type\":\"client_credentials\"}}", ParameterType.RequestBody);

      IRestResponse response = client.Execute(request);

      var deserializedResponse = JsonSerializer.Deserialize<JsonElement>(response.Content);
      return deserializedResponse.GetProperty("access_token").GetString();
    }

    public async Task AddRole(string authUserId, string roleId)
    {
      var rolesRequest = new Auth0.ManagementApi.Models.AssignRolesRequest
      {
        Roles = new string[] { roleId }
      };

      await Client.Users.AssignRolesAsync(authUserId, rolesRequest);
    }

    public async Task RemoveRole(string authUserId, string roleId)
    {
      var rolesRequest = new Auth0.ManagementApi.Models.AssignRolesRequest
      {
        Roles = new string[] { roleId }
      };

      await Client.Users.RemoveRolesAsync(authUserId, rolesRequest);
    }

    public async Task UpdateMetadataWithId(string authUserId, Guid newId)
    {
      JsonElement currentMetadataId;
      bool elementExists = AppMetadata.TryGetProperty("id", out currentMetadataId);
      if ( ! elementExists||
        (elementExists && currentMetadataId.GetString() != newId.ToString()))
      {
        dynamic newMetadata = new { id = newId };

        var userUpdateRequest = new Auth0.ManagementApi.Models.UserUpdateRequest
        {
          AppMetadata = newMetadata
        };

        await Client.Users.UpdateAsync(authUserId, userUpdateRequest);
      }
    }
  }
}
