using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System.Collections.Generic;

namespace WebPagesLoginLab6.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public IndexModel(ILogger<IndexModel> logger, IHttpContextAccessor httpContextAccessor)
        {
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
        }

        public void OnGet()
        {
            var userName = User.Identity.Name;
            var userName2 = _httpContextAccessor.HttpContext.User.Identity.Name;

            // ViewData["UserName"] = userName;
            ViewData["UserName"] = userName2;
        }

        public IActionResult OnGetProfile(int attendeeId)
        {
            ViewData["AttendeeId"] = attendeeId;

            // code omitted for brevity

            using (var workbook = new XSSFWorkbook())
            {
                ISheet sheet1 = workbook.CreateSheet("Sheet 1");

                sheet1.CreateRow(0).CreateCell(0).SetCellValue("Hello");
                sheet1.CreateRow(1).CreateCell(0).SetCellValue("World");

                using (var memoryStream = new MemoryStream())
                {
                    workbook.Write(memoryStream);
                    var content = memoryStream.ToArray();

                    return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "test.xlsx");
                }
            }
        }
    }
}
