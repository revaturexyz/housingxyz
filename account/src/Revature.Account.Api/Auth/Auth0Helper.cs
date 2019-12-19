using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Text.Json;
using System.Threading.Tasks;
using Auth0.ManagementApi;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using RestSharp;

namespace Revature.Account.Api
{
  public class Auth0Helper
  {
    public static readonly string CoordinatorRole = "coordinator";
    public static readonly string UnapprovedProviderRole = "unapproved_provider";
    public static readonly string ApprovedProviderRole = "approved_provider";

    private readonly ILogger _logger;

    public ManagementApiClient Client { get; private set; }
    public string Email { get; private set; }
    public IEnumerable<string> Roles { get; private set; }
    public JsonElement AppMetadata { get; private set; }

    public static string Domain { get; private set; }

    public static string Audience { get; private set; }

    public static string ClientId { get; private set; }

    public static string Secret { get; private set; }

    public static string ClaimsDomain { get; } = "https://revature.com/";

    /// <summary>
    /// Function to set the secret values, intended for use in Startup.
    /// </summary>
    /// <param name="domain"></param>
    /// <param name="audience"></param>
    /// <param name="clientId"></param>
    /// <param name="secret"></param>
    public static void SetSecretValues(string domain, string audience, string clientId, string secret)
    {
      Domain = domain;
      Audience = audience;
      ClientId = clientId;
      Secret = secret;
    }

    public Auth0Helper(HttpRequest request, ILogger logger)
    {
      string jwt = request.Headers["Authorization"];
      // Remove 'Bearer '
      jwt = jwt[7..];
      var handler = new JwtSecurityTokenHandler();
      var token = handler.ReadJwtToken(jwt);

      Email = (string)token.Payload[ClaimsDomain + "email"];
      Roles = JsonSerializer.Deserialize<string[]>(token.Payload[ClaimsDomain + "roles"].ToString());
      // Will only need the id field from the app metadata
      AppMetadata = JsonSerializer.Deserialize<dynamic>(token.Payload[ClaimsDomain + "app_metadata"].ToString());
      _logger = logger;
    }

    /// <summary>
    /// Runs code to set up the management client, which involves sending a request to Auth0 in order to get
    /// an authenticated token. Moved to a function so that it can be ignored if we just want
    /// to read the token's values.
    /// </summary>
    /// <returns></returns>
    public bool ConnectManagementClient()
    {
      var client = new RestClient($"https://{Domain}/oauth/token");
      var request = new RestRequest(Method.POST);

      request.AddHeader("content-type", "application/json");
      request.AddParameter("application/json", $"{{\"client_id\":\"{ClientId}\",\"client_secret\":\"{Secret}\",\"audience\":\"https://{Domain}/api/v2/\",\"grant_type\":\"client_credentials\"}}", ParameterType.RequestBody);

      var response = client.Execute(request);

      if (response.ErrorException != null)
      {
        _logger.LogError(response.ErrorException, "Error while making Auth0 request");
        return false;
      }
      try
      {
        var deserializedResponse = JsonSerializer.Deserialize<JsonElement>(response.Content);
        var managementToken = deserializedResponse.GetProperty("access_token").GetString();
        Client = new ManagementApiClient(managementToken, Domain);
        return true;
      }
      catch (JsonException ex)
      {
        _logger.LogError(ex, "Error while processing Auth0 response");
      }
      catch (InvalidOperationException ex)
      {
        _logger.LogError(ex, "Error while processing Auth0 response");
      }
      catch (KeyNotFoundException ex)
      {
        _logger.LogError(ex, "Error while processing Auth0 response");
      }
      return false;
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
      var elementExists = AppMetadata.TryGetProperty("id", out var currentMetadataId);
      if (!elementExists || currentMetadataId.GetString() != newId.ToString())
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
