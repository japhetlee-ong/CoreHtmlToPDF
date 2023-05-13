using CoreHtmlToPDF.Helpers;
using CoreHtmlToPDF.Models;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc;
using SelectPdf;
using System.Net.Mime;

namespace CoreHtmlToPDF.Controllers
{
    public class PdfController : Controller
    {
        public IActionResult Index()
        {
            List<SampleData> sampleDataList = new List<SampleData>();

            for(int x= 0; x<= 15; x++)
            {
                sampleDataList.Add(new SampleData
                {
                    Amount = x,
                    Price = x * 1.69M,
                    Name = "Item-"+x.ToString(),
                    Description = "Item description -" + x.ToString(),
                    Approved = (x%2 == 0)
                });
            }

            var sampleModel = new SampleModel
            {
                Name = "Budget request for new pc",
                Description = "Replacing monitors for A-403",
                EstimatedBudgetTotal = sampleDataList.Sum(x => x.Price),
                ApprovedBy = "Ruel V. Reyes",
                ItemList = sampleDataList
            };

            return View(sampleModel) ;
        }


        [HttpPost]
        public async Task<IActionResult> Test()
        {

            List<SampleData> sampleDataList = new List<SampleData>();

            for (int x = 0; x <= 15; x++)
            {
                sampleDataList.Add(new SampleData
                {
                    Amount = x,
                    Price = x * 1.69M,
                    Name = "Item-" + x.ToString(),
                    Description = "Item description -" + x.ToString(),
                    Approved = (x % 2 == 0)
                });
            }

            var sampleModel = new SampleModel
            {
                Name = "Budget request for new pc",
                Description = "Replacing monitors for A-403",
                EstimatedBudgetTotal = sampleDataList.Sum(x => x.Price),
                ApprovedBy = "Ruel V. Reyes",
                ItemList = sampleDataList
            };

            string _HTML = await HtmlGenerator.RenderViewToStringAsync("_pdfView", sampleModel, ControllerContext);

            PdfPageSize pageSize = (PdfPageSize)Enum.Parse(typeof(PdfPageSize),
            "A4", true);

            PdfPageOrientation pdfOrientation =
                (PdfPageOrientation)Enum.Parse(typeof(PdfPageOrientation),
                "Portrait", true);

            int webPageWidth = 1024;
            int webPageHeight = 1024;

            // instantiate a html to pdf converter object
            HtmlToPdf converter = new HtmlToPdf();

            // set converter options
            converter.Options.PdfPageSize = pageSize;
            converter.Options.PdfPageOrientation = pdfOrientation;
            converter.Options.WebPageWidth = webPageWidth;
            converter.Options.WebPageHeight = webPageHeight;

            // create a new pdf document converting an url
            PdfDocument doc = converter.ConvertHtmlString(_HTML, "https://localhost:7295/");

            // save pdf document
            byte[] pdf = doc.Save();

            // close pdf document
            doc.Close();

            FileResult fileResult = new FileContentResult(pdf, "application/pdf");
            fileResult.FileDownloadName = "Document.pdf";

            return fileResult;
        }
    }


}
