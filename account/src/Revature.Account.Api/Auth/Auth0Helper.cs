using RestSharp;
using System.Text.Json;
using System;
using System.Collections.Generic;
using System.Linq;
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

    public static readonly string CoordinatorRole = "coordinator";
    public static readonly string UnapprovedProviderRole = "unapproved_provider";
    public static readonly string ApprovedProviderRole = "approved_provider";

    public ManagementApiClient Client { get; private set; }
    public string Email { get; private set; }
    public string[] Roles { get; private set; }
    public dynamic AppMetadata { get; private set; }

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
      var jwt = request.Headers["Authorization"];
      var handler = new JwtSecurityTokenHandler();
      var token = handler.ReadToken(jwt) as JwtSecurityToken;

      Email = token.Claims.First(c => c.Type == "https://dbnd:auth0:com/email").Value;
      Roles = JsonSerializer.Deserialize<string[]>(token.Claims.First(c => c.Type == "https://dbnd:auth0:com/roles").Value);
      // Will only need the id field from the app metadata
      AppMetadata = JsonSerializer.Deserialize<dynamic>(token.Claims.First(c => c.Type == "https://dbnd:auth0:com/app_metadata").Value);

      var managementToken = GetManagementToken();
      Client = new ManagementApiClient(managementToken, _domain);
    }

    public string GetManagementToken()
    {
      var client = new RestClient($"https://{_domain}/oauth/token");
      var request = new RestRequest(Method.POST);

      request.AddHeader("content-type", "application/x-www-form-urlencoded");
      request.AddParameter("application/x-www-form-urlencoded",
        $"grant_type=client_credentials&client_id={_clientId}&client_secret={_secret}&audience=https%3A%2F%2F%24%7B{_domain}%7D%2Fapi%2Fv2%2F",
        ParameterType.RequestBody);

      IRestResponse response = client.Execute(request);

      return JsonSerializer.Deserialize<dynamic>(response.Content).access_token;
    }

    public async Task AddRole(string authUserId, string role)
    {
      var rolesRequest = new Auth0.ManagementApi.Models.AssignRolesRequest
      {
        Roles = new string[] { role }
      };

      await Client.Users.AssignRolesAsync(authUserId, rolesRequest);
    }

    public async Task RemoveRole(string authUserId, string role)
    {
      var rolesRequest = new Auth0.ManagementApi.Models.AssignRolesRequest
      {
        Roles = new string[] { role }
      };

      await Client.Users.RemoveRolesAsync(authUserId, rolesRequest);
    }

    public async Task UpdateMetadataWithId(string authUserId, Guid newId)
    {
      if (AppMetadata.id != newId)
      {
        AppMetadata.id = newId;

        var userUpdateRequest = new Auth0.ManagementApi.Models.UserUpdateRequest
        {
          AppMetadata = AppMetadata
        };

        await Client.Users.UpdateAsync(authUserId, userUpdateRequest);
      }
    }
  }
}
