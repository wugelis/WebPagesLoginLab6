using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebPagesLoginLab6.Pages
{
    public class LogoutModel : PageModel
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public LogoutModel(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public void OnGet()
        {
        }

        public void OnPost()
        {
            Logout();
            Response.Redirect("/");
        }

        /// <summary>
        /// µn¥X¨t²Î
        /// </summary>
        /// <returns></returns>
        public bool Logout()
        {
            bool result = true;

            try
            {
                _httpContextAccessor.HttpContext.SignOutAsync();
                _httpContextAccessor.HttpContext.Response.Cookies.Delete(UserInfo.LOGIN_USER_INFO);
            }
            catch
            {
                result = false;
            }

            return result;
        }
    }
}
