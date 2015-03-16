using System.Collections.Generic;

namespace ExportJSONtoPdf
{
    public class ReportEntity
    {

        public string ReportName { get; set; }
        public object Date { get; set; }
        public List<Product> products;

        public ReportEntity()
        {
            this.products = new List<Product>();
        }
    }
}
