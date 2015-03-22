using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApplication1;
using System.Web.Script.Serialization;
using Newtonsoft.Json;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main()
        {

            var db = new expencesEntities();

            var serializer = new JavaScriptSerializer();

            var allVendors = db.vendors.Select(v => new { name = v.name });

            var allProducts = db.products.Select(p => new
            {
                name = p.name,
                tax = p.tax,
                vendor_id = p.vendor_id,
                measure_id = p.measure_id
            });

            var allSales = db.sales.Select(s => new
            {
                sold_on = s.sold_on,
                quantity = s.quantity,
                price_per_unit = s.price_per_unit,
                cost = s.cost,
                supermarket_id = s.supermarket_id,
                product_id = s.product_id
            });

            var allExpences = db.expences.Select(e => new
            {
               name = e.name,
               created_on = e.created_on,
               vendor_id = e.vendor_id
            });

            var allSupermarkets = db.supermarkets.Select(su => new
            {
                name = su.name,
                town_id = su.town_id
            });

            var allTowns = db.towns.Select(t => new
            {
                name = t.name,
            });

            var allMesuers = db.measures.Select(m => new
            {
                name = m.name,
            });
              
            var VendorsEntityJson = JsonConvert.SerializeObject(allVendors);
            var ProductsEntityJson = JsonConvert.SerializeObject(allProducts);
            var SalesEntityJson = JsonConvert.SerializeObject(allSales);
            var ExpencesEntityJson = JsonConvert.SerializeObject(allExpences);
            var SupermarketsEntityJson = JsonConvert.SerializeObject(allSupermarkets);
            var TownsEntityJson = JsonConvert.SerializeObject(allTowns);
            var MesuersEntityJson = JsonConvert.SerializeObject(allMesuers);

            var finalJson = "{ \"vendor\":" + VendorsEntityJson 
                + ", \"product\":" + ProductsEntityJson 
                +", \"sales\":" + SalesEntityJson
                + ", \"expence\":" + ExpencesEntityJson
                + ", \"supermarket\":" + SupermarketsEntityJson
                + ", \"town\":" + TownsEntityJson
                + ", \"mesuer\":" + MesuersEntityJson
            + "}";

           // Console.WriteLine(finalJson);
        }
    }
}
