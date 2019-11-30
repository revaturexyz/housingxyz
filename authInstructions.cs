// Instructions to add auth and policies to a project

// 1. Add the following to ConfigureServices
services.AddSingleton<IAuthorizationHandler, RoleRequirementHandler>();

services.AddAuthentication(options =>
{
options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
options.Authority = $"http://{Configuration.GetSection("Auth0").GetValue<string>("Domain")}/";
options.Audience = Configuration.GetSection("Auth0").GetValue<string>("Audience");
});

services.AddAuthorization(options => {
options.AddPolicy("ApprovedProviderRole", policy =>
    policy.Requirements.Add(new RoleRequirement("approved_provider")));
options.AddPolicy("CoordinatorRole", policy =>
    policy.Requirements.Add(new RoleRequirement("coordinator")));

// To fix needing to manually specify the schema every time I want to call [Authorize]
// Found it at https://github.com/aspnet/AspNetCore/issues/2193
options.DefaultPolicy = new AuthorizationPolicyBuilder(JwtBearerDefaults.AuthenticationScheme)
    .RequireAuthenticatedUser()
    .Build();
});

// 2. Add the following to Configure
app.UseAuthorization();

// 3. Copy everything from the Auth folder into your .Api project
//   a. Verify the namespace and everything is all good for your project

// 4. Get the Auth0 section from Slack and copy it into your appsettings.Development.json