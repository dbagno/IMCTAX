using RestSharp.Extensions;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using Taxjar;
using Xamarin.Forms;

namespace IMCTAX.Helpers
{
    public static class WebApi
    {

        public static string GetToken()
        {
            try
            {
                App.Client = new TaxjarApi(App.Key);
                return App.Client.apiToken;
            }
            catch (Exception ex)
            {
                Application.Current.MainPage.DisplayAlert("Login Error", ex.Message, "OK");
                return "";
            }


        }


        public static System.Collections.Generic.List<SummaryRate> GetUSTaxRates()
        {
            App.Client = new TaxjarApi(App.Key);
            return App.Client.SummaryRates().Where(x => x.CountryCode == "US").OrderBy(x => x.Region).ToList();
        }

        public static SummaryRate GetTaxRateForRegion(string region)
        {
            App.Client = new TaxjarApi(App.Key);
            return App.Client.SummaryRates().Where(x => x.CountryCode == "US" && x.Region == region).OrderBy(x => x.Region).FirstOrDefault();

        }
        public static System.Collections.Generic.List<Category> GetCategories()
        {
            App.Client = new TaxjarApi(App.Key);
            return App.Client.Categories();
        }
        public static Models.TaxResultObject GetTaxForOrder(decimal price, decimal rate)
        {
            if (price > -1 && rate > -1)
            {
                Models.TaxResultObject result = new Models.TaxResultObject();
                var tax = price * rate;
                result.TotalTax = tax;
                var totalPrice = price += tax;
                result.TotalPrice = totalPrice;
                return result;
            }
            else
            {
                return null;
            }


        }
    }
}
