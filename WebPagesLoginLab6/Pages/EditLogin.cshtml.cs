using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebPagesLoginLab6.Models;

namespace WebPagesLoginLab6.Pages
{
    public class EditLoginModel : PageModel
    {
        private List<Account> _context = new List<Account>()
        {
            new Account { Id = 1, Email = ""},
            new Account { Id = 2, Email = ""},
            new Account { Id = 3, Email = ""}
        };

        [BindProperty]
        public Account? AccountData { get; set; }
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            AccountData = await Task.FromResult(_context.Where(m => m.Id == id).FirstOrDefault());

            if (AccountData == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
