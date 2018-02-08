using System.ComponentModel.DataAnnotations;

namespace CoolBaby.WebApi.Models
{
    /// <summary>
    /// Login model
    /// </summary>
    public class LoginViewModel
    {
        /// <summary>
        /// Username
        /// </summary>
        [Required]
        public string UserName { get; set; }

        /// <summary>
        /// Password
        /// </summary>
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        /// <summary>
        /// Remember username and password
        /// </summary>
        public bool RememberMe { get; set; }
    }
}