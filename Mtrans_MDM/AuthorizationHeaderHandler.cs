﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace Mtrans_MDM
{
    public class AuthorizationHeaderHandler : DelegatingHandler
    {
        //protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        //{
        //    // Initialization.   
        //    IEnumerable<string> apiKeyHeaderValues = null;
        //    AuthenticationHeaderValue authorization = request.Headers.Authorization;
        //    string userName = null;
        //    string password = null;
        //    // Verification.   
        //    //if (request.Headers.TryGetValues(ApiInfo.API_KEY_HEADER, out apiKeyHeaderValues) && !string.IsNullOrEmpty(authorization.Parameter))
        //    if (!string.IsNullOrEmpty(authorization?.Parameter))
        //    {
        //        var apiKeyHeaderValue = apiKeyHeaderValues.First();
        //        // Get the auth token   
        //        string authToken = authorization.Parameter;
        //        // Decode the token from BASE64   
        //        string decodedToken = Encoding.UTF8.GetString(Convert.FromBase64String(authToken));
        //        // Extract username and password from decoded token   
        //        userName = decodedToken.Substring(0, decodedToken.IndexOf(":"));
        //        password = decodedToken.Substring(decodedToken.IndexOf(":") + 1);
        //        // Verification.   
        //        //if (apiKeyHeaderValue.Equals(ApiInfo.API_KEY_VALUE) && userName.Equals(ApiInfo.USERNAME_VALUE) && password.Equals(ApiInfo.PASSWORD_VALUE))
        //        //if (apiKeyHeaderValue.Equals(ApiInfo.API_KEY_VALUE) && userName.Equals(ApiInfo.USERNAME_VALUE) && password.Equals(ApiInfo.PASSWORD_VALUE))
        //        //{
        //        //    // Setting   
        //        //    var identity = new GenericIdentity(userName);
        //        //    SetPrincipal(new GenericPrincipal(identity, null));
        //        //}
        //    }
        //    // Info.   
        //    return base.SendAsync(request, cancellationToken);
        //}
    }
}