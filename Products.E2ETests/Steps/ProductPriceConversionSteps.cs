using System;
using TechTalk.SpecFlow;
using System.Data;
using Should;
using System.Linq;

namespace Products.E2ETests.Steps
{
    [Binding]
    public class ProductSteps
    {
        [Given(@"I have the product price in Sterling for each variety")]
        public void GivenIHaveTheProductPriceInSterlingForEachVariety()
        {
            var sterlingPrices = new Repository.Products().GetSterlingPrices();
            ScenarioContext.Current.Add("sterlingPrices", sterlingPrices);
        }

        [Given(@"I have the product price in Euros for each variety")]
        public void GivenIHaveTheProductPriceInEurosForEachVariety()
        {
            var euroPrices = new Repository.Products().GetEuroPrices();
            ScenarioContext.Current.Add("euroPrices", euroPrices);
        }

        [When(@"I covert the product price from Sterling to Euros as per (.*) for each variety")]
        public void WhenICovertTheProductPriceFromSterlingToEurosAsPerForEachVariety(Decimal rate)
        {
            Decimal ConversionRate = rate;
            ScenarioContext.Current.Add("ConversionRate", ConversionRate);
        }

        [Then(@"the currency converstion From Sterling to Euros should match for product and variety")]
        public void ThenTheCurrencyConverstionFromSterlingToEurosShouldMatchForProductAndVariety()
        {
            const string PrimaryColumnName = "ProductId";

            var sterlingPrices = ScenarioContext.Current.Get<DataTable>("sterlingPrices");
            var euroPrices = ScenarioContext.Current.Get<DataTable>("euroPrices");
            var ConversionRate = ScenarioContext.Current.Get<Decimal>("ConversionRate");

            sterlingPrices.ShouldNotBeNull("SterlingPrices cannot be Empty");

            euroPrices.ShouldNotBeNull("EuroPrices cannot be Empty");

            var varieties = sterlingPrices.Columns.Cast<DataColumn>()
                .Select(x => x.ColumnName).Where(c => c != PrimaryColumnName).ToArray();
            varieties.ShouldNotBeNull("List of Product Varieties cannot be Empty");

            foreach (DataRow product in sterlingPrices.Rows)
            {
                var productId = product.Field<string>(PrimaryColumnName);
                productId.ShouldNotBeNull("ProductId column is missing");

                var euroProduct = euroPrices.AsEnumerable()
                    .SingleOrDefault(r => r.Field<string>(PrimaryColumnName) == productId);
                euroProduct.ShouldNotBeNull("Euro price row is missing for productId:" + productId);
                
                foreach (var variety in varieties)
                {
                    var sterlingPrice = product.Field<decimal?>(variety);
                    var euroPrice = euroProduct.Field<decimal?>(variety);

                    if (sterlingPrice.HasValue)
                    {
                        sterlingPrice.ShouldBeGreaterThan(0);

                        euroPrice.HasValue.ShouldBeTrue($"EuroPrice is not defined for productId:{productId} variety: {variety}");
                        (sterlingPrice * ConversionRate).ShouldEqual(euroPrice, $"Euro Conversion applied is incorrect for productId:{productId} variety: {variety}");
                    }
                    else
                        euroPrice.HasValue.ShouldBeFalse($"Sterling Price is not defined but Euro price exists for productId:{productId} variety: {variety}");
                }
            }


        }


    }
}
