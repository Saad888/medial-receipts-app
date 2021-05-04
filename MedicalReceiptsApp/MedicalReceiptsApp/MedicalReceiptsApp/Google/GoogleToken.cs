using System;
using System.Collections.Generic;

namespace MedicalReceiptsApp.Google
{
    public class GoogleToken
    {
        private string AccessToken { get; set; }
        private string RefreshToken { get; set; }
        private string TokenType { get; set; }
        private DateTime ExpireTime { get; set; }

        public GoogleToken(Dictionary<string, string> Properties)
        {
            AccessToken = Properties["access_token"];
            RefreshToken = Properties["refresh_token"];
            TokenType = Properties["token_type"];

            ExpireTime = DateTime.Now.AddMinutes(50);
            if (GlobalPreferences.SaveRefreshToken)
                GlobalPreferences.RefreshToken = RefreshToken;
        }

        public GoogleToken(string token, string refreshToken, string tokenType)
        {
            AccessToken = token;
            RefreshToken = RefreshToken;
            TokenType = tokenType;
            ExpireTime = DateTime.Now.AddMinutes(50);
        }

        public string GetAccessToken()
        {
            if (AccessToken == null) return null;
            if (DateTime.Now > ExpireTime) return null;
            return AccessToken;
        }

        public string GetRefreshToken()
        {
            return RefreshToken ?? GlobalPreferences.RefreshToken;
        }

    }
}