using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebPagesLoginLab6.Pages
{
    public class LoginModel : PageModel
    {
        public class Account
        {
            public string Email { get; set; }
            public string Password { get; set; }
        }

        [BindProperty]
        public Account? AccountData { get; set; }

        public void OnGet()
        {
        }
    }
}
