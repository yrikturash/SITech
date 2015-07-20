using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SITech.DTO
{  
        public class CustomerViewModel
        {
            public CustomerViewModel()
            {
                Roles = new List<System.Web.Mvc.SelectListItem>();
            }

            [Display(Name = "CustomerId")]
            public string CustomerId { get; set; }

            [Required]
            [Display(Name = "Username")]
            public string UserName { get; set; }

            [Required]
            [Display(Name = "First Name")]
            public string FirstName { get; set; }

            [Display(Name = "Middle Name")]
            public string MiddleName { get; set; }

            [Required]
            [Display(Name = "Last Name")]
            public string LastName { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirm Password")]
            [System.ComponentModel.DataAnnotations.Compare("Password",
                ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }

            [Required]
            [Display(Name = "Email")]
            [EmailAddress]
            public string Email { get; set; }

            [Display(Name = "Roles")]
            public IList<System.Web.Mvc.SelectListItem> Roles { get; set; }

            public string RoleName { get; set; }
        }

        public class EditCustomerViewModel
        {
            public EditCustomerViewModel()
            {
                Roles = new List<System.Web.Mvc.SelectListItem>();
            }

            [Display(Name = "CustomerId")]
            public string CustomerId { get; set; }

            [Required]
            [Display(Name = "Username")]
            public string UserName { get; set; }

            [Required]
            [Display(Name = "First Name")]
            public string FirstName { get; set; }

            [Display(Name = "Middle Name")]
            public string MiddleName { get; set; }

            [Required]
            [Display(Name = "Last Name")]
            public string LastName { get; set; }

            [Required]
            [Display(Name = "Email")]
            [EmailAddress]
            public string Email { get; set; }

            [Display(Name = "Roles")]
            public IList<System.Web.Mvc.SelectListItem> Roles { get; set; }

            [Display(Name = "RoleName")]
            public string RoleName { get; set; }
        }

}