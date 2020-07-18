using IMCTAX.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace IMCTAX
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public int selectedState;

        public MainPage()
        {
            InitializeComponent();
            //this.BindingContext = new CalculatorViewModel(this);

            ratePicker.ItemsSource = WebApi.GetUSTaxRates().Select(x => x.Region).ToList();
            ratePicker.SelectedIndexChanged += RatePicker_SelectedIndexChanged;

        }

        public void SetRatePicker(int index)
        {
            if (index > -1)
            {

                Taxjar.SummaryRate region = WebApi.GetTaxRateForRegion(ratePicker.SelectedItem.ToString());
                this.taxRateLabel.Text = region.MinimumRate.Rate.ToString();
                this.stateRateLabel.Text = region.MinimumRate.Label;
                this.averageLabel.Text = region.AverageRate.Label;
                this.averageTaxLabel.Text = region.AverageRate.Rate.ToString();
                SetVisiblity(true);
                Models.TaxResultObject taxResultObject = WebApi.GetTaxForOrder((decimal)895.9, (decimal)region.AverageRate.Rate);
                if (taxResultObject != null)
                {
                    totalTaxLabel.Text = taxResultObject.FormattedTax;
                    totalPriceLabel.Text = taxResultObject.FormattedCurrency;
                    submitButton.IsEnabled = true;
                }
                else
                {
                    submitButton.IsEnabled = false;
                }
            }
            else
            {
                SetVisiblity(false);
                submitButton.IsEnabled = false;
            }
        }
        private void RatePicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ratePicker.SelectedIndex > -1)
            {
                SetRatePicker(ratePicker.SelectedIndex);
            }
        }

        public void SetVisiblity(bool visiblity)
        {
            this.taxRateLabel.IsVisible = visiblity;

            stateRateLabel.IsVisible = visiblity;
            averageLabel.IsVisible = visiblity;
            averageTaxLabel.IsVisible = visiblity;
        }

        private void submitButton_Clicked(object sender, EventArgs e)
        {
            this.DisplayAlert("Order Confirmation!", "Your credit card has been charged " + this.totalPriceLabel.Text + "!", "Thanks");
        }
    }
}
