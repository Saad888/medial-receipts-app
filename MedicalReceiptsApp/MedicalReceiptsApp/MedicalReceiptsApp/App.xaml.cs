using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MedicalReceiptsApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new GoogleDriveExplorer();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
