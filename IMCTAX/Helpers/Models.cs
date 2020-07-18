using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace IMCTAX.Helpers
{
    public class Models
    {
        public class TaxResultObject
        {
            public decimal TotalTax { get; set; } = 0;
            public decimal TotalPrice  { get; set; } = 0;

          public string FormattedTax=> TotalTax.ToString("C", CultureInfo.CurrentCulture);
            public string FormattedCurrency=> TotalPrice.ToString("C", CultureInfo.CurrentCulture);
        }
    }
}
