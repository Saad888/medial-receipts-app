using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using MedicalReceiptsApp.Google;


namespace MedicalReceiptsApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GoogleDriveExplorer : ContentPage
    {
        public GoogleDriveExplorer()
        {
            InitializeComponent();

            Buttawn.Clicked += Button_Clicked;
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            await GoogleClient.TestMethod();
        }
    }
}