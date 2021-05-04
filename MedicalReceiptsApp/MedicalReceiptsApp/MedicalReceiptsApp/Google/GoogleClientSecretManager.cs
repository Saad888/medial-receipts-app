using System.IO;
using System.Reflection;
using Google.Apis.Auth.OAuth2;

namespace MedicalReceiptsApp.Google
{
    public static class GoogleClientSecretManager
    {
        public static string ClientID {get; set;}
        public static string ClientSecret { get; set; }

        static GoogleClientSecretManager()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var resourceName = "MedicalReceiptsApp.client_secret.json";

            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            {
                var googleCred = GoogleClientSecrets.Load(stream).Secrets;
                ClientID = googleCred.ClientId;
                ClientSecret = googleCred.ClientSecret;
            }
        }
    }
}