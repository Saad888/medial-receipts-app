using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;

namespace MedicalReceiptsApp
{
    public static class GlobalPreferences
    {
        /// <summary>
        /// Trigger to Save Refresh Token from google API
        /// </summary>
        public static bool SaveRefreshToken {
            get { return Preferences.Get("SaveRefreshToken", false); }
            set {        Preferences.Set("SaveRefreshToken", value); }
        }

        /// <summary>
        /// Refresh token key
        /// </summary>
        public static string RefreshToken
        {
            get { return Preferences.Get("RefreshToken", null); }
            set {        Preferences.Set("RefreshToken", value); }
        }


    }
}
