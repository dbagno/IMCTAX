using IMCTAX.Helpers;
using System;
using System.Linq;
using System.Net.Http;
using System.Security.Principal;
using Taxjar;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IMCTAX
{
    public partial class App : Application
    {
        public static TaxjarApi Client { get; set; }
        public static string Key = "5da2f821eee4035db4771edab942a4cc";
        public static string Token;
        public static System.Collections.Generic.List<SummaryRate> USTaxRates;
        public App()
        {
            InitializeComponent();
            Token = Helpers.WebApi.GetToken();
            if(Client!=null && !string.IsNullOrEmpty(Client.apiToken))
            {
               
                USTaxRates = Client.SummaryRates().Where(x => x.CountryCode == "US").OrderBy(x => x.Region).ToList();
                 
             
            }
            var page = new MainPage() { Title = "IMC Tax Calculator" };
            MainPage = new NavigationPage(page);
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
