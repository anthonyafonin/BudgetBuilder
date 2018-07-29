using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BudgetBuilder.Models
{
    public class ForgotPasswordRequestModel
    {
        [Required]
        public string Email { get; set; }
    }

    public class LoginRequestModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }

    public class RegisterRequestModel
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string FirstName { get; set; }

        public string LastName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
    }

    public class ResetPasswordRequestModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }

    public class AccountRequestModel
    {
        public string ApplicationUserID { get; set; }
    }

    public class ProfileDetailsModel
    {
        public string UserID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
    }
}
