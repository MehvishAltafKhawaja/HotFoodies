using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ASPNETMVC.ViewModel
{
    public class UpdatePassViewModel
    {   
       [Required(ErrorMessage ="Please enter Email")]
       [EmailAddress(ErrorMessage ="Please Enter a valid email")]
       public string Email {get; set;}


      [Required(ErrorMessage ="Please enter current password")]
      [StringLength(40, MinimumLength =8, ErrorMessage ="The {0} must be at {2} and at max {1} character long")]
      [DataType(DataType.Password)]
       public string CurrentPassword {get; set;}
    
       [Required(ErrorMessage = "Please enter new password")]
       [StringLength(40, MinimumLength =8, ErrorMessage ="The {0} must be at {2} and at max {1} character long")]
       [DataType(DataType.Password)]
       [Compare("ConfirmNewPassword", ErrorMessage ="Password does not match")]
       public string NewPassword {get; set;}

      [Required(ErrorMessage ="Please enter confirm new password")]
      [StringLength(40, MinimumLength =8, ErrorMessage ="The {0} must be at {2} and at max {1} character long")]
      [DataType(DataType.Password)] 
       public string ConfirmNewPassword {get; set;}
    }
}