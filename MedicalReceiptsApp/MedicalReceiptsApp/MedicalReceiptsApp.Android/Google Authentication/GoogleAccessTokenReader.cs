using System;
using System.Threading.Tasks;

using Google.Apis.Drive.v3;
using Google.Apis.Auth.OAuth2.Responses;

using Xamarin.Auth;
using Xamarin.Forms;

using Plugin.CurrentActivity;
using Google.Apis.Auth.OAuth2;

using MedicalReceiptsApp.Google;

[assembly: Dependency(typeof(MedicalReceiptsApp.Droid.GoogleAccessTokenReader))]
namespace MedicalReceiptsApp.Droid
{
    public class GoogleAccessTokenReader : IGoogleAccessTokenReader
    {
        public static readonly string[] GoogleAPIScopes =
        {
            DriveService.Scope.DriveFile,
        };

        private static GoogleToken Token { get; set; }
        public static OAuth2Authenticator Auth;

        public async Task<GoogleToken> GetAccessToken()
        {
            if (Auth == null)
            {
                Auth = new OAuth2Authenticator(
                GoogleClientSecretManager.ClientID, 
                GoogleClientSecretManager.ClientSecret,
                String.Join(" ", GoogleAPIScopes),
                new Uri("https://accounts.google.com/o/oauth2/v2/auth"),
                new Uri("com.companyname.medicalreceiptsapp:/oauth2redirect"),
                new Uri("https://www.googleapis.com/oauth2/v4/token"),
                isUsingNativeUI: true);

                Auth.Completed += OnAuthenticationCompleted;
            }

            if (Token != null)
                return Token;

            CustomTabsConfiguration.CustomTabsClosingMessage = null;

            var intent = Auth.GetUI(CrossCurrentActivity.Current.AppContext);
            CrossCurrentActivity.Current.Activity.StartActivity(intent);

            while (!Auth.HasCompleted)
            {
                await Task.Delay(500);
            }

            return Token;
        }

        private void OnAuthenticationCompleted(object sender, AuthenticatorCompletedEventArgs e)
        {
            if (e.IsAuthenticated)
            {
                Token = new GoogleToken(e.Account.Properties);
            }
        }

    }
}