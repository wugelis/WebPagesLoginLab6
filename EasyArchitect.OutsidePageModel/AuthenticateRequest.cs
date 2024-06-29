using System.ComponentModel.DataAnnotations;

namespace EasyArchitect.OutsidePageModel
{
    /// <summary>
    /// 
    /// </summary>
    public class AuthenticateRequest
    {
        /// <summary>
        /// 
        /// </summary>
        [Required]
        public string Username { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Required]
        public string Password { get; set; }
    }
}
