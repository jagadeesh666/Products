using System.Data;
using System.Linq;
using NUnit.Framework;
using Should;

namespace Products.Test
{
    [TestFixture]
    public class PriceConversionTests
    {
        const string PrimaryColumnName = "ProductId";
        const decimal ConversionRate = 1.5M;
        private static Repository.Products _products;

        #region Test Initialization

        [OneTimeSetUp]
        public static void SetUp()
        {
            _products = new Repository.Products();
        }
        #endregion

        #region Conversion Tests
        [Test]
        public void ShouldCheckIfConversionRateIsAppliedCorrectlyForEuro()
        {
            // verify sterling prices cannot be empty
            var sterlingPrices = _products.GetSterlingPrices();
            sterlingPrices.ShouldNotBeNull("SterlingPrices cannot be Empty");

            // verify euro prices cannot be empty
            var euroPrices = _products.GetEuroPrices();
            euroPrices.ShouldNotBeNull("EuroPrices cannot be Empty");

            // verify product varieties cannot be empty
            var varieties = sterlingPrices.Columns.Cast<DataColumn>()
                .Select(x => x.ColumnName).Where(c => c != PrimaryColumnName).ToArray();
            varieties.ShouldNotBeNull("List of Product Varieties cannot be Empty");

            foreach (DataRow product in sterlingPrices.Rows)
            {
                //verify if productid column is missing
                var productId = product.Field<string>(PrimaryColumnName);
                productId.ShouldNotBeNull("ProductId column is missing");

                //verify if Euro price row is missing for productId
                var euroProduct = euroPrices.AsEnumerable()
                    .SingleOrDefault(r => r.Field<string>(PrimaryColumnName) == productId);
                euroProduct.ShouldNotBeNull("Euro price row is missing for productId:" + productId);

                foreach (var variety in varieties)
                {
                    //verify for all fields including null value fields
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
        #endregion Conversion Tests

    }
}
