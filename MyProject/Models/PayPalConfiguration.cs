using PayPal.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace MyProject.Models
{
    // Get configuration from web.config file
    public static class PayPalConfiguration
    {
        public readonly static string ClientId;
        public readonly static string ClientSecret;

        static PayPalConfiguration()
        {
            var config = GetConfig();
            ClientId = config["clientId"];
            ClientSecret = config["clientSecret"];
        }

        public static Dictionary<string, string> GetConfig()
        {
            return PayPal.Api.ConfigManager.Instance.GetProperties();
        }

        private static string GetAccessToken()
        {
            // getting accesstocken from paypal                
            string accessToken = new OAuthTokenCredential(ClientId, ClientSecret, GetConfig()).GetAccessToken();
            return accessToken;
        }

        // This will return APIContext object
        public static APIContext GetAPIContext()
        {
            // return apicontext object by invoking it with the accesstoken
            APIContext apiContext = new APIContext(GetAccessToken());
            apiContext.Config = GetConfig();
            return apiContext;
        }

    }
}