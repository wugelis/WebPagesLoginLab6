using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

namespace WebPagesLoginLab6.Pages
{
    public class ExcelPageModel : PageModel
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ExcelPageModel(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }
        public void OnGet()
        {
        }

        public IActionResult OnGetExcelDownload()
        {
            using (var workbook = new XSSFWorkbook())
            {
                ISheet sheet1 = workbook.CreateSheet("My Sheet 1");

                sheet1.CreateRow(0).CreateCell(0).SetCellValue("嗨!");
                sheet1.CreateRow(1).CreateCell(0).SetCellValue("這是測試的 Excel 報表");

                using (var memoryStream = new MemoryStream())
                {
                    workbook.Write(memoryStream);
                    var content = memoryStream.ToArray();

                    return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "test.xlsx");
                }
            }
        }

        public IActionResult OnGetExcelDownload2()
        {
            var serverVirtualPath = _webHostEnvironment.WebRootPath;
            string reportFileName = getReportFilebyDaliy();

            return PhysicalFile(
                Path.Combine(serverVirtualPath, 
                @$"Download\{reportFileName}"), 
                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                reportFileName);
        }

        private string getReportFilebyDaliy()
        {
            DateTime UtcDate = new DateTime(2016, 8, 25); //DateTime.Now.ToUniversalTime();
            string reportCarrfour_Daliy01 = $"Daily_Sales_super_{UtcDate.ToString("yyyyMMdd")}.xlsx";
            return reportCarrfour_Daliy01;
        }
    }
}
