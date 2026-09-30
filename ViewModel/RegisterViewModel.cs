using System.ComponentModel.DataAnnotations;

namespace ASPNETMVC.ViewModel
{
   public class RegisterViewModel
    {    
        [Required(ErrorMessage ="Please Enter Name")]
         public string? Name { get; set; }

        [Required(ErrorMessage ="Please Enter Email")]
        [EmailAddress(ErrorMessage ="Please Enter a valid Email")]
         public string Email {get; set;}

         [Required(ErrorMessage ="Please Enter Password")]
         [StringLength(40,MinimumLength =8, ErrorMessage = "The {0} must be at {2} and max at {1} character long! ")]
         [Compare("confirmpassword",ErrorMessage ="Password must be Same")]
         [DataType(DataType.Password)]
         public string password {get; set;}

         [Required(ErrorMessage ="Please Enter Password")]
         [StringLength(40,MinimumLength =8, ErrorMessage = "The {0} must be at {2} and max at {1} character long! ")]
         [DataType(DataType.Password)]
         public string confirmpassword {get; set;}

         [Required(ErrorMessage ="Please Select Gender")]
         public string? EmpGender { get; set; }

         [Required(ErrorMessage ="Please Enter Porfile Picture")]
         public IFormFile? ImageUrl {get; set;}
    }
}