namespace DBPlayground
{
    using System;
    using System.IO;
    using System.Net;
    using System.Text;
    using SuperMarket.MySQL;

    using System.Collections.Generic;
    using System.Data.SqlTypes;

    using SuperMarket.Model;

    internal class Program
    {
        private static void Main(string[] args)
        {
            // Create MySQL Connection
            var dbMySQL = new DBConnect();
            var productsCount = dbMySQL.Count();
            // print number of products 
            Console.WriteLine("total products: " + productsCount);

            // Create a request using a URL that can receive a post. 
            var response = GetResponse();
        
            // Create flat JSON from response

            string[] responseJSON = response.ToString().Split(new char[] { ']' }, StringSplitOptions.RemoveEmptyEntries);
           
            var reportMongoDB =  GenerateReportForMongoDB(responseJSON);

            Console.WriteLine(reportMongoDB.ToString());
        }

        private static StringBuilder GenerateReportForMongoDB(string[] responseJSON)
        {
            StringBuilder sb = new StringBuilder();

            var totalSales = ParseSales(responseJSON);
            var totalProducts = ParseProducts(responseJSON);
            var totalVendors = parseVendors(responseJSON);
            foreach (var product in totalProducts)
            {
                sb.AppendLine("{");
                sb.AppendLine("\"product-id\" : " + product.Id + ",");
                sb.AppendLine("\"product-name\" : " + product.Name + ",");

                foreach (var vendor in totalVendors)
                {
                    if (vendor.Id == product.VendorId)
                    {
                        sb.AppendLine("\"vendor-name\" : " + vendor.Name + ",");
                    }
                }

                int salesCount = 0;
                decimal income = 0;
                foreach (var sale in totalSales)
                {
                    if (sale.ProductId == product.Id)
                    {
                        salesCount++;
                        income = sale.PricePerUnit - sale.Cost;
                    }
                }
                sb.AppendLine("\"total-quantity-sold\"" + " : " + salesCount + ",");
                string totalIncome = String.Format("\"total-income\" : {0:f2}, ", income*salesCount);
                sb.AppendLine(totalIncome);
                sb.AppendLine("}");
            }

            return sb;
        }

        private static List<Vendor> parseVendors(string[] response)
        {
            var parseVendors = new List<Vendor>();
            var vendors = response[6].Substring(11).Split(new char[] { '}' }, StringSplitOptions.RemoveEmptyEntries);
            char[] separator = { ',' };

            foreach (var vendor in vendors)
            {
                var v = vendor.Split(separator, StringSplitOptions.RemoveEmptyEntries);
                var id = int.Parse(v[0].Substring(v[0].IndexOf(':') + 1));
                var name = v[1].Substring(v[1].IndexOf(':') + 1);

                parseVendors.Add(new Vendor(id, name));
            }

            return parseVendors;
        }

        private static List<Product> ParseProducts(string[] response)
        {
            var parseProducts = new List<Product>();
            var products = response[2].Substring(11).Split(new char[] { '}' }, StringSplitOptions.RemoveEmptyEntries);
            char[] separator = { ',' };

            foreach (var product in products)
            {
                var p = product.Split(separator, StringSplitOptions.RemoveEmptyEntries);

                var id = int.Parse(p[0].Substring(p[0].IndexOf(':') + 1));
                var name = p[1].Substring(p[1].IndexOf(':') + 1);
                var tax = decimal.Parse(p[2].Substring(p[2].IndexOf(':') + 1));
                var vendorId = int.Parse(p[3].Substring(p[3].IndexOf(':') + 1));
                var measureId = int.Parse(p[4].Substring(p[4].IndexOf(':') + 1));

                parseProducts.Add(new Product(id, name, tax, vendorId, measureId));
            }

            return parseProducts;
        }


        private static List<Sale> ParseSales(string[] response)
        {
            List<Sale> parseSales = new List<Sale>();
            var sales = response[3].Substring(10).Split(new char[] {'}'}, StringSplitOptions.RemoveEmptyEntries);

            char[] separator = {','};

            foreach (var sale in sales)
            {
                var s = sale.Split(separator, StringSplitOptions.RemoveEmptyEntries);

                var id = int.Parse(s[0].Substring(s[0].IndexOf(':') + 1));
                var soldOn = new DateTime(int.Parse(s[1].Substring(s[1].IndexOf(':') + 2, 4)),
                    int.Parse(s[1].Substring(s[1].IndexOf(':') + 7, 2)), int.Parse(s[1].Substring(s[1].IndexOf(':') + 10, 2)));
                var quantity = int.Parse(s[2].Substring(s[2].IndexOf(':') + 1));
                var pricePerUnit = decimal.Parse(s[3].Substring(s[3].IndexOf(':') + 1));
                var cost = decimal.Parse(s[4].Substring(s[4].IndexOf(':') + 1));
                var supermarketId = int.Parse(s[5].Substring(s[5].IndexOf(':') + 1));
                var productId = int.Parse(s[6].Substring(s[6].IndexOf(':') + 1));

                parseSales.Add(new Sale(id, soldOn, quantity, pricePerUnit, cost, supermarketId, productId));
            }

            return parseSales;
        }

        private static StringBuilder GetResponse()
        {
            StringBuilder sb = new StringBuilder();
            string url = "http://localhost/dbApps-Core1.0/data/mysql";
            // Creates an HttpWebRequest with the specified URL. 
            HttpWebRequest myHttpWebRequest = (HttpWebRequest) WebRequest.Create(url);
            // Sends the HttpWebRequest and waits for the response.			
            HttpWebResponse myHttpWebResponse = (HttpWebResponse) myHttpWebRequest.GetResponse();
            // Gets the stream associated with the response.
            Stream receiveStream = myHttpWebResponse.GetResponseStream();
            Encoding encode = System.Text.Encoding.GetEncoding("utf-8");
            // Pipes the stream to a higher level stream reader with the required encoding format. 
            StreamReader readStream = new StreamReader(receiveStream, encode);
            Char[] read = new Char[256];
            // Reads 256 characters at a time.     
            int count = readStream.Read(read, 0, 256);
            while (count > 0)
            {
                // Dumps the 256 characters on a string and displays the string to the console.
                String str = new String(read, 0, count);
                sb.Append(str);
                count = readStream.Read(read, 0, 256);
            }
            // Releases the resources of the response.
            myHttpWebResponse.Close();
            // Releases the resources of the Stream.
            readStream.Close();
            return sb;
        }
    }
}