// Add permissions to tokens (turned off unless really needed)
function (user, context, callback) {
    var map = require('array-map');
    var ManagementClient = require('auth0@2.17.0').ManagementClient;
    var management = new ManagementClient({
      token: auth0.accessToken,
      domain: auth0.domain
    });
  
    var params = { id: user.user_id, page: 0, per_page: 50, include_totals: true };
    management.getUserPermissions(params, function (err, permissions) {
      if (err) {
        // Handle error.
        console.log('err: ', err);
        callback(err);
      } else {
        var permissionsArr = map(permissions.permissions, function (permission) {
          return permission.permission_name;
        });
        context.idToken[configuration.NAMESPACE + 'user_authorization'] = {
          permissions: permissionsArr
        };
      }
      callback(null, user, context);
    });
}

// Add roles to tokens
function (user, context, callback) {
    const assignedRoles = (context.authorization || {}).roles;
  
    let idTokenClaims = context.idToken || {};
    let accessTokenClaims = context.accessToken || {};
  
    idTokenClaims[configuration.NAMESPACE + 'roles'] = assignedRoles;
    accessTokenClaims[configuration.NAMESPACE + 'roles'] = assignedRoles;
  
    context.idToken = idTokenClaims;
    context.accessToken = accessTokenClaims;
    callback(null, user, context);
} 