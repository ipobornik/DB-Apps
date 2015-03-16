using System;
using System.Linq;
using System.IO;

using iTextSharp.text;
using iTextSharp.text.pdf;
using Newtonsoft.Json;

namespace ExportJSONtoPdf
{
    class ExportJsonToPdf
    {

        private static void ExtractJsonToPdf(string testJson)
        {
            var ReportEntityObj = JsonConvert.DeserializeObject<ReportEntity>(testJson);

            Document doc = new Document(iTextSharp.text.PageSize.LETTER, 10, 10, 42, 35);
            PdfWriter write = PdfWriter.GetInstance(doc, new FileStream("TestReport.pdf", FileMode.Create));
            doc.Open(); // open document to write

            // defining the font of the table
            Font arial = FontFactory.GetFont("Arial", 10, Font.BOLD, new BaseColor(125, 88, 15));

            // generating the table
            int tableRows = ReportEntityObj.products.Count;
            int tableCols = 5;

            PdfPTable table = new PdfPTable(tableCols);

            PdfPCell cellHeader = new PdfPCell(new Phrase(ReportEntityObj.ReportName, arial));
            cellHeader.Colspan = 5;
            cellHeader.HorizontalAlignment = 1; // 0=left, 1=center, 2=right
            table.AddCell(cellHeader);

            for (int i = 0; i < tableRows; i++)
            {
                table.AddCell((new Phrase(ReportEntityObj.products[i].Name.ToString(), arial)));
                table.AddCell((new Phrase(ReportEntityObj.products[i].Quantity.ToString(), arial)));
                table.AddCell((new Phrase(ReportEntityObj.products[i].UnitPrice.ToString(), arial)));
                table.AddCell((new Phrase(ReportEntityObj.products[i].Location.ToString(), arial)));
                table.AddCell((new Phrase(ReportEntityObj.products[i].Sum.ToString(), arial)));
            }

            var totalSum = ReportEntityObj.products.Sum(x => x.Sum);

            PdfPCell cellTotalSumForThePeriodStr =
                new PdfPCell(new Phrase("Total sum for " + ReportEntityObj.Date + ": ", arial));
            cellTotalSumForThePeriodStr.Colspan = 4;
            cellTotalSumForThePeriodStr.HorizontalAlignment = 2; // 0=left, 1=center, 2=right
            table.AddCell(cellTotalSumForThePeriodStr);

            PdfPCell cellTotalSum = new PdfPCell(new Phrase(totalSum.ToString(), arial));
            cellTotalSum.Colspan = 1;
            cellTotalSum.HorizontalAlignment = 2; // 0=left, 1=center, 2=right
            table.AddCell(cellTotalSum);

            doc.Add(table);

            doc.Close(); // close document to write

            System.Diagnostics.Process.Start("TestReport.pdf");
            // automatically open ITextSharp PDF after creating file in Debug folder
        }



        static void Main()
        {
            var testJson = @"{
    'ReportName': 'Aggregated Sales Report',
    'Date': '20-Jul-2014',

    'Products': [{
            'Name': 'Beer “Beck’s”',
            'Quantity': '40 liters',
            'UnitPrice': 1.20,
            'Location': 'Supermarket “Kaspichan – Center”',
            'Sum': 48.00
        },

        {
            'Name': 'Beer “Zagorka”',
            'Quantity': '37 liters',
            'UnitPrice': 1.00,
            'Location': 'Supermarket “Bourgas – Plaza”',
            'Sum': 37.00
        },

        {
            'Name': 'Chocolate “Milka”',
            'Quantity': '7 pieces',
            'UnitPrice': 2.85,
            'Location': 'Supermarket “Bay Ivan” – Zmeyovo',
            'Sum': 19.95
        },

        {
            'Name': 'Vodka “Targovishte”',
            'Quantity': '14 liters',
            'UnitPrice': 8.50,
            'Location': 'Supermarket “Bourgas – Plaza”',
            'Sum': 119.00
        }

    ]
}";
            ExtractJsonToPdf(testJson);
        }

    }
}
