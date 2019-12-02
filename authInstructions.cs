// Instructions to add auth and policies to a project

// 1. Configure Startup
//   a. Add to ConfigureServices():
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

//   b. In order to facilitate adding a token to Swagger for testing, your SwaggerGen should look like so:
services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Revature Account", Version = "v1" });
    c.OrderActionsBy((apiDesc) => $"{apiDesc.ActionDescriptor.RouteValues["controller"]}_{apiDesc.HttpMethod}");
    c.AddSecurityDefinition("BearerAuth", new OpenApiSecurityScheme
    {
        Type = SecuritySchemeType.ApiKey,
        Description = "Bearer authentication scheme with JWT, e.g. \"Bearer eyJhbGciOiJIUzI1NiJ9.e30\"",
        Name = "Authorization",
        In = ParameterLocation.Header
    });
    c.OperationFilter<SwaggerFilter>();
});

// 2. Add the following to Configure():
app.UseAuthorization();

// 3. Copy everything from the Auth folder and the Filters folder of Account's .Api into your .Api project
//   a. Verify the namespace and everything is all good for your project

// 4. Get the Auth0 section from Slack and copy it into your appsettings.Development.json

// 5. For any controller method you want any authenticated (logged in) user to access, add [Authorize] above the method name.
//   b. If you want only approved providers to access it, use [Authorize(Policy="approved_provider")]
//   c. Likewise, if you want only coordinators to access it, user [Authorize(Policy="coordinator")]