using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net.Http;

using Xamarin.Forms;

using Newtonsoft.Json;


namespace MedicalReceiptsApp.Google
{
    public static class GoogleClient
    {
        private static GoogleToken Token { get; set; }
        private static HttpClient Client { get; set; }
        private const string API_REFRESH = @"https://oauth2.googleapis.com/token";

        static GoogleClient()
        {
            Client = new HttpClient();
        }

        public static async Task TestMethod()
        {
            await GetAccessToken();
        }

        public async static Task<T> QueryGoogleApi<T>(string url, Dictionary<string, string> values = null, bool accessTokenRequired = true)
        {
            // If access token is not provided, get it
            var accessToken = accessTokenRequired ? await GetAccessToken() : null;

            // Content
            var content = new FormUrlEncodedContent(values);

            // Response
            var response = await Client.PostAsync(url, content);

            if (response.IsSuccessStatusCode)
            {
                // Var object
                return JsonConvert.DeserializeObject<T>(await response.Content.ReadAsStringAsync());
            }

            throw new HttpRequestException($"Request Failed - {await response.Content.ReadAsStringAsync()}");
        }

        private async static Task<string> GetAccessToken()
        {
            // Get access token
            var accessToken = Token != null ? Token.GetAccessToken() : null;
            if (!String.IsNullOrEmpty(accessToken))
                return accessToken;

            // If token is null, get refresh token
            var refreshToken = Token != null ? Token.GetRefreshToken() : GlobalPreferences.RefreshToken;
            if (String.IsNullOrEmpty(refreshToken))
            {
                // If refresh token is null, get access token from OAuth
                var reader = DependencyService.Get<IGoogleAccessTokenReader>();
                Token = await reader.GetAccessToken();
            }
            else
            {
                // If refresh token is not null, query refresh endpoint
                var reqObj = new Dictionary<string, string>();
                reqObj["client_id"] = GoogleClientSecretManager.ClientID;
                reqObj["client_secret"] = GoogleClientSecretManager.ClientSecret;
                reqObj["grant_type"] = "refresh_token";
                reqObj["refresh_token"] = refreshToken;

                var output = await QueryGoogleApi<RefeshTokenOuput>(API_REFRESH, reqObj, false);
                Token = new GoogleToken(output.AccessToken, refreshToken, output.TokenType);
            }

            return Token.GetAccessToken();
        }
    }
}
