using System.Data;

namespace Products.Repository
{
    public class Products
    {
        public DataTable GetSterlingPrices()
        {
            //Call real API and get prices and prepare datatable
            var table = new DataTable();
            table.Columns.Add("ProductId", typeof(string));
            table.PrimaryKey = new[] { table.Columns["ProductId"] };

            table.Columns.Add("Variety1", typeof(decimal));
            table.Columns.Add("Variety2", typeof(decimal));
            table.Columns.Add("Variety3", typeof(decimal));
            table.Columns.Add("Variety4", typeof(decimal));

            table.Rows.Add("Product1", 10, 12, 14, 45);
            table.Rows.Add("Product2", 20, 15, 24, null);
            table.Rows.Add("Product3", 22, 60, null, null);
            table.Rows.Add("Product4", 28, null, null, null);
            table.Rows.Add("Total", 80, 87, 38, 45);
            return table;
        }
        public DataTable GetEuroPrices()
        {
            //Call real API and get prices and prepare datatable

            var table = new DataTable();
            table.Columns.Add("ProductId", typeof(string));
            table.PrimaryKey = new[] { table.Columns["ProductId"] };

            table.Columns.Add("Variety1", typeof(decimal));
            table.Columns.Add("Variety2", typeof(decimal));
            table.Columns.Add("Variety3", typeof(decimal));
            table.Columns.Add("Variety4", typeof(decimal));

            table.Rows.Add("Product1", 15, 18, 21, 67.5);
            table.Rows.Add("Product2", 30, 22.5, 36, null);
            table.Rows.Add("Product3", 33, 90, null, null);
            table.Rows.Add("Product4", 42, null, null, null);
            table.Rows.Add("Total", 120, 130.5, 57, 67.5);
            return table;
        }
    }
}
