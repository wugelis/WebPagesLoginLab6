using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebPagesLoginLab6.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IAuthorizationService _authorizationService;

        public IndexModel(ILogger<IndexModel> logger, IAuthorizationService authorizationService)
        {
            _logger = logger;
            _authorizationService = authorizationService;
            _authorizationService.AuthorizeAsync(User, "MyPolicy");

            //var authorizeFilter = new AuthorizeFilter("MyPolicy");
            //Filters.Add(authorizeFilter);
        }

        public void OnGet()
        {
            
        }
    }
}
