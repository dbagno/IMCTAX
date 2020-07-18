using Microsoft.VisualStudio.TestTools.UnitTesting;
using IMCTAX;
using System;
using System.Collections.Generic;
using System.Text;
using Taxjar;
using System.Linq;
using System.Diagnostics;
using IMCTAX.Helpers;
using Xamarin.Forms.Mocks;
using Xamarin.Forms;
namespace IMCTAX.Tests
{
    [TestClass()]
    public class AppTests
    {

        public static string Key = "5da2f821eee4035db4771edab942a4cc";
        public static string Token;

        [TestMethod()]
        public void GetToken()
        {

            Token = Helpers.WebApi.GetToken();

            if (!string.IsNullOrEmpty(Token))
            {
                Assert.IsTrue(true, Token);
            }
            else
            {
                Assert.IsFalse(false, "no token received");
            }


        }
        [TestMethod()]
        public void UITestPickerSelectFlorida()
        {
            Xamarin.Forms.Mocks.MockForms.Init();
            var page = new MainPage() { Title = "IMC Tax Calculator" };
            if (page != null)
            {
                var picker = page.FindByName<Picker>("ratePicker");
                if (picker != null)
                {
                    picker.SelectedIndex = 9;
                    if (picker.SelectedIndex == 9 && picker.SelectedItem.ToString() == "Florida")
                    {

                        Assert.IsTrue(true, picker.SelectedIndex.ToString());
                    }
                    else
                    {
                        Assert.Fail();
                    }
                }

            }
            else
            {
                Assert.Fail();
            }

        }
        [TestMethod()]
        public void GetUSTaxRates()
        {

            var results = Helpers.WebApi.GetUSTaxRates();
            if (results?.Count() > 0)
            {
                Assert.IsTrue(true, results.Count().ToString());
            }
            else
            {
                Assert.Fail();
            }
        }

        [TestMethod()]
        public void GetCatgories()
        {

            var results = Helpers.WebApi.GetCategories();
            if (results?.Count() > 0)
            {
                Assert.IsTrue(true, results.Count().ToString());
            }
            else
            {
                Assert.Fail();
            }

        }

        [TestMethod()]
        public void GetAverageRateFor()
        {
            SummaryRate result = Helpers.WebApi.GetTaxRateForRegion("Florida");
            if (result != null)
            {
                Assert.IsTrue(true, result.AverageRate.Rate.ToString());
            }
            else
            {
                Assert.Fail();
            }
        }

        [TestMethod()]
        public void GetTotalTaxForOrder()
        {

            Helpers.Models.TaxResultObject result = Helpers.WebApi.GetTaxForOrder((decimal)895.9, (decimal)0.0682);
            if ((decimal)result.TotalTax == (decimal)61.10038)
            {
                Assert.IsTrue(true, result.TotalTax.ToString());
            }
            else
            {
                Assert.Fail();
            }

        }

        [TestMethod()]
        public void GetTotalPriceForOrder()
        {

            Helpers.Models.TaxResultObject result = Helpers.WebApi.GetTaxForOrder((decimal)895.9, (decimal)0.0682);
            if ((decimal)result.TotalPrice == (decimal)957.00038)
            {
                Assert.IsTrue(true, result.TotalPrice.ToString());
            }
            else
            {
                Assert.Fail();
            }

        }
        public void AppTest()
        {
            Assert.Fail();
        }
    }
}