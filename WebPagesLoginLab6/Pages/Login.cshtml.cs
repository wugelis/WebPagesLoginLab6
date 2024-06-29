using EasyArchitect.OutsidePageModel;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;
using System.Text.Json;
using WebPagesLoginLab6.Models;
//using ConfigurationManager = EasyArchitectCore.Core.ConfigurationManager;

namespace WebPagesLoginLab6.Pages
{
    public class LoginModel : PageModel
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public LoginModel(IHttpContextAccessor httpContextAccessor) 
        {
            _httpContextAccessor = httpContextAccessor;
        }

        [BindProperty]
        public Account? AccountData { get; set; }

        public void OnGet()
        {
            var timeout = EasyArchitectCore.Core.ConfigurationManager.AppSettings["TimeoutMinutes"];
            double.TryParse(timeout, out double timeoutMinutes);
        }

        public void OnPost()
        {
            if (ModelState.IsValid)
            {
                AuthenticateRequest account = new AuthenticateRequest()
                {
                    Username = AccountData.Email,
                    Password = AccountData.Password
                };

                if (ProcessLogin(account))
                {
                    Response.Redirect("/");
                }
            }
        }
        /// <summary>
        /// 執行登入作業
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        public bool ProcessLogin(AuthenticateRequest account)
        {
            bool result = true; 

            ClaimsPrincipal principal = new ClaimsPrincipal(
                new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, account.Username),
                    new Claim(ClaimTypes.Role, "Admin"),
                }, 
                CookieAuthenticationDefaults.AuthenticationScheme));

            try
            {
                _httpContextAccessor.HttpContext.SignInAsync(principal);

                double.TryParse(EasyArchitectCore.Core.ConfigurationManager.AppSettings["TimeoutMinutes"], out double timeoutMinutes);

                CookieOptions cookieOption = new CookieOptions()
                {
                    HttpOnly = true,
                    Expires = System.DateTime.Now.AddMinutes(timeoutMinutes)
                };

                _httpContextAccessor.HttpContext.Response.Cookies.Append(UserInfo.LOGIN_USER_INFO, JsonSerializer.Serialize(account), cookieOption);
            }
            catch
            {
                result = false;
            }

            return result;
        }
    }
}
