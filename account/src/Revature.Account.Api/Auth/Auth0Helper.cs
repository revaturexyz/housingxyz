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

    public static string ClaimsDomain
    {
      get
      {
        return _claimsDomain;
      }
    }

    /// <summary>
    /// Function to set the secret values, intended for use in Startup.
    /// </summary>
    /// <param name="domain"></param>
    /// <param name="audience"></param>
    /// <param name="clientId"></param>
    /// <param name="secret"></param>
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

      Email = (string)token.Payload[ClaimsDomain + "email"];
      Roles = JsonSerializer.Deserialize<string[]>(token.Payload[ClaimsDomain + "roles"].ToString());
      // Will only need the id field from the app metadata
      AppMetadata = JsonSerializer.Deserialize<dynamic>(token.Payload[ClaimsDomain + "app_metadata"].ToString());
    }

    /// <summary>
    /// Runs code to set up the management client, which involves sending a request to Auth0 in order to get
    /// an authenticated token. Moved to a function so that it can be ignored if we just want
    /// to read the token's values.
    /// </summary>
    /// <returns></returns>
    public bool ConnectManagementClient()
    {
      try
      {
        var client = new RestClient($"https://{_domain}/oauth/token");
        var request = new RestRequest(Method.POST);

        request.AddHeader("content-type", "application/json");
        request.AddParameter("application/json", $"{{\"client_id\":\"{ClientId}\",\"client_secret\":\"{Secret}\",\"audience\":\"https://{Domain}/api/v2/\",\"grant_type\":\"client_credentials\"}}", ParameterType.RequestBody);

        IRestResponse response = client.Execute(request);

        var deserializedResponse = JsonSerializer.Deserialize<JsonElement>(response.Content);
        var managementToken = deserializedResponse.GetProperty("access_token").GetString();

        Client = new ManagementApiClient(managementToken, _domain);

        if (Client != null)
          return true;

        return false;
      }
      catch (Exception e)
      {
        return false;
      }
    }

    /// <summary>
    /// Adds a role to the remote Auth0 profile.
    /// </summary>
    /// <param name="authUserId">UserId according to Auth0. Has to be retrieved from the Management client.</param>
    /// <param name="roleId">RoleId according to Auth0. Has to be retrieved from the Management client.</param>
    /// <returns></returns>
    public async Task AddRoleAsync(string authUserId, string roleId)
    {
      var rolesRequest = new Auth0.ManagementApi.Models.AssignRolesRequest
      {
        Roles = new string[] { roleId }
      };

      await Client.Users.AssignRolesAsync(authUserId, rolesRequest);
    }

    /// <summary>
    /// Removes a role from the remote Auth0 profile.
    /// </summary>
    /// <param name="authUserId">UserId according to Auth0. Has to be retrieved from the Management client.</param>
    /// <param name="roleId">RoleId according to Auth0. Has to be retrieved from the Management client.</param>
    /// <returns></returns>
    public async Task RemoveRoleAsync(string authUserId, string roleId)
    {
      var rolesRequest = new Auth0.ManagementApi.Models.AssignRolesRequest
      {
        Roles = new string[] { roleId }
      };

      await Client.Users.RemoveRolesAsync(authUserId, rolesRequest);
    }

    /// <summary>
    /// Updates remote Auth0 profile's app metadata to include the given Revature account id. 
    /// </summary>
    /// <param name="authUserId"></param>
    /// <param name="newId"></param>
    /// <returns></returns>
    public async Task UpdateMetadataWithIdAsync(string authUserId, Guid newId)
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
